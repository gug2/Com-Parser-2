using System;
using System.ComponentModel;
using System.IO.Ports;

namespace Com_Parser_2
{
    class SerialPortLogic
    {
        [Browsable(false)]
        public SerialPort Port { private set; get; }

        [Browsable(false)]
        public bool HasAvailablePorts { private set; get; }

        public event EventHandler Opening;
        public event EventHandler Closing;

        public string[] ScanPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            HasAvailablePorts = ports.Length > 0;

            return ports;
        }

        public void Connect(string name, SerialPortSettings settings)
        {
            if (Port != null && Port.IsOpen) throw new Exception("Порт уже открыт.");

            try
            {
                Port = new SerialPort(name, settings.Speed, settings.Parity, settings.DataBits, settings.StopBits);

                if (Opening != null)
                {
                    Opening.Invoke(this, EventArgs.Empty);
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
            if (Port == null || !Port.IsOpen)
            {
                return;
            }

            if (Closing != null)
            {
                Closing.Invoke(this, EventArgs.Empty);
            }

            Port.Close();
            Port.Dispose();
            Port = null;
        }
    }
}
