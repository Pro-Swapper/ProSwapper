namespace Pro_Swapper
{
    partial class Plugins
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
            this.ConvertB = new System.Windows.Forms.Button();
            this.RevertB = new System.Windows.Forms.Button();
            this.Swap = new System.ComponentModel.BackgroundWorker();
            this.Revert = new System.ComponentModel.BackgroundWorker();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.title = new System.Windows.Forms.RichTextBox();
            this.plugincreatortxt = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ConvertB
            // 
            this.ConvertB.BackColor = System.Drawing.Color.White;
            this.ConvertB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConvertB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ConvertB.Location = new System.Drawing.Point(240, 112);
            this.ConvertB.Name = "ConvertB";
            this.ConvertB.Size = new System.Drawing.Size(209, 39);
            this.ConvertB.TabIndex = 0;
            this.ConvertB.Text = "Convert";
            this.ConvertB.UseVisualStyleBackColor = false;
            this.ConvertB.Click += new System.EventHandler(this.button1_Click);
            // 
            // RevertB
            // 
            this.RevertB.BackColor = System.Drawing.Color.White;
            this.RevertB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RevertB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RevertB.Location = new System.Drawing.Point(240, 157);
            this.RevertB.Name = "RevertB";
            this.RevertB.Size = new System.Drawing.Size(209, 39);
            this.RevertB.TabIndex = 1;
            this.RevertB.Text = "Revert";
            this.RevertB.UseVisualStyleBackColor = false;
            this.RevertB.Click += new System.EventHandler(this.button2_Click);
            // 
            // Swap
            // 
            this.Swap.WorkerSupportsCancellation = true;
            this.Swap.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ReplaceBytes);
            // 
            // Revert
            // 
            this.Revert.WorkerSupportsCancellation = true;
            this.Revert.DoWork += new System.ComponentModel.DoWorkEventHandler(this.RevertBytes);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(274, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "ON/OFF";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(351, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 37);
            this.button1.TabIndex = 5;
            this.button1.Text = "Reset Config (This item only)";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.Black;
            this.title.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.title.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.title.Font = new System.Drawing.Font("Lucida Console", 7.5F);
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.Location = new System.Drawing.Point(0, 202);
            this.title.Name = "title";
            this.title.ReadOnly = true;
            this.title.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.title.Size = new System.Drawing.Size(455, 123);
            this.title.TabIndex = 10;
            this.title.Text = "";
            // 
            // plugincreatortxt
            // 
            this.plugincreatortxt.AutoSize = true;
            this.plugincreatortxt.BackColor = System.Drawing.Color.Transparent;
            this.plugincreatortxt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plugincreatortxt.ForeColor = System.Drawing.Color.White;
            this.plugincreatortxt.Location = new System.Drawing.Point(212, 9);
            this.plugincreatortxt.Name = "plugincreatortxt";
            this.plugincreatortxt.Size = new System.Drawing.Size(87, 13);
            this.plugincreatortxt.TabIndex = 11;
            this.plugincreatortxt.Text = "Plugin Made by";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.ErrorImage = null;
            this.pictureBox4.InitialImage = null;
            this.pictureBox4.Location = new System.Drawing.Point(0, 135);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(58, 61);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 9;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(209, 194);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // Plugins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(455, 325);
            this.Controls.Add(this.plugincreatortxt);
            this.Controls.Add(this.title);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.RevertB);
            this.Controls.Add(this.ConvertB);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Plugins";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConvertB;
        private System.Windows.Forms.Button RevertB;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.ComponentModel.BackgroundWorker Swap;
        private System.ComponentModel.BackgroundWorker Revert;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.RichTextBox title;
        private System.Windows.Forms.Label plugincreatortxt;
    }
}

