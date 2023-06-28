using System.IO.Ports;

namespace Com_Parser_2
{
    class SerialPortSettings
    {
        public static readonly SerialPortSettings Default = new SerialPortSettings(9600, Parity.None, 8, StopBits.One);

        public int Speed { get; }
        public Parity Parity { get; }
        public int DataBits { get; }
        public StopBits StopBits { get; }

        private SerialPortSettings(int speed, Parity parity, int dataBits, StopBits stopBits)
        {
            Speed = speed;
            Parity = parity;
            DataBits = dataBits;
            StopBits = stopBits;
        }

        public static SerialPortSettings Create(int speed, Parity parity, int dataBits, StopBits stopBits)
        {
            return new SerialPortSettings(speed, parity, dataBits, stopBits);
        }
    }
}
