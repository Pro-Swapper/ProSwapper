using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Pro_Swapper.API;
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
            patchnotes.Text = "Update " + global.version + Environment.NewLine;
            newstext.Text = api.apidata.newstext;
            string AutoPatchNotes = string.Empty;

            foreach (api.Item item in api.apidata.items.Skip(49))
            {
                AutoPatchNotes += $"Added {item.SwapsFrom} to {item.SwapsTo}\n";
            }
            patchnotes.Text += api.apidata.patchnotes + Environment.NewLine + AutoPatchNotes;
            try
            {
                news.ImageLocation = ((dynamic)JObject.Parse(global.web.DownloadString($"{api.FNAPIEndpoint}news/br"))).data.image;
            }
            catch { }
        }
    }
}
