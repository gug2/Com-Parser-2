
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
            this.RemoteServerGroup = new System.Windows.Forms.GroupBox();
            this.RemotePortValue = new System.Windows.Forms.MaskedTextBox();
            this.RemoteAddressValue = new System.Windows.Forms.MaskedTextBox();
            this.RemoteStatus = new System.Windows.Forms.Label();
            this.ConnectToRemote = new System.Windows.Forms.Button();
            this.RemotePortLabel = new System.Windows.Forms.Label();
            this.RemoteAddressLabel = new System.Windows.Forms.Label();
            this.StatusGroup = new System.Windows.Forms.GroupBox();
            this.StatusValue = new System.Windows.Forms.Label();
            this.RemoteServerGroup.SuspendLayout();
            this.StatusGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // RemoteServerGroup
            // 
            this.RemoteServerGroup.Controls.Add(this.RemotePortValue);
            this.RemoteServerGroup.Controls.Add(this.RemoteAddressValue);
            this.RemoteServerGroup.Controls.Add(this.RemoteStatus);
            this.RemoteServerGroup.Controls.Add(this.ConnectToRemote);
            this.RemoteServerGroup.Controls.Add(this.RemotePortLabel);
            this.RemoteServerGroup.Controls.Add(this.RemoteAddressLabel);
            this.RemoteServerGroup.Location = new System.Drawing.Point(13, 13);
            this.RemoteServerGroup.Name = "RemoteServerGroup";
            this.RemoteServerGroup.Size = new System.Drawing.Size(456, 55);
            this.RemoteServerGroup.TabIndex = 0;
            this.RemoteServerGroup.TabStop = false;
            this.RemoteServerGroup.Text = "Удаленный источник";
            // 
            // RemotePortValue
            // 
            this.RemotePortValue.Location = new System.Drawing.Point(211, 18);
            this.RemotePortValue.Mask = "00000";
            this.RemotePortValue.Name = "RemotePortValue";
            this.RemotePortValue.Size = new System.Drawing.Size(62, 20);
            this.RemotePortValue.TabIndex = 4;
            this.RemotePortValue.TextChanged += new System.EventHandler(this.RemotePortValue_TextChanged);
            // 
            // RemoteAddressValue
            // 
            this.RemoteAddressValue.Location = new System.Drawing.Point(53, 18);
            this.RemoteAddressValue.Mask = "999.999.999.999";
            this.RemoteAddressValue.Name = "RemoteAddressValue";
            this.RemoteAddressValue.Size = new System.Drawing.Size(100, 20);
            this.RemoteAddressValue.TabIndex = 2;
            this.RemoteAddressValue.TextChanged += new System.EventHandler(this.RemoteAddressValue_TextChanged);
            this.RemoteAddressValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RemoteAddressValue_KeyDown);
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
            this.ConnectToRemote.Location = new System.Drawing.Point(279, 16);
            this.ConnectToRemote.Name = "ConnectToRemote";
            this.ConnectToRemote.Size = new System.Drawing.Size(75, 23);
            this.ConnectToRemote.TabIndex = 5;
            this.ConnectToRemote.Text = "Подкл.";
            this.ConnectToRemote.UseVisualStyleBackColor = true;
            this.ConnectToRemote.Click += new System.EventHandler(this.ConnectToRemote_Click);
            // 
            // RemotePortLabel
            // 
            this.RemotePortLabel.AutoSize = true;
            this.RemotePortLabel.Location = new System.Drawing.Point(170, 21);
            this.RemotePortLabel.Name = "RemotePortLabel";
            this.RemotePortLabel.Size = new System.Drawing.Size(35, 13);
            this.RemotePortLabel.TabIndex = 3;
            this.RemotePortLabel.Text = "Порт:";
            // 
            // RemoteAddressLabel
            // 
            this.RemoteAddressLabel.AutoSize = true;
            this.RemoteAddressLabel.Location = new System.Drawing.Point(6, 21);
            this.RemoteAddressLabel.Name = "RemoteAddressLabel";
            this.RemoteAddressLabel.Size = new System.Drawing.Size(41, 13);
            this.RemoteAddressLabel.TabIndex = 1;
            this.RemoteAddressLabel.Text = "Адрес:";
            // 
            // StatusGroup
            // 
            this.StatusGroup.AutoSize = true;
            this.StatusGroup.Controls.Add(this.StatusValue);
            this.StatusGroup.Location = new System.Drawing.Point(13, 74);
            this.StatusGroup.Name = "StatusGroup";
            this.StatusGroup.Size = new System.Drawing.Size(456, 49);
            this.StatusGroup.TabIndex = 0;
            this.StatusGroup.TabStop = false;
            this.StatusGroup.Text = "Статус";
            // 
            // StatusValue
            // 
            this.StatusValue.AutoSize = true;
            this.StatusValue.ForeColor = System.Drawing.Color.Green;
            this.StatusValue.Location = new System.Drawing.Point(7, 20);
            this.StatusValue.Name = "StatusValue";
            this.StatusValue.Size = new System.Drawing.Size(67, 13);
            this.StatusValue.TabIndex = 0;
            this.StatusValue.Text = "Нет ошибок";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(531, 326);
            this.Controls.Add(this.StatusGroup);
            this.Controls.Add(this.RemoteServerGroup);
            this.Name = "ClientForm";
            this.Text = "Client terminal (Com-Parser-2)";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.RemoteServerGroup.ResumeLayout(false);
            this.RemoteServerGroup.PerformLayout();
            this.StatusGroup.ResumeLayout(false);
            this.StatusGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox RemoteServerGroup;
        private System.Windows.Forms.Label RemotePortLabel;
        private System.Windows.Forms.Label RemoteAddressLabel;
        private System.Windows.Forms.Button ConnectToRemote;
        private System.Windows.Forms.GroupBox StatusGroup;
        private System.Windows.Forms.Label StatusValue;
        private System.Windows.Forms.Label RemoteStatus;
        private System.Windows.Forms.MaskedTextBox RemoteAddressValue;
        private System.Windows.Forms.MaskedTextBox RemotePortValue;
    }
}

