using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Com_Parser_2_client
{
    public partial class IPTextBox : UserControl
    {
        private readonly MaskedTextBox[] parts;
        [Browsable(false)]
        public bool ValidState
        {
            get
            {
                bool flag = true;

                for (int i = 0; i < parts.Length; i++)
                {
                    flag &= partsValidState[i];
                }

                return flag;
            }
        }
        private string valueField;
        public string Value
        {
            set
            {
                valueField = value;

                string[] arr = value.Split('.');

                for (int i = 0; i < arr.Length; i++)
                {
                    parts[i].Text = arr[i];
                }
            }
            get
            {
                string res = "";

                if (ValidState)
                {
                    for (int i = 0; i < parts.Length; i++)
                    {
                        res += parts[i].Text;

                        if (i < parts.Length - 1)
                        {
                            res += '.';
                        }
                    }
                }
                else
                {
                    res = valueField;
                }

                return res;
            }
        }
        private readonly bool[] partsValidState;

        public IPTextBox()
        {
            InitializeComponent();
            parts = new MaskedTextBox[] { maskedTextBox1, maskedTextBox2, maskedTextBox3, maskedTextBox4 };
            partsValidState = new bool[parts.Length];
        }

        private void MaskedTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            MaskedTextBox tb = sender as MaskedTextBox;

            int i = tb.TabIndex;

            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (tb.Text.Length > 0 && tb.SelectionStart > 0)
                    {
                        break;
                    }

                    i--;
                    if (i < 0)
                    {
                        i = parts.Length - 1;
                    }

                    parts[i].Focus();
                    break;
                case Keys.Right:
                    if (tb.Text.Length > 0 && tb.SelectionStart < tb.Text.Length)
                    {
                        break;
                    }

                    i++;
                    if (i >= parts.Length)
                    {
                        i = 0;
                    }

                    parts[i].Focus();
                    break;
                default:
                    break;
            }
        }

        private void MaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            MaskedTextBox tb = sender as MaskedTextBox;

            if (string.IsNullOrEmpty(tb.Text))
            {
                return;
            }

            bool flag = IsIPValid(tb.Text);

            partsValidState[tb.TabIndex] = flag;
            tb.ForeColor = flag ? Color.Black : Color.Red;
        }

        private bool IsIPValid(string ip)
        {
            int i = int.Parse(ip);

            return i >= 0 && i <= 255;
        }
    }
}
