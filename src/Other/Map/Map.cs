using System;
using System.Net;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;

namespace Pro_Swapper.UI
{
    public partial class Map : Form
    {
        private const string MapEndpoint = "https://fortnite-api.com/images/";
        private Image Map1;
        private Image Map2;
        public Map()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = global.MainMenu;
            Icon = Main.appIcon;
            Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, Width, Height, 50,50));
            WindowState = FormWindowState.Normal;
        }

        private void Splash_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) global.FormMove(Handle);
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            Map1 = Task.Run(() => UrlToImage($"{MapEndpoint}map.png")).Result;
            Map2 = Task.Run(() => UrlToImage($"{MapEndpoint}map_en.png")).Result;
            pictureBox1.Image = Map1;
        }

        private Image UrlToImage(string url)
        {
            using (WebClient web = new WebClient() { Proxy = null })
            {
                Task<Stream> stream = Task.Run(() => web.OpenRead(url));
                return Image.FromStream(stream.Result);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                pictureBox1.Image = Map2;
            else
                pictureBox1.Image = Map1;
        }
    }
}
