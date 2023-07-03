using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Com_Parser_2_client
{
    public partial class ClientForm : Form
    {
        public static StatusLogging StatusLogging;

        private NetTransferLogic netTransferLogic;
        private Stream netStream;
        private DisplayLogic displayLogic;
        private string fileForParse;

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
                byte[] flushed = new byte[last];
                netStream.Seek(-flushed.Length, SeekOrigin.Current);
                netStream.Read(flushed, 0, flushed.Length);
                displayLogic.FlushParsingStream(ParseStreamWorker, flushed);
                Invoke(new EventHandler(NetTransferLogicServer_Disconnecting), o, a);
            };

            ParseStreamWorker.DoWork += (o, a) =>
            {
                displayLogic.DoWork(o, a);
            };
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            displayLogic = new DisplayLogic(ChartFlowPanel, TextFlowPanel);

            RemoteAddress.TextValidator((text) =>
            {
                bool flag = VerifyRemoteAddress(RemoteAddress, text);

                if (!flag)
                {
                    StatusLogging.Error("Некорректный формат! Адрес должен находиться в пределах от 0.0.0.0 до 255.255.255.255");
                }

                return flag;
            });
        }

        private void NetRxCountUpdating(object sender, int[] e)
        {
            NetRxCount.SetFormatArgs(e[0], e[1]);
        }

        private void NetTransferLogicServer_Disconnecting(object sender, EventArgs e)
        {
            Console.WriteLine();
            DisconnectRemote(sender, e);
        }

        long last = 0;
        byte[] chunkBuffer = new byte[DisplayLogic.PW_CHUNK_SIZE];
        private void NetTransferLogic_PacketReceived(object sender, object[] e)
        {
            netStream.Write((byte[])e[0], 0, (int)e[1]);
            last += (int)e[1];

            if (last >= DisplayLogic.PW_CHUNK_SIZE)
            {
                netStream.Seek(-last, SeekOrigin.Current);
                netStream.Read(chunkBuffer, 0, chunkBuffer.Length);
                last = last - DisplayLogic.PW_CHUNK_SIZE;
                netStream.Seek(last, SeekOrigin.Current);
                displayLogic.ScheduleParsingChunk(ParseStreamWorker, chunkBuffer);
            }
        }

        private void ClientForm_Layout(object sender, LayoutEventArgs e)
        {
            ConnectToRemote.Enabled = CheckRemoteIP();
        }

        private bool CheckRemoteIP()
        {
            return RemoteAddress.MaskCompleted && RemotePort.MaskCompleted;
        }

        private async void ConnectRemote(object sender, EventArgs e)
        {
            if (!CheckRemoteIP())
            {
                return;
            }

            string address = TrimRemoteAddress(RemoteAddress, false);
            int port = Convert.ToInt32(RemotePort.Text);

            try
            {
                await netTransferLogic.Connect(address, port);

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

        private void RemotePort_TextChanged(object sender, EventArgs e)
        {
            ConnectToRemote.Enabled = CheckRemoteIP();
        }

        private void RemoteAddress_Validated(object sender, EventArgs e)
        {
            RemoteAddress.Text = TrimRemoteAddress(RemoteAddress);

            ConnectToRemote.Enabled = CheckRemoteIP();
        }

        private string TrimRemoteAddress(NullableMaskedTextBox textBox, bool shouldPadCell = true)
        {
            string[] cells = textBox.Text.Split(textBox.Delimiter);

            int cellSize;
            for (int i = 0; i < cells.Length; i++)
            {
                if (!shouldPadCell)
                {
                    cells[i] = cells[i].Trim(new char[] { textBox.PromptChar, ' ' });
                }

                cellSize = cells[i].Length;

                // удаляем незначащие нули, если имеются
                cells[i] = TrimExtraZeros(cells[i], textBox.PromptChar);

                // заполняем пустое место, если необходимо
                if (shouldPadCell && cells[i].Length < cellSize)
                {
                    cells[i] = cells[i].PadRight(cellSize, textBox.PromptChar);
                }
            }

            return String.Join(textBox.Delimiter.ToString(), cells);
        }

        private string TrimExtraZeros(string s, char promptChar)
        {
            int k = 0;
            while (k < s.Length - 1 && s[k] == '0' && s[k + 1] != promptChar)
            {
                k++;
            }

            return s.Remove(0, k);
        }

        private bool VerifyRemoteAddress(NullableMaskedTextBox textBox, string address)
        {
            string[] cells = address.Split(textBox.Delimiter);

            if (cells.Length != 4)
            {
                return false;
            }

            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = cells[i].Trim(new char[] { textBox.PromptChar, ' ' });

                if (cells[i].Length == 0)
                {
                    return false;
                }

                if (!int.TryParse(cells[i], out int result))
                {
                    return false;
                }
                else
                {
                    if (result < 0 || result > 255)
                    {
                        return false;
                    }
                }
            }

            return true;
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
                        netStream = new MemoryStream();
                        displayLogic.InitParsingStream();

                        //ToolStripItem item = new ToolStripMenuItem();
                        //Format_TS.GetCurrentParent().Items.Add(script.AssemblyPath);
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
                dialog.Filter = "Двоичные данные (*.txt;*.bin;*.dat)|*.txt;*.bin;*.dat;";

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
