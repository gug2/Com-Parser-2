using System;
using System.IO;
using System.IO.Ports;
using System.Runtime.Serialization.Formatters.Binary;

namespace Com_Parser_2
{
    class GlobalFields
    {
        // длина пакета принимаемого сообщения
        public static readonly int MESSAGE_SIZE = 60;
        // частота принимаемых сообщений
        private static readonly int MESSAGES_PER_SECOND = 4;
        // предполагаемый запас для буффера в секундах
        private static readonly int WORKING_TIMEOUT_SEC = 10;
        // итоговый размер буффера
        private static readonly int SERIAL_BUFFER_SIZE = MESSAGE_SIZE * MESSAGES_PER_SECOND * WORKING_TIMEOUT_SEC;

        private static readonly GlobalFields instance = new GlobalFields();
        
        private SerialPort currentSerial = null;
        public static readonly int[] serialSpeeds = { 75, 110, 300, 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200 };

        public delegate void SerialDataDisplayHandler(string data);
        public delegate void MessagesPerSecondHandler(int messagesPerSecond);


        public static GlobalFields Get()
        {
            return instance;
        }

        public bool IsCurrentSerialEmpty()
        {
            return currentSerial == null;
        }

        public void ResetCurrentSerial()
        {
            currentSerial = null;
        }

        public void SetCurrentSerial(SerialPort serial)
        {
            currentSerial = serial;
        }

        public SerialPort GetCurrentSerial()
        {
            return currentSerial;
        }

        public ExtendedSerialSettingsForm extendedSerialSettingsForm = new ExtendedSerialSettingsForm();
        private bool essfOpened = false;

        public void ShowExtendedSerialSettingsForm()
        {
            if (!essfOpened)
            {
                extendedSerialSettingsForm = new ExtendedSerialSettingsForm();
                essfOpened = true;
            }

            extendedSerialSettingsForm.Show();
            extendedSerialSettingsForm.Select();
        }

        public void NotifyExtendedSerialSettingsFormClosed()
        {
            essfOpened = false;
        }
        
        public void SaveFormSettings(string file, FormSetting[] settings)
        {
            // сделать сериализацию объекта настройки
            using (FileStream w = new FileStream(file, FileMode.OpenOrCreate))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                
                binaryFormatter.Serialize(w, settings);
            }
        }

        public FormSetting[] LoadFormSettings(string file)
        {
            if (!File.Exists(file))
            {
                return null;
            }

            // сделать десериализацию объекта настройки
            object obj;

            using (FileStream r = new FileStream(file, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                obj = binaryFormatter.Deserialize(r);
            }

            if (obj is FormSetting[] result)
            {
                return result;
            }

            throw new Exception(String.Format("Ошибка десериализации объекта {0}!", obj));
        }

        public int SerialRxCount { set; get; }
        public bool EnableMeasure { set; get; }
        public int MessagesPerSecondCounter { set; get; }
    }

    [Serializable]
    public class FormSetting
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public FormSetting(string fieldName, object fieldValue)
        {
            Name = fieldName;
            Value = fieldValue.ToString();
        }
    }
}
