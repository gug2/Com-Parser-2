using System.IO;

namespace Com_Parser_2_client
{
    interface IPacketParser
    {
        int SuccessPackets { get; }
        int BrokenPackets { get; }
        Stream Source { get; }

        void Parse(IPacketHandler handler);
    }
}
