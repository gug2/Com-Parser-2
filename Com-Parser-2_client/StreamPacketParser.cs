using System;
using System.IO;
using System.Linq;

namespace Com_Parser_2_client
{
    class StreamPacketParser
    {
        public byte[] StartMark { set; get; }
        public int PacketSize { set; get; }
        public bool ValidateByChecksum { set; get; }

        public int ReceivedPacketIndex { private set; get; }

        private readonly Stream stream;

        public StreamPacketParser(Stream source, PacketFormat packetFormat)
        {
            stream = source;
            StartMark = packetFormat.StartMarkPattern;
            PacketSize = packetFormat.PacketSize;
            ValidateByChecksum = packetFormat.ValidateByChecksum;
        }

        private bool IsEqual(byte[] arrA, byte[] arrB)
        {
            return Enumerable.SequenceEqual(arrA, arrB);
        }

        // ожидание синхронизации приема (ожидаем стартовую метку)
        private bool WaitForSync()
        {
            if (StartMark.Length == 0)
            {
                return true;
            }

            byte[] buffer = new byte[StartMark.Length];
            while (stream.Position < stream.Length - buffer.Length + 1)
            {
                stream.Read(buffer, 0, buffer.Length);

                // нашли стартовую метку, откатываемся на позицию до этой метки
                if (IsEqual(buffer, StartMark))
                {
                    stream.Seek(-buffer.Length, SeekOrigin.Current);
                    //Console.WriteLine("Синхронизировались на позиции {0} (до стартовой метки)", stream.Position);
                    return true;
                }

                // если не нашли, переходим на следующий байт
                if (buffer.Length > 1)
                {
                    stream.Seek(-buffer.Length + 1, SeekOrigin.Current);
                }
            }

            Console.WriteLine("Не удалось синхронизироваться с потоком данных!");
            return false;
        }

        private bool ValidateChecksum(byte[] packet, int packetSize)
        {
            if (packetSize < 2)
            {
                return false;
            }

            int checksumPosition = 26;//62;
            byte checksumReceived = packet[checksumPosition];

            byte checksum = packet[0];
            for (int i = 1; i < checksumPosition; i++)
            {
                checksum ^= packet[i];
            }

            return checksum == checksumReceived;
        }

        private bool IsStartMarkValid(byte[] packet)
        {
            if (packet.Length < StartMark.Length)
            {
                return false;
            }

            bool flag = true;
            for (int i = 0; i < StartMark.Length; i++)
            {
                if (packet[i] != StartMark[i])
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }

        public void Parse(Action<byte[]> packetHandling, Action<byte[]> brokenPacketHandling)
        {
            byte[] buffer = new byte[512];

            long totalSize = stream.Length;
            long bytesRead = 0;
            int readed;

            bool sync = WaitForSync();

            if (!sync)
            {
                return;
            }

            while (bytesRead < totalSize)
            {
                readed = stream.Read(buffer, 0, PacketSize);

#warning замена знаков 0x20 на нули
                // замена знаков 0x20 на нули
                /*for (int i = 0; i < readed; i++)
                {
                    if (buffer[i] == 0x20)
                    {
                        buffer[i] = 0;
                    }
                }*/

                // проверка длины пакета
                if (readed == 0)
                {
                    Console.WriteLine("Конец потока");
                    return;
                }
                if (readed != PacketSize)
                {
                    bytesRead += readed;
                    continue;
                }

                ReceivedPacketIndex++;

                // проверка контрольной суммы или стартовой метки
                if (!IsStartMarkValid(buffer) || (ValidateByChecksum && !ValidateChecksum(buffer, PacketSize)))
                {
                    Console.WriteLine("Ошибка контрольной суммы на позиции {0}", stream.Position);

                    brokenPacketHandling(buffer);

                    if (!WaitForSync())
                    {
                        Console.WriteLine("Ошибка синхронизации");
                        return;
                    }
                    else
                    {
                        // синхронизировались, повторяем попытку чтения пакета
                        continue;
                    }
                }

                packetHandling(buffer);

                bytesRead += readed;
            }
        }
    }
}
