
namespace Com_Parser_2
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.GroupBox SerialSettingsGroup;
            System.Windows.Forms.Panel panel1;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Panel panel2;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Panel panel3;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.ToolStripMenuItem SerialPort_TS_Group;
            System.Windows.Forms.Panel StatusBar;
            System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
            this.SerialSettingsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SerialParities = new System.Windows.Forms.ComboBox();
            this.SerialDataBits = new System.Windows.Forms.ComboBox();
            this.SerialStopBits = new System.Windows.Forms.ComboBox();
            this.ShowSerialSettings = new System.Windows.Forms.Button();
            this.SerialStatus = new System.Windows.Forms.Label();
            this.ConnectToSerial = new System.Windows.Forms.Button();
            this.SerialSpeeds = new System.Windows.Forms.ComboBox();
            this.SerialNames = new System.Windows.Forms.ComboBox();
            this.UpdatePorts_TS = new System.Windows.Forms.ToolStripMenuItem();
            this.Status = new System.Windows.Forms.Label();
            this.ConnectedClientsGroup = new System.Windows.Forms.GroupBox();
            this.ConnectedClients = new System.Windows.Forms.CheckedListBox();
            this.StartServer = new System.Windows.Forms.Button();
            this.StopServer = new System.Windows.Forms.Button();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.BytesPerSecTimer = new System.Windows.Forms.Timer(this.components);
            this.ServerWorker = new System.ComponentModel.BackgroundWorker();
            this.SerialRxCount = new Com_Parser_2.LabelFormat();
            this.BytesPerSecCount = new Com_Parser_2.LabelFormat();
            SerialSettingsGroup = new System.Windows.Forms.GroupBox();
            panel1 = new System.Windows.Forms.Panel();
            label1 = new System.Windows.Forms.Label();
            panel2 = new System.Windows.Forms.Panel();
            label2 = new System.Windows.Forms.Label();
            panel3 = new System.Windows.Forms.Panel();
            label3 = new System.Windows.Forms.Label();
            SerialPort_TS_Group = new System.Windows.Forms.ToolStripMenuItem();
            StatusBar = new System.Windows.Forms.Panel();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            SerialSettingsGroup.SuspendLayout();
            this.SerialSettingsFlowPanel.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            StatusBar.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            this.ConnectedClientsGroup.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // SerialSettingsGroup
            // 
            SerialSettingsGroup.AutoSize = true;
            SerialSettingsGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            SerialSettingsGroup.Controls.Add(this.BytesPerSecCount);
            SerialSettingsGroup.Controls.Add(this.SerialRxCount);
            SerialSettingsGroup.Controls.Add(this.SerialSettingsFlowPanel);
            SerialSettingsGroup.Controls.Add(this.ShowSerialSettings);
            SerialSettingsGroup.Controls.Add(this.SerialStatus);
            SerialSettingsGroup.Controls.Add(this.ConnectToSerial);
            SerialSettingsGroup.Controls.Add(this.SerialSpeeds);
            SerialSettingsGroup.Controls.Add(this.SerialNames);
            SerialSettingsGroup.Location = new System.Drawing.Point(3, 3);
            SerialSettingsGroup.Name = "SerialSettingsGroup";
            SerialSettingsGroup.Size = new System.Drawing.Size(507, 130);
            SerialSettingsGroup.TabIndex = 0;
            SerialSettingsGroup.TabStop = false;
            SerialSettingsGroup.Text = "Настройки последовательного порта";
            // 
            // SerialSettingsFlowPanel
            // 
            this.SerialSettingsFlowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SerialSettingsFlowPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SerialSettingsFlowPanel.Controls.Add(panel1);
            this.SerialSettingsFlowPanel.Controls.Add(panel2);
            this.SerialSettingsFlowPanel.Controls.Add(panel3);
            this.SerialSettingsFlowPanel.Location = new System.Drawing.Point(6, 49);
            this.SerialSettingsFlowPanel.Name = "SerialSettingsFlowPanel";
            this.SerialSettingsFlowPanel.Size = new System.Drawing.Size(495, 62);
            this.SerialSettingsFlowPanel.TabIndex = 7;
            this.SerialSettingsFlowPanel.Visible = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(this.SerialParities);
            panel1.Controls.Add(label1);
            panel1.Location = new System.Drawing.Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(86, 51);
            panel1.TabIndex = 0;
            // 
            // SerialParities
            // 
            this.SerialParities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SerialParities.FormattingEnabled = true;
            this.SerialParities.Location = new System.Drawing.Point(3, 20);
            this.SerialParities.Name = "SerialParities";
            this.SerialParities.Size = new System.Drawing.Size(76, 21);
            this.SerialParities.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(3, 4);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(55, 13);
            label1.TabIndex = 0;
            label1.Text = "Четность";
            // 
            // panel2
            // 
            panel2.Controls.Add(this.SerialDataBits);
            panel2.Controls.Add(label2);
            panel2.Location = new System.Drawing.Point(95, 3);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(86, 51);
            panel2.TabIndex = 9;
            // 
            // SerialDataBits
            // 
            this.SerialDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SerialDataBits.FormattingEnabled = true;
            this.SerialDataBits.Location = new System.Drawing.Point(3, 20);
            this.SerialDataBits.Name = "SerialDataBits";
            this.SerialDataBits.Size = new System.Drawing.Size(76, 21);
            this.SerialDataBits.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(3, 4);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(73, 13);
            label2.TabIndex = 0;
            label2.Text = "Биты данных";
            // 
            // panel3
            // 
            panel3.Controls.Add(this.SerialStopBits);
            panel3.Controls.Add(label3);
            panel3.Location = new System.Drawing.Point(187, 3);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(86, 51);
            panel3.TabIndex = 9;
            // 
            // SerialStopBits
            // 
            this.SerialStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SerialStopBits.FormattingEnabled = true;
            this.SerialStopBits.Location = new System.Drawing.Point(3, 20);
            this.SerialStopBits.Name = "SerialStopBits";
            this.SerialStopBits.Size = new System.Drawing.Size(76, 21);
            this.SerialStopBits.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(3, 4);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(59, 13);
            label3.TabIndex = 0;
            label3.Text = "Стоп биты";
            // 
            // ShowSerialSettings
            // 
            this.ShowSerialSettings.BackgroundImage = global::Com_Parser_2.Properties.Resources.downArrow;
            this.ShowSerialSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ShowSerialSettings.Location = new System.Drawing.Point(477, 16);
            this.ShowSerialSettings.Name = "ShowSerialSettings";
            this.ShowSerialSettings.Size = new System.Drawing.Size(24, 24);
            this.ShowSerialSettings.TabIndex = 6;
            this.ShowSerialSettings.UseVisualStyleBackColor = true;
            this.ShowSerialSettings.Click += new System.EventHandler(this.ShowSerialSettings_Click);
            // 
            // SerialStatus
            // 
            this.SerialStatus.AutoSize = true;
            this.SerialStatus.ForeColor = System.Drawing.Color.Red;
            this.SerialStatus.Location = new System.Drawing.Point(269, 22);
            this.SerialStatus.Name = "SerialStatus";
            this.SerialStatus.Size = new System.Drawing.Size(63, 13);
            this.SerialStatus.TabIndex = 3;
            this.SerialStatus.Text = "Отключено";
            // 
            // ConnectToSerial
            // 
            this.ConnectToSerial.Location = new System.Drawing.Point(170, 18);
            this.ConnectToSerial.Name = "ConnectToSerial";
            this.ConnectToSerial.Size = new System.Drawing.Size(75, 23);
            this.ConnectToSerial.TabIndex = 2;
            this.ConnectToSerial.Text = "Подкл.";
            this.ConnectToSerial.UseVisualStyleBackColor = true;
            this.ConnectToSerial.Click += new System.EventHandler(this.ConnectToSerial_Click);
            // 
            // SerialSpeeds
            // 
            this.SerialSpeeds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SerialSpeeds.FormattingEnabled = true;
            this.SerialSpeeds.Location = new System.Drawing.Point(88, 19);
            this.SerialSpeeds.Name = "SerialSpeeds";
            this.SerialSpeeds.Size = new System.Drawing.Size(76, 21);
            this.SerialSpeeds.TabIndex = 1;
            // 
            // SerialNames
            // 
            this.SerialNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SerialNames.FormattingEnabled = true;
            this.SerialNames.Location = new System.Drawing.Point(6, 19);
            this.SerialNames.Name = "SerialNames";
            this.SerialNames.Size = new System.Drawing.Size(76, 21);
            this.SerialNames.TabIndex = 0;
            // 
            // SerialPort_TS_Group
            // 
            SerialPort_TS_Group.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UpdatePorts_TS});
            SerialPort_TS_Group.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            SerialPort_TS_Group.Name = "SerialPort_TS_Group";
            SerialPort_TS_Group.ShortcutKeyDisplayString = "";
            SerialPort_TS_Group.Size = new System.Drawing.Size(163, 20);
            SerialPort_TS_Group.Text = "Последовательные порты";
            // 
            // UpdatePorts_TS
            // 
            this.UpdatePorts_TS.Name = "UpdatePorts_TS";
            this.UpdatePorts_TS.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.UpdatePorts_TS.Size = new System.Drawing.Size(147, 22);
            this.UpdatePorts_TS.Text = "Обновить";
            this.UpdatePorts_TS.Click += new System.EventHandler(this.UpdatePorts_TS_Click);
            // 
            // StatusBar
            // 
            StatusBar.AutoScroll = true;
            StatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            StatusBar.Controls.Add(this.Status);
            StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            StatusBar.Location = new System.Drawing.Point(0, 278);
            StatusBar.Name = "StatusBar";
            StatusBar.Size = new System.Drawing.Size(531, 48);
            StatusBar.TabIndex = 7;
            // 
            // Status
            // 
            this.Status.AutoEllipsis = true;
            this.Status.ForeColor = System.Drawing.Color.Green;
            this.Status.Location = new System.Drawing.Point(3, 5);
            this.Status.Margin = new System.Windows.Forms.Padding(3);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(523, 38);
            this.Status.TabIndex = 0;
            this.Status.Text = "Нет ошибок";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Controls.Add(SerialSettingsGroup);
            flowLayoutPanel1.Controls.Add(this.ConnectedClientsGroup);
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(531, 254);
            flowLayoutPanel1.TabIndex = 8;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.ClientSizeChanged += new System.EventHandler(this.flowLayoutPanel1_ClientSizeChanged);
            // 
            // ConnectedClientsGroup
            // 
            this.ConnectedClientsGroup.Controls.Add(this.ConnectedClients);
            this.ConnectedClientsGroup.Controls.Add(this.StartServer);
            this.ConnectedClientsGroup.Controls.Add(this.StopServer);
            this.ConnectedClientsGroup.Location = new System.Drawing.Point(3, 139);
            this.ConnectedClientsGroup.Name = "ConnectedClientsGroup";
            this.ConnectedClientsGroup.Size = new System.Drawing.Size(507, 104);
            this.ConnectedClientsGroup.TabIndex = 6;
            this.ConnectedClientsGroup.TabStop = false;
            this.ConnectedClientsGroup.Text = "Подключенные узлы";
            // 
            // ConnectedClients
            // 
            this.ConnectedClients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectedClients.FormattingEnabled = true;
            this.ConnectedClients.Location = new System.Drawing.Point(9, 48);
            this.ConnectedClients.Name = "ConnectedClients";
            this.ConnectedClients.Size = new System.Drawing.Size(495, 49);
            this.ConnectedClients.TabIndex = 2;
            // 
            // StartServer
            // 
            this.StartServer.Location = new System.Drawing.Point(9, 19);
            this.StartServer.Name = "StartServer";
            this.StartServer.Size = new System.Drawing.Size(96, 23);
            this.StartServer.TabIndex = 8;
            this.StartServer.Text = "Вкл сервер";
            this.StartServer.UseVisualStyleBackColor = true;
            this.StartServer.Click += new System.EventHandler(this.StartServer_Click);
            // 
            // StopServer
            // 
            this.StopServer.Enabled = false;
            this.StopServer.Location = new System.Drawing.Point(111, 19);
            this.StopServer.Name = "StopServer";
            this.StopServer.Size = new System.Drawing.Size(96, 23);
            this.StopServer.TabIndex = 9;
            this.StopServer.Text = "Выкл сервер";
            this.StopServer.UseVisualStyleBackColor = true;
            this.StopServer.Click += new System.EventHandler(this.StopServer_Click);
            // 
            // MenuStrip
            // 
            this.MenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            SerialPort_TS_Group});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.MenuStrip.Size = new System.Drawing.Size(531, 24);
            this.MenuStrip.TabIndex = 3;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // BytesPerSecTimer
            // 
            this.BytesPerSecTimer.Interval = 1000;
            this.BytesPerSecTimer.Tick += new System.EventHandler(this.BytesPerSecTimer_Tick);
            // 
            // ServerWorker
            // 
            this.ServerWorker.WorkerSupportsCancellation = true;
            // 
            // SerialRxCount
            // 
            this.SerialRxCount.AutoSize = true;
            this.SerialRxCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SerialRxCount.ForeColor = System.Drawing.Color.DarkGreen;
            this.SerialRxCount.Format = "RX: {0}";
            this.SerialRxCount.Location = new System.Drawing.Point(338, 23);
            this.SerialRxCount.Name = "SerialRxCount";
            this.SerialRxCount.Size = new System.Drawing.Size(34, 13);
            this.SerialRxCount.TabIndex = 8;
            this.SerialRxCount.Text = "RX: 0";
            // 
            // BytesPerSecCount
            // 
            this.BytesPerSecCount.AutoSize = true;
            this.BytesPerSecCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BytesPerSecCount.ForeColor = System.Drawing.Color.DarkCyan;
            this.BytesPerSecCount.Format = "Bytes/sec: {0}";
            this.BytesPerSecCount.Location = new System.Drawing.Point(390, 22);
            this.BytesPerSecCount.Name = "BytesPerSecCount";
            this.BytesPerSecCount.Size = new System.Drawing.Size(67, 13);
            this.BytesPerSecCount.TabIndex = 9;
            this.BytesPerSecCount.Text = "Bytes/sec: 0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 326);
            this.Controls.Add(flowLayoutPanel1);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(StatusBar);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Com-Parser-2";
            this.Load += new System.EventHandler(this.MainForm_Load);
            SerialSettingsGroup.ResumeLayout(false);
            SerialSettingsGroup.PerformLayout();
            this.SerialSettingsFlowPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            StatusBar.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            this.ConnectedClientsGroup.ResumeLayout(false);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label SerialStatus;
        private System.Windows.Forms.Button ConnectToSerial;
        private System.Windows.Forms.ComboBox SerialSpeeds;
        private System.Windows.Forms.ComboBox SerialNames;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem UpdatePorts_TS;
        private System.Windows.Forms.Timer BytesPerSecTimer;
        private System.Windows.Forms.GroupBox ConnectedClientsGroup;
        private System.Windows.Forms.CheckedListBox ConnectedClients;
        private System.Windows.Forms.Button ShowSerialSettings;
        private System.Windows.Forms.ComboBox SerialParities;
        private System.Windows.Forms.ComboBox SerialDataBits;
        private System.Windows.Forms.ComboBox SerialStopBits;
        private System.Windows.Forms.FlowLayoutPanel SerialSettingsFlowPanel;
        private System.ComponentModel.BackgroundWorker ServerWorker;
        private System.Windows.Forms.Button StartServer;
        private System.Windows.Forms.Button StopServer;
        private LabelFormat SerialRxCount;
        private LabelFormat BytesPerSecCount;
    }
}

