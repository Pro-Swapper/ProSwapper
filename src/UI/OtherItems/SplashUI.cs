using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
namespace Pro_Swapper
{
    public partial class SplashUI : Form
    {
        private readonly string SplashPath = global.ReadSetting(global.Setting.Paks).Replace(@"Content\Paks", @"Binaries\Win64\EasyAntiCheat\Launcher\SplashScreen.png");
        public SplashUI()
        {
            InitializeComponent();
            Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            label1.ForeColor = global.TextColor;
            BackColor = global.MainMenu;
            items.BackColor = global.MainMenu;
            string[] imglinks = { "xYxVkVq.png", "PLseeQ0.png", "kk1bjpe.png", "0tMRVES.png", "2nBa1Si.png", "ffRyYUY.png", "XhLw59i.png", "RCLc2NK.png", "jqOW6K5.png", "f1fO6FL.png", "C33Drri.png", "sJKC6i8.png", "w1Zga0x.png", "pEJvhZ2.png", "n6YJEqD.png", "hy8KCat.png", "lBl1Lzf.png", "NAF3VJm.png", "G1Wv8cv.png", "oyR6VMM.png", "054q6dD.png", "6EsE3vt.png", "QaQakei.png", "3Xb8iYR.png" };
            foreach (string image in imglinks)
            {
                PictureBox picturebox = new PictureBox();
                picturebox.Cursor = Cursors.Hand;
                new Thread(() =>
                {
                    picturebox.Image = global.ItemIcon(image);
                }).Start();
                picturebox.Location = new Point(3, 3);
                picturebox.Size = new Size(196, 129);
                picturebox.SizeMode = PictureBoxSizeMode.Zoom;
                picturebox.TabIndex = 1;
                picturebox.TabStop = false;
                picturebox.Click += new EventHandler(SplashClick);
                items.Controls.Add(picturebox);
            }
        }
        private void ThemeCreator_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Main.FormMove(Handle);
        }
        private void button1_Click(object sender, EventArgs e)=> Close();
        private void button6_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog a = new OpenFileDialog())
            {
                if (a.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(a.FileName, SplashPath, true);
                    MessageBox.Show("Set Splash Screen!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void SplashClick(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image.Save(SplashPath);
            MessageBox.Show("Set Splash Screen!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
