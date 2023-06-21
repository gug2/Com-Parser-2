using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Com_Parser_2_client
{
    class DisplayLogic
    {
        private readonly FlowLayoutPanel chartsPanel, textPanel;
        private readonly Dictionary<string, Chart> Charts = new Dictionary<string, Chart>();
        private readonly Dictionary<string, Label> Texts = new Dictionary<string, Label>();
        private object Packet;
        private int PacketSize;
        private PacketFormat PacketFormat;
        private DataObject Time;
        public int SuccessPackets { private set; get; }
        public int BrokenPackets { private set; get; }

        private delegate void DisplayToChartHandler(string name, object x, object y);
        private delegate void DisplayToTextHandler(string name, object value);

        public DisplayLogic(FlowLayoutPanel chartsPanel, FlowLayoutPanel textPanel)
        {
            this.chartsPanel = chartsPanel;
            this.textPanel = textPanel;
        }

        private void HandlePacket(StreamPacketParser parser, object outStruct)
        {
            if (outStruct == null)
            {
                Console.WriteLine("Отсутствует выходная структура пакета!");
                return;
            }

            object xValue;

            foreach (FieldInfo info in outStruct.GetType().GetFields())
            {
                if (Charts.ContainsKey(info.Name))
                {
                    if (Time == null || info.Name == Time.Name)
                    {
                        xValue = parser.ReceivedPacketIndex;
                    }
                    else
                    {
                        xValue = Time.Field.GetValue(outStruct);
                    }

                    Charts[info.Name].Invoke(new DisplayToChartHandler(DisplayToChart), new object[] { info.Name, xValue, info.GetValue(outStruct) });
                }
                
                if (Texts.ContainsKey(info.Name))
                {
                    Texts[info.Name].Invoke(new DisplayToTextHandler(DisplayToText), new object[] { info.Name, info.GetValue(outStruct) });
                }
            }
        }

        private void DisplayToChart(string name, object x, object y)
        {
            Charts[name].Series[0].Points.AddXY(x, y);
        }

        private void DisplayToText(string name, object value)
        {
            Texts[name].Text = value.ToString();
        }

        private void AddChartFor(DataObject dataObject)
        {
            Chart chart = new Chart() { BackColor = Color.Gray };
            ChartArea area = new ChartArea() { Name = "chartArea" };
            Legend legend = new Legend("legend") { DockedToChartArea = area.Name };
            Series series = new Series(dataObject.Name, 1) { ChartArea = area.Name, ChartType = SeriesChartType.Line, Legend = legend.Name };
            chart.ChartAreas.Add(area);
            chart.Legends.Add(legend);
            chart.Series.Add(series);
            chart.Size = new Size(500, 250);

            chartsPanel.Controls.Add(chart);
            chartsPanel.Controls.SetChildIndex(chart, dataObject.Plot);

            Charts.Add(dataObject.Name, chart);
        }

        private void AddTextFor(DataObject dataObject)
        {
            TableLayoutPanel panel = new TableLayoutPanel()
            {
                BackColor = Color.Yellow,
                ColumnCount = 2,
                RowCount = 1,
                AutoSize = true,
                Margin = new Padding(0, 10, 0, 10)
            };

            Label label = new Label()
            {
                Name = "key",
                BackColor = Color.DarkGray,
                AutoSize = true,
                Font = new Font("Arial", 15.0F)
            };
            Label label2 = new Label()
            {
                Name = "value",
                BackColor = Color.LightGray,
                AutoSize = true,
                Font = new Font("Arial", 15.0F)
            };
            label.Text = dataObject.Name;
            label2.Text = "no value";

            panel.Controls.Add(label);
            panel.Controls.Add(label2);

            textPanel.Controls.Add(panel);

            Texts.Add(dataObject.Name, label2);
        }

        public void ParseBinFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Файл не найден! Путь: \"{0}\"", Path.GetFullPath(path));
                return;
            }

            if (PacketFormat == null || Packet == null)
            {
                Console.WriteLine("Отсутствует формат пакета!");
                return;
            }

            string outPath = path + "_parser_out.txt";


            object filledStruct, outStruct;
            using (Stream outStream = File.Create(outPath))
            {
                using (Stream stream = File.OpenRead(path))
                {
                    StreamPacketParser parser = new StreamPacketParser(stream)
                    {
                        StartMark = new byte[] { 0x24 },
                        PacketSize = PacketSize,
                        ValidateByChecksum = false
                    };

                    Stopwatch sw = Stopwatch.StartNew();
                    parser.Parse(buffer =>
                    {
                        filledStruct = PacketFormat.MarshalDeserializeByArray(Packet.GetType(), buffer);
                        SuccessPackets++;

                        outStruct = PacketFormat.GetOutputStruct(filledStruct);

                        Out(String.Format("PACKET {0} ->\"", parser.ReceivedPacketIndex), outStream);
                        var s = outStruct.GetType().GetFields().Select(info => String.Format("{0}: {1}", info.Name, info.GetValue(outStruct)));
                        Out(String.Join(", ", s), outStream);
                        Out("\"\n", outStream);

                        HandlePacket(parser, outStruct);
                    },
                    buffer =>
                    {
                        filledStruct = PacketFormat.MarshalDeserializeByArray(Packet.GetType(), buffer);
                        BrokenPackets++;

                        outStruct = PacketFormat.GetOutputStruct(filledStruct);

                        Out(String.Format("PACKET {0} ->\"", parser.ReceivedPacketIndex), outStream);
                        var s = outStruct.GetType().GetFields().Select(info => String.Format("{0}: {1}", info.Name, info.GetValue(outStruct)));
                        Out(String.Join(", ", s), outStream);
                        Out("\" BROKEN\n", outStream);
                    });
                    sw.Stop();
                    Console.WriteLine("Время: {0} мс", sw.ElapsedMilliseconds);
                }
            }
        }

        private void Out(string msg, Stream stream)
        {
            byte[] array = Encoding.Default.GetBytes(msg);
            stream.Write(array, 0, array.Length);
        }

        public void Load(PacketFormat packetFormat)
        {
            object inputStruct = packetFormat.GetInputStruct();
            object outputStruct = packetFormat.GetOutputStruct(inputStruct);

            if (inputStruct == null || outputStruct == null)
            {
                Console.WriteLine("Ошибка! Отсутствует объект структуры.");
                return;
            }

            Packet = inputStruct;
            PacketFormat = packetFormat;
            PacketSize = PacketFormat.SizeOf(inputStruct);

            List<DataObject> dataObjects = outputStruct.GetType().GetFields().Select(info => DataObject.ConvertField(info)).ToList();

            foreach (DataObject obj in dataObjects)
            {
                if (obj.Name.Contains("time"))
                {
                    Time = obj;
                }

                if (obj.Plot >= 0)
                {
                    AddChartFor(obj);
                }

                if (!string.IsNullOrEmpty(obj.Text))
                {
                    AddTextFor(obj);
                }

                Console.WriteLine(obj);
            }
        }
    }
}
