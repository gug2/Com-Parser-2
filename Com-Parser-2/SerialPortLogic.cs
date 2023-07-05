using System;
using System.IO.Ports;

namespace Com_Parser_2
{
    class SerialPortLogic
    {
        public static readonly SerialPortLogic Instance = new SerialPortLogic();

        private SerialPort Port;
        public bool HasAvailablePorts { private set; get; }

        public event EventHandler Opening;
        public event EventHandler Closing;

        private SerialPortLogic()
        {

        }

        public string[] ScanPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            HasAvailablePorts = ports.Length > 0;

            return ports;
        }

        public bool Poll()
        {
            if (Port == null)
            {
                return false;
            }
            if (Port.IsOpen)
            {
                return false;
            }

            try
            {
                // проверяем не оборвалось ли соединение с последовательным портом
                int i = Port.BytesToRead;
            }
            catch
            {
                if (Closing != null)
                {
                    Closing.Invoke(Port, EventArgs.Empty);
                }

#warning maybe crash
                Port.Close();
                Port.Dispose();
                Port = null;

                return false;
            }

            return true;
        }

        public void Connect(string name, SerialPortSettings settings)
        {
            if (Port != null && Port.IsOpen)
            {
                throw new Exception("Порт уже открыт.");
            }

            try
            {
                Port = new SerialPort(name, settings.Speed, settings.Parity, settings.DataBits, settings.StopBits);

                if (Opening != null)
                {
                    Opening.Invoke(Port, EventArgs.Empty);
                }

                Port.Open();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Disconnect()
        {
            if (Port == null)
            {
                return;
            }
            if (!Port.IsOpen)
            {
                return;
            }

            if (Closing != null)
            {
                Closing.Invoke(Port, EventArgs.Empty);
            }

            Port.Close();
            Port.Dispose();
            Port = null;
        }
    }
}
