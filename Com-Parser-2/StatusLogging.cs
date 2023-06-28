using System;
using System.Drawing;
using System.Windows.Forms;

namespace Com_Parser_2
{
    public class StatusLogging
    {
        private static readonly ToolTip toolTip = new ToolTip()
        {
            ShowAlways = true
        };

        private readonly Control control;

        private StatusLogging(Control control)
        {
            this.control = control;
        }

        public static StatusLogging From(Control control)
        {
            return new StatusLogging(control);
        }

        public void Error(string msg, bool logTime = true)
        {
            toolTip.ToolTipIcon = ToolTipIcon.Error;

            if (control.InvokeRequired)
            {
                control.Invoke(new EventHandler((obj, args) =>
                {
                    ShowMessage(msg, Color.Red, logTime);
                }));
            }
            else
            {
                ShowMessage(msg, Color.Red, logTime);
            }
        }

        public void Info(string msg, bool logTime = true)
        {
            toolTip.ToolTipIcon = ToolTipIcon.Info;

            if (control.InvokeRequired)
            {
                control.Invoke(new EventHandler((obj, args) =>
                {
                    ShowMessage(msg, Color.Green, logTime);
                }));
            }
            else
            {
                ShowMessage(msg, Color.Green, logTime);
            }
        }

        private void ShowMessage(string msg, Color msgColor, bool logTime)
        {
            control.Text = (logTime ? DateTime.Now.ToLongTimeString() + " -> " : "") + msg;
            control.ForeColor = msgColor;

            toolTip.SetToolTip(control, control.Text);
        }
    }
}
