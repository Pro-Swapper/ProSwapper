using System;
using System.Net;
using Newtonsoft.Json.Linq;

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
            patchnotes.Text = "Update " + global.version + Environment.NewLine + API.api.apidata.patchnotes;
            newstext.Text = API.api.apidata.newstext;
            try
            {
                news.ImageLocation = ((dynamic)JObject.Parse(new WebClient().DownloadString($"{API.api.FNAPIEndpoint}news/br"))).data.image;
            }
            catch { }
        }
    }
}
