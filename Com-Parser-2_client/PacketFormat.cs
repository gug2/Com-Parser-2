using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Com_Parser_2_client
{
    class PacketFormat
    {
        private readonly string sourceCodePath, outputAssemblyPath;
        private Assembly assembly;

        public PacketFormat(string sourceCodePath, string outputAssemblyPath)
        {
            this.sourceCodePath = sourceCodePath;
            this.outputAssemblyPath = outputAssemblyPath;
        }
        
        public bool AssemblyCode(bool KeepInTempDir = false)
        {
            if (!File.Exists(sourceCodePath))
            {
                Console.WriteLine("Ошибка компиляции! Файл скрипта не найден. Путь: \"{0}\"", Path.GetFullPath(sourceCodePath));
                return false;
            }

            Console.WriteLine("Загрузка скрипта...");

            string[] source = PreprocessSource();

            Console.WriteLine("Компиляция...");

            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters(null, outputAssemblyPath, false)
            {
                // compile as dll
                GenerateExecutable = false,
                // compile as physical file
                GenerateInMemory = false,
            };

            if (KeepInTempDir)
            {
                parameters.TempFiles = new TempFileCollection("dll_bin", true);
            }

            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, String.Join("\n", source));
            
            if (results.Errors.HasErrors)
            {
                Console.WriteLine("Ошибка компиляции для сборки \"{0}\"", outputAssemblyPath);
                foreach (CompilerError error in results.Errors)
                {
                    Console.WriteLine(error.ToString());
                }
                return false;
            }

            Console.WriteLine("Сборка \"{0}\" скомпилирована.", outputAssemblyPath);

            assembly = Assembly.LoadFrom(outputAssemblyPath);

            return true;
        }

        public object GetInputStruct()
        {
            if (!File.Exists(outputAssemblyPath) || assembly == null)
            {
                Console.WriteLine("Файл сборки не найден. Путь: \"{0}\"", Path.GetFullPath(outputAssemblyPath));
                return null;
            }
            
            string inDataName = "inData";
            Type inDataType = assembly.GetTypes().FirstOrDefault(type => type.Name == inDataName);

            if (inDataType == null)
            {
                Console.WriteLine("Не найдена структура с именем \"{0}\".", inDataName);
                return null;
            }

            return Activator.CreateInstance(inDataType);
        }

        public object GetOutputStruct(object inputStruct)
        {
            if (!File.Exists(outputAssemblyPath) || assembly == null)
            {
                Console.WriteLine("Файл сборки не найден. Путь: \"{0}\"", Path.GetFullPath(outputAssemblyPath));
                return null;
            }

            object outStruct = null;

            object[] args = new object[] { inputStruct, outStruct };

            assembly.GetTypes()[0].GetMethod("ProcessData").Invoke(null, args);

            return args[1];
        }

        private string[] PreprocessSource()
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

        private byte[] MarshalSerialize(object target)
        {
            int size = Marshal.SizeOf(target);
            byte[] array = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(target, ptr, true);
            Marshal.Copy(ptr, array, 0, size);
            Marshal.FreeHGlobal(ptr);
            return array;
        }

        private object MarshalDeserialize(Type type)
        {
            int size = Marshal.SizeOf(type);
            IntPtr ptr = Marshal.AllocHGlobal(size);

            byte[] source = new byte[size];
            Marshal.Copy(source, 0, ptr, size);

            object structure = Marshal.PtrToStructure(ptr, type);
            Marshal.FreeHGlobal(ptr);

            return structure;
        }

        public static object MarshalDeserializeByArray(Type type, byte[] array)
        {
            int size = Marshal.SizeOf(type);
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.Copy(array, 0, ptr, size);

            object structure = Marshal.PtrToStructure(ptr, type);
            Marshal.FreeHGlobal(ptr);

            return structure;
        }

        public static int SizeOf(object obj)
        {
            return Marshal.SizeOf(obj);
        }
    }
}
