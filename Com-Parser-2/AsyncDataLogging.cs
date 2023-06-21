using System.IO;
using System.Threading.Tasks;

namespace Com_Parser_2
{
    class AsyncDataLogging
    {
        private const int FLUSH_BUFFER_SIZE = 1024;
        private Stream Stream { get; }

        public AsyncDataLogging(Stream Stream)
        {
            this.Stream = Stream;
        }

        public async Task Flush()
        {
            if (Stream.Length >= FLUSH_BUFFER_SIZE)
            {
                await Stream.FlushAsync();
            }
        }

        public async Task Write(byte[] data)
        {
            await Stream.WriteAsync(data, 0, data.Length);
        }
    }
}
