using System;
using System.IO;

namespace Com_Parser_2_client
{
    static class Parsers
    {
        public static StreamParser ParseBinFile(string inputFile, PacketFormat format, Action<float> progressCallback)
        {
            StreamParser parser = null;

            using (Stream inStream = File.OpenRead(inputFile))
            {
                FileParseHandler handler = new FileParseHandler()
                {
                    Format = format,
                    HandleCallback = progressCallback
                };

                parser = new StreamParser()
                {
                    Source = inStream
                };

                parser.ResetStatistics();
                parser.Parse(handler);
            }

            return parser;
        }
    }
}
