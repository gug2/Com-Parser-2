
namespace Com_Parser_2_client
{
    partial class ClientForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox RemoteServerGroup;
            System.Windows.Forms.Label RemotePortLabel;
            System.Windows.Forms.Label RemoteAddressLabel;
            System.Windows.Forms.Panel StatusBar;
            System.Windows.Forms.ToolStripMenuItem Datas_TS;
            System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
            this.RemotePortValue = new System.Windows.Forms.MaskedTextBox();
            this.RemoteStatus = new System.Windows.Forms.Label();
            this.ConnectToRemote = new System.Windows.Forms.Button();
            this.StatusValue = new System.Windows.Forms.Label();
            this.Format_TS = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.RemoteReceiveWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.шрифтToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TextLabel_TS = new System.Windows.Forms.ToolStripMenuItem();
            this.TextValue_TS = new System.Windows.Forms.ToolStripMenuItem();
            this.NetRxCount = new Com_Parser_2_client.LabelFormat();
            this.RemoteAddressValue = new Com_Parser_2_client.NullableMaskedTextBox();
            RemoteServerGroup = new System.Windows.Forms.GroupBox();
            RemotePortLabel = new System.Windows.Forms.Label();
            RemoteAddressLabel = new System.Windows.Forms.Label();
            StatusBar = new System.Windows.Forms.Panel();
            Datas_TS = new System.Windows.Forms.ToolStripMenuItem();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            RemoteServerGroup.SuspendLayout();
            StatusBar.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RemoteServerGroup
            // 
            RemoteServerGroup.Controls.Add(this.NetRxCount);
            RemoteServerGroup.Controls.Add(this.RemotePortValue);
            RemoteServerGroup.Controls.Add(this.RemoteStatus);
            RemoteServerGroup.Controls.Add(this.ConnectToRemote);
            RemoteServerGroup.Controls.Add(RemotePortLabel);
            RemoteServerGroup.Controls.Add(RemoteAddressLabel);
            RemoteServerGroup.Controls.Add(this.RemoteAddressValue);
            RemoteServerGroup.Location = new System.Drawing.Point(3, 3);
            RemoteServerGroup.Name = "RemoteServerGroup";
            RemoteServerGroup.Size = new System.Drawing.Size(506, 55);
            RemoteServerGroup.TabIndex = 0;
            RemoteServerGroup.TabStop = false;
            RemoteServerGroup.Text = "Удаленный узел";
            // 
            // RemotePortValue
            // 
            this.RemotePortValue.HidePromptOnLeave = true;
            this.RemotePortValue.Location = new System.Drawing.Point(190, 18);
            this.RemotePortValue.Mask = "00000";
            this.RemotePortValue.Name = "RemotePortValue";
            this.RemotePortValue.ResetOnPrompt = false;
            this.RemotePortValue.ResetOnSpace = false;
            this.RemotePortValue.Size = new System.Drawing.Size(40, 20);
            this.RemotePortValue.TabIndex = 4;
            this.RemotePortValue.Text = "65000";
            this.RemotePortValue.TextChanged += new System.EventHandler(this.RemotePortValue_TextChanged);
            // 
            // RemoteStatus
            // 
            this.RemoteStatus.AutoSize = true;
            this.RemoteStatus.ForeColor = System.Drawing.Color.Red;
            this.RemoteStatus.Location = new System.Drawing.Point(360, 21);
            this.RemoteStatus.Name = "RemoteStatus";
            this.RemoteStatus.Size = new System.Drawing.Size(89, 13);
            this.RemoteStatus.TabIndex = 6;
            this.RemoteStatus.Text = "Нет соединения";
            // 
            // ConnectToRemote
            // 
            this.ConnectToRemote.Enabled = false;
            this.ConnectToRemote.Location = new System.Drawing.Point(268, 16);
            this.ConnectToRemote.Name = "ConnectToRemote";
            this.ConnectToRemote.Size = new System.Drawing.Size(75, 23);
            this.ConnectToRemote.TabIndex = 5;
            this.ConnectToRemote.Text = "Подкл.";
            this.ConnectToRemote.UseVisualStyleBackColor = true;
            this.ConnectToRemote.Click += new System.EventHandler(this.ConnectToRemote_Click);
            // 
            // RemotePortLabel
            // 
            RemotePortLabel.AutoSize = true;
            RemotePortLabel.Location = new System.Drawing.Point(149, 21);
            RemotePortLabel.Name = "RemotePortLabel";
            RemotePortLabel.Size = new System.Drawing.Size(35, 13);
            RemotePortLabel.TabIndex = 3;
            RemotePortLabel.Text = "Порт:";
            // 
            // RemoteAddressLabel
            // 
            RemoteAddressLabel.AutoSize = true;
            RemoteAddressLabel.Location = new System.Drawing.Point(6, 21);
            RemoteAddressLabel.Name = "RemoteAddressLabel";
            RemoteAddressLabel.Size = new System.Drawing.Size(41, 13);
            RemoteAddressLabel.TabIndex = 1;
            RemoteAddressLabel.Text = "Адрес:";
            // 
            // StatusBar
            // 
            StatusBar.AutoScroll = true;
            StatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            StatusBar.Controls.Add(this.StatusValue);
            StatusBar.Location = new System.Drawing.Point(3, 76);
            StatusBar.Name = "StatusBar";
            StatusBar.Size = new System.Drawing.Size(506, 80);
            StatusBar.TabIndex = 1;
            // 
            // StatusValue
            // 
            this.StatusValue.AutoSize = true;
            this.StatusValue.ForeColor = System.Drawing.Color.Green;
            this.StatusValue.Location = new System.Drawing.Point(3, 10);
            this.StatusValue.Name = "StatusValue";
            this.StatusValue.Size = new System.Drawing.Size(67, 13);
            this.StatusValue.TabIndex = 0;
            this.StatusValue.Text = "Нет ошибок";
            // 
            // Datas_TS
            // 
            Datas_TS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Format_TS});
            Datas_TS.Name = "Datas_TS";
            Datas_TS.Size = new System.Drawing.Size(62, 20);
            Datas_TS.Text = "Данные";
            // 
            // Format_TS
            // 
            this.Format_TS.Name = "Format_TS";
            this.Format_TS.Size = new System.Drawing.Size(117, 22);
            this.Format_TS.Text = "Формат";
            this.Format_TS.Click += new System.EventHandler(this.Format_TS_Click);
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            flowLayoutPanel1.Controls.Add(RemoteServerGroup);
            flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            flowLayoutPanel1.Controls.Add(this.flowLayoutPanel3);
            flowLayoutPanel1.Controls.Add(StatusBar);
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(531, 302);
            flowLayoutPanel1.TabIndex = 4;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.Layout += new System.Windows.Forms.LayoutEventHandler(this.flowLayoutPanel1_Layout);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 64);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 70);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel3.TabIndex = 5;
            // 
            // RemoteReceiveWorker
            // 
            this.RemoteReceiveWorker.WorkerSupportsCancellation = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Datas_TS,
            this.шрифтToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(531, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // шрифтToolStripMenuItem
            // 
            this.шрифтToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TextLabel_TS,
            this.TextValue_TS});
            this.шрифтToolStripMenuItem.Name = "шрифтToolStripMenuItem";
            this.шрифтToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.шрифтToolStripMenuItem.Text = "Шрифт";
            // 
            // TextLabel_TS
            // 
            this.TextLabel_TS.Name = "TextLabel_TS";
            this.TextLabel_TS.Size = new System.Drawing.Size(127, 22);
            this.TextLabel_TS.Text = "Название";
            this.TextLabel_TS.Click += new System.EventHandler(this.TextLabel_TS_Click);
            // 
            // TextValue_TS
            // 
            this.TextValue_TS.Name = "TextValue_TS";
            this.TextValue_TS.Size = new System.Drawing.Size(127, 22);
            this.TextValue_TS.Text = "Значение";
            this.TextValue_TS.Click += new System.EventHandler(this.TextValue_TS_Click);
            // 
            // NetRxCount
            // 
            this.NetRxCount.AutoSize = true;
            this.NetRxCount.ForeColor = System.Drawing.Color.Green;
            this.NetRxCount.Format = "RX: {0}/{1}";
            this.NetRxCount.Location = new System.Drawing.Point(455, 21);
            this.NetRxCount.Name = "NetRxCount";
            this.NetRxCount.Size = new System.Drawing.Size(45, 13);
            this.NetRxCount.TabIndex = 7;
            this.NetRxCount.Text = "RX: 0/0";
            // 
            // RemoteAddressValue
            // 
            this.RemoteAddressValue.Delimiter = '.';
            this.RemoteAddressValue.Location = new System.Drawing.Point(53, 18);
            this.RemoteAddressValue.Mask = "000\\.000\\.000\\.000";
            this.RemoteAddressValue.Name = "RemoteAddressValue";
            this.RemoteAddressValue.ResetOnSpace = false;
            this.RemoteAddressValue.Size = new System.Drawing.Size(90, 20);
            this.RemoteAddressValue.TabIndex = 2;
            this.RemoteAddressValue.Text = "127000000001";
            this.RemoteAddressValue.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.RemoteAddressValue.Validated += new System.EventHandler(this.RemoteAddressValue_Validated);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(531, 326);
            this.Controls.Add(flowLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "ClientForm";
            this.Text = "Client terminal (Com-Parser-2)";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ClientForm_Layout);
            RemoteServerGroup.ResumeLayout(false);
            RemoteServerGroup.PerformLayout();
            StatusBar.ResumeLayout(false);
            StatusBar.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ConnectToRemote;
        private System.Windows.Forms.Label StatusValue;
        private System.Windows.Forms.Label RemoteStatus;
        private System.Windows.Forms.MaskedTextBox RemotePortValue;
        private NullableMaskedTextBox RemoteAddressValue;
        private System.ComponentModel.BackgroundWorker RemoteReceiveWorker;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Format_TS;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private LabelFormat NetRxCount;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.ToolStripMenuItem шрифтToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TextLabel_TS;
        private System.Windows.Forms.ToolStripMenuItem TextValue_TS;
    }
}

