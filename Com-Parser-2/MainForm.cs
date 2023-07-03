﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Com_Parser_2
{
    public partial class MainForm : Form
    {
        private const string LOGFILE_EXTENSION = ".bin";
        private readonly List<int> SERIAL_SPEEDS = new List<int>() { 75, 110, 300, 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200 };

        public static StatusLogging StatusLogging;

        private readonly SerialPortLogic portLogic = SerialPortLogic.Instance;
        private AsyncDataLogging asyncDataLogging;
        private AsyncNetTransfer asyncNetTransfer;
        private int RxCountField;
        private int PacketPerSec;
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

        public MainForm()
        {
            InitializeComponent();
            StatusLogging = StatusLogging.From(Status);

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

            SerialPortSettings settings = SerialPortSettings.Default;
            SerialSpeeds.SelectedIndex = SerialSpeeds.FindString(settings.Speed.ToString());
            SerialParities.SelectedItem = settings.Parity;
            SerialDataBits.SelectedIndex = SerialDataBits.FindString(settings.DataBits.ToString());
            SerialStopBits.SelectedItem = settings.StopBits;

            asyncNetTransfer = new AsyncNetTransfer(ServerWorker);
            asyncNetTransfer.ClientConnected += (obj, args) =>
            {
                Invoke(new EventHandler(AsyncNetTransfer_ClientConnected), obj, args);
            };
            asyncNetTransfer.ClientDisconnecting += (obj, args) =>
            {
                Invoke(new EventHandler(AsyncNetTransfer_ClientDisconnecting), obj, args);
            };
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

                StatusLogging.Info(String.Format("Подключен клиент {0}", name));
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

                StatusLogging.Info(String.Format("Отключен клиент {0}", name));
            }
        }

        private void PortLogic_Opening(object sender, EventArgs e)
        {
            SerialPort port = sender as SerialPort;

            RxCount = 0;
            asyncDataLogging = new AsyncDataLogging("log_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + LOGFILE_EXTENSION);

            port.DataReceived += Port_DataReceived;

            if (!PacketPerSecTimer.Enabled)
            {
                PacketPerSecTimer.Start();
            }

            StatusLogging.Info(String.Format("Порт {0} открыт.", port.PortName));

            ConnectToSerial.Click -= ConnectToSerial_Click;
            ConnectToSerial.Click += ClosePort;
            ConnectToSerial.Text = "Откл.";

            SerialStatus.Text = "Подключено";
            SerialStatus.ForeColor = Color.Green;
        }

        private void PortLogic_Closing(object sender, EventArgs e)
        {
            SerialPort port = sender as SerialPort;

            if (PacketPerSecTimer.Enabled)
            {
                PacketPerSecTimer.Stop();
            }

            port.DataReceived -= Port_DataReceived;

            asyncDataLogging.Dispose();
            StatusLogging.Info(String.Format("Порт закрыт."));

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

            Invoke(new EventHandler((obj, args) =>
            {
                RxCount++;
                PacketPerSec++;
            }), this, EventArgs.Empty);
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
                string[] ports = portLogic.ScanPorts();
                bool flag = portLogic.HasAvailablePorts;

                SerialNames.DataSource = flag ? ports : new string[] { "None" };
                SerialSpeeds.Enabled = flag;
                ShowSerialSettings.Enabled = flag;
                ConnectToSerial.Enabled = flag;

                if (flag)
                {
                    StatusLogging.Info(String.Format("Найдено {0} последовательных портов.", ports.Length));   
                    return;
                }

                StatusLogging.Error("Последовательные порты не найдены!");
            }
            catch (Exception e)
            {
                StatusLogging.Error(e.ToString());
            }
        }

        private void OpenPort(object sender, EventArgs args)
        {
            string name = SerialNames.SelectedItem.ToString();
            SerialPortSettings settings = SerialPortSettings.Create(SERIAL_SPEEDS[SerialSpeeds.SelectedIndex], (Parity)SerialParities.SelectedItem, Convert.ToInt32(SerialDataBits.SelectedItem), (StopBits)SerialStopBits.SelectedItem);

            try
            {
                portLogic.Connect(name, settings);
            }
            catch (Exception e)
            {
                StatusLogging.Error(e.ToString());
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
                StatusLogging.Error(e.ToString());
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

        private void PacketPerSecTimer_Tick(object sender, EventArgs e)
        {
            PacketPerSecCount.SetFormatArgs(PacketPerSec);
            PacketPerSec = 0;

            portLogic.Poll();
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

        private void StartServer_Click(object sender, EventArgs e)
        {
            ConnectedClients.Items.Clear();
            asyncNetTransfer.Start();
            SendPacket.Enabled = true;
            StartServer.Enabled = false;
            StopServer.Enabled = true;
        }

        private void StopServer_Click(object sender, EventArgs e)
        {
            asyncNetTransfer.Stop();
            SendPacket.Enabled = false;
            StartServer.Enabled = true;
            StopServer.Enabled = false;
        }

        private void SendPacket_Click(object sender, EventArgs e)
        {
            char startMark = '$';
            byte[] packet = new byte[60];

            Random r = new Random();
            r.NextBytes(packet);
            packet[0] = (byte)startMark;

            byte hor = packet[0];
            
            for (int i = 1; i < packet.Length - 1; i++)
            {
                hor ^= packet[i];
            }
            packet[packet.Length - 1] = hor;

            asyncNetTransfer.SendToAll(packet);
        }

        private void flowLayoutPanel1_ClientSizeChanged(object sender, EventArgs e)
        {
            FlowLayoutPanel flow = sender as FlowLayoutPanel;

            foreach (Control c in flow.Controls)
            {
                c.Width = flow.ClientSize.Width - c.Left * 2;
            }
        }
    }
}
