using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Com_Parser_2
{
    class AsyncNetTransfer
    {
        const int BACKLOG = 5;
        static List<Socket> Clients = new List<Socket>();

        // Вызывать в событии Form_Load
        public static void Test()
        {
            Socket server = StartServer();

            var asyncListen = ListenClients(server);
            var asyncHandle = HandleClients();
        }

        public static Socket StartServer()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                EndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 65000);
                socket.Bind(localEndPoint);
                Console.WriteLine("Запущено!");
                Console.WriteLine("Наш адрес: {0}", socket.LocalEndPoint);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка! -> {0}", e.ToString());
            }

            return socket;
        }

        public async static Task ListenClients(Socket socket)
        {
            socket.Listen(BACKLOG);
            Clients.Clear();

            while (true)
            {
                Socket clientSocket = await socket.AcceptAsync();
                Clients.Add(clientSocket);
                Console.WriteLine("Подключен новый сокет {0}", clientSocket.RemoteEndPoint);
            }
        }

        public async static Task HandleClients()
        {
            while (true)
            {
                foreach (Socket client in Clients)
                {
                    client.Send(Encoding.UTF8.GetBytes(DateTime.Now + "Привет, клиент" + client.RemoteEndPoint + "!"));
                }
                await Task.Delay(100);
            }
        }
    }
}
