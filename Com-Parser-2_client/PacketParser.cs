using System;
using System.IO;
using System.Linq;

namespace Com_Parser_2_client
{
    class PacketParser
    {
        public int ReceivedPacketIndex { private set; get; }

        public PacketFormat packetFormat;
        private Action<byte[]> success, broken;
        private Stream stream;

        public PacketParser(PacketFormat packetFormat)
        {
            this.packetFormat = packetFormat;
        }

        public PacketParser(Stream source, PacketFormat packetFormat) : this(packetFormat)
        {
            Source(source);
        }

        public void Source(Stream source)
        {
            stream = source;
        }

        private bool IsEqual(byte[] arrA, byte[] arrB)
        {
            return Enumerable.SequenceEqual(arrA, arrB);
        }

        // ожидание синхронизации приема (ожидаем стартовую метку)
        private bool WaitForSync()
        {
            if (packetFormat.StartMarkPattern.Length == 0)
            {
                return true;
            }

            byte[] buffer = new byte[packetFormat.StartMarkPattern.Length];
            while (stream.Position < stream.Length - buffer.Length + 1)
            {
                stream.Read(buffer, 0, buffer.Length);

                // нашли стартовую метку, откатываемся на позицию до этой метки
                if (IsEqual(buffer, packetFormat.StartMarkPattern))
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

        private bool ValidateChecksum(byte[] packet, int packetSize, int checksumPosition)
        {
            if (packetSize < 2)
            {
                return false;
            }

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
            if (packet.Length < packetFormat.StartMarkPattern.Length)
            {
                return false;
            }

            bool flag = true;
            for (int i = 0; i < packetFormat.StartMarkPattern.Length; i++)
            {
                if (packet[i] != packetFormat.StartMarkPattern[i])
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }

        public void Use(Action<byte[]> success, Action<byte[]> broken)
        {
            this.success = success;
            this.broken = broken;
        }

        public void Parse()
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
                readed = stream.Read(buffer, 0, packetFormat.PacketSize);

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
                if (readed != packetFormat.PacketSize)
                {
                    bytesRead += readed;
                    continue;
                }

                ReceivedPacketIndex++;

                // проверка контрольной суммы или стартовой метки
                if (!IsStartMarkValid(buffer) || (packetFormat.ValidateByChecksum && !ValidateChecksum(buffer, packetFormat.PacketSize, packetFormat.ChecksumPosition)))
                {
                    Console.WriteLine("Ошибка контрольной суммы на позиции {0}", stream.Position);

                    broken(buffer);

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

                success(buffer);

                bytesRead += readed;
            }
        }
    }
}
