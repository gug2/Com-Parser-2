using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Linq;
using System.IO;
using System.Drawing;

namespace Com_Parser_2
{
    public partial class MainForm : Form
    {
        private const string LOGFILE_NAME = "log.bin";
        private const int DEFAULT_PORT_SPEED = 9600;
        //private GlobalFields.SerialDataDisplayHandler serialDataDisplayHandler;
        private FileStream LogStream;
        private readonly SerialPortLogic portLogic = new SerialPortLogic();
        private AsyncDataLogging asyncDataLogging;

        public MainForm()
        {
            InitializeComponent();
            portLogic.Opened += PortLogic_Opened;
            portLogic.Closed += PortLogic_Closed;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //serialDataDisplayHandler = SerialDataDisplay;

            PacketFormat.Test();

            SerialNames.DataSource = new string[] { "None" };
            ScanPorts();

            SerialSpeeds.DataSource = GlobalFields.serialSpeeds.Select(elem => elem + " бод").ToArray();
            SerialSpeeds.SelectedIndex = Array.FindIndex(GlobalFields.serialSpeeds, elem => elem == DEFAULT_PORT_SPEED);
        }

        private void PortLogic_Opened(object sender, EventArgs e)
        {
            UpdateSerialRxCount(0);
            portLogic.Port.DataReceived += Port_DataReceived;

            LogStream = File.Open(LOGFILE_NAME, FileMode.OpenOrCreate, FileAccess.Write);
            asyncDataLogging = new AsyncDataLogging(LogStream);

            if (!MessagesPerSecondTimer.Enabled)
            {
                MessagesPerSecondTimer.Start();
            }
        }

        private void PortLogic_Closed(object sender, EventArgs e)
        {
            if (MessagesPerSecondTimer.Enabled)
            {
                MessagesPerSecondTimer.Stop();
            }

            portLogic.Port.DataReceived -= Port_DataReceived;

            if (LogStream != null)
            {
                LogStream.Flush();
                LogStream.Close();
                LogStream.Dispose();
            }
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // обрабатывать искючения порта в случае закрытия/зависа порта

            SerialPort port = (SerialPort)sender;

            if (port.BytesToRead == 0) return;

            byte[] b = new byte[port.BytesToRead];

            port.Read(b, 0, b.Length);

            var asyncWrite = asyncDataLogging.Write(b);
            var asyncFlush = asyncDataLogging.Flush();

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
            //this.serialDataArea.Invoke(this.serialDataDisplayHandler, new object[] { null });
        }

        private void ScanPorts()
        {
            try
            {
                if (SerialPortLogic.ScanPorts(out string[] ports))
                {
                    SerialNames.DataSource = ports;
                    SerialSpeeds.Enabled = ports.Length > 0;

                    StatusLogging.Info(Status, String.Format("Найдено {0} последовательных портов.", ports.Length));
                }
                else
                {
                    StatusLogging.Error(Status, "Последовательные порты не найдены!");
                }
            }
            catch (Exception e)
            {
                StatusLogging.Error(Status, e.ToString());
            }
        }

        private void OpenPort(object sender, EventArgs args)
        {
            string name = SerialNames.SelectedItem.ToString();
            int speed = GlobalFields.serialSpeeds[SerialSpeeds.SelectedIndex];
            Parity parity = GlobalFields.Get().extendedSerialSettingsForm.getParity();
            int dataBits = GlobalFields.Get().extendedSerialSettingsForm.getDataBits();
            StopBits stopBit = GlobalFields.Get().extendedSerialSettingsForm.getStopBit();

            portLogic.Port = new SerialPort(name, speed, parity, dataBits, stopBit);

            try
            {
                portLogic.Connect();
                StatusLogging.Info(Status, String.Format("Порт {0} открыт.", portLogic.Port.PortName));

                ConnectToSerial.Click -= OpenPort;
                ConnectToSerial.Click += ClosePort;
                ConnectToSerial.Text = "Откл.";

                SerialStatus.Text = "Подключено";
                SerialStatus.ForeColor = Color.Green;
            }
            catch (Exception e)
            {
                StatusLogging.Error(Status, e.ToString());
            }
        }

        private void ClosePort(object sender, EventArgs args)
        {
            try
            {
                portLogic.Disconnect();
                StatusLogging.Info(Status, String.Format("Порт {0} закрыт.", portLogic.Port.PortName));

                ConnectToSerial.Click -= ClosePort;
                ConnectToSerial.Click += OpenPort;
                ConnectToSerial.Text = "Подкл.";

                SerialStatus.Text = "Отключено";
                SerialStatus.ForeColor = Color.Red;
            }
            catch (Exception e)
            {
                StatusLogging.Error(Status, e.ToString());
            }
        }

