using GMap.NET;
using GMap.NET.MapProviders;
using System;
using System.Windows.Forms;

namespace Com_Parser_2_client
{
    public partial class GMapControl : UserControl
    {
        public GMapControl()
        {
            InitializeComponent();
        }

        private void GMapControl_Load(object sender, EventArgs e)
        {
            gMapControl1.Manager.Mode = AccessMode.ServerAndCache;
            gMapControl1.CacheLocation = "gmapCache";
            //gMapControl1.SetPositionByKeywords("Kamenovo airport, Vladimir Oblast, Russia");
            //gMapControl1.Position = new PointLatLng(56.4096174, 40.9732512);
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MinZoom = trackBar1.Minimum;
            gMapControl1.MaxZoom = trackBar1.Maximum;
            gMapControl1.Zoom = trackBar1.Value;

            radioButton1.CheckedChanged += (obj, args) =>
            {
                if (radioButton1.Checked)
                {
                    gMapControl1.MapProvider = GMapProviders.OpenStreetMap;
                }
            };

            radioButton2.CheckedChanged += (obj, args) =>
            {
                if (radioButton2.Checked)
                {
                    gMapControl1.MapProvider = GMapProviders.GoogleSatelliteMap;
                }
            };

            button1.Click += (obj, args) =>
            {
                trackBar1.Value = Math.Min(trackBar1.Value + trackBar1.SmallChange, trackBar1.Maximum);
            };

            button2.Click += (obj, args) =>
            {
                trackBar1.Value = Math.Max(trackBar1.Value - trackBar1.SmallChange, trackBar1.Minimum);
            };

            trackBar1.ValueChanged += (obj, args) =>
            {
                gMapControl1.Zoom = trackBar1.Value;
                label1.Text = gMapControl1.Zoom.ToString();
            };

            radioButton1.Checked = true;

            FindWithKeywords.Click += (obj, args) =>
            {
                gMapControl1.SetPositionByKeywords(PositionKeywords.Text);
            };

            GoToPointLatLng.Click += (obj, args) =>
            {
                gMapControl1.Position = new PointLatLng(double.Parse(Latitude.Text), double.Parse(Longitude.Text));
            };
        }
    }
}
