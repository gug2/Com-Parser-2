using System;
using System.Windows.Forms;

namespace Com_Parser_2_client
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            
        }

        private bool CheckRemoteIP()
        {
            return this.RemoteAddressValue.Text.Length > 0 && this.RemotePortValue.Text.Length > 0;
        }

        private void Connect()
        {
            this.ConnectToRemote.Click -= new EventHandler(this.ConnectToRemote_Click);
            this.ConnectToRemote.Click += new EventHandler(this.Disconnect);
            this.ConnectToRemote.Text = "Откл.";
            Status.Info(this.RemoteStatus, "Соединено", logTime: false);
        }

        private void Disconnect(object sender, EventArgs e)
        {
            this.ConnectToRemote.Click -= new EventHandler(this.Disconnect);
            this.ConnectToRemote.Click += new EventHandler(this.ConnectToRemote_Click);
            this.ConnectToRemote.Text = "Подкл.";
            Status.Error(this.RemoteStatus, "Нет соединения", logTime: false);
        }

        private void ConnectToRemote_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void RemoteAddressValue_TextChanged(object sender, EventArgs e)
        {
            this.ConnectToRemote.Enabled = CheckRemoteIP();
        }

        private void RemotePortValue_TextChanged(object sender, EventArgs e)
        {
            this.ConnectToRemote.Enabled = CheckRemoteIP();
        }

        int pos = 0;

        private void AddPos(int positionsLength)
        {
            if (pos < positionsLength)
            {
                pos++;
            }
        }

        private void SubPos()
        {
            if (pos > 0)
            {
                pos--;
            }
        }

        private void RemoteAddressValue_KeyDown(object sender, KeyEventArgs e)
        {
            MaskedTextBox text = (MaskedTextBox)sender;

            int[] positions = new int[] { 0, 3, 6, 9 };

            bool isEnglish = InputLanguage.CurrentInputLanguage.Culture.Name.Equals("en-US");

            switch (e.KeyCode)
            {
                case Keys.Right:
                case Keys.Space:
                    AddPos(positions.Length);
                    break;
                case Keys.Left:
                case Keys.Back:
                    SubPos();
                    break;
                default:
                    if (isEnglish)
                    {
                        if (e.KeyCode == Keys.Oemcomma)
                        {
                            AddPos(positions.Length);
                        }
                    }
                    else
                    {
                        if (e.KeyCode == Keys.Oem2)
                        {
                            AddPos(positions.Length);
                        }
                    }
                    break;
            }
        }
    }
}
