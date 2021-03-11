namespace Pro_Swapper
{
    partial class swap
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
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(328, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "ON";
            // 
            // logbox
            // 
            this.logbox.BackColor = System.Drawing.Color.Black;
            this.logbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logbox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logbox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logbox.ForeColor = System.Drawing.Color.White;
            this.logbox.Location = new System.Drawing.Point(0, 202);
            this.logbox.Name = "logbox";
            this.logbox.ReadOnly = true;
            this.logbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.logbox.ShortcutsEnabled = false;
            this.logbox.Size = new System.Drawing.Size(455, 123);
            this.logbox.TabIndex = 10;
            this.logbox.Text = "";
            // 
            // image
            // 
            this.image.BackColor = System.Drawing.Color.Transparent;
            this.image.ErrorImage = null;
            this.image.InitialImage = null;
            this.image.Location = new System.Drawing.Point(3, 2);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(209, 194);
            this.image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.image.TabIndex = 2;
            this.image.TabStop = false;
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
            this.ConvertB.Location = new System.Drawing.Point(240, 112);
            this.ConvertB.Name = "ConvertB";
            this.ConvertB.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.ConvertB.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.ConvertB.OnHoverTextColor = System.Drawing.Color.White;
            this.ConvertB.selected = false;
            this.ConvertB.Size = new System.Drawing.Size(209, 39);
            this.ConvertB.TabIndex = 11;
            this.ConvertB.Text = "Convert";
            this.ConvertB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ConvertB.Textcolor = System.Drawing.Color.White;
            this.ConvertB.TextFont = new System.Drawing.Font("Segoe UI Semibold", 9.25F, System.Drawing.FontStyle.Bold);
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
            this.RevertB.Location = new System.Drawing.Point(240, 157);
            this.RevertB.Name = "RevertB";
            this.RevertB.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.RevertB.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.RevertB.OnHoverTextColor = System.Drawing.Color.White;
            this.RevertB.selected = false;
            this.RevertB.Size = new System.Drawing.Size(209, 39);
            this.RevertB.TabIndex = 12;
            this.RevertB.Text = "Revert";
            this.RevertB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RevertB.Textcolor = System.Drawing.Color.White;
            this.RevertB.TextFont = new System.Drawing.Font("Segoe UI Semibold", 9.25F, System.Drawing.FontStyle.Bold);
            this.RevertB.Click += new System.EventHandler(this.SwapButton_Click);
            // 
            // swap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(455, 325);
            this.Controls.Add(this.RevertB);
            this.Controls.Add(this.ConvertB);
            this.Controls.Add(this.logbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.image);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "swap";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox image;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox logbox;
        private Bunifu.Framework.UI.BunifuFlatButton ConvertB;
        private Bunifu.Framework.UI.BunifuFlatButton RevertB;
    }
}

