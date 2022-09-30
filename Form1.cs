using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace Com_Parser_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox1.Items.AddRange(GetAvailableSerialPorts().ToArray());
        }

        private List<string> GetAvailableSerialPorts()
        {
            List<string> available = new List<string>();
            for (int i = 0; i < 256; i++)
            {
                available.Add("COM" + i);
            }

            SerialPort port = null;
            foreach (string name in available)
            {
                try
                {
                    port = new SerialPort(name, 9600, Parity.None, 8, StopBits.One);
                    //port.Close();
                }
                catch
                {
                    available.Remove(name);
                }
            }

            return available;
        }
    }
}
