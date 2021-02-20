using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Pro_Swapper
{
    public partial class ThemeCreator : Form
    {
        public ThemeCreator()
        {
            InitializeComponent();
            Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
        }
        private void ThemeCreator_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Main.FormMove(Handle);
        }
        private void button1_Click(object sender, EventArgs e)=> this.Close();
        private void button9_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog a = new OpenFileDialog())
            {
                a.Title = "Select a Pro Swapper Theme to import";
                a.DefaultExt = "protheme";
                a.Filter = "Pro Swapper Theme (.protheme)|*.protheme";
                a.ShowDialog();
                if (File.Exists(a.FileName))
                {
                    string[] data = File.ReadAllText(a.FileName).Split(';');
                    string[] panel1d = data[0].Split(',');
                    string[] panel2d = data[1].Split(',');
                    string[] panel3d = data[2].Split(',');
                    string[] panel4d = data[3].Split(',');
                    panel1.BackColor = Color.FromArgb(255, int.Parse(panel1d[0]), int.Parse(panel1d[1]), int.Parse(panel1d[2]));
                    panel2.BackColor = Color.FromArgb(255, int.Parse(panel2d[0]), int.Parse(panel2d[1]), int.Parse(panel2d[2]));
                    panel3.BackColor = Color.FromArgb(255, int.Parse(panel3d[0]), int.Parse(panel3d[1]), int.Parse(panel3d[2]));
                    panel4.BackColor = Color.FromArgb(255, int.Parse(panel4d[0]), int.Parse(panel4d[1]), int.Parse(panel4d[2]));
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog a = new SaveFileDialog())
            {
                a.Title = "Save the current theme";
                a.DefaultExt = "protheme";
                a.FileName = "Pro Swapper Theme.protheme";
                a.Filter = "Pro Swapper Theme (.protheme)|*.protheme";
                if (a.ShowDialog() == DialogResult.OK)
                    File.WriteAllText(a.FileName, panel1.BackColor.R + "," + panel1.BackColor.G + "," + panel1.BackColor.B + ";" + panel2.BackColor.R + "," + panel2.BackColor.G + "," + panel2.BackColor.B + ";" + panel3.BackColor.R + "," + panel3.BackColor.G + "," + panel3.BackColor.B + ";" + panel4.BackColor.R + "," + panel4.BackColor.G + "," + panel4.BackColor.B);
            }
        }


        private void ColorPanel_Click(object sender, MouseEventArgs e)
        {
            using (ColorDialog a = new ColorDialog())
                if (a.ShowDialog() == DialogResult.OK)
                    ((Panel)sender).BackColor = a.Color;
        }



        private void button3_Click(object sender, EventArgs e)
        {
            global.WriteSetting(panel1.BackColor.R + "," + panel1.BackColor.G + "," + panel1.BackColor.B + ";" + panel2.BackColor.R + "," + panel2.BackColor.G + "," + panel2.BackColor.B + ";" + panel3.BackColor.R + "," + panel3.BackColor.G + "," + panel3.BackColor.B + ";" + panel4.BackColor.R + "," + panel4.BackColor.G + "," + panel4.BackColor.B, global.Setting.theme);
            MessageBox.Show("Pro Swapper needs to be restarted to load the theme. Restarting Pro Swapper now...", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start(AppDomain.CurrentDomain.FriendlyName);
            Main.Cleanup();
        }

        private void ThemeCreator_Load(object sender, EventArgs e)
        {
            panel1.BackColor = global.MainMenu;
            panel2.BackColor = global.ItemsBG;
            panel3.BackColor = global.Button;
            panel4.BackColor = global.TextColor;
            button2.BackColor = global.Button;
            button2.ForeColor = global.TextColor;

            button9.BackColor = global.Button;
            button9.ForeColor = global.TextColor;

            button3.BackColor = global.Button;
            button3.ForeColor = global.TextColor;

            button4.BackColor = global.Button;
            button4.ForeColor = global.TextColor;
            BackColor = global.MainMenu;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] data = "0,33,113;64,85,170;65,105,255;255,255,255".Split(';');
            string[] panel1d = data[0].Split(',');
            string[] panel2d = data[1].Split(',');
            string[] panel3d = data[2].Split(',');
            string[] panel4d = data[3].Split(',');
            panel1.BackColor = Color.FromArgb(255, int.Parse(panel1d[0]), int.Parse(panel1d[1]), int.Parse(panel1d[2]));
            panel2.BackColor = Color.FromArgb(255, int.Parse(panel2d[0]), int.Parse(panel2d[1]), int.Parse(panel2d[2]));
            panel3.BackColor = Color.FromArgb(255, int.Parse(panel3d[0]), int.Parse(panel3d[1]), int.Parse(panel3d[2]));
            panel4.BackColor = Color.FromArgb(255, int.Parse(panel4d[0]), int.Parse(panel4d[1]), int.Parse(panel4d[2]));
        }
    }
}
