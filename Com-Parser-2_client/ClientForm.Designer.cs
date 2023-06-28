﻿
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
            System.Windows.Forms.Panel StatusBar;
            System.Windows.Forms.ToolStripMenuItem Datas_TS;
            System.Windows.Forms.ToolStripMenuItem Font_TS;
            System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
            System.Windows.Forms.GroupBox ParsedFileGroup;
            System.Windows.Forms.GroupBox RemoteNodeGroup;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            this.StatusValue = new System.Windows.Forms.Label();
            this.Format_TS = new System.Windows.Forms.ToolStripMenuItem();
            this.TextLabel_TS = new System.Windows.Forms.ToolStripMenuItem();
            this.TextValue_TS = new System.Windows.Forms.ToolStripMenuItem();
            this.ParseFile = new System.Windows.Forms.Button();
            this.ParsedFilePath = new Com_Parser_2_client.LabelFormat();
            this.BrowseParsedFilePath = new System.Windows.Forms.Button();
            this.NetRxCount = new Com_Parser_2_client.LabelFormat();
            this.ConnectionStatus = new System.Windows.Forms.Label();
            this.ConnectToRemote = new System.Windows.Forms.Button();
            this.RemotePort = new System.Windows.Forms.MaskedTextBox();
            this.RemoteAddress = new Com_Parser_2_client.NullableMaskedTextBox();
            this.ParsedFileProgress = new System.Windows.Forms.ProgressBar();
            this.ChartFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.TextFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.RemoteReceiveWorker = new System.ComponentModel.BackgroundWorker();
            StatusBar = new System.Windows.Forms.Panel();
            Datas_TS = new System.Windows.Forms.ToolStripMenuItem();
            Font_TS = new System.Windows.Forms.ToolStripMenuItem();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ParsedFileGroup = new System.Windows.Forms.GroupBox();
            RemoteNodeGroup = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            StatusBar.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ParsedFileGroup.SuspendLayout();
            RemoteNodeGroup.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusBar
            // 
            StatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            StatusBar.Controls.Add(this.StatusValue);
            StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            StatusBar.Location = new System.Drawing.Point(0, 286);
            StatusBar.Name = "StatusBar";
            StatusBar.Size = new System.Drawing.Size(531, 40);
            StatusBar.TabIndex = 1;
            // 
            // StatusValue
            // 
            this.StatusValue.AutoEllipsis = true;
            this.StatusValue.ForeColor = System.Drawing.Color.Green;
            this.StatusValue.Location = new System.Drawing.Point(3, 5);
            this.StatusValue.Margin = new System.Windows.Forms.Padding(3);
            this.StatusValue.Name = "StatusValue";
            this.StatusValue.Size = new System.Drawing.Size(523, 30);
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
            // Font_TS
            // 
            Font_TS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TextLabel_TS,
            this.TextValue_TS});
            Font_TS.Name = "Font_TS";
            Font_TS.Size = new System.Drawing.Size(58, 20);
            Font_TS.Text = "Шрифт";
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
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            flowLayoutPanel1.Controls.Add(ParsedFileGroup);
            flowLayoutPanel1.Controls.Add(RemoteNodeGroup);
            flowLayoutPanel1.Controls.Add(this.ParsedFileProgress);
            flowLayoutPanel1.Controls.Add(this.ChartFlowPanel);
            flowLayoutPanel1.Controls.Add(this.TextFlowPanel);
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(531, 262);
            flowLayoutPanel1.TabIndex = 5;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.ClientSizeChanged += new System.EventHandler(this.flowLayoutPanel1_ClientSizeChanged);
            // 
            // ParsedFileGroup
            // 
            ParsedFileGroup.Controls.Add(this.ParseFile);
            ParsedFileGroup.Controls.Add(this.ParsedFilePath);
            ParsedFileGroup.Controls.Add(this.BrowseParsedFilePath);
            ParsedFileGroup.Location = new System.Drawing.Point(3, 3);
            ParsedFileGroup.Name = "ParsedFileGroup";
            ParsedFileGroup.Size = new System.Drawing.Size(506, 100);
            ParsedFileGroup.TabIndex = 6;
            ParsedFileGroup.TabStop = false;
            ParsedFileGroup.Text = "Данные из файла";
            ParsedFileGroup.ClientSizeChanged += new System.EventHandler(this.ParsedFileGroup_ClientSizeChanged);
            // 
            // ParseFile
            // 
            this.ParseFile.Enabled = false;
            this.ParseFile.Location = new System.Drawing.Point(91, 20);
            this.ParseFile.Name = "ParseFile";
            this.ParseFile.Size = new System.Drawing.Size(75, 23);
            this.ParseFile.TabIndex = 2;
            this.ParseFile.Text = "Старт";
            this.ParseFile.UseVisualStyleBackColor = true;
            this.ParseFile.Click += new System.EventHandler(this.ParseFile_Click);
            // 
            // ParsedFilePath
            // 
            this.ParsedFilePath.AutoEllipsis = true;
            this.ParsedFilePath.Format = "Путь: {0}";
            this.ParsedFilePath.Location = new System.Drawing.Point(10, 46);
            this.ParsedFilePath.Name = "ParsedFilePath";
            this.ParsedFilePath.Size = new System.Drawing.Size(490, 51);
            this.ParsedFilePath.TabIndex = 1;
            this.ParsedFilePath.Text = "Путь:";
            // 
            // BrowseParsedFilePath
            // 
            this.BrowseParsedFilePath.Location = new System.Drawing.Point(10, 20);
            this.BrowseParsedFilePath.Name = "BrowseParsedFilePath";
            this.BrowseParsedFilePath.Size = new System.Drawing.Size(75, 23);
            this.BrowseParsedFilePath.TabIndex = 0;
            this.BrowseParsedFilePath.Text = "Выбрать";
            this.BrowseParsedFilePath.UseVisualStyleBackColor = true;
            this.BrowseParsedFilePath.Click += new System.EventHandler(this.BrowseParsedFilePath_Click);
            // 
            // RemoteNodeGroup
            // 
            RemoteNodeGroup.Controls.Add(label2);
            RemoteNodeGroup.Controls.Add(label1);
            RemoteNodeGroup.Controls.Add(this.NetRxCount);
            RemoteNodeGroup.Controls.Add(this.ConnectionStatus);
            RemoteNodeGroup.Controls.Add(this.ConnectToRemote);
            RemoteNodeGroup.Controls.Add(this.RemotePort);
            RemoteNodeGroup.Controls.Add(this.RemoteAddress);
            RemoteNodeGroup.Location = new System.Drawing.Point(3, 109);
            RemoteNodeGroup.Name = "RemoteNodeGroup";
            RemoteNodeGroup.Size = new System.Drawing.Size(506, 84);
            RemoteNodeGroup.TabIndex = 7;
            RemoteNodeGroup.TabStop = false;
            RemoteNodeGroup.Text = "Удаленный узел";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = System.Drawing.SystemColors.ControlText;
            label2.Location = new System.Drawing.Point(151, 25);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(32, 13);
            label2.TabIndex = 1;
            label2.Text = "Порт";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = System.Drawing.SystemColors.ControlText;
            label1.Location = new System.Drawing.Point(10, 25);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(38, 13);
            label1.TabIndex = 0;
            label1.Text = "Адрес";
            // 
            // NetRxCount
            // 
            this.NetRxCount.AutoSize = true;
            this.NetRxCount.ForeColor = System.Drawing.Color.Green;
            this.NetRxCount.Format = "RX: {0}/{1}";
            this.NetRxCount.Location = new System.Drawing.Point(194, 60);
            this.NetRxCount.Name = "NetRxCount";
            this.NetRxCount.Size = new System.Drawing.Size(45, 13);
            this.NetRxCount.TabIndex = 2;
            this.NetRxCount.Text = "RX: 0/0";
            // 
            // ConnectionStatus
            // 
            this.ConnectionStatus.AutoSize = true;
            this.ConnectionStatus.ForeColor = System.Drawing.Color.Red;
            this.ConnectionStatus.Location = new System.Drawing.Point(103, 60);
            this.ConnectionStatus.Name = "ConnectionStatus";
            this.ConnectionStatus.Size = new System.Drawing.Size(85, 13);
            this.ConnectionStatus.TabIndex = 2;
            this.ConnectionStatus.Text = "Не подключено";
            // 
            // ConnectToRemote
            // 
            this.ConnectToRemote.Location = new System.Drawing.Point(10, 55);
            this.ConnectToRemote.Name = "ConnectToRemote";
            this.ConnectToRemote.Size = new System.Drawing.Size(87, 23);
            this.ConnectToRemote.TabIndex = 2;
            this.ConnectToRemote.Text = "Подключить";
            this.ConnectToRemote.UseVisualStyleBackColor = true;
            this.ConnectToRemote.Click += new System.EventHandler(this.ConnectToRemote_Click);
            // 
            // RemotePort
            // 
            this.RemotePort.Location = new System.Drawing.Point(189, 22);
            this.RemotePort.Mask = "00000";
            this.RemotePort.Name = "RemotePort";
            this.RemotePort.Size = new System.Drawing.Size(50, 20);
            this.RemotePort.TabIndex = 1;
            this.RemotePort.Text = "65000";
            this.RemotePort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RemotePort.TextChanged += new System.EventHandler(this.RemotePort_TextChanged);
            // 
            // RemoteAddress
            // 
            this.RemoteAddress.Delimiter = '.';
            this.RemoteAddress.Location = new System.Drawing.Point(54, 22);
            this.RemoteAddress.Mask = "000\\.000\\.000\\.000";
            this.RemoteAddress.Name = "RemoteAddress";
            this.RemoteAddress.Size = new System.Drawing.Size(91, 20);
            this.RemoteAddress.TabIndex = 0;
            this.RemoteAddress.Text = "127000000001";
            this.RemoteAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RemoteAddress.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.RemoteAddress.Validated += new System.EventHandler(this.RemoteAddress_Validated);
            // 
            // ParsedFileProgress
            // 
            this.ParsedFileProgress.Location = new System.Drawing.Point(3, 199);
            this.ParsedFileProgress.Name = "ParsedFileProgress";
            this.ParsedFileProgress.Size = new System.Drawing.Size(249, 23);
            this.ParsedFileProgress.Step = 1;
            this.ParsedFileProgress.TabIndex = 8;
            // 
            // ChartFlowPanel
            // 
            this.ChartFlowPanel.AutoSize = true;
            this.ChartFlowPanel.Location = new System.Drawing.Point(3, 228);
            this.ChartFlowPanel.MinimumSize = new System.Drawing.Size(100, 100);
            this.ChartFlowPanel.Name = "ChartFlowPanel";
            this.ChartFlowPanel.Size = new System.Drawing.Size(100, 100);
            this.ChartFlowPanel.TabIndex = 4;
            // 
            // TextFlowPanel
            // 
            this.TextFlowPanel.AutoSize = true;
            this.TextFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.TextFlowPanel.Location = new System.Drawing.Point(3, 334);
            this.TextFlowPanel.MinimumSize = new System.Drawing.Size(100, 100);
            this.TextFlowPanel.Name = "TextFlowPanel";
            this.TextFlowPanel.Size = new System.Drawing.Size(100, 100);
            this.TextFlowPanel.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Datas_TS,
            Font_TS});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(531, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // RemoteReceiveWorker
            // 
            this.RemoteReceiveWorker.WorkerSupportsCancellation = true;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 326);
            this.Controls.Add(flowLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(StatusBar);
            this.Name = "ClientForm";
            this.Text = "Client terminal (Com-Parser-2)";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ClientForm_Layout);
            StatusBar.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ParsedFileGroup.ResumeLayout(false);
            RemoteNodeGroup.ResumeLayout(false);
            RemoteNodeGroup.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label StatusValue;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Format_TS;
        private System.Windows.Forms.ToolStripMenuItem TextLabel_TS;
        private System.Windows.Forms.ToolStripMenuItem TextValue_TS;
        private LabelFormat ParsedFilePath;
        private System.Windows.Forms.Button BrowseParsedFilePath;
        private LabelFormat NetRxCount;
        private System.Windows.Forms.Label ConnectionStatus;
        private System.Windows.Forms.Button ConnectToRemote;
        private System.Windows.Forms.MaskedTextBox RemotePort;
        private NullableMaskedTextBox RemoteAddress;
        private System.Windows.Forms.ProgressBar ParsedFileProgress;
        private System.Windows.Forms.FlowLayoutPanel ChartFlowPanel;
        private System.Windows.Forms.FlowLayoutPanel TextFlowPanel;
        private System.ComponentModel.BackgroundWorker RemoteReceiveWorker;
        private System.Windows.Forms.Button ParseFile;
    }
}

