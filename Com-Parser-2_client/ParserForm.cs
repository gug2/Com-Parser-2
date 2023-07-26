using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Com_Parser_2_client
{
    public partial class ParserForm : Form
    {
        public static StatusLogging StatusLogging;
        public static DisplayLogic DisplayLogic;
        private string ParsingFilePathStr;
        private NetTransferLogic NetLogic;
        private NetParser NetParser;
        private NetParseHandler NetParseHandler;

        public ParserForm()
        {
            InitializeComponent();
            StatusLogging = StatusLogging.From(StatusValue);
            DisplayLogic = DisplayLogic.From(this);
        }

        private void ParserForm_Load(object sender, EventArgs e)
        {
            FileParseWorker.DoWork += (obj, args) =>
            {
                if (MdiParent != null && MdiParent is ClientForm client)
                {
                    client.ClearCharts();
                }
                StreamParser parser = Parsers.ParseBinFile(ParsingFilePathStr, DisplayLogic.SelectedFormat, (progress) => FileParseWorker.ReportProgress((int)Math.Round(progress * 100)));
                NetRxCount.BeginInvoke(new EventHandler((obj2, args2) => NetRxCount.SetFormatArgs(parser.SuccessPackets, parser.BrokenPackets)));
            };
            FileParseWorker.ProgressChanged += (obj, args) =>
            {
                ParseProgress.Value = args.ProgressPercentage;
            };

            NetParser = new NetParser()
            {
                Source = new MemoryStream()
            };
            NetParseWorker.DoWork += (obj, args) =>
            {
                NetParser.Parse(NetParseHandler);
                StatusLogging.Info(String.Format("{0}/{1}", NetParser.SuccessPackets, NetParser.BrokenPackets));
            };

            NetLogic = new NetTransferLogic(NetWorker);
            NetLogic.ServerConnecting += (obj, args) =>
            {
                NetParseHandler = new NetParseHandler()
                {
                    Format = DisplayLogic.SelectedFormat
                };
                NetParser.Init(NetParseHandler);
                if (!NetParseWorker.IsBusy)
                {
                    NetParseWorker.RunWorkerAsync();
                }
            };
            NetLogic.ServerDisconnecting += (obj, args) =>
            {
                BeginInvoke(new EventHandler(DisconnectFromRemote_Click), obj, args);

                NetParser.FlushDataChunks();
                NetParser.Stop();
            };
            NetLogic.PacketReceived += (obj, args) =>
            {
                NetParser.ScheduleDataChunk((byte[])args[0], (int)args[1]);
            };
        }

        private async void ConnectToRemote_Click(object sender, EventArgs e)
        {
            if (!RemotePort.MaskCompleted || !ipTextBox1.ValidState)
            {
                return;
            }

            bool result = await NetLogic.Connect(ipTextBox1.Value, Convert.ToUInt16(RemotePort.Text));

            if (!result)
            {
                return;
            }

            ConnectToRemote.Text = "Отключить";
            ConnectToRemote.Click -= ConnectToRemote_Click;
            ConnectToRemote.Click += DisconnectFromRemote_Click;

            ConnectionStatus.Text = "Подключено";
            ConnectionStatus.ForeColor = Color.Green;
        }

        private void DisconnectFromRemote_Click(object sender, EventArgs e)
        {
            NetLogic.Disconnect();

            ConnectToRemote.Text = "Подключить";
            ConnectToRemote.Click -= DisconnectFromRemote_Click;
            ConnectToRemote.Click += ConnectToRemote_Click;

            ConnectionStatus.Text = "Отключено";
            ConnectionStatus.ForeColor = Color.Red;
        }

        private void Format_TS_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.CheckPathExists = true;
                dialog.CheckFileExists = true;
                dialog.Multiselect = false;
                dialog.Filter = "Форматы данных (*.cs)|*.cs";
                
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (DisplayLogic.TryLoadFormat(dialog.FileName))
                    {
                        Format_TS.GetCurrentParent().Items.Add(dialog.FileName, null, (obj, args) =>
                        {
                            ToolStripItem ts = obj as ToolStripItem;
                            DisplayLogic.TryLoadFormat(ts.Text);
                        });
                    }
                }
            }
        }

        private void BrowseFileForParse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.CheckPathExists = true;
                dialog.CheckFileExists = true;
                dialog.Multiselect = false;
                dialog.Filter = "Двоичные данные (*.txt;*.bin;*.dat;*.log)|*.txt;*.bin;*.dat;*.log;";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ParsingFilePathStr = dialog.FileName;
                    ParsingFilePath.SetFormatArgs(dialog.FileName);

                    Parse.Enabled = !string.IsNullOrEmpty(dialog.FileName);
                }
            }
        }

        private void Parse_Click(object sender, EventArgs e)
        {
            if (FileParseWorker.IsBusy)
            {
                return;
            }

            FileParseWorker.RunWorkerAsync();
        }

        private void flowLayoutPanel1_ClientSizeChanged(object sender, EventArgs e)
        {
            FlowLayoutPanel flow = sender as FlowLayoutPanel;

            foreach (Control c in flow.Controls)
            {
                c.Width = flow.ClientSize.Width - c.Left * 2;
            }
        }
    }
}
