using System.IO;
using System.IO.Ports;

namespace Com_Parser_2
{
    class GlobalFields
    {
        // длина пакета принимаемого сообщения
        private static readonly int MESSAGE_SIZE = 60;
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

        public static GlobalFields get()
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

        private ExtendedSerialSettingsForm extendedSerialSettingsForm = null;
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
            using (StreamWriter w = new StreamWriter(file, false))
            {
                foreach (FormSetting setting in settings)
                {
                    w.WriteLine(setting.Name + " " + setting.Value);
                }
            }
        }

        public FormSetting[] LoadFormSettings(string file)
        {
            if (!File.Exists(file))
            {
                return null;
            }

            string[] readed = File.ReadAllLines(file);

            FormSetting[] result = new FormSetting[readed.Length];
            int i = 0;

            foreach (string line in readed)
            {
                string[] array = line.Split(' ');

                if (array.Length == 2)
                {
                    result[i] = new FormSetting(array[0], array[1]);
                }
                else
                {
                    // Если ошибка чтения, оставляем значение пустым, оно будет по умолчанию
                    result[i] = null;
                }

                i++;
            }

            return result;
        }

        public int serialRxCount { set; get; }

        private byte[] serialBuffer = new byte[SERIAL_BUFFER_SIZE];
        private int serialBufferWriteIndex = 0;
        private int serialBufferReadIndex = 0;

        public void AddToSerialBuffer(byte value)
        {
            if (serialBufferWriteIndex >= serialBuffer.Length)
            {
                return;
            }

            serialBuffer[serialBufferWriteIndex++] = value;
        }
    }

    class FormSetting
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public FormSetting(string fieldName, object fieldValue)
        {
            Name = fieldName;
            Value = fieldValue.ToString();
        }
    }
}
