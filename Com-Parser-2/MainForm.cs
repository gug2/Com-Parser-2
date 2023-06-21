using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Linq;
using System.IO;
using System.Net.Sockets;
using System.Drawing;
using System.Collections.Generic;

namespace Com_Parser_2
{
    public partial class MainForm : Form
    {
        private const string LOGFILE_EXTENSION = ".bin";
        private readonly List<int> SERIAL_SPEEDS = new List<int>() { 75, 110, 300, 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200 };
        
        private FileStream LogStream;
        private readonly SerialPortLogic portLogic = new SerialPortLogic();
        private AsyncDataLogging asyncDataLogging;
        private AsyncNetTransfer asyncNetTransfer;
        private int RxCountField;
        private int MsgPerSec;
        private int RxCount
        {
            set
            {
                RxCountField = value;
                SerialRxCount.SetFormatArgs(value);
            }
            get
            {
                return RxCountField;
            }
        }

        private delegate void SendToMainThread(object[] args);

        public MainForm()
        {
            InitializeComponent();
            portLogic.Opening += PortLogic_Opening;
            portLogic.Closing += PortLogic_Closing;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SerialNames.DataSource = new string[] { "None" };
            ScanPorts();

            SerialSpeeds.DataSource = SERIAL_SPEEDS.Select(elem => elem + " бод").ToList();
            SerialParities.DataSource = Enum.GetValues(typeof(Parity));
            SerialDataBits.DataSource = new int[] { 5, 6, 7, 8, 9 };
            SerialStopBits.DataSource = Enum.GetValues(typeof(StopBits));

            SerialPortSettings settings = new SerialPortSettings();

            SerialSpeeds.SelectedIndex = SerialSpeeds.FindString(settings.Speed.ToString());
            SerialParities.SelectedItem = settings.Parity;
            SerialDataBits.SelectedIndex = SerialDataBits.FindString(settings.DataBits.ToString());
            SerialStopBits.SelectedItem = settings.StopBits;

            asyncNetTransfer = new AsyncNetTransfer(ServerWorker);
            asyncNetTransfer.ClientConnected += (o, a) =>
            {
                Invoke(new EventHandler(AsyncNetTransfer_ClientConnected), o, a);
            };
            asyncNetTransfer.ClientDisconnecting += (o, a) =>
            {
                Invoke(new EventHandler(AsyncNetTransfer_ClientDisconnecting), o, a);
            };
        }

        private void PortLogic_Opening(object sender, EventArgs e)
        {
            RxCount = 0;

            string path = "log_" + DateTime.Now.ToString().Replace(':', '_') + LOGFILE_EXTENSION;

            LogStream = File.Open(path, FileMode.Create, FileAccess.Write);
            asyncDataLogging = new AsyncDataLogging(LogStream);

            portLogic.Port.DataReceived += Port_DataReceived;

            if (!MessagesPerSecondTimer.Enabled)
            {
                MessagesPerSecondTimer.Start();
            }

            StatusLogging.Info(Status, String.Format("Порт {0} открыт.", portLogic.Port.PortName));

            ConnectToSerial.Click -= ConnectToSerial_Click;
            ConnectToSerial.Click += ClosePort;
            ConnectToSerial.Text = "Откл.";

            SerialStatus.Text = "Подключено";
            SerialStatus.ForeColor = Color.Green;
        }

        private void AsyncNetTransfer_ClientConnected(object sender, EventArgs e)
        {
            if (sender is TcpClient tcpClient)
            {
                string name = tcpClient.Client.RemoteEndPoint.ToString();

                if (!ConnectedClients.Items.Contains(name))
                {
                    ConnectedClients.Items.Add(name);
                }

                StatusLogging.Info(Status, String.Format("Подключен клиент {0}", name));
            }
        }

        private void AsyncNetTransfer_ClientDisconnecting(object sender, EventArgs e)
        {
            if (sender is TcpClient tcpClient)
            {
                string name = tcpClient.Client.RemoteEndPoint.ToString();

                if (ConnectedClients.Items.Contains(name))
                {
                    ConnectedClients.Items.Remove(name);
                }

                StatusLogging.Info(Status, String.Format("Отключен клиент {0}", name));
            }
        }

