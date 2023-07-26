
namespace Com_Parser_2_client
{
    partial class ParserForm
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
            System.Windows.Forms.ToolStripMenuItem Datas_TS;
            System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
            System.Windows.Forms.GroupBox ParsedFileGroup;
            System.Windows.Forms.GroupBox RemoteNodeGroup;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            this.Format_TS = new System.Windows.Forms.ToolStripMenuItem();
            this.Parse = new System.Windows.Forms.Button();
            this.ParsingFilePath = new Com_Parser_2_client.LabelFormat();
            this.BrowseFileForParse = new System.Windows.Forms.Button();
            this.ipTextBox1 = new Com_Parser_2_client.IPTextBox();
            this.NetRxCount = new Com_Parser_2_client.LabelFormat();
            this.ConnectionStatus = new System.Windows.Forms.Label();
            this.ConnectToRemote = new System.Windows.Forms.Button();
            this.RemotePort = new System.Windows.Forms.MaskedTextBox();
            this.ParseProgress = new System.Windows.Forms.ProgressBar();
            this.StatusBar = new System.Windows.Forms.Panel();
            this.StatusValue = new System.Windows.Forms.Label();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileParseWorker = new System.ComponentModel.BackgroundWorker();
            this.NetWorker = new System.ComponentModel.BackgroundWorker();
            this.NetParseWorker = new System.ComponentModel.BackgroundWorker();
            Datas_TS = new System.Windows.Forms.ToolStripMenuItem();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ParsedFileGroup = new System.Windows.Forms.GroupBox();
            RemoteNodeGroup = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            flowLayoutPanel1.SuspendLayout();
            ParsedFileGroup.SuspendLayout();
            RemoteNodeGroup.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
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
            flowLayoutPanel1.Controls.Add(ParsedFileGroup);
            flowLayoutPanel1.Controls.Add(RemoteNodeGroup);
            flowLayoutPanel1.Controls.Add(this.ParseProgress);
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
            ParsedFileGroup.Controls.Add(this.Parse);
            ParsedFileGroup.Controls.Add(this.ParsingFilePath);
            ParsedFileGroup.Controls.Add(this.BrowseFileForParse);
            ParsedFileGroup.Location = new System.Drawing.Point(3, 3);
            ParsedFileGroup.Name = "ParsedFileGroup";
            ParsedFileGroup.Size = new System.Drawing.Size(506, 100);
            ParsedFileGroup.TabIndex = 6;
            ParsedFileGroup.TabStop = false;
            ParsedFileGroup.Text = "Данные из файла";
            // 
            // Parse
            // 
            this.Parse.Enabled = false;
            this.Parse.Location = new System.Drawing.Point(91, 20);
            this.Parse.Name = "Parse";
            this.Parse.Size = new System.Drawing.Size(75, 23);
            this.Parse.TabIndex = 2;
            this.Parse.Text = "Старт";
            this.Parse.UseVisualStyleBackColor = true;
            this.Parse.Click += new System.EventHandler(this.Parse_Click);
            // 
            // ParsingFilePath
            // 
            this.ParsingFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ParsingFilePath.AutoEllipsis = true;
            this.ParsingFilePath.Format = "Путь: {0}";
            this.ParsingFilePath.Location = new System.Drawing.Point(10, 46);
            this.ParsingFilePath.Name = "ParsingFilePath";
            this.ParsingFilePath.Size = new System.Drawing.Size(490, 51);
            this.ParsingFilePath.TabIndex = 1;
            this.ParsingFilePath.Text = "Путь:";
            // 
            // BrowseFileForParse
            // 
            this.BrowseFileForParse.Location = new System.Drawing.Point(10, 20);
            this.BrowseFileForParse.Name = "BrowseFileForParse";
            this.BrowseFileForParse.Size = new System.Drawing.Size(75, 23);
            this.BrowseFileForParse.TabIndex = 0;
            this.BrowseFileForParse.Text = "Выбрать";
            this.BrowseFileForParse.UseVisualStyleBackColor = true;
            this.BrowseFileForParse.Click += new System.EventHandler(this.BrowseFileForParse_Click);
            // 
            // RemoteNodeGroup
            // 
            RemoteNodeGroup.Controls.Add(this.ipTextBox1);
            RemoteNodeGroup.Controls.Add(label2);
            RemoteNodeGroup.Controls.Add(label1);
            RemoteNodeGroup.Controls.Add(this.NetRxCount);
            RemoteNodeGroup.Controls.Add(this.ConnectionStatus);
            RemoteNodeGroup.Controls.Add(this.ConnectToRemote);
            RemoteNodeGroup.Controls.Add(this.RemotePort);
            RemoteNodeGroup.Location = new System.Drawing.Point(3, 109);
            RemoteNodeGroup.Name = "RemoteNodeGroup";
            RemoteNodeGroup.Size = new System.Drawing.Size(506, 84);
            RemoteNodeGroup.TabIndex = 7;
            RemoteNodeGroup.TabStop = false;
            RemoteNodeGroup.Text = "Удаленный узел";
            // 
            // ipTextBox1
            // 
            this.ipTextBox1.Location = new System.Drawing.Point(54, 22);
            this.ipTextBox1.Name = "ipTextBox1";
            this.ipTextBox1.Size = new System.Drawing.Size(143, 20);
            this.ipTextBox1.TabIndex = 3;
            this.ipTextBox1.Value = "127.0.0.1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = System.Drawing.SystemColors.ControlText;
            label2.Location = new System.Drawing.Point(203, 25);
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
            this.RemotePort.Location = new System.Drawing.Point(241, 22);
            this.RemotePort.Mask = "00000";
            this.RemotePort.Name = "RemotePort";
            this.RemotePort.Size = new System.Drawing.Size(50, 20);
            this.RemotePort.TabIndex = 1;
            this.RemotePort.Text = "65000";
            this.RemotePort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ParseProgress
            // 
            this.ParseProgress.Location = new System.Drawing.Point(3, 199);
            this.ParseProgress.Name = "ParseProgress";
            this.ParseProgress.Size = new System.Drawing.Size(506, 23);
            this.ParseProgress.Step = 1;
            this.ParseProgress.TabIndex = 8;
            // 
            // StatusBar
            // 
            this.StatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusBar.Controls.Add(this.StatusValue);
            this.StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StatusBar.Location = new System.Drawing.Point(0, 286);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Padding = new System.Windows.Forms.Padding(3);
            this.StatusBar.Size = new System.Drawing.Size(531, 40);
            this.StatusBar.TabIndex = 1;
            // 
            // StatusValue
            // 
            this.StatusValue.AutoEllipsis = true;
            this.StatusValue.AutoSize = true;
            this.StatusValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusValue.ForeColor = System.Drawing.Color.Green;
            this.StatusValue.Location = new System.Drawing.Point(3, 3);
            this.StatusValue.Margin = new System.Windows.Forms.Padding(3);
            this.StatusValue.MaximumSize = new System.Drawing.Size(523, 0);
            this.StatusValue.Name = "StatusValue";
            this.StatusValue.Size = new System.Drawing.Size(67, 13);
            this.StatusValue.TabIndex = 0;
            this.StatusValue.Text = "Нет ошибок";
            // 
            // MenuStrip
            // 
            this.MenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Datas_TS});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.MenuStrip.Size = new System.Drawing.Size(531, 24);
            this.MenuStrip.TabIndex = 3;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // FileParseWorker
            // 
            this.FileParseWorker.WorkerReportsProgress = true;
            // 
            // NetWorker
            // 
            this.NetWorker.WorkerSupportsCancellation = true;
            // 
            // ParserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 326);
            this.Controls.Add(flowLayoutPanel1);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.StatusBar);
            this.Name = "ParserForm";
            this.Text = "Client terminal (Com-Parser-2)";
            this.Load += new System.EventHandler(this.ParserForm_Load);
            flowLayoutPanel1.ResumeLayout(false);
            ParsedFileGroup.ResumeLayout(false);
            RemoteNodeGroup.ResumeLayout(false);
            RemoteNodeGroup.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label StatusValue;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem Format_TS;
        private LabelFormat ParsingFilePath;
        private System.Windows.Forms.Button BrowseFileForParse;
        private LabelFormat NetRxCount;
        private System.Windows.Forms.Label ConnectionStatus;
        private System.Windows.Forms.Button ConnectToRemote;
        private System.Windows.Forms.MaskedTextBox RemotePort;
        private System.Windows.Forms.ProgressBar ParseProgress;
        private System.Windows.Forms.Button Parse;
        private System.ComponentModel.BackgroundWorker FileParseWorker;
        private System.Windows.Forms.Panel StatusBar;
        private IPTextBox ipTextBox1;
        private System.ComponentModel.BackgroundWorker NetWorker;
        private System.ComponentModel.BackgroundWorker NetParseWorker;
    }
}

