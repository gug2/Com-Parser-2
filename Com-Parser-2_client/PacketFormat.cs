using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Com_Parser_2_client
{
    class PacketFormat
    {
        public byte[] StartMarkPattern { get; }
        public bool ValidateByChecksum { get; }
        public int PacketSize { get; }

        private readonly Assembly assembly;

        public PacketFormat(UserScript script)
        {
            try
            {
                assembly = Assembly.LoadFrom(script.AssemblyPath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка загрузки скрипта {0}.", script.AssemblyPath);
                Console.WriteLine(e.ToString());
                return;
            }

            StartMarkPattern = GetPacketProperty<byte[]>("StartMarkPattern");
            ValidateByChecksum = GetPacketProperty<bool>("ValidateByChecksum");
            object inputStruct = GetInputStruct();
            PacketSize = inputStruct == null ? 0 : Marshal.SizeOf(inputStruct);
        }

        private T GetPacketProperty<T>(string propertyName)
        {
            if (assembly == null)
            {
                Console.WriteLine("Объект скрипта не найден.");
                return Activator.CreateInstance<T>();
            }

            FieldInfo info = assembly.GetTypes()[0].GetFields(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(field => field.FieldType == typeof(T) && field.Name == propertyName);

            if (info == null)
            {
                Console.WriteLine("Не найдена переменная с именем \"{0}\".", propertyName);
                return Activator.CreateInstance<T>();
            }

            return (T)info.GetValue(null);
        }

        public object GetInputStruct()
        {
            if (assembly == null)
            {
                Console.WriteLine("Объект скрипта не найден.");
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
            if (assembly == null)
            {
                Console.WriteLine("Объект скрипта не найден.");
                return null;
            }

            object outStruct = null;

            object[] args = new object[] { inputStruct, outStruct };

            assembly.GetTypes()[0].GetMethod("ProcessData").Invoke(null, args);

            return args[1];
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
    }
}
