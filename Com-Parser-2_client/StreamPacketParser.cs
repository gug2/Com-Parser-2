using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Com_Parser_2_client
{
    class StreamPacketParser
    {
        private const int PACKETS_IN_CHUNK = 10;
        private readonly int chunkSize;
        private readonly Queue<byte[]> chunkQueue = new Queue<byte[]>();
        private Stream outStream;
        private PacketParser p;
        private readonly BackgroundWorker worker;
        private int SuccessPackets, BrokenPackets;
        private readonly Stream netStream;
        private int last;
        private readonly byte[] chunkBuffer;

        public StreamPacketParser(BackgroundWorker worker, DisplayLogic displayLogic, PacketFormat format)
        {
            this.worker = worker;
            chunkSize = format.PacketSize * PACKETS_IN_CHUNK;

            netStream = new MemoryStream();
            chunkBuffer = new byte[chunkSize];
            SuccessPackets = 0;
            BrokenPackets = 0;

            int sp = 0, bp = 0;
            object filledStruct, outStruct;
            p = new PacketParser(format);
            p.Use(buffer =>
            {
                filledStruct = format.MarshalDeserialize(buffer);
                outStruct = format.GetOutputStruct(filledStruct);

                SuccessPackets++;
                sp++;

                displayLogic.HandlePacket(p, outStruct);
                ClientForm.StatusLogging.Info(SuccessPackets + "/" + BrokenPackets);
                outStream.Write(buffer, 0, p.packetFormat.PacketSize);
            },
            buffer =>
            {
                BrokenPackets++;
                bp++;
                ClientForm.StatusLogging.Info(SuccessPackets + "/" + BrokenPackets);
                outStream.Write(buffer, 0, p.packetFormat.PacketSize);
            });

            worker.DoWork += ProcessChunk;
        }

        public void ScheduleChunk(byte[] data, int len)
        {
            netStream.Write(data, 0, len);
            last += len;

            if (last < chunkSize)
            {
                return;
            }

            netStream.Seek(-last, SeekOrigin.Current);
            netStream.Read(chunkBuffer, 0, chunkBuffer.Length);
            last -= chunkSize;
            netStream.Seek(last, SeekOrigin.Current);

            chunkQueue.Enqueue(chunkBuffer);

            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
            }
        }

        public void ProcessChunk(object sender, DoWorkEventArgs e)
        {
            if (outStream == null)
            {
                outStream = File.Create("stream_out_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".txt");
                Console.WriteLine("файл записи открыт");
            }

            Console.WriteLine("Обработка буфера");
            byte[] chunk;

            using (Stream s = new MemoryStream())
            {
                while (chunkQueue.Count > 0)
                {
                    chunk = chunkQueue.Dequeue();
                    s.Write(chunk, 0, chunk.Length);
                }
                s.Seek(0, SeekOrigin.Begin);

                p.Source(s);
                p.Parse();
                Console.WriteLine("обработано {0}/{1} пакетов", SuccessPackets, BrokenPackets);
            }

            outStream.Flush();
            Console.WriteLine("сохранение");
        }

        public void FlushParser()
        {
            byte[] flushed = new byte[last];
            netStream.Seek(-flushed.Length, SeekOrigin.Current);
            netStream.Read(flushed, 0, flushed.Length);

            Console.WriteLine("сбрасываем остатки - длина {0}", flushed.Length);

            ScheduleChunk(flushed, flushed.Length);
            while (worker.IsBusy);
            StopParser();
        }

        private void StopParser()
        {
            if (outStream != null)
            {
                outStream.Close();
                Console.WriteLine("файл записи закрыт");
            }
        }
    }
}
