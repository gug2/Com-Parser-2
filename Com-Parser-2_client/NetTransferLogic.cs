using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;using System.IO;

namespace Com_Parser_2_client
{
    class NetTransferLogic
    {
        private const int NET_BUFFER_SIZE = 1024;
        private readonly BackgroundWorker Worker;
        private TcpClient tcpClient;

        public event EventHandler<byte[]> PacketReceived;
        public event EventHandler ServerDisconnecting;

        public NetTransferLogic(BackgroundWorker Worker)
        {
            this.Worker = Worker;
            this.Worker.DoWork += Worker_DoWork;
        }

        private async void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] buffer = new byte[512];

            while (true)
            {
                if (Worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                try
                {
                    if (!IsSocketConnected(tcpClient.Client))
                    {
                        throw new Exception("сервер недоступен.");
                    }

                    int rx = await tcpClient.GetStream().ReadAsync(buffer, 0, buffer.Length);

                    if (rx > 0)
                    {
                        Array.Resize(ref buffer, rx);

                        if (PacketReceived != null)
                        {
                            PacketReceived.Invoke(tcpClient, buffer);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Сервер принудительно закрыл соединение. {0}", ex.ToString());

                    if (ServerDisconnecting != null)
                    {
                        ServerDisconnecting.Invoke(tcpClient, EventArgs.Empty);
                    }

                    e.Cancel = true;
                    break;
                }
            }
        }

        private bool IsSocketConnected(Socket socket)
        {
            return !(socket.Poll(1000, SelectMode.SelectRead) && socket.Available == 0);
        }

        public async Task Connect(string host, int port)
        {
            if (tcpClient != null && tcpClient.Connected)
            {
                return;
            }

            if (IPAddress.TryParse(host, out IPAddress address))
            {
                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(address, port);

                if (tcpClient.Connected)
                {
                    Worker.RunWorkerAsync();

                    Console.WriteLine("Подключились к узлу {0}!", tcpClient.Client.RemoteEndPoint);
                }
            }
        }

        public void Disconnect()
        {
            if (tcpClient == null || !tcpClient.Connected)
            {
                return;
            }

            Worker.CancelAsync();

            tcpClient.Close();
            tcpClient = null;
        }
    }
}
