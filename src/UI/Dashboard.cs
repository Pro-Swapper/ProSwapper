using System;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Net;
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
            patchnotes.Text = "Update " + global.version + Environment.NewLine + Program.apidata.patchnotes;
            newstext.Text = Program.apidata.newstext;
            try
            {
                news.ImageLocation = Convert.ToString(new JavaScriptSerializer().Deserialize<dynamic>(new WebClient().DownloadString("https://fortnite-api.com/v2/news/br"))["data"]["image"]);
            }
            catch { }
        }
    }
}
