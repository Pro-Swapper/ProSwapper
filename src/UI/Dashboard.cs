using System;
using System.Net;
using Newtonsoft.Json;
namespace Pro_Swapper
{
    public partial class Dashboard : System.Windows.Forms.UserControl
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
            BackColor = global.MainMenu;
            patchnotes.BackColor = global.MainMenu;
            newstext.BackColor = global.MainMenu;
            newstext.ForeColor = global.TextColor;
            label2.ForeColor = global.TextColor;
            label3.ForeColor = global.TextColor;
            patchnotes.Text = "Update " + global.version + Environment.NewLine + api.apidata.patchnotes;
            newstext.Text = api.apidata.newstext;
            try
            {
                news.ImageLocation = JsonConvert.DeserializeObject<fnapi>(new WebClient().DownloadString("https://fortnite-api.com/v2/news/br")).data.image;
            }
            catch { }
        }
        public class Data
        {
            public string image { get; set; }
        }
        public class fnapi
        {
            public Data data { get; set; }
        }
    }
}