        private void PortLogic_Closing(object sender, EventArgs e)
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

            StatusLogging.Info(Status, String.Format("Порт закрыт."));

            ConnectToSerial.Click -= ClosePort;
            ConnectToSerial.Click += ConnectToSerial_Click;
            ConnectToSerial.Text = "Подкл.";

            SerialStatus.Text = "Отключено";
            SerialStatus.ForeColor = Color.Red;
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // обрабатывать искючения порта в случае закрытия/зависа порта
            SerialPort port = (SerialPort)sender;

            byte[] b = new byte[port.BytesToRead];

            port.Read(b, 0, b.Length);

            var asyncWrite = asyncDataLogging.Write(b);
            var asyncFlush = asyncDataLogging.Flush();
            asyncNetTransfer.SendToAll(b);

            CallInMainThread((args) =>
            {
                RxCount++;
                MsgPerSec++;
            });
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

        private void CallInMainThread(SendToMainThread target, params object[] args)
        {
            Invoke(target, new object[] { args });
        }

        private void ScanPorts()
        {
            try
            {
                SerialPortLogic portLogic = new SerialPortLogic();
                string[] ports = portLogic.ScanPorts();

                bool flag = portLogic.HasAvailablePorts;

                SerialNames.DataSource = flag ? ports : new string[] { "None" };
                SerialSpeeds.Enabled = flag;
                ShowSerialSettings.Enabled = flag;
                ConnectToSerial.Enabled = flag;

                if (flag)
                {
                    StatusLogging.Info(Status, String.Format("Найдено {0} последовательных портов.", ports.Length));   
                    return;
                }

                StatusLogging.Error(Status, "Последовательные порты не найдены!");
            }
            catch (Exception e)
            {
                StatusLogging.Error(Status, e.ToString());
            }
        }

        private void OpenPort(object sender, EventArgs args)
        {
            string name = SerialNames.SelectedItem.ToString();
            SerialPortSettings settings = new SerialPortSettings()
            {
                Speed = SERIAL_SPEEDS[SerialSpeeds.SelectedIndex],
                Parity = (Parity)SerialParities.SelectedItem,
                DataBits = Convert.ToInt32(SerialDataBits.SelectedItem),
                StopBits = (StopBits)SerialStopBits.SelectedItem
            };

            try
            {
                portLogic.Connect(name, settings);
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
            }
            catch (Exception e)
            {
                StatusLogging.Error(Status, e.ToString());
            }
        }

        private void UpdatePorts_TS_Click(object sender, EventArgs e)
        {
            ScanPorts();
        }

        private void ConnectToSerial_Click(object sender, EventArgs e)
        {
            OpenPort(sender, e);
        }

        private void MessagesPerSecondTimer_Tick(object sender, EventArgs e)
        {
            MessagesPerSecond.Text = MsgPerSec.ToString();
            MsgPerSec = 0;

            if (portLogic.Port != null)
            {
                try
                {
                    // проверяем не оборвалось ли соединение с последовательным портом
                    int i = portLogic.Port.BytesToRead;
                }
                catch (Exception ex)
                {
                    PortLogic_Closing(sender, EventArgs.Empty);
                    StatusLogging.Error(Status, ex.ToString());
                }
            }
        }

        private void ShowSerialSettings_Click(object sender, EventArgs e)
        {
            if (SerialSettingsFlowPanel.Visible)
            {
                ShowSerialSettings.BackgroundImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                ShowSerialSettings.Invalidate();
                SerialSettingsFlowPanel.Hide();
            }
            else
            {
                ShowSerialSettings.BackgroundImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                ShowSerialSettings.Invalidate();
                SerialSettingsFlowPanel.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectedClients.Items.Clear();
            asyncNetTransfer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            asyncNetTransfer.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*char startMark = '$';
            byte[] packet = new byte[60];

            Random r = new Random();
            r.NextBytes(packet);
            packet[0] = (byte)startMark;

            byte hor = packet[0];
            
            for (int i = 1; i < packet.Length - 1; i++)
            {
                hor ^= packet[i];
            }
            packet[packet.Length - 1] = hor;*/

            //asyncNetTransfer.SendToAll(packet);
        }
    }
}
