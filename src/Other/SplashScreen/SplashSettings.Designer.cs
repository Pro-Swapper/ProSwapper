
namespace Pro_Swapper.Splash
{
    partial class SplashSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.logoposBox = new System.Windows.Forms.ComboBox();
            this.DisableSplashScreenBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.HideUIControlsBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LogoImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.LogoImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(47, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Logo Position:";
            // 
            // logoposBox
            // 
            this.logoposBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.logoposBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logoposBox.FormattingEnabled = true;
            this.logoposBox.Items.AddRange(new object[] {
            "top-left",
            "bottom-right"});
            this.logoposBox.Location = new System.Drawing.Point(136, 33);
            this.logoposBox.Name = "logoposBox";
            this.logoposBox.Size = new System.Drawing.Size(162, 23);
            this.logoposBox.TabIndex = 1;
            this.logoposBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // DisableSplashScreenBox
            // 
            this.DisableSplashScreenBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DisableSplashScreenBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DisableSplashScreenBox.FormattingEnabled = true;
            this.DisableSplashScreenBox.Items.AddRange(new object[] {
            "true",
            "false"});
            this.DisableSplashScreenBox.Location = new System.Drawing.Point(136, 327);
            this.DisableSplashScreenBox.Name = "DisableSplashScreenBox";
            this.DisableSplashScreenBox.Size = new System.Drawing.Size(162, 23);
            this.DisableSplashScreenBox.TabIndex = 3;
            this.DisableSplashScreenBox.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(10, 327);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Disable Splash Screen:";
            // 
            // HideUIControlsBox
            // 
            this.HideUIControlsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HideUIControlsBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HideUIControlsBox.FormattingEnabled = true;
            this.HideUIControlsBox.Items.AddRange(new object[] {
            "true",
            "false"});
            this.HideUIControlsBox.Location = new System.Drawing.Point(136, 381);
            this.HideUIControlsBox.Name = "HideUIControlsBox";
            this.HideUIControlsBox.Size = new System.Drawing.Size(162, 23);
            this.HideUIControlsBox.TabIndex = 5;
            this.HideUIControlsBox.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(33, 389);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Hide UI Controls:";
            // 
            // LogoImage
            // 
            this.LogoImage.Location = new System.Drawing.Point(10, 62);
            this.LogoImage.Name = "LogoImage";
            this.LogoImage.Size = new System.Drawing.Size(366, 210);
            this.LogoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LogoImage.TabIndex = 6;
            this.LogoImage.TabStop = false;
            // 
            // SplashSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(33)))), ((int)(((byte)(113)))));
            this.Controls.Add(this.LogoImage);
            this.Controls.Add(this.HideUIControlsBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DisableSplashScreenBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.logoposBox);
            this.Controls.Add(this.label1);
            this.Name = "SplashSettings";
            this.Size = new System.Drawing.Size(826, 552);
            this.Load += new System.EventHandler(this.SplashSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LogoImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox logoposBox;
        private System.Windows.Forms.ComboBox DisableSplashScreenBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox HideUIControlsBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox LogoImage;
    }
}
