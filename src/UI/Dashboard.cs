using System;
using System.Linq;
using Pro_Swapper.API;
using System.Windows.Forms;
namespace Pro_Swapper
{
    public partial class Dashboard : UserControl
    {
        private static Dashboard _instance;
        public static Dashboard Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Dashboard();
                return _instance;
            }
        }
        public Dashboard()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = global.MainMenu;
            patchnotes.BackColor = global.MainMenu;
            newstext.BackColor = global.MainMenu;
            newstext.ForeColor = global.TextColor;
            label2.ForeColor = global.TextColor;
            label3.ForeColor = global.TextColor;
            patchnotes.Text = "Update " + global.version + Environment.NewLine;
            newstext.Text = api.apidata.newstext.Split(';')[0];
            newstext.Font = newstext.Font = new System.Drawing.Font("Segoe UI", float.Parse(api.apidata.newstext.Split(';')[1]), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            string AutoPatchNotes = string.Empty;
            foreach (api.Item item in api.apidata.items.Skip(61))
            {
                AutoPatchNotes += $"Added {item.SwapsFrom} to {item.SwapsTo}\n";
            }
            patchnotes.Text += api.apidata.patchnotes + Environment.NewLine + AutoPatchNotes;
            try
            {
                news.ImageLocation = msgpack.MsgPacklz4($"{api.FNAPIEndpoint}news/br?responseFormat=msgpack_lz4&responseOptions=ignore_null").data.image;
            }
            catch { }
        }

        private void yt_Click(object sender, EventArgs e)=> global.OpenUrl("https://youtube.com/proswapperofficial");
         private void twitter_Click(object sender, EventArgs e)=> global.OpenUrl("https://twitter.com/Pro_Swapper");
        private void discord_Click(object sender, EventArgs e)=> global.OpenUrl(api.apidata.discordurl);
        private void pictureBox1_Click(object sender, EventArgs e)=> global.OpenUrl("https://github.com/Pro-Swapper");
        private void pictureBox2_Click(object sender, EventArgs e)=> global.OpenUrl("http://tiktok.com/@proswapperofficial");

        private void news_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) 
                global.FormMove(Main.Mainform.Handle);
        }
    }
}
