using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;

namespace Com_Parser_2_client
{
    class DisplayLogic
    {
        private readonly FlowLayoutPanel chartsPanel, textPanel;
        private readonly ComboBox packetFormatData;
        private readonly Dictionary<string, Chart> Charts = new Dictionary<string, Chart>();
        private readonly Dictionary<string, Label> Texts = new Dictionary<string, Label>();
        private PacketFormat PacketFormat;
        private DataObject Time;

        public int SuccessPackets { private set; get; }
        public int BrokenPackets { private set; get; }

        private delegate void DisplayToChartHandler(string name, object x, object y);
        private delegate void DisplayToTextHandler(string name, object value);

        public DisplayLogic(FlowLayoutPanel chartsPanel, FlowLayoutPanel textPanel, ComboBox comboBox)
        {
            this.chartsPanel = chartsPanel;
            this.textPanel = textPanel;
            packetFormatData = comboBox;
        }

        public void HandlePacket(PacketParser parser, object outStruct)
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
            if (Time == null || name == Time.Name)
            {
                Charts[name].ChartAreas[0].AxisX.Title = "Пакеты";
            }
            else
            {
                Charts[name].ChartAreas[0].AxisX.Title = "Время";
            }

            Charts[name].ChartAreas[0].AxisY.Title = name;
            Charts[name].Series[0].Points.AddXY(x, y);
        }

        private void DisplayToText(string name, object value)
        {
            Texts[name].Text = value.ToString();
        }

        private void AddChartFor(DataObject dataObject)
        {
            Chart chart = new Chart() { BackColor = Color.LightGray };
            ChartArea area = new ChartArea() { Name = "chartArea" };
            Legend legend = new Legend("legend") { Docking = Docking.Bottom, Alignment = StringAlignment.Far, DockedToChartArea = area.Name };
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

        private void Out(string msg, Stream stream)
        {
            byte[] array = Encoding.Default.GetBytes(msg);
            stream.Write(array, 0, array.Length);
        }

        public void Load(PacketFormat packetFormat)
        {
            chartsPanel.Controls.Clear();
            textPanel.Controls.Clear();
            Charts.Clear();
            Texts.Clear();

            object inputStruct = packetFormat.GetInputStruct();
            object outputStruct = packetFormat.GetOutputStruct(inputStruct);

            if (inputStruct == null || outputStruct == null)
            {
                Console.WriteLine("Ошибка! Отсутствует объект структуры.");
                ClientForm.StatusLogging.Error("Ошибка! Отсутствует объект структуры.");
                return;
            }

            PacketFormat = packetFormat;

            List<DataObject> dataObjects = outputStruct.GetType().GetFields().Select(info => DataObject.ConvertField(info)).ToList();

            packetFormatData.Items.Clear();
            int maxWidth = 0;
            foreach (DataObject obj in dataObjects)
            {
                packetFormatData.Items.Add(obj.Name);
                maxWidth = Math.Max(maxWidth, TextRenderer.MeasureText(obj.Name, packetFormatData.Font).Width);

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

                Console.WriteLine("Загружен {0}", obj);
            }
            packetFormatData.SelectedIndex = 0;
            packetFormatData.Width = maxWidth + 18;
        }

        public void ParseBinFile(BackgroundWorker worker, string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Файл не найден! Путь: \"{0}\"", Path.GetFullPath(path));
                ClientForm.StatusLogging.Error(String.Format("Файл не найден! Путь: \"{0}\"", Path.GetFullPath(path)));
                return;
            }

            if (PacketFormat == null)
            {
                Console.WriteLine("Отсутствует формат пакета!");
                ClientForm.StatusLogging.Error("Отсутствует формат пакета!");
                return;
            }

            chartsPanel.Invoke(new EventHandler((o, a) =>
            {
                SuccessPackets = 0;
                BrokenPackets = 0;
                foreach (Chart chart in chartsPanel.Controls)
                {
                    chart.Series[0].Points.Clear();
                }

            }), chartsPanel);

            string outPath = path + "_parser_out.txt";


            object filledStruct, outStruct;
            using (Stream outStream = File.Create(outPath))
            {
                using (Stream stream = File.OpenRead(path))
                {
                    PacketParser parser = new PacketParser(stream, PacketFormat);
                    parser.Use(buffer =>
                    {
                        filledStruct = PacketFormat.MarshalDeserialize(buffer);
                        SuccessPackets++;

                        outStruct = PacketFormat.GetOutputStruct(filledStruct);

                        Out(String.Format("PACKET {0} ->\"", parser.ReceivedPacketIndex), outStream);
                        var s = outStruct.GetType().GetFields().Select(info => String.Format("{0}: {1}", info.Name, info.GetValue(outStruct)));
                        Out(String.Join(", ", s), outStream);
                        Out("\"\n", outStream);

                        HandlePacket(parser, outStruct);

                        worker.ReportProgress((int)(stream.Position / (float)stream.Length * 100));
                    },
                    buffer =>
                    {
                        filledStruct = PacketFormat.MarshalDeserialize(buffer);
                        BrokenPackets++;

                        outStruct = PacketFormat.GetOutputStruct(filledStruct);

                        Out(String.Format("PACKET {0} ->\"", parser.ReceivedPacketIndex), outStream);
                        var s = outStruct.GetType().GetFields().Select(info => String.Format("{0}: {1}", info.Name, info.GetValue(outStruct)));
                        Out(String.Join(", ", s), outStream);
                        Out("\" BROKEN\n", outStream);

                        worker.ReportProgress((int)(stream.Position / (float)stream.Length * 100));
                    });

                    parser.Parse();

                    worker.ReportProgress(100);
                }
            }
        }
    }
}
