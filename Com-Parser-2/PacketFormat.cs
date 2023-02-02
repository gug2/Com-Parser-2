using System;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.Linq;

namespace Com_Parser_2
{
    class PacketFormat
    {
        // Вызывать в событии Form_Load
        public static void Test()
        {
            // lsb first format
            byte[] byteArr = new byte[] { 0xAA, 0xBB, 0xA4, 0x70, 0x9D, 0x3F, 0x11, 0x22, 0xFF, 0xCC, 0xDD, 0xEE };
            // last byte - xor checksum of all bytes, exclude last byte


            AssemblyCode("loadme.cs", "loadme.dll");
            GetAssembly("loadme.dll", byteArr);
        }


        public static void AssemblyCode(string sourceCodePath, string outputAssemblyPath)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters(null, outputAssemblyPath, false)
            {
                // compile as dll
                GenerateExecutable = false,
                // compile as physical file
                GenerateInMemory = false
            };
            CompilerResults results = codeProvider.CompileAssemblyFromFile(parameters, sourceCodePath);

            if (results.Errors.HasErrors)
            {
                Console.WriteLine("There are some errors during compilation of \"{0}\"", outputAssemblyPath);
                foreach (CompilerError error in results.Errors)
                {
                    Console.WriteLine(error.ToString());
                }
            }
            else
            {
                Console.WriteLine("Compilation of \"{0}\" was successfull!", outputAssemblyPath);
            }
        }

        public static void GetAssembly(string assemblyPath, byte[] byteArray)
        {
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
