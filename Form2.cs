using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace Com_Parser_2
{
    public partial class ExtendedSerialSettingsForm : Form
    {
        public static string FORM_SETTING_FILE_PATH = "essf.txt";

        public ExtendedSerialSettingsForm()
        {
            InitializeComponent();
        }

        private void ExtendedSerialSettingsForm_Load(object sender, EventArgs e)
        {
            this.paritiesList.DataSource = Enum.GetValues(typeof(Parity));
            this.paritiesList.SelectedItem = Parity.None;

            int defaultDataBits = 8;
            this.dataBitsList.DataSource = new int[] { 5, 6, 7, 8, 9 };
            this.dataBitsList.SelectedItem = defaultDataBits;

            this.stopBitsList.DataSource = Enum.GetValues(typeof(StopBits));
            this.stopBitsList.SelectedItem = StopBits.One;

            FormSetting[] settings = GlobalFields.get().LoadFormSettings(FORM_SETTING_FILE_PATH);
            if (settings == null)
            {
                return;
            }

            foreach (FormSetting setting in settings)
            {
                if (setting != null)
                {
                    switch (setting.Name)
                    {
                        case "parity":
                            if (Enum.IsDefined(typeof(Parity), setting.Value))
                            {
                                this.paritiesList.SelectedItem = Enum.Parse(typeof(Parity), setting.Value);
                            }
                            break;
                        case "dataBit":
                            this.dataBitsList.SelectedItem = Convert.ToInt32(setting.Value);
                            break;
                        case "stopBit":
                            if (Enum.IsDefined(typeof(StopBits), setting.Value))
                            {
                                this.stopBitsList.SelectedItem = Enum.Parse(typeof(StopBits), setting.Value);
                            }
                            break;
                        default: break;
                    }
                }
            }
        }

        private void ExtendedSerialSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalFields.get().SaveFormSettings(FORM_SETTING_FILE_PATH, new FormSetting[] {
                new FormSetting("parity", this.paritiesList.SelectedItem.ToString()),
                new FormSetting("dataBit", this.dataBitsList.SelectedItem.ToString()),
                new FormSetting("stopBit", this.stopBitsList.SelectedItem.ToString())
            });

            GlobalFields.get().NotifyExtendedSerialSettingsFormClosed();
        }
    }
}
