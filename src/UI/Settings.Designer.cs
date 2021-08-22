namespace Pro_Swapper
{
    partial class Settings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.button1 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.paksBox = new System.Windows.Forms.TextBox();
            this.Restart = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.folder = new System.Windows.Forms.PictureBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.AesKeySourceComboBox = new System.Windows.Forms.ComboBox();
            this.manualAES = new System.Windows.Forms.TextBox();
            this.manualAESLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkPing = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.folder)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.Location = new System.Drawing.Point(601, 12);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Century Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(12, 9);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(202, 56);
            this.label13.TabIndex = 89;
            this.label13.Text = "Settings";
            this.label13.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SettingsForm_MouseDown);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.RoyalBlue;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(454, 76);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(148, 31);
            this.button2.TabIndex = 93;
            this.button2.Text = "Open Paks Folder";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(19, 72);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 17);
            this.label1.TabIndex = 92;
            this.label1.Text = "Fortnite Paks Location:";
            // 
            // paksBox
            // 
            this.paksBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.paksBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.paksBox.Location = new System.Drawing.Point(22, 90);
            this.paksBox.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.paksBox.MaxLength = 500;
            this.paksBox.Multiline = true;
            this.paksBox.Name = "paksBox";
            this.paksBox.ReadOnly = true;
            this.paksBox.Size = new System.Drawing.Size(373, 16);
            this.paksBox.TabIndex = 91;
            // 
            // Restart
            // 
            this.Restart.BackColor = System.Drawing.Color.RoyalBlue;
            this.Restart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Restart.FlatAppearance.BorderSize = 0;
            this.Restart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Restart.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Restart.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Restart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Restart.Location = new System.Drawing.Point(22, 118);
            this.Restart.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Restart.Name = "Restart";
            this.Restart.Size = new System.Drawing.Size(189, 47);
            this.Restart.TabIndex = 96;
            this.Restart.Text = "Restart Swapper";
            this.Restart.UseVisualStyleBackColor = false;
            this.Restart.Click += new System.EventHandler(this.Restart_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.RoyalBlue;
            this.button9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button9.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.Location = new System.Drawing.Point(217, 118);
            this.button9.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(189, 47);
            this.button9.TabIndex = 97;
            this.button9.Text = "Launch Fortnite";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.RoyalBlue;
            this.button7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.Location = new System.Drawing.Point(217, 171);
            this.button7.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(189, 47);
            this.button7.TabIndex = 102;
            this.button7.Text = "Verify Fortnite";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.RoyalBlue;
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(22, 223);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(189, 47);
            this.button4.TabIndex = 99;
            this.button4.Text = "Get Converted Items List";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.ConvertedItemsList);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.RoyalBlue;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(22, 171);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(189, 47);
            this.button3.TabIndex = 98;
            this.button3.Text = "Mark items as OFF";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.RoyalBlue;
            this.button10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button10.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button10.Location = new System.Drawing.Point(413, 118);
            this.button10.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(189, 47);
            this.button10.TabIndex = 111;
            this.button10.Text = "Theme Editor";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // folder
            // 
            this.folder.BackColor = System.Drawing.Color.Transparent;
            this.folder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.folder.ErrorImage = null;
            this.folder.Image = ((System.Drawing.Image)(resources.GetObject("folder.Image")));
            this.folder.ImageLocation = "";
            this.folder.InitialImage = null;
            this.folder.Location = new System.Drawing.Point(401, 65);
            this.folder.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.folder.Name = "folder";
            this.folder.Size = new System.Drawing.Size(47, 44);
            this.folder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.folder.TabIndex = 94;
            this.folder.TabStop = false;
            this.folder.Click += new System.EventHandler(this.pictureBox7_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.RoyalBlue;
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(413, 171);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(189, 47);
            this.button5.TabIndex = 117;
            this.button5.Text = "Credits, About, Debug";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.RoyalBlue;
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(217, 223);
            this.button6.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(189, 47);
            this.button6.TabIndex = 118;
            this.button6.Text = "Delete Cache and Settings";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(22, 298);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 17);
            this.label2.TabIndex = 119;
            this.label2.Text = "AES Key Source:";
            // 
            // AesKeySourceComboBox
            // 
            this.AesKeySourceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AesKeySourceComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AesKeySourceComboBox.ForeColor = System.Drawing.Color.White;
            this.AesKeySourceComboBox.FormattingEnabled = true;
            this.AesKeySourceComboBox.Location = new System.Drawing.Point(22, 316);
            this.AesKeySourceComboBox.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.AesKeySourceComboBox.Name = "AesKeySourceComboBox";
            this.AesKeySourceComboBox.Size = new System.Drawing.Size(193, 23);
            this.AesKeySourceComboBox.TabIndex = 120;
            this.AesKeySourceComboBox.SelectedIndexChanged += new System.EventHandler(this.AesKeySourceComboBox_SelectedIndexChanged);
            // 
            // manualAES
            // 
            this.manualAES.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.manualAES.Location = new System.Drawing.Point(234, 319);
            this.manualAES.MaxLength = 66;
            this.manualAES.Name = "manualAES";
            this.manualAES.Size = new System.Drawing.Size(359, 16);
            this.manualAES.TabIndex = 121;
            this.manualAES.Visible = false;
            this.manualAES.TextChanged += new System.EventHandler(this.manualAES_TextChanged);
            // 
            // manualAESLabel
            // 
            this.manualAESLabel.AutoSize = true;
            this.manualAESLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.manualAESLabel.ForeColor = System.Drawing.Color.White;
            this.manualAESLabel.Location = new System.Drawing.Point(234, 298);
            this.manualAESLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.manualAESLabel.Name = "manualAESLabel";
            this.manualAESLabel.Size = new System.Drawing.Size(105, 17);
            this.manualAESLabel.TabIndex = 122;
            this.manualAESLabel.Text = "Manual AES Key:";
            this.manualAESLabel.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(22, 353);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(327, 15);
            this.label3.TabIndex = 123;
            this.label3.Text = "Pro Swapper requires a restart after changing AES Key Source";
            this.label3.Visible = false;
            // 
            // checkPing
            // 
            this.checkPing.BackColor = System.Drawing.Color.RoyalBlue;
            this.checkPing.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkPing.FlatAppearance.BorderSize = 0;
            this.checkPing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkPing.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkPing.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.checkPing.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.checkPing.Location = new System.Drawing.Point(228, 316);
            this.checkPing.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.checkPing.Name = "checkPing";
            this.checkPing.Size = new System.Drawing.Size(158, 23);
            this.checkPing.TabIndex = 124;
            this.checkPing.Text = "Check AES Source ping";
            this.checkPing.UseVisualStyleBackColor = false;
            this.checkPing.Click += new System.EventHandler(this.button8_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(113)))));
            this.ClientSize = new System.Drawing.Size(652, 394);
            this.Controls.Add(this.checkPing);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.manualAESLabel);
            this.Controls.Add(this.manualAES);
            this.Controls.Add(this.AesKeySourceComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.Restart);
            this.Controls.Add(this.folder);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.paksBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SettingsForm_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.folder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox folder;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox paksBox;
        private System.Windows.Forms.Button Restart;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox AesKeySourceComboBox;
        private System.Windows.Forms.TextBox manualAES;
        private System.Windows.Forms.Label manualAESLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button checkPing;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}