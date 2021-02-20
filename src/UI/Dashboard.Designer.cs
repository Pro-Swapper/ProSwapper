namespace Pro_Swapper
{
    partial class Dashboard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.news = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.patchnotes = new System.Windows.Forms.RichTextBox();
            this.newstext = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.news)).BeginInit();
            this.SuspendLayout();
            // 
            // news
            // 
            this.news.BackColor = System.Drawing.Color.Transparent;
            this.news.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.news.ErrorImage = null;
            this.news.InitialImage = null;
            this.news.Location = new System.Drawing.Point(479, 113);
            this.news.Name = "news";
            this.news.Size = new System.Drawing.Size(501, 357);
            this.news.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.news.TabIndex = 1;
            this.news.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(403, 77);
            this.label3.TabIndex = 2;
            this.label3.Text = "Patch Notes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(663, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 77);
            this.label2.TabIndex = 3;
            this.label2.Text = "News";
            // 
            // patchnotes
            // 
            this.patchnotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.patchnotes.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.patchnotes.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.patchnotes.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.patchnotes.Location = new System.Drawing.Point(23, 182);
            this.patchnotes.Name = "patchnotes";
            this.patchnotes.ReadOnly = true;
            this.patchnotes.Size = new System.Drawing.Size(383, 405);
            this.patchnotes.TabIndex = 4;
            this.patchnotes.TabStop = false;
            this.patchnotes.Text = "";
            // 
            // newstext
            // 
            this.newstext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.newstext.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.newstext.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newstext.Location = new System.Drawing.Point(479, 476);
            this.newstext.Name = "newstext";
            this.newstext.ReadOnly = true;
            this.newstext.Size = new System.Drawing.Size(501, 127);
            this.newstext.TabIndex = 5;
            this.newstext.TabStop = false;
            this.newstext.Text = "";
            // 
            // Dashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.newstext);
            this.Controls.Add(this.news);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.patchnotes);
            this.Name = "Dashboard";
            this.Size = new System.Drawing.Size(995, 606);
            ((System.ComponentModel.ISupportInitialize)(this.news)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox news;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox patchnotes;
        private System.Windows.Forms.RichTextBox newstext;
    }
}
