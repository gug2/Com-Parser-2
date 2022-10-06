
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
            this.serialSettingsGroup = new System.Windows.Forms.GroupBox();
            this.serialRxCount = new System.Windows.Forms.Label();
            this.serialRxCountLabel = new System.Windows.Forms.Label();
            this.serialStatus = new System.Windows.Forms.Label();
            this.connectToSerial = new System.Windows.Forms.Button();
            this.serialSpeedsList = new System.Windows.Forms.ComboBox();
            this.serialNamesList = new System.Windows.Forms.ComboBox();
            this.serialDataGroup = new System.Windows.Forms.GroupBox();
            this.delimiterLabel = new System.Windows.Forms.Label();
            this.saveDelimiterValue = new System.Windows.Forms.Button();
            this.delimiterValue = new System.Windows.Forms.TextBox();
            this.statusGroup = new System.Windows.Forms.GroupBox();
            this.status = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.последовательныеПортыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обновитьF5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.раширенныеНастройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.графикF1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialDataArea = new System.Windows.Forms.RichTextBox();
            this.serialSettingsGroup.SuspendLayout();
            this.serialDataGroup.SuspendLayout();
            this.statusGroup.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialSettingsGroup
            // 
            this.serialSettingsGroup.Controls.Add(this.serialRxCount);
            this.serialSettingsGroup.Controls.Add(this.serialRxCountLabel);
            this.serialSettingsGroup.Controls.Add(this.serialStatus);
            this.serialSettingsGroup.Controls.Add(this.connectToSerial);
            this.serialSettingsGroup.Controls.Add(this.serialSpeedsList);
            this.serialSettingsGroup.Controls.Add(this.serialNamesList);
            this.serialSettingsGroup.Location = new System.Drawing.Point(12, 27);
            this.serialSettingsGroup.Name = "serialSettingsGroup";
            this.serialSettingsGroup.Size = new System.Drawing.Size(456, 52);
            this.serialSettingsGroup.TabIndex = 0;
            this.serialSettingsGroup.TabStop = false;
            this.serialSettingsGroup.Text = "Настройки последовательного порта";
            // 
            // serialRxCount
            // 
            this.serialRxCount.AutoSize = true;
            this.serialRxCount.ForeColor = System.Drawing.Color.Green;
            this.serialRxCount.Location = new System.Drawing.Point(374, 23);
            this.serialRxCount.Name = "serialRxCount";
            this.serialRxCount.Size = new System.Drawing.Size(13, 13);
            this.serialRxCount.TabIndex = 5;
            this.serialRxCount.Text = "0";
            // 
            // serialRxCountLabel
            // 
            this.serialRxCountLabel.AutoSize = true;
            this.serialRxCountLabel.ForeColor = System.Drawing.Color.Green;
            this.serialRxCountLabel.Location = new System.Drawing.Point(352, 23);
            this.serialRxCountLabel.Name = "serialRxCountLabel";
            this.serialRxCountLabel.Size = new System.Drawing.Size(25, 13);
            this.serialRxCountLabel.TabIndex = 4;
            this.serialRxCountLabel.Text = "RX:";
            // 
            // serialStatus
            // 
            this.serialStatus.AutoSize = true;
            this.serialStatus.ForeColor = System.Drawing.Color.Red;
            this.serialStatus.Location = new System.Drawing.Point(269, 22);
            this.serialStatus.Name = "serialStatus";
            this.serialStatus.Size = new System.Drawing.Size(63, 13);
            this.serialStatus.TabIndex = 3;
            this.serialStatus.Text = "Отключено";
            // 
            // connectToSerial
            // 
            this.connectToSerial.Location = new System.Drawing.Point(170, 18);
            this.connectToSerial.Name = "connectToSerial";
            this.connectToSerial.Size = new System.Drawing.Size(75, 23);
            this.connectToSerial.TabIndex = 2;
            this.connectToSerial.Text = "Подкл.";
            this.connectToSerial.UseVisualStyleBackColor = true;
            this.connectToSerial.Click += new System.EventHandler(this.connectToSerial_Click);
            // 
            // serialSpeedsList
            // 
            this.serialSpeedsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serialSpeedsList.FormattingEnabled = true;
            this.serialSpeedsList.Location = new System.Drawing.Point(88, 19);
            this.serialSpeedsList.Name = "serialSpeedsList";
            this.serialSpeedsList.Size = new System.Drawing.Size(76, 21);
            this.serialSpeedsList.TabIndex = 1;
            // 
            // serialNamesList
            // 
            this.serialNamesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serialNamesList.FormattingEnabled = true;
            this.serialNamesList.Location = new System.Drawing.Point(6, 19);
            this.serialNamesList.Name = "serialNamesList";
            this.serialNamesList.Size = new System.Drawing.Size(76, 21);
            this.serialNamesList.TabIndex = 0;
            // 
            // serialDataGroup
            // 
            this.serialDataGroup.Controls.Add(this.delimiterLabel);
            this.serialDataGroup.Controls.Add(this.saveDelimiterValue);
            this.serialDataGroup.Controls.Add(this.delimiterValue);
            this.serialDataGroup.Location = new System.Drawing.Point(12, 85);
            this.serialDataGroup.Name = "serialDataGroup";
            this.serialDataGroup.Size = new System.Drawing.Size(456, 58);
            this.serialDataGroup.TabIndex = 1;
            this.serialDataGroup.TabStop = false;
            this.serialDataGroup.Text = "Импорт данных";
            // 
            // delimiterLabel
            // 
            this.delimiterLabel.AutoSize = true;
            this.delimiterLabel.Location = new System.Drawing.Point(9, 22);
            this.delimiterLabel.Name = "delimiterLabel";
            this.delimiterLabel.Size = new System.Drawing.Size(73, 13);
            this.delimiterLabel.TabIndex = 3;
            this.delimiterLabel.Text = "Разделитель";
            // 
            // saveDelimiterValue
            // 
            this.saveDelimiterValue.Enabled = false;
            this.saveDelimiterValue.Location = new System.Drawing.Point(257, 17);
            this.saveDelimiterValue.Name = "saveDelimiterValue";
            this.saveDelimiterValue.Size = new System.Drawing.Size(75, 23);
            this.saveDelimiterValue.TabIndex = 2;
            this.saveDelimiterValue.Text = "Сохранить";
            this.saveDelimiterValue.UseVisualStyleBackColor = true;
            // 
            // delimiterValue
            // 
            this.delimiterValue.Location = new System.Drawing.Point(88, 19);
            this.delimiterValue.Name = "delimiterValue";
            this.delimiterValue.Size = new System.Drawing.Size(157, 20);
            this.delimiterValue.TabIndex = 0;
            // 
            // statusGroup
            // 
            this.statusGroup.AutoSize = true;
            this.statusGroup.Controls.Add(this.serialDataArea);
            this.statusGroup.Controls.Add(this.status);
            this.statusGroup.Location = new System.Drawing.Point(12, 215);
            this.statusGroup.Name = "statusGroup";
            this.statusGroup.Size = new System.Drawing.Size(482, 266);
            this.statusGroup.TabIndex = 2;
            this.statusGroup.TabStop = false;
            this.statusGroup.Text = "Статус";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.ForeColor = System.Drawing.Color.Green;
            this.status.Location = new System.Drawing.Point(7, 20);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(67, 13);
            this.status.TabIndex = 0;
            this.status.Text = "Нет ошибок";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.последовательныеПортыToolStripMenuItem,
            this.видToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(494, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip1";
            // 
            // последовательныеПортыToolStripMenuItem
            // 
            this.последовательныеПортыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обновитьF5ToolStripMenuItem,
            this.раширенныеНастройкиToolStripMenuItem});
            this.последовательныеПортыToolStripMenuItem.Name = "последовательныеПортыToolStripMenuItem";
            this.последовательныеПортыToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.последовательныеПортыToolStripMenuItem.Size = new System.Drawing.Size(163, 20);
            this.последовательныеПортыToolStripMenuItem.Text = "Последовательные порты";
            // 
            // обновитьF5ToolStripMenuItem
            // 
            this.обновитьF5ToolStripMenuItem.Name = "обновитьF5ToolStripMenuItem";
            this.обновитьF5ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.обновитьF5ToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.обновитьF5ToolStripMenuItem.Text = "Обновить";
            this.обновитьF5ToolStripMenuItem.Click += new System.EventHandler(this.обновитьF5ToolStripMenuItem_Click);
            // 
            // раширенныеНастройкиToolStripMenuItem
            // 
            this.раширенныеНастройкиToolStripMenuItem.Name = "раширенныеНастройкиToolStripMenuItem";
            this.раширенныеНастройкиToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.раширенныеНастройкиToolStripMenuItem.Text = "Раширенные настройки";
            this.раширенныеНастройкиToolStripMenuItem.Click += new System.EventHandler(this.раширенныеНастройкиToolStripMenuItem_Click);
            // 
            // видToolStripMenuItem
            // 
            this.видToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.графикF1ToolStripMenuItem});
            this.видToolStripMenuItem.Name = "видToolStripMenuItem";
            this.видToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.видToolStripMenuItem.Text = "Вид";
            // 
            // графикF1ToolStripMenuItem
            // 
            this.графикF1ToolStripMenuItem.Name = "графикF1ToolStripMenuItem";
            this.графикF1ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.графикF1ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.графикF1ToolStripMenuItem.Text = "График";
            // 
            // serialDataArea
            // 
            this.serialDataArea.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.serialDataArea.Location = new System.Drawing.Point(6, 76);
            this.serialDataArea.Name = "serialDataArea";
            this.serialDataArea.ReadOnly = true;
            this.serialDataArea.Size = new System.Drawing.Size(432, 171);
            this.serialDataArea.TabIndex = 1;
            this.serialDataArea.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 309);
            this.Controls.Add(this.statusGroup);
            this.Controls.Add(this.serialDataGroup);
            this.Controls.Add(this.serialSettingsGroup);
            this.Controls.Add(this.menuStrip);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Com-Parser-2";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.serialSettingsGroup.ResumeLayout(false);
            this.serialSettingsGroup.PerformLayout();
            this.serialDataGroup.ResumeLayout(false);
            this.serialDataGroup.PerformLayout();
            this.statusGroup.ResumeLayout(false);
            this.statusGroup.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox serialSettingsGroup;
        private System.Windows.Forms.Label serialStatus;
        private System.Windows.Forms.Button connectToSerial;
        private System.Windows.Forms.ComboBox serialSpeedsList;
        private System.Windows.Forms.ComboBox serialNamesList;
        private System.Windows.Forms.GroupBox serialDataGroup;
        private System.Windows.Forms.Button saveDelimiterValue;
        private System.Windows.Forms.TextBox delimiterValue;
        private System.Windows.Forms.GroupBox statusGroup;
        private System.Windows.Forms.Label serialRxCount;
        private System.Windows.Forms.Label serialRxCountLabel;
        private System.Windows.Forms.Label delimiterLabel;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem последовательныеПортыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обновитьF5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem видToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem графикF1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem раширенныеНастройкиToolStripMenuItem;
        private System.Windows.Forms.RichTextBox serialDataArea;
    }
}

