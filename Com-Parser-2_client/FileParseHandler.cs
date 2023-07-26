using System;

namespace Com_Parser_2_client
{
    class FileParseHandler : IPacketHandler
    {
        public PacketFormat Format { set; get; }

        public Action<float> HandleCallback { set; get; }

        private void Callback(IPacketParser parser)
        {
            HandleCallback(parser.Source.Position / (float)parser.Source.Length);
        }

        public void HandleSuccess(IPacketParser parser, byte[] buffer)
        {
            var datas = Format.ReadDataObjectsFrom(buffer);
            ParserForm.DisplayLogic.HandlePacket(datas);
            Callback(parser);
        }

        public void HandleBroken(IPacketParser parser, byte[] buffer)
        {
            Callback(parser);
        }
    }
}
