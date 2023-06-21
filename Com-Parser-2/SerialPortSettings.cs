using System.IO.Ports;

namespace Com_Parser_2
{
    class SerialPortSettings
    {
        public int Speed { set; get; }
        public Parity Parity { set; get; }
        public int DataBits { set; get; }
        public StopBits StopBits { set; get; }
    
        public SerialPortSettings()
        {
            Speed = 9600;
            Parity = Parity.None;
            DataBits = 8;
            StopBits = StopBits.One;
        }
    }
}
