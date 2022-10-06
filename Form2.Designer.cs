
namespace Com_Parser_2
{
    partial class ExtendedSerialSettingsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataBitsList = new System.Windows.Forms.ComboBox();
            this.stopBitsList = new System.Windows.Forms.ComboBox();
            this.dataBitsLabel = new System.Windows.Forms.Label();
            this.stopBitLabel = new System.Windows.Forms.Label();
            this.parityLabel = new System.Windows.Forms.Label();
            this.paritiesList = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataBitsList);
            this.groupBox1.Controls.Add(this.stopBitsList);
            this.groupBox1.Controls.Add(this.dataBitsLabel);
            this.groupBox1.Controls.Add(this.stopBitLabel);
            this.groupBox1.Controls.Add(this.parityLabel);
            this.groupBox1.Controls.Add(this.paritiesList);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 70);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dataBitsList
            // 
            this.dataBitsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dataBitsList.FormattingEnabled = true;
            this.dataBitsList.Location = new System.Drawing.Point(125, 32);
            this.dataBitsList.Name = "dataBitsList";
            this.dataBitsList.Size = new System.Drawing.Size(80, 21);
            this.dataBitsList.TabIndex = 6;
            // 
            // stopBitsList
            // 
            this.stopBitsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stopBitsList.FormattingEnabled = true;
            this.stopBitsList.Location = new System.Drawing.Point(225, 32);
            this.stopBitsList.Name = "stopBitsList";
            this.stopBitsList.Size = new System.Drawing.Size(100, 21);
            this.stopBitsList.TabIndex = 5;
            // 
            // dataBitsLabel
            // 
            this.dataBitsLabel.AutoSize = true;
            this.dataBitsLabel.Location = new System.Drawing.Point(125, 16);
            this.dataBitsLabel.Name = "dataBitsLabel";
            this.dataBitsLabel.Size = new System.Drawing.Size(73, 13);
            this.dataBitsLabel.TabIndex = 3;
            this.dataBitsLabel.Text = "Биты данных";
            // 
            // stopBitLabel
            // 
            this.stopBitLabel.AutoSize = true;
            this.stopBitLabel.Location = new System.Drawing.Point(225, 16);
            this.stopBitLabel.Name = "stopBitLabel";
            this.stopBitLabel.Size = new System.Drawing.Size(51, 13);
            this.stopBitLabel.TabIndex = 2;
            this.stopBitLabel.Text = "Стоп бит";
            // 
            // parityLabel
            // 
            this.parityLabel.AutoSize = true;
            this.parityLabel.Location = new System.Drawing.Point(6, 16);
            this.parityLabel.Name = "parityLabel";
            this.parityLabel.Size = new System.Drawing.Size(55, 13);
            this.parityLabel.TabIndex = 1;
            this.parityLabel.Text = "Четность";
            // 
            // paritiesList
            // 
            this.paritiesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paritiesList.FormattingEnabled = true;
            this.paritiesList.Location = new System.Drawing.Point(6, 32);
            this.paritiesList.Name = "paritiesList";
            this.paritiesList.Size = new System.Drawing.Size(100, 21);
            this.paritiesList.TabIndex = 0;
            // 
            // ExtendedSerialSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 95);
            this.Controls.Add(this.groupBox1);
            this.Name = "ExtendedSerialSettingsForm";
            this.Text = "Расширенные настройки последовательного порта";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExtendedSerialSettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.ExtendedSerialSettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox paritiesList;
        private System.Windows.Forms.Label dataBitsLabel;
        private System.Windows.Forms.Label stopBitLabel;
        private System.Windows.Forms.Label parityLabel;
        private System.Windows.Forms.ComboBox stopBitsList;
        private System.Windows.Forms.ComboBox dataBitsList;
    }
}