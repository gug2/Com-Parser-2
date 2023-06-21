using System;
using System.Windows.Forms;

namespace Com_Parser_2_client
{
    class LabelFormat : Label
    {
        public string Format { set; get; }

        public void SetFormatArgs(params object[] args)
        {
            Text = String.Format(Format, args);
        }
    }
}
