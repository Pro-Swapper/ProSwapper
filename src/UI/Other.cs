using System;
using System.Windows.Forms;
namespace Pro_Swapper
{
    public partial class OtherTab : System.Windows.Forms.UserControl
    {
        private const string yturl = "https://youtu.be/";
        public OtherTab()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = global.ItemsBG;
            pictureBox8.Image = global.ItemIcon("EzIhXbh.png");
            StretchRes.Image = global.ItemIcon("npHpZes.png");
            Music.Image = global.ItemIcon("pnjMa2G.png");
            Aimbot.Image = global.ItemIcon("xXqdfcu.png");
            Splash.Image = global.ItemIcon("jH5L1Y2.png");
        }
        private void pictureBox2_Click(object sender, EventArgs e)=> global.OpenUrl(yturl + "BrKBtzta3-4");
        private void pictureBox3_Click(object sender, EventArgs e)=> global.OpenUrl(yturl + "dQw4w9WgXcQ");
        private void pictureBox4_Click_1(object sender, EventArgs e) => global.OpenUrl("https://github.com/Pro-Swapper/SplashScreen/blob/main/README.md");
        private void pictureBox8_Click(object sender, EventArgs e)=> global.OpenUrl("https://api.nitestats.com/v1/shop/image?header=Pro%20Swapper&textcolor=03c6fc&background=3262a8");
        private void label12_Click(object sender, EventArgs e)=> global.OpenUrl("https://github.com/Pro-Swapper/faq");
        private void pictureBox1_Click(object sender, EventArgs e)=> global.OpenUrl(yturl + "I_alZqXJgec");
    }
}
 