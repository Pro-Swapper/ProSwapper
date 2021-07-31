namespace Pro_Swapper
{
    partial class ZlibSwap
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
            this.label3 = new System.Windows.Forms.Label();
            this.logbox = new System.Windows.Forms.RichTextBox();
            this.image = new System.Windows.Forms.PictureBox();
            this.ConvertB = new Bunifu.Framework.UI.BunifuFlatButton();
            this.RevertB = new Bunifu.Framework.UI.BunifuFlatButton();
            this.ExitButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.swapsfrom = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.swapsfrom)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(166, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "ON";
            // 
            // logbox
            // 
            this.logbox.BackColor = System.Drawing.Color.White;
            this.logbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logbox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logbox.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.logbox.ForeColor = System.Drawing.Color.White;
            this.logbox.Location = new System.Drawing.Point(0, 272);
            this.logbox.Name = "logbox";
            this.logbox.ReadOnly = true;
            this.logbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.logbox.ShortcutsEnabled = false;
            this.logbox.Size = new System.Drawing.Size(367, 57);
            this.logbox.TabIndex = 10;
            this.logbox.Text = "";
            // 
            // image
            // 
            this.image.BackColor = System.Drawing.Color.Transparent;
            this.image.ErrorImage = null;
            this.image.InitialImage = null;
            this.image.Location = new System.Drawing.Point(12, 59);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(119, 107);
            this.image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.image.TabIndex = 2;
            this.image.TabStop = false;
            this.image.MouseDown += new System.Windows.Forms.MouseEventHandler(this.swap_MouseDown);
            // 
            // ConvertB
            // 
            this.ConvertB.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.ConvertB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.ConvertB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ConvertB.BorderRadius = 0;
            this.ConvertB.ButtonText = "Convert";
            this.ConvertB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ConvertB.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.ConvertB.Iconcolor = System.Drawing.Color.Transparent;
            this.ConvertB.Iconimage = null;
            this.ConvertB.Iconimage_right = null;
            this.ConvertB.Iconimage_right_Selected = null;
            this.ConvertB.Iconimage_Selected = null;
            this.ConvertB.IconMarginLeft = 0;
            this.ConvertB.IconMarginRight = 0;
            this.ConvertB.IconRightVisible = true;
            this.ConvertB.IconRightZoom = 0D;
            this.ConvertB.IconVisible = true;
            this.ConvertB.IconZoom = 90D;
            this.ConvertB.IsTab = false;
            this.ConvertB.Location = new System.Drawing.Point(12, 172);
            this.ConvertB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ConvertB.Name = "ConvertB";
            this.ConvertB.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.ConvertB.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.ConvertB.OnHoverTextColor = System.Drawing.Color.White;
            this.ConvertB.selected = false;
            this.ConvertB.Size = new System.Drawing.Size(344, 39);
            this.ConvertB.TabIndex = 11;
            this.ConvertB.Text = "Convert";
            this.ConvertB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ConvertB.Textcolor = System.Drawing.Color.White;
            this.ConvertB.TextFont = new System.Drawing.Font("Segoe UI Semibold", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ConvertB.Click += new System.EventHandler(this.SwapButton_Click);
            // 
            // RevertB
            // 
            this.RevertB.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.RevertB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.RevertB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RevertB.BorderRadius = 0;
            this.RevertB.ButtonText = "Revert";
            this.RevertB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RevertB.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.RevertB.Iconcolor = System.Drawing.Color.Transparent;
            this.RevertB.Iconimage = null;
            this.RevertB.Iconimage_right = null;
            this.RevertB.Iconimage_right_Selected = null;
            this.RevertB.Iconimage_Selected = null;
            this.RevertB.IconMarginLeft = 0;
            this.RevertB.IconMarginRight = 0;
            this.RevertB.IconRightVisible = true;
            this.RevertB.IconRightZoom = 0D;
            this.RevertB.IconVisible = true;
            this.RevertB.IconZoom = 90D;
            this.RevertB.IsTab = false;
            this.RevertB.Location = new System.Drawing.Point(12, 217);
            this.RevertB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RevertB.Name = "RevertB";
            this.RevertB.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.RevertB.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.RevertB.OnHoverTextColor = System.Drawing.Color.White;
            this.RevertB.selected = false;
            this.RevertB.Size = new System.Drawing.Size(344, 39);
            this.RevertB.TabIndex = 12;
            this.RevertB.Text = "Revert";
            this.RevertB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RevertB.Textcolor = System.Drawing.Color.White;
            this.RevertB.TextFont = new System.Drawing.Font("Segoe UI Semibold", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.RevertB.Click += new System.EventHandler(this.SwapButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitButton.FlatAppearance.BorderSize = 0;
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ExitButton.Location = new System.Drawing.Point(320, 2);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(36, 32);
            this.ExitButton.TabIndex = 14;
            this.ExitButton.TabStop = false;
            this.ExitButton.Text = "X";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button2.Location = new System.Drawing.Point(288, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(36, 32);
            this.button2.TabIndex = 13;
            this.button2.TabStop = false;
            this.button2.Text = "-";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(49, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "Label";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.swap_MouseDown);
            // 
            // swapsfrom
            // 
            this.swapsfrom.BackColor = System.Drawing.Color.Transparent;
            this.swapsfrom.ErrorImage = null;
            this.swapsfrom.InitialImage = null;
            this.swapsfrom.Location = new System.Drawing.Point(237, 59);
            this.swapsfrom.Name = "swapsfrom";
            this.swapsfrom.Size = new System.Drawing.Size(119, 107);
            this.swapsfrom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.swapsfrom.TabIndex = 16;
            this.swapsfrom.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(166, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "-->";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OodleSwap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(367, 329);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.swapsfrom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.RevertB);
            this.Controls.Add(this.ConvertB);
            this.Controls.Add(this.logbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.image);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "OodleSwap";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.swap_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.swapsfrom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox image;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox logbox;
        private Bunifu.Framework.UI.BunifuFlatButton ConvertB;
        private Bunifu.Framework.UI.BunifuFlatButton RevertB;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox swapsfrom;
        private System.Windows.Forms.Label label2;
    }
}

