using System;
using System.Diagnostics;
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
                    Console.WriteLine("Синхронизировались на позиции {0} (до стартовой метки)", stream.Position);
                    return true;
                }

                // если не нашли, переходим на следующий байт
                stream.Seek(-buffer.Length + 1, SeekOrigin.Current);
            }

            Console.WriteLine("Не удалось синхронизироваться с потоком данных!");
            return false;
        }

        public bool ValidateChecksum(byte[] packet)
        {
            if (packet.Length < 2)
            {
                return false;
            }

            byte checksumReceived = packet[packet.Length - 1];

            byte checksum = packet[0];
            for (int i = 1; i < packet.Length - 1; i++)
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
                }
            }

            return flag;
        }

        public void Parse(Action<byte[]> packetHandling, Action<byte[]> brokenPacketHandling)
        {
            Stopwatch sw = Stopwatch.StartNew();

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
                if ((!ValidateByChecksum && !IsStartMarkValid(buffer)) || (ValidateByChecksum && !ValidateChecksum(buffer)))
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

            sw.Stop();
            Console.WriteLine("Время: {0} мс", sw.ElapsedMilliseconds);
            ClientForm.StatusLogging.Info(String.Format("Время: {0} мс", sw.ElapsedMilliseconds));
        }
    }
}
