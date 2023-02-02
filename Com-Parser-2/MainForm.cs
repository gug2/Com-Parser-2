using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Linq;
using System.IO;

namespace Com_Parser_2
{
    public partial class MainForm : Form
    {
        private GlobalFields.SerialDataDisplayHandler serialDataDisplayHandler;

        private FileStream LogStream;

        public MainForm()
        {
            InitializeComponent();
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
                Status.Error(this.status, ex.ToString());
            }

            this.serialSpeedsList.Enabled = hasAvailableSerials;

            if (!hasAvailableSerials)
            {
                this.serialNamesList.DataSource = new string[] { "None" };
                Status.Error(this.status, "Последовательные порты не найдены!");
            }
            else
            {
                Status.Info(this.status, "Найдено " + portNames.Length + " последовательных портов.");
            }
        }

        private void ConnectToSerial()
        {
            if(GlobalFields.Get().IsCurrentSerialEmpty())
            {
                try
                {
                    SerialPort serial = new SerialPort(this.serialNamesList.SelectedItem.ToString(), Convert.ToInt32(GlobalFields.serialSpeeds[this.serialSpeedsList.SelectedIndex]), GlobalFields.Get().extendedSerialSettingsForm.getParity(), GlobalFields.Get().extendedSerialSettingsForm.getDataBits(), GlobalFields.Get().extendedSerialSettingsForm.getStopBit());
                    GlobalFields.Get().SetCurrentSerial(serial);
                    
                    if (!GlobalFields.Get().IsCurrentSerialEmpty() && !GlobalFields.Get().GetCurrentSerial().IsOpen)
                    {
                        GlobalFields.Get().GetCurrentSerial().Open();
                        Status.Info(this.status, "Порт " + GlobalFields.Get().GetCurrentSerial().PortName + " открыт!");

                        UpdateSerialRxCount(0);
                        GlobalFields.Get().GetCurrentSerial().DataReceived += new SerialDataReceivedEventHandler(this.SerialDataReceive);

                        LogStream = File.Open("log.bin", FileMode.OpenOrCreate, FileAccess.Write);

                        if (!this.messagesPerSecondTimer.Enabled)
                        {
                            this.messagesPerSecondTimer.Start();
                        }
                    }
                }
                catch (Exception e)
                {
                    GlobalFields.Get().ResetCurrentSerial();

                    Status.Error(this.status, e.ToString());
                    return;
                }
            }

            this.connectToSerial.Click -= new EventHandler(this.connectToSerial_Click);
            this.connectToSerial.Click += new EventHandler(this.DisconnectFromSerial);
            this.connectToSerial.Text = "Откл.";

            this.serialStatus.Text = "Подключено";
            this.serialStatus.ForeColor = System.Drawing.Color.Green;
        }

        private void SerialDataReceive(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;

            if (port.BytesToRead == 0) return;

            byte[] b = new byte[port.BytesToRead];

            port.Read(b, 0, b.Length);

            var asyncWrite = AsyncDataLogging.Write(LogStream, b);
            var asyncFlush = AsyncDataLogging.Flush(LogStream);

            /*byte[] bytes = new byte[GlobalFields.MESSAGE_SIZE];

            if (port.BytesToRead < bytes.Length)
            {
                return;
            }

            port.Read(bytes, 0, bytes.Length);

            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            foreach (byte b in bytes)
            {
                if (this.serialHexMode.Checked)
                {
                    builder.Append(b.ToString("X").PadLeft(2, '0') + " ");
                }
                else
                {
                    builder.Append((char)b == '\0' ? "end" : " " + (char)b);
                }
            }

            string data = builder.ToString();*/
            this.serialDataArea.Invoke(this.serialDataDisplayHandler, new object[] { null });
        }

        private void SerialDataDisplay(string data)
        {
            this.serialDataArea.AppendText(data);
            this.serialDataArea.ScrollToCaret();
            UpdateSerialRxCount(GlobalFields.Get().SerialRxCount + 1);

            if (GlobalFields.Get().EnableMeasure)
            {
                GlobalFields.Get().MessagesPerSecondCounter += 1;
            }
        }

        public void UpdateSerialRxCount(int value)
        {
            if (GlobalFields.Get().SerialRxCount != value)
            {
                GlobalFields.Get().SerialRxCount = value;
                this.serialRxCount.Text = GlobalFields.Get().SerialRxCount.ToString();
            }
        }

        private void DisconnectFromSerial(object sender, EventArgs e)
        {
            if (!GlobalFields.Get().IsCurrentSerialEmpty())
            {
                try
                {
                    if (GlobalFields.Get().GetCurrentSerial().IsOpen)
                    {
                        if (this.messagesPerSecondTimer.Enabled)
                        {
                            this.messagesPerSecondTimer.Stop();
                        }

                        GlobalFields.Get().GetCurrentSerial().DataReceived -= new SerialDataReceivedEventHandler(this.SerialDataReceive);
                        GlobalFields.Get().GetCurrentSerial().Close();

                        if (LogStream != null)
                        {
                            LogStream.Close();
                            LogStream.Dispose();
                        }

                        Status.Info(this.status, "Порт " + GlobalFields.Get().GetCurrentSerial().PortName + " закрыт!");
                        GlobalFields.Get().ResetCurrentSerial();
                    }
                }
                catch (Exception ex)
                {
                    Status.Error(this.status, ex.ToString());
                    return;
                }
            }

            this.connectToSerial.Click -= new EventHandler(this.DisconnectFromSerial);
            this.connectToSerial.Click += new EventHandler(this.connectToSerial_Click);
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
                e.Handled = true;
            }
        }

        private void connectToSerial_Click(object sender, EventArgs e)
        {
            ConnectToSerial();
        }

        private void раширенныеНастройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalFields.Get().ShowExtendedSerialSettingsForm();
        }

        private void messagesPerSecondTimer_Tick(object sender, EventArgs e)
        {
            this.messagesPerSecond.Text = GlobalFields.Get().MessagesPerSecondCounter.ToString();
            GlobalFields.Get().MessagesPerSecondCounter = 0;

            GlobalFields.Get().EnableMeasure = !GlobalFields.Get().EnableMeasure;
        }
    }
}
