using System;
using System.Drawing;
using System.Windows.Forms;

namespace Com_Parser_2
{
    class Status
    {
        public static void Error(Label status, string msg)
        {
            ShowMessage(status, msg, Color.Red);
        }

        public static void Info(Label status, string msg)
        {
            ShowMessage(status, msg, Color.Green);
        }

        private static void ShowMessage(Label status, string msg, Color msgColor)
        {
            status.Text = DateTime.Now.ToLongTimeString() + " -> " + msg;
            status.ForeColor = msgColor;
        }
    }
}
