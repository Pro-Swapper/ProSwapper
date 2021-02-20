using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pro_Swapper
{
    public partial class bkswap : Form
    {
        public bkswap()
        {
            InitializeComponent();
            Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
            frozenshield.ImageLocation = "https://cdn.discordapp.com/attachments/778776789866512394/778827745039089765/frozen-red-shield-icon.png";
            redshield.ImageLocation = "https://cdn.discordapp.com/attachments/778776789866512394/778848244624261120/red-shield-icon.png";
            darkshield.ImageLocation = "https://cdn.discordapp.com/attachments/778776789866512394/778849656553734165/dark-shield-icon.png";
        }
        private void banners_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Main.FormMove(Handle);
        }

        private void PictureBox26_Click(object sender, EventArgs e)
        {
            new swap(3).Show();
            Close();
        }
       
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new swap(4).Show();
            Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            new swap(5).Show();
            Close();
        }
        private void button1_Click(object sender, EventArgs e) => Close();
    }
}
