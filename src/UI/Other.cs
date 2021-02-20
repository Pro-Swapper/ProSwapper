using System;
using System.Windows.Forms;
using System.Diagnostics;
namespace Pro_Swapper
{
    public partial class OtherTab : System.Windows.Forms.UserControl
    {
        private static OtherTab _instance;
        public static OtherTab Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OtherTab();
                return _instance;
            }
        }
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
            Plugins.Image = global.ItemIcon("JtOsC7V.png");
        }
        private void pictureBox2_Click(object sender, EventArgs e)=> Process.Start(yturl + "BrKBtzta3-4");
        private void pictureBox3_Click(object sender, EventArgs e)=> Process.Start(yturl + "dQw4w9WgXcQ");
        private void pictureBox4_Click_1(object sender, EventArgs e)=> new SplashUI().ShowDialog();
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select a Pro Swapper Plugin file (*.pro) and you will be able to swap an item from the file!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
            using (OpenFileDialog plugin = new OpenFileDialog())
            {
                plugin.Filter = "Pro Swapper Plugins (*.pro)|*.pro";
                plugin.Title = "Select a Pro Swapper Plugin";
                plugin.ShowDialog();
                if (!plugin.FileName.EndsWith(".pro"))
                {
                    return;
                }
                new Plugins(plugin.FileName).Show();
            }
        }
        private void pictureBox8_Click(object sender, EventArgs e)=> Process.Start("https://api.nitestats.com/v1/shop/image?header=Pro%20Swapper&textcolor=03c6fc&background=3262a8");
        private void label12_Click(object sender, EventArgs e)=> Process.Start("https://github.com/kyeondiscord/pro-swapper-wiki/blob/master/wiki.md");
        private void pictureBox1_Click(object sender, EventArgs e)=> Process.Start(yturl + "I_alZqXJgec");
    }
}
 