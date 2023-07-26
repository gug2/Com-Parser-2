using System;

namespace Com_Parser_2_client
{
    class NetParseHandler : IPacketHandler
    {
        public PacketFormat Format { set; get; }
        public Action<float> HandleCallback { set; get; }

        public void HandleBroken(IPacketParser parser, byte[] buffer)
        {

        }

        public void HandleSuccess(IPacketParser parser, byte[] buffer)
        {
            var datas = Format.ReadDataObjectsFrom(buffer);
            ParserForm.DisplayLogic.HandlePacket(datas);
        }
    }
}
