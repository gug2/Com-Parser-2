using System;
using System.Windows.Forms;

namespace Com_Parser_2
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
