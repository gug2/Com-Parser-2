using System;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace Com_Parser_2_client
{
    class PacketFormat
    {
        // Вызывать в событии Form_Load
        public static void Test()
        {
            // lsb first format
            byte[] byteArr = new byte[] { 0xAA, 0xBB, 0xA4, 0x70, 0x9D, 0x3F, 0x11, 0x22, 0xFF, 0xCC, 0xDD, 0xEE, 0x55 };
            // last byte - xor checksum of all bytes, exclude last byte


            AssemblyCode("loadme.cs", "loadme.dll");
            GetAssembly("loadme.dll", byteArr);
        }
        
        public static void AssemblyCode(string sourceCodePath, string outputAssemblyPath)
        {
            string[] source = PreprocessSource(sourceCodePath);

            Console.WriteLine("Компиляция...");

            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters(null, outputAssemblyPath, false)
            {
                // compile as dll
                GenerateExecutable = false,
                // compile as physical file
                GenerateInMemory = false,
                TempFiles = new TempFileCollection("dll_bin", true)
            };

            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, String.Join("\n", source));
            
            if (results.Errors.HasErrors)
            {
                Console.WriteLine("Ошибка компиляции для сборки \"{0}\"", outputAssemblyPath);
                foreach (CompilerError error in results.Errors)
                {
                    Console.WriteLine(error.ToString());
                }
                return;
            }

            Console.WriteLine("Сборка \"{0}\" скомпилирована.", outputAssemblyPath);
        }

        public static void GetAssembly(string assemblyPath, byte[] byteArray)
        {
            if (!File.Exists(assemblyPath))
            {
                Console.WriteLine("Файл сборки не найден. Путь: \"{0}\"", Path.GetFullPath(assemblyPath));
                return;
            }

            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            Type inDataType = assembly.GetTypes().ToList().Find(type => type.Name == "inData");

            // convert byte array to structure with given type
            object inData = MarshalDeserialize(inDataType, byteArray);
            ref object inDataRef = ref inData;
            object outData = null;

            // invoke ProcessData method
            object[] array = new object[] { inDataRef, outData };
            assembly.GetTypes()[0].GetMethod("ProcessData").Invoke(null, array);

            object resultOutData = array[1];
        }

        private static string[] PreprocessSource(string sourceCodePath)
        {
            Console.WriteLine("Модификация кода...");

            List<string> lines = File.ReadAllLines(sourceCodePath).ToList();

            lines.Insert(0, "using System.Runtime.InteropServices;");
            int index = lines.FindIndex(line => line.Contains("public struct inData"));

            if (index != -1)
            {
                lines.Insert(index, "[StructLayout(LayoutKind.Sequential, Pack=1)]");
            }

            return lines.ToArray();
        }

        public static byte[] MarshalSerialize(object target)
        {
            int size = Marshal.SizeOf(target);
            byte[] array = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(target, ptr, true);
            Marshal.Copy(ptr, array, 0, size);
            Marshal.FreeHGlobal(ptr);
            return array;
        }

        public static object MarshalDeserialize(Type type, byte[] array)
        {
            int size = Marshal.SizeOf(type);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(array, 0, ptr, size);
            object structure = Marshal.PtrToStructure(ptr, type);
            Marshal.FreeHGlobal(ptr);
            return structure;
        }
    }
}
