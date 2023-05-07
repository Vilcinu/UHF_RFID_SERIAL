namespace UHF_RFID_UART
{
    partial class Form1
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
            this.PortControl = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Send_button = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.commandsComboBox = new System.Windows.Forms.ComboBox();
            this.adressBox = new System.Windows.Forms.TextBox();
            this.dataBox = new System.Windows.Forms.TextBox();
            this.dataTextBox = new System.Windows.Forms.TextBox();
            this.continiousCheckBox = new System.Windows.Forms.CheckBox();
            this.addressFieldButton = new System.Windows.Forms.Button();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // PortControl
            // 
            this.PortControl.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.PortControl.Location = new System.Drawing.Point(12, 12);
            this.PortControl.Name = "PortControl";
            this.PortControl.Size = new System.Drawing.Size(75, 21);
            this.PortControl.TabIndex = 1;
            this.PortControl.Text = "Open";
            this.PortControl.UseVisualStyleBackColor = true;
            this.PortControl.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(93, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.comboBox1_MouseClick);
            // 
            // Send_button
            // 
            this.Send_button.Enabled = false;
            this.Send_button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Send_button.Location = new System.Drawing.Point(12, 39);
            this.Send_button.Name = "Send_button";
            this.Send_button.Size = new System.Drawing.Size(75, 21);
            this.Send_button.TabIndex = 2;
            this.Send_button.Text = "Send";
            this.Send_button.UseVisualStyleBackColor = true;
            this.Send_button.Click += new System.EventHandler(this.Send_button_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(221, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(330, 48);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // commandsComboBox
            // 
            this.commandsComboBox.FormattingEnabled = true;
            this.commandsComboBox.Location = new System.Drawing.Point(93, 39);
            this.commandsComboBox.Name = "commandsComboBox";
            this.commandsComboBox.Size = new System.Drawing.Size(121, 21);
            this.commandsComboBox.TabIndex = 4;
            // 
            // adressBox
            // 
            this.adressBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.adressBox.Location = new System.Drawing.Point(12, 66);
            this.adressBox.Name = "adressBox";
            this.adressBox.ReadOnly = true;
            this.adressBox.Size = new System.Drawing.Size(75, 20);
            this.adressBox.TabIndex = 5;
            this.adressBox.Text = "Address";
            this.adressBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dataBox
            // 
            this.dataBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataBox.Location = new System.Drawing.Point(12, 92);
            this.dataBox.Name = "dataBox";
            this.dataBox.ReadOnly = true;
            this.dataBox.Size = new System.Drawing.Size(75, 20);
            this.dataBox.TabIndex = 6;
            this.dataBox.Text = "Data";
            this.dataBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dataTextBox
            // 
            this.dataTextBox.Location = new System.Drawing.Point(93, 92);
            this.dataTextBox.Name = "dataTextBox";
            this.dataTextBox.Size = new System.Drawing.Size(458, 20);
            this.dataTextBox.TabIndex = 8;
            // 
            // continiousCheckBox
            // 
            this.continiousCheckBox.Location = new System.Drawing.Point(430, 65);
            this.continiousCheckBox.Name = "continiousCheckBox";
            this.continiousCheckBox.Size = new System.Drawing.Size(121, 21);
            this.continiousCheckBox.TabIndex = 13;
            this.continiousCheckBox.Text = "Continious";
            this.continiousCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.continiousCheckBox.UseVisualStyleBackColor = true;
            // 
            // addressFieldButton
            // 
            this.addressFieldButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.addressFieldButton.Location = new System.Drawing.Point(220, 66);
            this.addressFieldButton.Name = "addressFieldButton";
            this.addressFieldButton.Size = new System.Drawing.Size(121, 21);
            this.addressFieldButton.TabIndex = 14;
            this.addressFieldButton.Text = "Address";
            this.addressFieldButton.UseVisualStyleBackColor = true;
            this.addressFieldButton.Click += new System.EventHandler(this.addressFieldButton_Click);
            // 
            // addressTextBox
            // 
            this.addressTextBox.Location = new System.Drawing.Point(93, 66);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(121, 20);
            this.addressTextBox.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(564, 121);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.addressFieldButton);
            this.Controls.Add(this.continiousCheckBox);
            this.Controls.Add(this.dataTextBox);
            this.Controls.Add(this.dataBox);
            this.Controls.Add(this.adressBox);
            this.Controls.Add(this.commandsComboBox);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.Send_button);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.PortControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UHF RFID Commander";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PortControl;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button Send_button;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox commandsComboBox;
        private System.Windows.Forms.TextBox adressBox;
        private System.Windows.Forms.TextBox dataBox;
        private System.Windows.Forms.TextBox dataTextBox;
        private System.Windows.Forms.CheckBox continiousCheckBox;
        private System.Windows.Forms.Button addressFieldButton;
        private System.Windows.Forms.TextBox addressTextBox;
    }
}