        /*private void ConnectToSerial()
        {
            if(GlobalFields.Get().IsCurrentSerialEmpty())
            {
                try
                {
                    SerialPort serial = new SerialPort(this.SerialNames.SelectedItem.ToString(), Convert.ToInt32(GlobalFields.serialSpeeds[this.SerialSpeeds.SelectedIndex]), GlobalFields.Get().extendedSerialSettingsForm.getParity(), GlobalFields.Get().extendedSerialSettingsForm.getDataBits(), GlobalFields.Get().extendedSerialSettingsForm.getStopBit());
                    GlobalFields.Get().SetCurrentSerial(serial);
                    
                    if (!GlobalFields.Get().IsCurrentSerialEmpty() && !GlobalFields.Get().GetCurrentSerial().IsOpen)
                    {
                        GlobalFields.Get().GetCurrentSerial().Open();
                        Com_Parser_2.Status.Info(this.Status, "Порт " + GlobalFields.Get().GetCurrentSerial().PortName + " открыт!");

                        UpdateSerialRxCount(0);
                        GlobalFields.Get().GetCurrentSerial()..DataReceived += new SerialDataReceivedEventHandler(this.SerialDataReceive);

                        
                    }
                }
                catch (Exception e)
                {
                    GlobalFields.Get().ResetCurrentSerial();

                    Com_Parser_2.Status.Error(this.Status, e.ToString());
                    return;
                }
            }

            this.ConnectToSerial.Click -= new EventHandler(this.ConnectToSerial_Click);
            this.ConnectToSerial.Click += new EventHandler(this.DisconnectFromSerial);
            this.ConnectToSerial.Text = "Откл.";

            this.SerialStatus.Text = "Подключено";
            this.SerialStatus.ForeColor = System.Drawing.Color.Green;
        }*/

        /*private void DisconnectFromSerial(object sender, EventArgs e)
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

                        Com_Parser_2.Status.Info(this.Status, "Порт " + GlobalFields.Get().GetCurrentSerial().PortName + " закрыт!");
                        GlobalFields.Get().ResetCurrentSerial();
                    }
                }
                catch (Exception ex)
                {
                    Com_Parser_2.Status.Error(this.Status, ex.ToString());
                    return;
                }
            }

            this.ConnectToSerial.Click -= new EventHandler(this.DisconnectFromSerial);
            this.ConnectToSerial.Click += new EventHandler(this.connectToSerial_Click);
            this.ConnectToSerial.Text = "Подкл.";

            this.SerialStatus.Text = "Отключено";
            this.SerialStatus.ForeColor = System.Drawing.Color.Red;
        }*/

        /*private void SerialDataReceive(object sender, SerialDataReceivedEventArgs e)
        {
            
        }*/

        /*private void SerialDataDisplay(string data)
        {
            //this.serialDataArea.AppendText(data);
            //this.serialDataArea.ScrollToCaret();
            UpdateSerialRxCount(GlobalFields.Get().SerialRxCount + 1);

            if (GlobalFields.Get().EnableMeasure)
            {
                GlobalFields.Get().MessagesPerSecondCounter += 1;
            }
        }*/

        public void UpdateSerialRxCount(int value)
        {
            if (GlobalFields.Get().SerialRxCount != value)
            {
                GlobalFields.Get().SerialRxCount = value;
                SerialRxCount.Text = GlobalFields.Get().SerialRxCount.ToString();
            }
        }

        private void UpdatePorts_TS_Click(object sender, EventArgs e)
        {
            ScanPorts();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                ScanPorts();
                e.Handled = true;
            }
        }

        private void ConnectToSerial_Click(object sender, EventArgs e)
        {
            OpenPort(sender, e);
        }

        private void PortExtendedSettings_TS_Click(object sender, EventArgs e)
        {
            GlobalFields.Get().ShowExtendedSerialSettingsForm();
        }

        private void ImportPacketFormat_TS_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog()) {
                dialog.Filter = "Скрипт формата пакета|*.cs";
                dialog.Multiselect = false;
                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string name = Path.GetFileNameWithoutExtension(dialog.FileName);
                    string directory = Path.GetDirectoryName(dialog.FileName);
                    if (!string.IsNullOrEmpty(directory))
                    {
                        directory += '\\';
                    }
                    PacketFormat.AssemblyCode(dialog.FileName, directory + name + ".dll");
                }
            }
        }

        private void MessagesPerSecondTimer_Tick(object sender, EventArgs e)
        {
            MessagesPerSecond.Text = GlobalFields.Get().MessagesPerSecondCounter.ToString();
            GlobalFields.Get().MessagesPerSecondCounter = 0;

            GlobalFields.Get().EnableMeasure = !GlobalFields.Get().EnableMeasure;
        }

    }
}
