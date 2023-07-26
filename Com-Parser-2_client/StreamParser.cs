using System;
using System.IO;
using System.Linq;

namespace Com_Parser_2_client
{
    class StreamParser : IPacketParser
    {
        public int SuccessPackets { private set; get; }
        public int BrokenPackets { private set; get; }
        public int PacketIndex { private set; get; }
        public Stream Source { set; get; }

        private IPacketHandler Handler;

        private bool IsEqual(byte[] arrA, byte[] arrB)
        {
            return Enumerable.SequenceEqual(arrA, arrB);
        }

        private bool ValidateChecksum(byte[] packet)
        {
            if (Handler.Format.PacketSize < 2)
            {
                return false;
            }

            byte checksumReceived = packet[Handler.Format.ChecksumPosition];

            byte checksum = packet[0];
            for (int i = 1; i < Handler.Format.ChecksumPosition; i++)
            {
                checksum ^= packet[i];
            }

            return checksum == checksumReceived;
        }

        private bool IsStartMarkValid(byte[] packet)
        {
            if (packet.Length < Handler.Format.StartMarkPattern.Length)
            {
                return false;
            }

            bool flag = true;
            for (int i = 0; i < Handler.Format.StartMarkPattern.Length; i++)
            {
                if (packet[i] != Handler.Format.StartMarkPattern[i])
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }

        private bool WaitForSync()
        {
#warning добавить проверку на Null для Handler.Format
            if (Handler.Format.StartMarkPattern.Length == 0)
            {
                return true;
            }

            byte[] buffer = new byte[Handler.Format.StartMarkPattern.Length];
            while (Source.Position < Source.Length - buffer.Length + 1)
            {
                Source.Read(buffer, 0, buffer.Length);

                // нашли стартовую метку, откатываемся на позицию до этой метки
                if (IsEqual(buffer, Handler.Format.StartMarkPattern))
                {
                    Source.Seek(-buffer.Length, SeekOrigin.Current);
                    //Console.WriteLine("Синхронизировались на позиции {0} (до стартовой метки)", stream.Position);
                    return true;
                }

                // если не нашли, переходим на следующий байт
                if (buffer.Length > 1)
                {
                    Source.Seek(-buffer.Length + 1, SeekOrigin.Current);
                }
            }

            Console.WriteLine("Не удалось синхронизироваться с потоком данных!");
            return false;
        }

        public void ResetStatistics()
        {
            PacketIndex = 0;
            SuccessPackets = 0;
            BrokenPackets = 0;
        }

        public void Parse(IPacketHandler handler)
        {
            Handler = handler;

            if (Source == null)
            {
                Console.WriteLine("Ошибка чтения. Отсутствует поток данных.");
                return;
            }

            if (Handler == null)
            {
                Console.WriteLine("Ошибка чтения. Отсутствует обработчик.");
                return;
            }

            if (Handler.Format == null)
            {
                Console.WriteLine("Ошибка чтения. Отсутствует формат пакета.");
                return;
            }

            byte[] buffer = new byte[512];

            long totalSize = Source.Length;
            long bytesRead = 0;
            int readed;

            bool sync = WaitForSync();

            if (!sync)
            {
                return;
            }

            while (bytesRead < totalSize)
            {
                readed = Source.Read(buffer, 0, Handler.Format.PacketSize);

                // проверка длины пакета
                if (readed == 0)
                {
                    Console.WriteLine("Конец потока");
                    return;
                }
                if (readed != Handler.Format.PacketSize)
                {
                    bytesRead += readed;
                    continue;
                }

                PacketIndex++;

                // проверка контрольной суммы или стартовой метки
                if (!IsStartMarkValid(buffer) || (Handler.Format.ValidateByChecksum && !ValidateChecksum(buffer)))
                {
                    Console.WriteLine("Ошибка контрольной суммы на позиции {0}", Source.Position);

                    BrokenPackets++;
                    Handler.HandleBroken(this, buffer);

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

                SuccessPackets++;
                Handler.HandleSuccess(this, buffer);

                bytesRead += readed;
            }
        }
    }
}
