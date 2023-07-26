
namespace Com_Parser_2_client
{
    partial class ClientForm
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
            System.Windows.Forms.ToolStripMenuItem Screens_TS_Group;
            System.Windows.Forms.ToolStripMenuItem View_TS_Group;
            this.Parser_TS = new System.Windows.Forms.ToolStripMenuItem();
            this.Map_TS = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewCascade_TS = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewRow_TS = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewStack_TS = new System.Windows.Forms.ToolStripMenuItem();
            this.Charts_TS_Group = new System.Windows.Forms.ToolStripMenuItem();
            this.Texts_TS_Group = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            Screens_TS_Group = new System.Windows.Forms.ToolStripMenuItem();
            View_TS_Group = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // Screens_TS_Group
            // 
            Screens_TS_Group.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Parser_TS,
            this.Map_TS});
            Screens_TS_Group.Name = "Screens_TS_Group";
            Screens_TS_Group.Size = new System.Drawing.Size(47, 20);
            Screens_TS_Group.Text = "Окна";
            // 
            // Parser_TS
            // 
            this.Parser_TS.Name = "Parser_TS";
            this.Parser_TS.Size = new System.Drawing.Size(149, 22);
            this.Parser_TS.Text = "Визуализатор";
            this.Parser_TS.Click += new System.EventHandler(this.Parser_TS_Click);
            // 
            // Map_TS
            // 
            this.Map_TS.Name = "Map_TS";
            this.Map_TS.Size = new System.Drawing.Size(149, 22);
            this.Map_TS.Text = "Карта";
            this.Map_TS.Click += new System.EventHandler(this.Map_TS_Click);
            // 
            // View_TS_Group
            // 
            View_TS_Group.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewCascade_TS,
            this.ViewRow_TS,
            this.ViewStack_TS});
            View_TS_Group.Name = "View_TS_Group";
            View_TS_Group.Size = new System.Drawing.Size(39, 20);
            View_TS_Group.Text = "Вид";
            // 
            // ViewCascade_TS
            // 
            this.ViewCascade_TS.Name = "ViewCascade_TS";
            this.ViewCascade_TS.Size = new System.Drawing.Size(113, 22);
            this.ViewCascade_TS.Text = "Каскад";
            this.ViewCascade_TS.Click += new System.EventHandler(this.ViewCascade_TS_Click);
            // 
            // ViewRow_TS
            // 
            this.ViewRow_TS.Name = "ViewRow_TS";
            this.ViewRow_TS.Size = new System.Drawing.Size(113, 22);
            this.ViewRow_TS.Text = "Ряд";
            this.ViewRow_TS.Click += new System.EventHandler(this.ViewRow_TS_Click);
            // 
            // ViewStack_TS
            // 
            this.ViewStack_TS.Name = "ViewStack_TS";
            this.ViewStack_TS.Size = new System.Drawing.Size(113, 22);
            this.ViewStack_TS.Text = "Стопка";
            this.ViewStack_TS.Click += new System.EventHandler(this.ViewStack_TS_Click);
            // 
            // Charts_TS_Group
            // 
            this.Charts_TS_Group.Name = "Charts_TS_Group";
            this.Charts_TS_Group.Size = new System.Drawing.Size(67, 20);
            this.Charts_TS_Group.Text = "Графики";
            // 
            // Texts_TS_Group
            // 
            this.Texts_TS_Group.Name = "Texts_TS_Group";
            this.Texts_TS_Group.Size = new System.Drawing.Size(48, 20);
            this.Texts_TS_Group.Text = "Текст";
            // 
            // MenuStrip
            // 
            this.MenuStrip.AllowMerge = false;
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Screens_TS_Group,
            this.Charts_TS_Group,
            this.Texts_TS_Group,
            View_TS_Group});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(531, 24);
            this.MenuStrip.TabIndex = 1;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 326);
            this.Controls.Add(this.MenuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MenuStrip;
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem Parser_TS;
        private System.Windows.Forms.ToolStripMenuItem Map_TS;
        private System.Windows.Forms.ToolStripMenuItem ViewCascade_TS;
        private System.Windows.Forms.ToolStripMenuItem ViewRow_TS;
        private System.Windows.Forms.ToolStripMenuItem ViewStack_TS;
        private System.Windows.Forms.ToolStripMenuItem Charts_TS_Group;
        private System.Windows.Forms.ToolStripMenuItem Texts_TS_Group;
    }
}