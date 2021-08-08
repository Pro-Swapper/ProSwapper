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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.news = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.patchnotes = new System.Windows.Forms.RichTextBox();
            this.discord = new System.Windows.Forms.PictureBox();
            this.twitter = new System.Windows.Forms.PictureBox();
            this.yt = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.newstext = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.news)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.discord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.twitter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
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
            this.news.MouseDown += new System.Windows.Forms.MouseEventHandler(this.news_MouseDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(11, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(403, 77);
            this.label3.TabIndex = 2;
            this.label3.Text = "Patch Notes";
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.news_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(629, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 77);
            this.label2.TabIndex = 3;
            this.label2.Text = "News";
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.news_MouseDown);
            // 
            // patchnotes
            // 
            this.patchnotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.patchnotes.BulletIndent = 1;
            this.patchnotes.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.patchnotes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.patchnotes.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.patchnotes.Location = new System.Drawing.Point(21, 113);
            this.patchnotes.Name = "patchnotes";
            this.patchnotes.ReadOnly = true;
            this.patchnotes.Size = new System.Drawing.Size(383, 378);
            this.patchnotes.TabIndex = 4;
            this.patchnotes.TabStop = false;
            this.patchnotes.Text = "";
            // 
            // discord
            // 
            this.discord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.discord.ErrorImage = null;
            this.discord.Image = ((System.Drawing.Image)(resources.GetObject("discord.Image")));
            this.discord.ImageLocation = "";
            this.discord.InitialImage = null;
            this.discord.Location = new System.Drawing.Point(701, 527);
            this.discord.Name = "discord";
            this.discord.Size = new System.Drawing.Size(57, 36);
            this.discord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.discord.TabIndex = 119;
            this.discord.TabStop = false;
            this.discord.Click += new System.EventHandler(this.discord_Click);
            // 
            // twitter
            // 
            this.twitter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.twitter.ErrorImage = null;
            this.twitter.Image = ((System.Drawing.Image)(resources.GetObject("twitter.Image")));
            this.twitter.ImageLocation = "";
            this.twitter.InitialImage = null;
            this.twitter.Location = new System.Drawing.Point(629, 527);
            this.twitter.Name = "twitter";
            this.twitter.Size = new System.Drawing.Size(57, 36);
            this.twitter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.twitter.TabIndex = 118;
            this.twitter.TabStop = false;
            this.twitter.Click += new System.EventHandler(this.twitter_Click);
            // 
            // yt
            // 
            this.yt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yt.ErrorImage = null;
            this.yt.Image = ((System.Drawing.Image)(resources.GetObject("yt.Image")));
            this.yt.ImageLocation = "";
            this.yt.InitialImage = null;
            this.yt.Location = new System.Drawing.Point(557, 527);
            this.yt.Name = "yt";
            this.yt.Size = new System.Drawing.Size(57, 36);
            this.yt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.yt.TabIndex = 117;
            this.yt.TabStop = false;
            this.yt.Click += new System.EventHandler(this.yt_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(773, 527);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(57, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 120;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.ErrorImage = null;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.ImageLocation = "";
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(845, 527);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(57, 36);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 121;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // newstext
            // 
            this.newstext.AutoSize = true;
            this.newstext.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.newstext.Location = new System.Drawing.Point(479, 476);
            this.newstext.Name = "newstext";
            this.newstext.Size = new System.Drawing.Size(38, 15);
            this.newstext.TabIndex = 122;
            this.newstext.Text = "label1";
            this.newstext.MouseDown += new System.Windows.Forms.MouseEventHandler(this.news_MouseDown);
            // 
            // Dashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.Controls.Add(this.newstext);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.discord);
            this.Controls.Add(this.twitter);
            this.Controls.Add(this.yt);
            this.Controls.Add(this.news);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.patchnotes);
            this.Name = "Dashboard";
            this.Size = new System.Drawing.Size(999, 608);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.news_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.news)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.discord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.twitter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox news;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox patchnotes;
        private System.Windows.Forms.PictureBox discord;
        private System.Windows.Forms.PictureBox twitter;
        private System.Windows.Forms.PictureBox yt;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label newstext;
    }
}
