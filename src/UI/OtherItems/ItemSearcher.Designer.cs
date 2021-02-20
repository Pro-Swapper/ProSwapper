namespace Pro_Swapper
{
    partial class ItemSearcher
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Results = new System.Windows.Forms.TextBox();
            this.image = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.featured = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.itemtype = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.namedesc = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.featured)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(12, 62);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(239, 55);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(257, 62);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 55);
            this.button1.TabIndex = 1;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Results
            // 
            this.Results.BackColor = System.Drawing.Color.White;
            this.Results.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Results.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Results.Location = new System.Drawing.Point(12, 157);
            this.Results.Multiline = true;
            this.Results.Name = "Results";
            this.Results.ReadOnly = true;
            this.Results.Size = new System.Drawing.Size(203, 36);
            this.Results.TabIndex = 2;
            // 
            // image
            // 
            this.image.ErrorImage = null;
            this.image.InitialImage = null;
            this.image.Location = new System.Drawing.Point(12, 205);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(244, 337);
            this.image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.image.TabIndex = 3;
            this.image.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 22);
            this.label1.TabIndex = 6;
            this.label1.Text = "Search Item Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 22);
            this.label2.TabIndex = 7;
            this.label2.Text = "ID (CID, BID, EID, Etc):";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(221, 157);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 36);
            this.button2.TabIndex = 8;
            this.button2.Text = "Copy";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // featured
            // 
            this.featured.ErrorImage = null;
            this.featured.InitialImage = null;
            this.featured.Location = new System.Drawing.Point(262, 326);
            this.featured.Name = "featured";
            this.featured.Size = new System.Drawing.Size(298, 216);
            this.featured.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.featured.TabIndex = 9;
            this.featured.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(262, 303);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Featured:";
            this.label3.Visible = false;
            // 
            // itemtype
            // 
            this.itemtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.itemtype.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.itemtype.FormattingEnabled = true;
            this.itemtype.Items.AddRange(new object[] {
            "Skin",
            "Backbling",
            "Emote",
            "Pickaxe",
            "All"});
            this.itemtype.Location = new System.Drawing.Point(344, 157);
            this.itemtype.Name = "itemtype";
            this.itemtype.Size = new System.Drawing.Size(216, 24);
            this.itemtype.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(340, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 22);
            this.label4.TabIndex = 12;
            this.label4.Text = "Item Type:";
            // 
            // namedesc
            // 
            this.namedesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.namedesc.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namedesc.Location = new System.Drawing.Point(266, 205);
            this.namedesc.Multiline = true;
            this.namedesc.Name = "namedesc";
            this.namedesc.ReadOnly = true;
            this.namedesc.Size = new System.Drawing.Size(294, 95);
            this.namedesc.TabIndex = 13;
            // 
            // ItemSearcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 583);
            this.Controls.Add(this.namedesc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.itemtype);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.featured);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.image);
            this.Controls.Add(this.Results);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "ItemSearcher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pro Swapper - Item Searcher";
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.featured)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Results;
        private System.Windows.Forms.PictureBox image;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox featured;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox itemtype;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox namedesc;
    }
}