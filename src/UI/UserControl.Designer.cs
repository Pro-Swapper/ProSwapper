namespace Pro_Swapper
{
    partial class UserControl
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
            this.skinsflowlayout = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // skinsflowlayout
            // 
            this.skinsflowlayout.AutoScroll = true;
            this.skinsflowlayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinsflowlayout.Location = new System.Drawing.Point(0, 0);
            this.skinsflowlayout.Name = "skinsflowlayout";
            this.skinsflowlayout.Size = new System.Drawing.Size(995, 606);
            this.skinsflowlayout.TabIndex = 0;
            // 
            // UserControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.skinsflowlayout);
            this.DoubleBuffered = true;
            this.Name = "UserControl";
            this.Size = new System.Drawing.Size(995, 606);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel skinsflowlayout;
    }
}
