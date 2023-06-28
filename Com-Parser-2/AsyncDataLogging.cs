using System;
using System.IO;
using System.Threading.Tasks;

namespace Com_Parser_2
{
    class AsyncDataLogging : IDisposable
    {
        private const int FLUSH_BUFFER_SIZE = 1024;
        private readonly Stream stream;

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

        public async Task Flush()
        {
            if (stream.Length >= FLUSH_BUFFER_SIZE)
            {
                await stream.FlushAsync();
            }
        }

        public async Task Write(byte[] data)
        {
            await stream.WriteAsync(data, 0, data.Length);
        }
    }
}
