using System.IO;
using System.Threading.Tasks;

namespace Com_Parser_2
{
    class AsyncDataLogging
    {
        const int FLUSH_BUFFER_SIZE = 1024;

        public async static Task Flush(Stream stream)
        {
            if (stream.Length >= FLUSH_BUFFER_SIZE)
            {
                await stream.FlushAsync();
            }
        }

        public async static Task Write(Stream stream, byte[] data)
        {
            await stream.WriteAsync(data, 0, data.Length);
        }
    }
}
