using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Com_Parser_2_client
{
    class NetTransferLogic
    {
        private const int NETWORK_TIMEOUT = 100;
        private readonly BackgroundWorker Worker;
        private TcpClient tcpClient;

        public event EventHandler ServerConnecting;
        public event EventHandler<object[]> PacketReceived;
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
                    StopWorker(e);
                    break;
                }

                try
                {
                    if (!IsSocketConnected(tcpClient.Client))
                    {
                        throw new Exception("Сервер недоступен.");
                    }

                    int rx = await tcpClient.GetStream().ReadAsync(buffer, 0, buffer.Length);

                    if (rx > 0)
                    {
                        if (PacketReceived != null)
                        {
                            PacketReceived.Invoke(tcpClient, new object[] { buffer, rx });
                        }
                    }
                }
                catch (Exception ex)
                {
                    ParserForm.StatusLogging.Error(String.Format("Сервер принудительно закрыл соединение: {0}", ex.ToString()));

                    StopWorker(e);
                    break;
                }

                Thread.Sleep(NETWORK_TIMEOUT);
            }
        }

        private void StopWorker(DoWorkEventArgs e)
        {
            if (ServerDisconnecting != null)
            {
                ServerDisconnecting.Invoke(tcpClient, EventArgs.Empty);
            }

            e.Cancel = true;
        }

        private bool IsSocketConnected(Socket socket)
        {
            return !(socket.Poll(1000, SelectMode.SelectRead) && socket.Available == 0);
        }

        public async Task<bool> Connect(string host, int port)
        {
            if (tcpClient != null && tcpClient.Connected)
            {
                return false;
            }

            if (IPAddress.TryParse(host, out IPAddress address))
            {
                tcpClient = new TcpClient();

                try
                {
                    await tcpClient.ConnectAsync(address, port);
                }
                catch (Exception e)
                {
                    ParserForm.StatusLogging.Error(e.ToString());
                    return false;
                }

                if (!tcpClient.Connected)
                {
                    return false;
                }

                if (ServerConnecting != null)
                {
                    ServerConnecting.Invoke(tcpClient, EventArgs.Empty);
                }

                ParserForm.StatusLogging.Info(String.Format("Подключились к узлу {0}.", tcpClient.Client.RemoteEndPoint));

                if (!Worker.IsBusy)
                {
                    Worker.RunWorkerAsync();
                }
            }

            return true;
        }

        public void Disconnect()
        {
            if (tcpClient == null)
            {
                return;
            }

            if (!tcpClient.Connected)
            {
                return;
            }

            Worker.CancelAsync();

            while (Worker.IsBusy);

            tcpClient.Close();
            tcpClient.Dispose();
            tcpClient = null;
        }
    }
}
