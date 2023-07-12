
namespace Com_Parser_2_client
{
    partial class IPTextBox
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label dot1;
            System.Windows.Forms.Label dot2;
            System.Windows.Forms.Label dot3;
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox3 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox4 = new System.Windows.Forms.MaskedTextBox();
            dot1 = new System.Windows.Forms.Label();
            dot2 = new System.Windows.Forms.Label();
            dot3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dot1
            // 
            dot1.AutoSize = true;
            dot1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dot1.Location = new System.Drawing.Point(26, 0);
            dot1.Margin = new System.Windows.Forms.Padding(0);
            dot1.Name = "dot1";
            dot1.Size = new System.Drawing.Size(13, 20);
            dot1.TabIndex = 0;
            dot1.Text = ".";
            // 
            // dot2
            // 
            dot2.AutoSize = true;
            dot2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dot2.Location = new System.Drawing.Point(65, 0);
            dot2.Margin = new System.Windows.Forms.Padding(0);
            dot2.Name = "dot2";
            dot2.Size = new System.Drawing.Size(13, 20);
            dot2.TabIndex = 1;
            dot2.Text = ".";
            // 
            // dot3
            // 
            dot3.AutoSize = true;
            dot3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dot3.Location = new System.Drawing.Point(104, 0);
            dot3.Margin = new System.Windows.Forms.Padding(0);
            dot3.Name = "dot3";
            dot3.Size = new System.Drawing.Size(13, 20);
            dot3.TabIndex = 2;
            dot3.Text = ".";
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.BeepOnError = true;
            this.maskedTextBox1.Location = new System.Drawing.Point(0, 0);
            this.maskedTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.maskedTextBox1.Mask = "099";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(26, 20);
            this.maskedTextBox1.TabIndex = 0;
            this.maskedTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maskedTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MaskedTextBox_KeyDown);
            this.maskedTextBox1.TextChanged += new System.EventHandler(this.MaskedTextBox_TextChanged);
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.BeepOnError = true;
            this.maskedTextBox2.Location = new System.Drawing.Point(39, 0);
            this.maskedTextBox2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.maskedTextBox2.Mask = "099";
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(26, 20);
            this.maskedTextBox2.TabIndex = 1;
            this.maskedTextBox2.TabStop = false;
            this.maskedTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maskedTextBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MaskedTextBox_KeyDown);
            this.maskedTextBox2.TextChanged += new System.EventHandler(this.MaskedTextBox_TextChanged);
            // 
            // maskedTextBox3
            // 
            this.maskedTextBox3.BeepOnError = true;
            this.maskedTextBox3.Location = new System.Drawing.Point(78, 0);
            this.maskedTextBox3.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.maskedTextBox3.Mask = "099";
            this.maskedTextBox3.Name = "maskedTextBox3";
            this.maskedTextBox3.Size = new System.Drawing.Size(26, 20);
            this.maskedTextBox3.TabIndex = 2;
            this.maskedTextBox3.TabStop = false;
            this.maskedTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maskedTextBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MaskedTextBox_KeyDown);
            this.maskedTextBox3.TextChanged += new System.EventHandler(this.MaskedTextBox_TextChanged);
            // 
            // maskedTextBox4
            // 
            this.maskedTextBox4.BeepOnError = true;
            this.maskedTextBox4.Location = new System.Drawing.Point(117, 0);
            this.maskedTextBox4.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.maskedTextBox4.Mask = "099";
            this.maskedTextBox4.Name = "maskedTextBox4";
            this.maskedTextBox4.Size = new System.Drawing.Size(26, 20);
            this.maskedTextBox4.TabIndex = 3;
            this.maskedTextBox4.TabStop = false;
            this.maskedTextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maskedTextBox4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MaskedTextBox_KeyDown);
            this.maskedTextBox4.TextChanged += new System.EventHandler(this.MaskedTextBox_TextChanged);
            // 
            // IPTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.maskedTextBox4);
            this.Controls.Add(dot3);
            this.Controls.Add(this.maskedTextBox3);
            this.Controls.Add(dot2);
            this.Controls.Add(this.maskedTextBox2);
            this.Controls.Add(this.maskedTextBox1);
            this.Controls.Add(dot1);
            this.Name = "IPTextBox";
            this.Size = new System.Drawing.Size(143, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox3;
        private System.Windows.Forms.MaskedTextBox maskedTextBox4;
    }
}
