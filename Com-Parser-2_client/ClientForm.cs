using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Com_Parser_2_client
{
    public partial class ClientForm : Form
    {
        public readonly Dictionary<Type, Form> SubForms = new Dictionary<Type, Form>();
        private readonly List<ChartDisplayForm> DisplayedCharts = new List<ChartDisplayForm>();
        
        public ClientForm()
        {
            InitializeComponent();
        }

        public void ClearDropDownItems()
        {
            Charts_TS_Group.DropDownItems.Clear();
        }

        public void ClearCharts()
        {
            foreach (var chart in DisplayedCharts)
            {
                chart.Clear();
            }
        }

        public ChartDisplayForm AddChart(DataObject xDataObject, DataObject yDataObject)
        {
            if (Charts_TS_Group.DropDownItems.ContainsKey(yDataObject.Name))
            {
                return null;
            }

            bool hasXAxis = xDataObject != null && xDataObject != yDataObject;

            ChartDisplayForm chart = new ChartDisplayForm(hasXAxis)
            {
                MdiParent = this,
                MinimizeBox = false
            };

            chart.FormClosing += SubForm_Closing;
            chart.SetTitle(yDataObject.Name);

            if (hasXAxis)
            {
                chart.SetAxis(xDataObject.Name, yDataObject.Name);
            }
            else
            {
                chart.SetAxis(yDataObject.Name);
            }

            Charts_TS_Group.DropDownItems.Add(yDataObject.Name, null, (obj, args) =>
            {
                if (chart.Visible)
                {
                    chart.Focus();
                }
                else
                {
                    chart.Show();
                }
            });

            DisplayedCharts.Add(chart);

            return chart;
        }

        private Form CreateSubForm(Type type, params object[] args)
        {
            if (SubForms.ContainsKey(type))
            {
                return null;
            }

            if (Activator.CreateInstance(type, args) is Form form)
            {
                form.MdiParent = this;
                form.MinimizeBox = false;

                form.FormClosing += SubForm_Closing;

                SubForms.Add(type, form);
                return form;
            }

            return null;
        }

        private void ShowSubForm(Type type)
        {
            if (!SubForms.ContainsKey(type))
            {
                CreateSubForm(type);
            }

            if (SubForms[type].Visible)
            {
                SubForms[type].Focus();
            }
            else
            {
                SubForms[type].Show();
            }
        }

        private void SubForm_Closing(object sender, FormClosingEventArgs e)
        {
            Form form = sender as Form;
            form.Hide();
            e.Cancel = e.CloseReason != CloseReason.MdiFormClosing;
        }

        private void Parser_TS_Click(object sender, EventArgs e)
        {
            ShowSubForm(typeof(ParserForm));
        }

        private void Map_TS_Click(object sender, EventArgs e)
        {
            ShowSubForm(typeof(MapForm));
        }

        private void ViewCascade_TS_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void ViewRow_TS_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ViewStack_TS_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }
    }
}
