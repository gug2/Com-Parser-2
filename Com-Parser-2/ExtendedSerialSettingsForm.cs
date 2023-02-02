using System;
using System.Windows.Forms;
using System.IO.Ports;

namespace Com_Parser_2
{
    public partial class ExtendedSerialSettingsForm : Form
    {
        public static string FORM_SETTING_FILE_PATH = "essf.bin";

        public ExtendedSerialSettingsForm()
        {
            InitializeComponent();
            SetDefaultSettings();
        }

        private void SetDefaultSettings()
        {
            this.paritiesList.DataSource = Enum.GetValues(typeof(Parity));
            this.paritiesList.SelectedItem = Parity.None;

            int defaultDataBits = 8;
            this.dataBitsList.DataSource = new int[] { 5, 6, 7, 8, 9 };
            this.dataBitsList.SelectedItem = defaultDataBits;

            this.stopBitsList.DataSource = Enum.GetValues(typeof(StopBits));
            this.stopBitsList.SelectedItem = StopBits.One;
        }

        private void ExtendedSerialSettingsForm_Load(object sender, EventArgs e)
        {
            // сделать десериализацию объекта настройки
            FormSetting[] settings;

            try
            {
                settings = GlobalFields.Get().LoadFormSettings(FORM_SETTING_FILE_PATH);
            }
            catch
            {
                return;
            }

            foreach (FormSetting setting in settings)
            {
                if (setting != null)
                {
                    switch (setting.Name)
                    {
                        case nameof(this.paritiesList):
                            this.paritiesList.SelectedIndex = Convert.ToInt32(setting.Value);
                            break;
                        case nameof(this.dataBitsList):
                            this.dataBitsList.SelectedIndex = Convert.ToInt32(setting.Value);
                            break;
                        case nameof(this.stopBitsList):
                            this.stopBitsList.SelectedIndex = Convert.ToInt32(setting.Value);
                            break;
                        default: break;
                    }
                }
            }
        }

        private void ExtendedSerialSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // сделать сериализацию объекта настройки
            GlobalFields.Get().SaveFormSettings(FORM_SETTING_FILE_PATH, new FormSetting[] {
                new FormSetting(nameof(this.paritiesList), this.paritiesList.SelectedIndex),
                new FormSetting(nameof(this.dataBitsList), this.dataBitsList.SelectedIndex),
                new FormSetting(nameof(this.stopBitsList), this.stopBitsList.SelectedIndex)
            });

            GlobalFields.Get().NotifyExtendedSerialSettingsFormClosed();
        }

        public Parity getParity()
        {
            return (Parity)this.paritiesList.SelectedItem;
        }

        public int getDataBits()
        {
            return (int)this.dataBitsList.SelectedItem;
        }

        public StopBits getStopBit()
        {
            return (StopBits)this.stopBitsList.SelectedItem;
        }
    }
}
