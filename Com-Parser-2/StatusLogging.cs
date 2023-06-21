using System;
using System.Drawing;
using System.Windows.Forms;

namespace Com_Parser_2
{
    class StatusLogging
    {
        private static readonly ToolTip toolTip = new ToolTip()
        {
            ShowAlways = true
        };

        public static void Error(Label status, string msg)
        {
            toolTip.ToolTipIcon = ToolTipIcon.Error;
            ShowMessage(status, msg, Color.Red);
        }

        public static void Info(Label status, string msg)
        {
            toolTip.ToolTipIcon = ToolTipIcon.Info;
            ShowMessage(status, msg, Color.Green);
        }

        private static void ShowMessage(Label status, string msg, Color msgColor, bool logTime = true)
        {
            status.Text = (logTime ? DateTime.Now.ToLongTimeString() + " -> " : "") + msg;
            status.ForeColor = msgColor;

            toolTip.SetToolTip(status, status.Text);
        }
    }
}
