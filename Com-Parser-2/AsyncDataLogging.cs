using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Com_Parser_2
{
    class AsyncDataLogging : IDisposable
    {
        private const int FLUSH_BUFFER_SIZE = 1024; //1kb
        private readonly Stream stream;
        private int bufferSize;

        public AsyncDataLogging(string logPath)
        {
            try
            {
                stream = File.Open(logPath, FileMode.Create, FileAccess.Write);
            }
            catch (Exception e)
            {
                MainForm.StatusLogging.Error(String.Format("Ошибка при открытии файла {0}: {1}", Path.GetFullPath(logPath), e.ToString()));
            }
        }

        public void Dispose()
        {
            if (stream != null)
            {
                stream.Flush();
                stream.Close();
                stream.Dispose();
            }
        }

        public void Write(byte[] data)
        {
            stream.Write(data, 0, data.Length);
            bufferSize += data.Length;

            if (bufferSize >= FLUSH_BUFFER_SIZE)
            {
                stream.Flush();
                Console.WriteLine("сохранение... " + "," + Process.GetCurrentProcess().Threads.Count);
                bufferSize -= FLUSH_BUFFER_SIZE;
            }
        }
    }
}
