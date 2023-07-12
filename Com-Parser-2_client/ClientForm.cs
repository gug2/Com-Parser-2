using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;

namespace Com_Parser_2_client
{
    public partial class ClientForm : Form
    {
        public static StatusLogging StatusLogging;

        private NetTransferLogic netTransferLogic;
        private DisplayLogic displayLogic;
        private StreamPacketParser chunkParser;
        private string fileForParse;
        private readonly Dictionary<string, PacketFormat> LoadedFormats = new Dictionary<string, PacketFormat>();

        public ClientForm()
        {
            InitializeComponent();
            StatusLogging = StatusLogging.From(StatusValue);

            ParserWorker.ProgressChanged += (o, a) =>
            {
                ParsedFileProgress.Value = a.ProgressPercentage;
            };
            ParserWorker.DoWork += (o, a) =>
            {
                displayLogic.ParseBinFile(ParserWorker, fileForParse);
                NetRxCount.Invoke(new EventHandler<int[]>(NetRxCountUpdating), NetRxCount, new int[] { displayLogic.SuccessPackets, displayLogic.BrokenPackets });
            };

            netTransferLogic = new NetTransferLogic(RemoteReceiveWorker);
            netTransferLogic.PacketReceived += (o, a) =>
            {
                NetTransferLogic_PacketReceived(o, a);
            };
            netTransferLogic.ServerDisconnecting += (o, a) =>
            {
                chunkParser.FlushParser();
                Invoke(new EventHandler(NetTransferLogicServer_Disconnecting), o, a);
            };
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            displayLogic = new DisplayLogic(ChartFlowPanel, TextFlowPanel, PacketFormatData);
        }

        private void NetRxCountUpdating(object sender, int[] e)
        {
            NetRxCount.SetFormatArgs(e[0], e[1]);
        }

        private void NetTransferLogicServer_Disconnecting(object sender, EventArgs e)
        {
            DisconnectRemote(sender, e);
        }

        private void NetTransferLogic_PacketReceived(object sender, object[] e)
        {
            if (chunkParser == null)
            {
                return;
            }

            chunkParser.ScheduleChunk((byte[])e[0], (int)e[1]);
        }

        private async void ConnectRemote(object sender, EventArgs e)
        {
            if (!ipTextBox1.ValidState)
            {
                StatusLogging.Error("Недопустимый формат адреса.");
                return;
            }

            try
            {
                await netTransferLogic.Connect(ipTextBox1.Value, Convert.ToUInt16(RemotePort.Text));

                ConnectToRemote.Click -= ConnectToRemote_Click;
                ConnectToRemote.Click += DisconnectRemote;
                ConnectToRemote.Text = "Отключить";
                ConnectionStatus.Text = "Соединено";
                ConnectionStatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                StatusLogging.Error(ex.ToString());
            }
        }

        private void DisconnectRemote(object sender, EventArgs e)
        {
            netTransferLogic.Disconnect();

            ConnectToRemote.Click -= DisconnectRemote;
            ConnectToRemote.Click += ConnectToRemote_Click;
            ConnectToRemote.Text = "Подключить";
            ConnectionStatus.Text = "Не подключено";
            ConnectionStatus.ForeColor = Color.Red;
        }

        private void ConnectToRemote_Click(object sender, EventArgs e)
        {
            ConnectRemote(sender, e);
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
                    UserScript script = new UserScript(dialog.FileName);

                    if (script.Compile())
                    {
                        // загрузка скрипта
                        PacketFormat format = new PacketFormat(script);
                        displayLogic.Load(format);
                        LoadedFormats.Add(script.AssemblyPath, format);
                        chunkParser = new StreamPacketParser(ParseStreamWorker, displayLogic, format);

                        Format_TS.GetCurrentParent().Items.Add(script.AssemblyPath, null, (obj, args) =>
                        {
                            ToolStripItem ts = obj as ToolStripItem;
                            displayLogic.Load(LoadedFormats[ts.Text]);
                        });
                    }
                }
            }
        }

        private void BrowseParsedFilePath_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.CheckPathExists = true;
                dialog.CheckFileExists = true;
                dialog.Multiselect = false;
                dialog.Filter = "Двоичные данные (*.txt;*.bin;*.dat;*.log)|*.txt;*.bin;*.dat;*.log;";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    fileForParse = dialog.FileName;
                    ParsedFilePath.SetFormatArgs(dialog.FileName);

                    ParseFile.Enabled = !string.IsNullOrEmpty(fileForParse);
                }
            }
        }

        private void TextLabel_TS_Click(object sender, EventArgs e)
        {
            FontDialog dialog = new FontDialog()
            {
                ShowEffects = true,
                AllowSimulations = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                TextFlowPanel.Font = dialog.Font;
            }
        }

        private void TextValue_TS_Click(object sender, EventArgs e)
        {
            FontDialog dialog = new FontDialog()
            {
                ShowEffects = true,
                AllowSimulations = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (Control c in TextFlowPanel.Controls)
                {
                    if (c is Label l && l.Name == "value")
                    {
                        l.Font = dialog.Font;
                    }
                }
            }
        }

        private void flowLayoutPanel1_ClientSizeChanged(object sender, EventArgs e)
        {
            FlowLayoutPanel flow = sender as FlowLayoutPanel;

            foreach (Control c in flow.Controls)
            {
                c.Width = flow.ClientSize.Width - c.Left * 2;
            }
        }

        private void ParsedFileGroup_ClientSizeChanged(object sender, EventArgs e)
        {
            Control c = sender as Control;

            ParsedFilePath.Width = c.ClientSize.Width - ParsedFilePath.Left * 2;
        }

        private void ParseFile_Click(object sender, EventArgs e)
        {
            if (!ParserWorker.IsBusy)
            {
                ParserWorker.RunWorkerAsync();
            }
        }
    }
}
