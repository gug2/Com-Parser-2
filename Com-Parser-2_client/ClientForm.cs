using System;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace Com_Parser_2_client
{
    public partial class ClientForm : Form
    {
        private NetTransferLogic netTransferLogic;
        private DisplayLogic cl;

        public ClientForm()
        {
            InitializeComponent();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            cl = new DisplayLogic(flowLayoutPanel2, flowLayoutPanel3);

            PacketFormat pf = new PacketFormat("loadme.cs", "loadme.dll");
            pf.AssemblyCode();
            cl.Load(pf);
            cl.ParseBinFile("log_cn.bin");
            //cl.ParseBinFile("var8_noised.dat");
            //cl.ParseBinFile("var8_clean.dat");

            NetRxCount.SetFormatArgs(cl.SuccessPackets, cl.BrokenPackets);

            netTransferLogic = new NetTransferLogic(RemoteReceiveWorker);
            netTransferLogic.PacketReceived += (o, a) =>
            {
                Invoke(new EventHandler<byte[]>(NetTransferLogic_PacketReceived), o, a);
            };
            netTransferLogic.ServerDisconnecting += (o, a) =>
            {
                Invoke(new EventHandler(NetTransferLogicServer_Disconnecting), o, a);
            };

            RemoteAddressValue.TextValidator((text) =>
            {
                bool flag = VerifyRemoteAddress(text);

                if (!flag)
                {
                    StatusLogging.Error(StatusValue, "Некорректный формат! Адрес должен находиться в пределах от 0.0.0.0 до 255.255.255.255");
                }

                return flag;
            });
        }

        private void NetTransferLogicServer_Disconnecting(object sender, EventArgs e)
        {
            DisconnectRemote(sender, e);
        }

        private void NetTransferLogic_PacketReceived(object sender, byte[] e)
        {
            StatusLogging.Info(StatusValue, String.Format("принят пакет: {0}", BitConverter.ToString(e)));
        }

        private void ClientForm_Layout(object sender, LayoutEventArgs e)
        {
            ConnectToRemote.Enabled = CheckRemoteIP();
        }

        private bool CheckRemoteIP()
        {
            return RemoteAddressValue.MaskCompleted && RemotePortValue.MaskCompleted;
        }

        private async void ConnectRemote(object sender, EventArgs e)
        {
            if (!RemoteAddressValue.MaskCompleted || !RemotePortValue.MaskCompleted)
            {
                return;
            }

            string address = TrimRemoteAddress(RemoteAddressValue.Text, false);
            int port = Convert.ToInt32(RemotePortValue.Text);

            try
            {
                await netTransferLogic.Connect(address, port);

                ConnectToRemote.Click -= ConnectToRemote_Click;
                ConnectToRemote.Click += DisconnectRemote;
                ConnectToRemote.Text = "Откл.";
                StatusLogging.Info(RemoteStatus, "Соединено", logTime: false);
            }
            catch (Exception ex)
            {
                StatusLogging.Error(StatusValue, ex.ToString());
            }
        }

        private void DisconnectRemote(object sender, EventArgs e)
        {
            netTransferLogic.Disconnect();

            ConnectToRemote.Click -= DisconnectRemote;
            ConnectToRemote.Click += ConnectToRemote_Click;
            ConnectToRemote.Text = "Подкл.";
            StatusLogging.Error(RemoteStatus, "Нет соединения", logTime: false);
        }

        private void ConnectToRemote_Click(object sender, EventArgs e)
        {
            ConnectRemote(sender, e);
        }

        private void RemotePortValue_TextChanged(object sender, EventArgs e)
        {
            ConnectToRemote.Enabled = CheckRemoteIP();
        }

        private void RemoteAddressValue_Validated(object sender, EventArgs e)
        {
            RemoteAddressValue.Text = TrimRemoteAddress(RemoteAddressValue.Text);

            ConnectToRemote.Enabled = CheckRemoteIP();
        }

        private string TrimRemoteAddress(string address, bool shouldPadCell = true)
        {
            string[] cells = address.Split(RemoteAddressValue.Delimiter);

            int cellSize;
            for (int i = 0; i < cells.Length; i++)
            {
                if (!shouldPadCell)
                {
                    cells[i] = cells[i].Trim(new char[] { RemoteAddressValue.PromptChar, ' ' });
                }

                cellSize = cells[i].Length;

                // удаляем незначащие нули, если имеются
                cells[i] = TrimExtraZeros(cells[i], RemoteAddressValue.PromptChar);

                // заполняем пустое место, если необходимо
                if (shouldPadCell && cells[i].Length < cellSize)
                {
                    cells[i] = cells[i].PadRight(cellSize, RemoteAddressValue.PromptChar);
                }
            }

            return String.Join(RemoteAddressValue.Delimiter.ToString(), cells);
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

        private bool VerifyRemoteAddress(string address)
        {
            string[] cells = address.Split(RemoteAddressValue.Delimiter);

            if (cells.Length != 4)
            {
                return false;
            }

            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = cells[i].Trim(new char[] { RemoteAddressValue.PromptChar, ' ' });

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
                    string name = Path.GetFileNameWithoutExtension(dialog.FileName);
                    string directory = Path.GetDirectoryName(dialog.FileName);
                    if (!string.IsNullOrEmpty(directory))
                    {
                        directory += '\\';
                    }
                    //PacketFormat.AssemblyCode(dialog.FileName, directory + name + ".dll");
                }
            }
        }

        private void flowLayoutPanel1_Layout(object sender, LayoutEventArgs e)
        {
            FlowLayoutPanel flow = sender as FlowLayoutPanel;

            foreach (Control c in flow.Controls)
            {
                c.Width = (int)(flow.Width * 0.95F);
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
                flowLayoutPanel3.Font = dialog.Font;
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
                foreach (Control c in flowLayoutPanel3.Controls)
                {
                    if (c is Label l && l.Name == "value")
                    {
                        l.Font = dialog.Font;
                    }
                }
            }
        }
    }
}
