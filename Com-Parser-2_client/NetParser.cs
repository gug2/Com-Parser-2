using System;
using System.Collections.Generic;
using System.IO;

namespace Com_Parser_2_client
{
    class NetParser : IPacketParser
    {
        public int SuccessPackets { private set; get; }
        public int BrokenPackets { private set; get; }
        public Stream Source { set; get; }

        private IPacketHandler Handler;
        private const int PACKETS_IN_CHUNK = 5;
        private readonly Queue<byte[]> dataChunks = new Queue<byte[]>();
        private byte[] dataChunk;
        private Stream networkStream;
        private StreamParser parser;
        private int bytesInBuffers;
        private bool ParserActive;

        public NetParser()
        {

        }

        public void Init(IPacketHandler handler)
        {
            Handler = handler;

            if (Source == null)
            {
                Console.WriteLine("Ошибка инициализации. Отсутствует поток данных.");
                return;
            }

            if (Handler == null)
            {
                Console.WriteLine("Ошибка инициализации. Отсутствует обработчик.");
                return;
            }

            if (Handler.Format == null)
            {
                Console.WriteLine("Ошибка инициализации. Отсутствует формат пакета.");
                return;
            }

            dataChunk = new byte[Handler.Format.PacketSize * PACKETS_IN_CHUNK];
            networkStream = new MemoryStream();
            bytesInBuffers = 0;

            parser = new StreamParser();

            Console.WriteLine("Инициализирован потоковый обработчик.");
        }

#warning подумать над тем, как реализовать принудительную обработку пакетов, без ожидания заполнения буфера
        public void ScheduleDataChunk(byte[] buffer, int length)
        {
            networkStream.Write(buffer, 0, length);
            bytesInBuffers += length;

            if (bytesInBuffers < dataChunk.Length)
            {
                return;
            }

            networkStream.Seek(-bytesInBuffers, SeekOrigin.Current);
            int readed = networkStream.Read(dataChunk, 0, dataChunk.Length);
            bytesInBuffers -= readed;
            networkStream.Seek(bytesInBuffers, SeekOrigin.Current);

            dataChunks.Enqueue(dataChunk);
        }

        public void FlushDataChunks()
        {
            if (bytesInBuffers <= 0)
            {
                return;
            }

            byte[] remainingChunk = new byte[bytesInBuffers];
            networkStream.Seek(-bytesInBuffers, SeekOrigin.Current);
            int readed = networkStream.Read(remainingChunk, 0, remainingChunk.Length);
            bytesInBuffers -= readed;
            networkStream.Seek(bytesInBuffers, SeekOrigin.Current);

            dataChunks.Enqueue(remainingChunk);
        }

        public void Stop()
        {
            ParserActive = false;
        }

        public void Parse(IPacketHandler handler)
        {
            ParserActive = true;

            Console.WriteLine("запуск поточной обработки.");

            while (ParserActive)
            {
                if (dataChunks.Count == 0)
                {
                    continue;
                }

                using (Stream stream = new MemoryStream())
                {
                    byte[] chunk;

                    while (dataChunks.Count > 0)
                    {
                        chunk = dataChunks.Dequeue();
                        stream.Write(chunk, 0, chunk.Length);
                    }

                    stream.Seek(0, SeekOrigin.Begin);

                    parser.Source = stream;
                    parser.ResetStatistics();
                    parser.Parse(handler);

                    Console.WriteLine("Обработан фрагмент {0}/{1}", parser.SuccessPackets, parser.BrokenPackets);
                    SuccessPackets += parser.SuccessPackets;
                    BrokenPackets += parser.BrokenPackets;
                }
            }

            Console.WriteLine("завершение поточной обработки.");
        }
    }
}
