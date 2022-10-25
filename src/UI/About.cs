using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace Pro_Swapper.UI
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            BackColor = global.MainMenu;
            Icon = Main.appIcon;
#if RELEASE//GitHub's 60 requests per hour for no auth
            //GitHub requires a user agent in the request so just paste in whatever so i just put this in
            string data = Program.httpClient.GetStringAsync("https://api.github.com/repos/Pro-Swapper/ProSwapper/contributors").GetAwaiter().GetResult();
            Contributors[] contributors = JsonConvert.DeserializeObject<Contributors[]>(data);
            flowLayoutPanel1.Controls.AddRange(contributors.Select(x => new GridItem(x.avatar_url, $"{x.login} ({x.contributions} contributions)", x.html_url)).ToArray());

#endif
            Region = Native.RoundedFormRegion(Width, Height, 10);
            this.Paint += (sender, e) =>
            {
                Graphics g = e.Graphics;
                GraphicsPath GP = new GraphicsPath();
                //GP.AddRectangle(new Rectangle(new Point(0, 0), this.Size));
                GP.AddRectangle(Region.GetBounds(g));
                g.DrawPath(new Pen(global.ChangeColorBrightness(BackColor, 0.15f)) { Width = 10f }, GP);
            };
            label1.Text = $"The best FREE Fortnite skin swapper!\nProduct Information:\nLicense: MIT\nCopyright(©) 2019 - {DateTime.Now:yyyy} Pro Swapper\nVersion: {global.version}\nMD5: {global.FileToMd5(Process.GetCurrentProcess().MainModule.FileName)}\nLast Update: {global.CalculateTimeSpan(global.UnixTimeStampToDateTime(API.api.apidata.timestamp))}\nNumber of swappable items: {API.api.apidata.items.Length}";
        }
        private void button1_Click(object sender, EventArgs e) => Close();


        public class Contributors
        {
            public string login { get; set; }
            public int id { get; set; }
            public string node_id { get; set; }
            public string avatar_url { get; set; }
            public string gravatar_id { get; set; }
            public string url { get; set; }
            public string html_url { get; set; }
            public string followers_url { get; set; }
            public string following_url { get; set; }
            public string gists_url { get; set; }
            public string starred_url { get; set; }
            public string subscriptions_url { get; set; }
            public string organizations_url { get; set; }
            public string repos_url { get; set; }
            public string events_url { get; set; }
            public string received_events_url { get; set; }
            public string type { get; set; }
            public bool site_admin { get; set; }
            public int contributions { get; set; }
        }

        private void label13_MouseDown(object sender, MouseEventArgs e) => global.MoveForm(e, Handle);

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            global.OpenUrl("https://github.com/Pro-Swapper/ProSwapper");
        }
    }
}
