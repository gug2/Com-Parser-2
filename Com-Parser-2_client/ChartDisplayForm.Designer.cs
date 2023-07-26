
namespace Com_Parser_2_client
{
    partial class ChartDisplayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.XAxisValues = new System.Windows.Forms.ComboBox();
            this.YAxisValues = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.Chart)).BeginInit();
            this.SuspendLayout();
            // 
            // Chart
            // 
            this.Chart.BackColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.Title = "Ось X";
            chartArea1.AxisY.Title = "Ось Y";
            chartArea1.Name = "ChartArea1";
            this.Chart.ChartAreas.Add(chartArea1);
            this.Chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Alignment = System.Drawing.StringAlignment.Far;
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            this.Chart.Legends.Add(legend1);
            this.Chart.Location = new System.Drawing.Point(3, 3);
            this.Chart.Name = "Chart";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.Chart.Series.Add(series1);
            this.Chart.Size = new System.Drawing.Size(523, 283);
            this.Chart.TabIndex = 0;
            this.Chart.Text = "chart1";
            title1.Name = "Title1";
            title1.Text = "Название";
            this.Chart.Titles.Add(title1);
            // 
            // XAxisValues
            // 
            this.XAxisValues.FormattingEnabled = true;
            this.XAxisValues.Location = new System.Drawing.Point(79, 262);
            this.XAxisValues.Name = "XAxisValues";
            this.XAxisValues.Size = new System.Drawing.Size(121, 21);
            this.XAxisValues.TabIndex = 1;
            // 
            // YAxisValues
            // 
            this.YAxisValues.FormattingEnabled = true;
            this.YAxisValues.Location = new System.Drawing.Point(317, 262);
            this.YAxisValues.Name = "YAxisValues";
            this.YAxisValues.Size = new System.Drawing.Size(121, 21);
            this.YAxisValues.TabIndex = 2;
            // 
            // ChartDisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 289);
            this.Controls.Add(this.YAxisValues);
            this.Controls.Add(this.XAxisValues);
            this.Controls.Add(this.Chart);
            this.Name = "ChartDisplayForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "ChartDisplayForm";
            ((System.ComponentModel.ISupportInitialize)(this.Chart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart Chart;
        private System.Windows.Forms.ComboBox XAxisValues;
        private System.Windows.Forms.ComboBox YAxisValues;
    }
}