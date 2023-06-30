using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Com_Parser_2
{
    class AsyncNetTransfer
    {
        const int BACKLOG = 5;
        const int ACCEPT_CLIENT_DELAY_MS = 10;
        public List<TcpClient> Clients = new List<TcpClient>();
        private TcpListener tcpServer;

        public event EventHandler ClientConnected, ClientDisconnecting;
        
        private readonly BackgroundWorker Worker;

        public AsyncNetTransfer(BackgroundWorker worker)
        {
            Worker = worker;
            Worker.DoWork += Worker_DoWork;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                Clients[i].Close();
                Clients.RemoveAt(i);
            }
            tcpServer.Stop();
            MainForm.StatusLogging.Info("Сервер завершил работу");
        }

        public void Start()
        {
            Worker.RunWorkerAsync();
        }

        public void Stop()
        {
            Worker.CancelAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            tcpServer = new TcpListener(IPAddress.Parse("127.0.0.1"), 65000);

            tcpServer.Start(BACKLOG);
            Clients.Clear();
            MainForm.StatusLogging.Info(String.Format("Сервер запущен {0}", tcpServer.LocalEndpoint));

            while (true)
            {
                if (Worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                CheckClients();

                if (!tcpServer.Pending())
                {
                    Thread.Sleep(ACCEPT_CLIENT_DELAY_MS);
                    continue;
                }

                TcpClient client = tcpServer.AcceptTcpClient();
                Clients.Add(client);

                if (ClientConnected != null)
                {
                    ClientConnected.Invoke(client, EventArgs.Empty);
                }

                Thread.Sleep(ACCEPT_CLIENT_DELAY_MS);
            }
        }

        private void CheckClients()
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                if (!IsSocketConnected(Clients[i].Client))
                {
                    if (ClientDisconnecting != null)
                    {
                        ClientDisconnecting.Invoke(Clients[i], EventArgs.Empty);
                    }

                    Clients[i].Close();
                    Clients.RemoveAt(i);
                }
            }
        }

        public void SendToAll(byte[] packet)
        {
            if (!Worker.IsBusy)
            {
                return;
            }

            for (int i = 0; i < Clients.Count; i++)
            {
                SendTo(Clients[i], packet);
            }
        }

        private async void SendTo(TcpClient client, byte[] packet)
        {
            if (!IsSocketConnected(client.Client))
            {
                MainForm.StatusLogging.Error("Ошибка при отправке данных. Клиент не подключен.");
                return;
            }

            await client.GetStream().WriteAsync(packet, 0, packet.Length);
        }

        private bool IsSocketConnected(Socket socket)
        {
            return !(socket.Poll(1000, SelectMode.SelectRead) && socket.Available == 0);
        }

        /*public async Task Handle(byte[] buffer)
        {
            Socket client = Clients[0];

            await Task.Factory.FromAsync(
                client.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, null, client),
                client.EndSend
            ).ConfigureAwait(false);
        }*/
    }
}
