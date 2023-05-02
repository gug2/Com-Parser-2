using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.ComponentModel;

namespace Com_Parser_2
{
    class SerialPortLogic
    {
        [Browsable(false)]
        public SerialPort Port { set; get; }

        [Browsable(false)]
        public bool HasAvailablePorts { private set; get; }

        public event EventHandler Opened;
        public event EventHandler Closed;

        public static bool ScanPorts(out string[] ports)
        {
            ports = SerialPort.GetPortNames();

            return ports.Length > 0;
        }

        public void Connect()
        {
            if (Port == null || Port.IsOpen)
            {
                return;
            }

            Port.Open();

            if (Opened != null)
            {
                Opened.Invoke(this, EventArgs.Empty);
            }
        }

        public void Disconnect()
        {
            if (Port == null || !Port.IsOpen)
            {
                return;
            }

            Port.Close();

            if (Closed != null)
            {
                Closed.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
