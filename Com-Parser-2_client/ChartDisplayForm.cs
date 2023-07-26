using System;
using System.Windows.Forms;

namespace Com_Parser_2_client
{
    public partial class ChartDisplayForm : Form
    {
        private readonly bool HasXAxis;

        public ChartDisplayForm()
        {
            InitializeComponent();
        }

        public ChartDisplayForm(bool hasXAxis) : this()
        {
            HasXAxis = hasXAxis;
            Chart.Series[0].IsXValueIndexed = !hasXAxis;
        }

        public void SetTitle(string title)
        {
            Text = title;
        }

        public void SetAxis(string yName)
        {
            SetAxis("Индекс", yName);
        }

        public void SetAxis(string xName, string yName)
        {
            Chart.ChartAreas[0].AxisX.Title = xName;
            Chart.ChartAreas[0].AxisY.Title = yName;
            Chart.Series[0].LegendText = yName;
            Chart.Titles[0].Text = String.Format("График {0} от {1}", yName, xName);
        }

        public void Clear()
        {
            if (Chart.InvokeRequired)
            {
                Chart.BeginInvoke(new EventHandler((obj, args) =>
                {
                    Chart.Series[0].Points.Clear();
                }));
            }
            else
            {
                Chart.Series[0].Points.Clear();
            }
        }

        public void AddPoints(object x, object y)
        {
            if (Chart.InvokeRequired)
            {
                Chart.BeginInvoke(new EventHandler((obj, args) =>
                {
                    if (HasXAxis)
                    {
                        Chart.Series[0].Points.AddXY(x, y);
                    }
                    else
                    {
                        Chart.Series[0].Points.AddY(y);
                    }
                }));
            }
            else
            {
                if (HasXAxis)
                {
                    Chart.Series[0].Points.AddXY(x, y);
                }
                else
                {
                    Chart.Series[0].Points.AddY(y);
                }
            }
        }
    }
}
