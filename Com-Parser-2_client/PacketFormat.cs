using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Com_Parser_2_client
{
    public class PacketFormat
    {
        public bool ScriptCompiled { get; }
        public byte[] StartMarkPattern { get; }
        public bool ValidateByChecksum { get; }
        public int ChecksumPosition { get; }
        public int PacketSize { get; }

#warning сделать коллекцию неизменяемой извне
        private List<DataObject> DataObjects { set; get; }

        private readonly Assembly assembly;
        private readonly Type type;

        public PacketFormat(string scriptPath)
        {
            UserScript script = new UserScript(scriptPath);
            ScriptCompiled = script.Compile();

            if (!ScriptCompiled)
            {
                return;
            }

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
            PacketSize = Marshal.SizeOf(inputStruct);
            type = inputStruct.GetType();

            ChecksumPosition = GetPacketProperty<int>("ChecksumPosition");
            if (ChecksumPosition == 0)
            {
                ChecksumPosition = PacketSize - 1;
            }
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

        private object GetInputStruct()
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

        private object GetOutputStruct(object inputStruct)
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

        public List<DataObject> GetOrCreateDataObjects()
        {
            if (DataObjects == null)
            {
                object o = GetOutputStruct(GetInputStruct());
                DataObjects = o.GetType().GetFields().Select(field => DataObject.From(field, o)).ToList();
            }
            
            return DataObjects;
        }

        public List<DataObject> ReadDataObjectsFrom(byte[] buffer)
        {
            object input = MarshalDeserialize(buffer);
            object o = GetOutputStruct(input);
            DataObjects = o.GetType().GetFields().Select(field => DataObject.From(field, o)).ToList();

            return DataObjects;
        }

        private object MarshalDeserialize(byte[] array)
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
