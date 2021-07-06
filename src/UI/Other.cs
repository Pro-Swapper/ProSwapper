using System;
using Pro_Swapper.API;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;
namespace Pro_Swapper
{
    public partial class OtherTab : System.Windows.Forms.UserControl
    {
        private const string yturl = "https://youtu.be/";
        private const string githuburl = "https://github.com/Pro-Swapper/";
        public OtherTab()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = global.ItemsBG;
            pictureBox8.Image = global.ItemIcon("90ZggOY.png");
        }
        private void pictureBox2_Click(object sender, EventArgs e)=> global.OpenUrl(yturl + "BrKBtzta3-4");
        private void pictureBox3_Click(object sender, EventArgs e)=> global.OpenUrl(yturl + "dQw4w9WgXcQ");
        private void pictureBox4_Click_1(object sender, EventArgs e) => global.OpenUrl("https://github.com/Pro-Swapper/SplashScreen/blob/main/README.md");
        private void pictureBox8_Click(object sender, EventArgs e)=> global.OpenUrl("https://api.nitestats.com/v1/shop/image?header=Pro%20Swapper&textcolor=03c6fc&background=3262a8");
        private void label12_Click(object sender, EventArgs e)=> global.OpenUrl($"{githuburl}faq");
        private void pictureBox1_Click(object sender, EventArgs e)=> global.OpenUrl(yturl + "I_alZqXJgec");
        private void label2_Click(object sender, EventArgs e) => global.OpenUrl($"{githuburl}Plugins-Json");
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog o = new OpenFileDialog())
            {
                o.Title = "Select a plugin (.json) file";
                o.Filter = "Pro Swapper Plugin (*.json)|*.json|All files (*.*)|*.*";
                if (o.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filedata = File.ReadAllText(o.FileName);
                        api.Item pluginitem = JsonConvert.DeserializeObject<api.Item>(filedata);
                        new OodleSwap(pluginitem).ShowDialog();
                    }
                    catch
                    {
                        MessageBox.Show("That's not a valid plugin! Plugins must be in a Json format! Please read Plugins Tutorial for more information", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        
    }
}
 