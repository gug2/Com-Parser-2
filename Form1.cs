using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Linq;

namespace Com_Parser_2
{
    public partial class MainForm : Form
    {
        private GlobalFields.SerialDataDisplayHandler serialDataDisplayHandler;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Error(string msg)
        {
            this.status.Text = DateTime.Now.ToLongTimeString() + " -> " + msg;
            this.status.ForeColor = System.Drawing.Color.Red;
        }

        private void Info(string msg)
        {
            this.status.Text = DateTime.Now.ToLongTimeString() + " -> " + msg;
            this.status.ForeColor = System.Drawing.Color.Green;
        }

        private void UpdatePortNames()
        {
            string[] portNames = new string[] { };
            bool hasAvailableSerials = false;

            try
            {
                portNames = SerialPort.GetPortNames();

                this.serialNamesList.DataSource = portNames;
                hasAvailableSerials = portNames.Length > 0;   
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Error(ex.ToString());
            }

            this.serialSpeedsList.Enabled = hasAvailableSerials;

            if (!hasAvailableSerials)
            {
                this.serialNamesList.DataSource = new string[] { "None" };
                Error("Последовательные порты не найдены!");
            }
            else
            {
                Info("Найдено " + portNames.Length + " последовательных портов.");
            }
        }

        private void ConnectToSerial()
        {
            if(GlobalFields.get().IsCurrentSerialEmpty())
            {
                try
                {
                    SerialPort serial = new SerialPort(this.serialNamesList.SelectedItem.ToString(), Convert.ToInt32(GlobalFields.serialSpeeds[this.serialSpeedsList.SelectedIndex]), Parity.None, 8, StopBits.One);
                    GlobalFields.get().SetCurrentSerial(serial);
                    
                    if (!GlobalFields.get().IsCurrentSerialEmpty() && !GlobalFields.get().GetCurrentSerial().IsOpen)
                    {
                        GlobalFields.get().GetCurrentSerial().Open();
                        Info("Порт " + GlobalFields.get().GetCurrentSerial().PortName + " открыт!");

                        UpdateSerialRxCount(0);
                        GlobalFields.get().GetCurrentSerial().DataReceived += new SerialDataReceivedEventHandler(this.SerialDataReceive);
                    }
                }
                catch (Exception e)
                {
                    GlobalFields.get().ResetCurrentSerial();

                    Error(e.ToString());
                    return;
                }
            }

            this.connectToSerial.Click -= new System.EventHandler(this.connectToSerial_Click);
            this.connectToSerial.Click += new System.EventHandler(this.DisconnectFromSerial);
            this.connectToSerial.Text = "Откл.";

            this.serialStatus.Text = "Подключено";
            this.serialStatus.ForeColor = System.Drawing.Color.Green;
        }

        private void SerialDataReceive(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;

            //byte[] bytes = new byte[60];
            //int symbol = port.ReadByte();
            GlobalFields.get().AddToSerialBuffer((byte)port.ReadByte());

            //if (symbol != (int)'$') return;

            //bytes[0] = (byte)symbol;
            //int readed = port.Read(bytes, 1, 59);

            //if (readed == 0) return;
            //if (bytes[58] != (byte)'\n') return;

            //string data = BitConverter.ToString(bytes);
            //this.serialDataArea.Invoke(this.serialDataDisplayHandler, new object[] { data });
        }

        private void SerialDataDisplay(string data)
        {
            this.serialDataArea.AppendText(data);
            this.serialDataArea.ScrollToCaret();
            UpdateSerialRxCount(GlobalFields.get().serialRxCount + 1);
        }

        public void UpdateSerialRxCount(int value)
        {
            if (GlobalFields.get().serialRxCount != value)
            {
                GlobalFields.get().serialRxCount = value;
            }

            this.serialRxCount.Text = GlobalFields.get().serialRxCount.ToString();
        }

        private void DisconnectFromSerial(object sender, EventArgs e)
        {
            if (!GlobalFields.get().IsCurrentSerialEmpty())
            {
                try
                {
                    if (GlobalFields.get().GetCurrentSerial().IsOpen)
                    {
                        GlobalFields.get().GetCurrentSerial().DataReceived -= new SerialDataReceivedEventHandler(this.SerialDataReceive);
                        GlobalFields.get().GetCurrentSerial().Close();

                        Info("Порт " + GlobalFields.get().GetCurrentSerial().PortName + " закрыт!");
                        GlobalFields.get().ResetCurrentSerial();
                    }
                }
                catch (Exception ex)
                {
                    Error(ex.ToString());
                    return;
                }
            }

            this.connectToSerial.Click -= new System.EventHandler(this.DisconnectFromSerial);
            this.connectToSerial.Click += new System.EventHandler(this.connectToSerial_Click);
            this.connectToSerial.Text = "Подкл.";

            this.serialStatus.Text = "Отключено";
            this.serialStatus.ForeColor = System.Drawing.Color.Red;
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.serialDataDisplayHandler = new GlobalFields.SerialDataDisplayHandler(this.SerialDataDisplay);

            UpdatePortNames();
            int defaultSerialSpeed = 9600;

            this.serialSpeedsList.DataSource = GlobalFields.serialSpeeds.Select(value => value + " бод").ToArray();
            this.serialSpeedsList.SelectedIndex = Array.FindIndex(GlobalFields.serialSpeeds, value => value == defaultSerialSpeed);
        }

        private void обновитьF5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePortNames();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                UpdatePortNames();
            }
        }

        private void connectToSerial_Click(object sender, EventArgs e)
        {
            ConnectToSerial();
        }

        private void раширенныеНастройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalFields.get().ShowExtendedSerialSettingsForm();
        }
    }
}
