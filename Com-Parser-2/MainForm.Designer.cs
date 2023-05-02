
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
            System.Windows.Forms.GroupBox serialSettingsGroup;
            System.Windows.Forms.Label SerialRxCountLabel;
            System.Windows.Forms.ToolStripMenuItem SerialPort_TS_Group;
            System.Windows.Forms.Label MessagesPerSecondLabel;
            System.Windows.Forms.ToolStripMenuItem Data_TS_Group;
            this.SerialRxCount = new System.Windows.Forms.Label();
            this.SerialStatus = new System.Windows.Forms.Label();
            this.ConnectToSerial = new System.Windows.Forms.Button();
            this.SerialSpeeds = new System.Windows.Forms.ComboBox();
            this.SerialNames = new System.Windows.Forms.ComboBox();
            this.UpdatePorts_TS = new System.Windows.Forms.ToolStripMenuItem();
            this.PortExtendedSettings_TS = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportPacketFormat_TS = new System.Windows.Forms.ToolStripMenuItem();
            this.Status = new System.Windows.Forms.Label();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.MessagesPerSecond = new System.Windows.Forms.Label();
            this.MessagesPerSecondTimer = new System.Windows.Forms.Timer(this.components);
            this.serialOutGroup = new System.Windows.Forms.GroupBox();
            this.ConnectedClients = new System.Windows.Forms.CheckedListBox();
            this.StatusBar = new System.Windows.Forms.Panel();
            serialSettingsGroup = new System.Windows.Forms.GroupBox();
            SerialRxCountLabel = new System.Windows.Forms.Label();
            SerialPort_TS_Group = new System.Windows.Forms.ToolStripMenuItem();
            MessagesPerSecondLabel = new System.Windows.Forms.Label();
            Data_TS_Group = new System.Windows.Forms.ToolStripMenuItem();
            serialSettingsGroup.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            this.serialOutGroup.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialSettingsGroup
            // 
            serialSettingsGroup.Controls.Add(this.SerialRxCount);
            serialSettingsGroup.Controls.Add(SerialRxCountLabel);
            serialSettingsGroup.Controls.Add(this.SerialStatus);
            serialSettingsGroup.Controls.Add(this.ConnectToSerial);
            serialSettingsGroup.Controls.Add(this.SerialSpeeds);
            serialSettingsGroup.Controls.Add(this.SerialNames);
            serialSettingsGroup.Location = new System.Drawing.Point(12, 27);
            serialSettingsGroup.Name = "serialSettingsGroup";
            serialSettingsGroup.Size = new System.Drawing.Size(456, 52);
            serialSettingsGroup.TabIndex = 0;
            serialSettingsGroup.TabStop = false;
            serialSettingsGroup.Text = "Настройки последовательного порта";
            // 
            // SerialRxCount
            // 
            this.SerialRxCount.AutoSize = true;
            this.SerialRxCount.ForeColor = System.Drawing.Color.Green;
            this.SerialRxCount.Location = new System.Drawing.Point(374, 23);
            this.SerialRxCount.Name = "SerialRxCount";
            this.SerialRxCount.Size = new System.Drawing.Size(13, 13);
            this.SerialRxCount.TabIndex = 5;
            this.SerialRxCount.Text = "0";
            // 
            // SerialRxCountLabel
            // 
            SerialRxCountLabel.AutoSize = true;
            SerialRxCountLabel.ForeColor = System.Drawing.Color.Green;
            SerialRxCountLabel.Location = new System.Drawing.Point(352, 23);
            SerialRxCountLabel.Name = "SerialRxCountLabel";
            SerialRxCountLabel.Size = new System.Drawing.Size(25, 13);
            SerialRxCountLabel.TabIndex = 4;
            SerialRxCountLabel.Text = "RX:";
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
            this.UpdatePorts_TS,
            this.PortExtendedSettings_TS});
            SerialPort_TS_Group.Name = "SerialPort_TS_Group";
            SerialPort_TS_Group.ShortcutKeyDisplayString = "";
            SerialPort_TS_Group.Size = new System.Drawing.Size(163, 20);
            SerialPort_TS_Group.Text = "Последовательные порты";
            // 
            // UpdatePorts_TS
            // 
            this.UpdatePorts_TS.Name = "UpdatePorts_TS";
            this.UpdatePorts_TS.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.UpdatePorts_TS.Size = new System.Drawing.Size(208, 22);
            this.UpdatePorts_TS.Text = "Обновить";
            this.UpdatePorts_TS.Click += new System.EventHandler(this.UpdatePorts_TS_Click);
            // 
            // PortExtendedSettings_TS
            // 
            this.PortExtendedSettings_TS.Name = "PortExtendedSettings_TS";
            this.PortExtendedSettings_TS.Size = new System.Drawing.Size(208, 22);
            this.PortExtendedSettings_TS.Text = "Раширенные настройки";
            this.PortExtendedSettings_TS.Click += new System.EventHandler(this.PortExtendedSettings_TS_Click);
            // 
            // MessagesPerSecondLabel
            // 
            MessagesPerSecondLabel.AutoSize = true;
            MessagesPerSecondLabel.Location = new System.Drawing.Point(8, 10);
            MessagesPerSecondLabel.Name = "MessagesPerSecondLabel";
            MessagesPerSecondLabel.Size = new System.Drawing.Size(76, 13);
            MessagesPerSecondLabel.TabIndex = 4;
            MessagesPerSecondLabel.Text = "messages/sec";
            // 
            // Data_TS_Group
            // 
            Data_TS_Group.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportPacketFormat_TS});
            Data_TS_Group.Name = "Data_TS_Group";
            Data_TS_Group.Size = new System.Drawing.Size(62, 20);
            Data_TS_Group.Text = "Данные";
            // 
            // ImportPacketFormat_TS
            // 
            this.ImportPacketFormat_TS.Name = "ImportPacketFormat_TS";
            this.ImportPacketFormat_TS.Size = new System.Drawing.Size(180, 22);
            this.ImportPacketFormat_TS.Text = "Импорт формата";
            this.ImportPacketFormat_TS.Click += new System.EventHandler(this.ImportPacketFormat_TS_Click);
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.ForeColor = System.Drawing.Color.Green;
            this.Status.Location = new System.Drawing.Point(8, 32);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(67, 13);
            this.Status.TabIndex = 0;
            this.Status.Text = "Нет ошибок";
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            SerialPort_TS_Group,
            Data_TS_Group});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(531, 24);
            this.MenuStrip.TabIndex = 3;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // MessagesPerSecond
            // 
            this.MessagesPerSecond.AutoSize = true;
            this.MessagesPerSecond.Location = new System.Drawing.Point(104, 10);
            this.MessagesPerSecond.Name = "MessagesPerSecond";
            this.MessagesPerSecond.Size = new System.Drawing.Size(13, 13);
            this.MessagesPerSecond.TabIndex = 5;
            this.MessagesPerSecond.Text = "0";
            // 
            // MessagesPerSecondTimer
            // 
            this.MessagesPerSecondTimer.Interval = 1000;
            this.MessagesPerSecondTimer.Tick += new System.EventHandler(this.MessagesPerSecondTimer_Tick);
            // 
            // serialOutGroup
            // 
            this.serialOutGroup.AutoSize = true;
            this.serialOutGroup.Controls.Add(this.ConnectedClients);
            this.serialOutGroup.Location = new System.Drawing.Point(12, 85);
            this.serialOutGroup.Name = "serialOutGroup";
            this.serialOutGroup.Size = new System.Drawing.Size(466, 102);
            this.serialOutGroup.TabIndex = 6;
            this.serialOutGroup.TabStop = false;
            this.serialOutGroup.Text = "Подключенные узлы";
            // 
            // ConnectedClients
            // 
            this.ConnectedClients.FormattingEnabled = true;
            this.ConnectedClients.Location = new System.Drawing.Point(10, 19);
            this.ConnectedClients.Name = "ConnectedClients";
            this.ConnectedClients.Size = new System.Drawing.Size(450, 64);
            this.ConnectedClients.TabIndex = 2;
            // 
            // StatusBar
            // 
            this.StatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusBar.Controls.Add(this.Status);
            this.StatusBar.Controls.Add(MessagesPerSecondLabel);
            this.StatusBar.Controls.Add(this.MessagesPerSecond);
            this.StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StatusBar.Location = new System.Drawing.Point(0, 271);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(531, 55);
            this.StatusBar.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 326);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.serialOutGroup);
            this.Controls.Add(serialSettingsGroup);
            this.Controls.Add(this.MenuStrip);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Com-Parser-2";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            serialSettingsGroup.ResumeLayout(false);
            serialSettingsGroup.PerformLayout();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.serialOutGroup.ResumeLayout(false);
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label SerialStatus;
        private System.Windows.Forms.Button ConnectToSerial;
        private System.Windows.Forms.ComboBox SerialSpeeds;
        private System.Windows.Forms.ComboBox SerialNames;
        private System.Windows.Forms.Label SerialRxCount;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem UpdatePorts_TS;
        private System.Windows.Forms.ToolStripMenuItem PortExtendedSettings_TS;
        private System.Windows.Forms.Label MessagesPerSecond;
        private System.Windows.Forms.Timer MessagesPerSecondTimer;
        private System.Windows.Forms.GroupBox serialOutGroup;
        private System.Windows.Forms.CheckedListBox ConnectedClients;
        private System.Windows.Forms.ToolStripMenuItem ImportPacketFormat_TS;
        private System.Windows.Forms.Panel StatusBar;
    }
}

