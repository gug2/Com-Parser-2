using System;
using System.Drawing;
using System.Windows.Forms;

namespace Com_Parser_2_client
{
    class Status
    {
        public static void Error(Label status, string msg, bool logTime = true)
        {
            ShowMessage(status, msg, Color.Red, logTime);
        }

        public static void Info(Label status, string msg, bool logTime = true)
        {
            ShowMessage(status, msg, Color.Green, logTime);
        }

        private static void ShowMessage(Label status, string msg, Color msgColor, bool logTime)
        {
            string time = "";
            if (logTime)
            {
                time = DateTime.Now.ToLongTimeString() + " -> ";
            }
            status.Text = time + msg;
            status.ForeColor = msgColor;
        }
    }
}
