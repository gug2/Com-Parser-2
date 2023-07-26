using System;

namespace Com_Parser_2_client
{
    interface IPacketHandler
    {
        PacketFormat Format { set; get; }
        Action<float> HandleCallback { set; get; }

        void HandleSuccess(IPacketParser parser, byte[] buffer);
        void HandleBroken(IPacketParser parser, byte[] buffer);
    }
}
