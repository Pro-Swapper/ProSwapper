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
        private static Image Map1 = null;
        private static Image Map2 = null;
        public Map()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = global.MainMenu;
            Icon = Main.appIcon;
            Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
            WindowState = FormWindowState.Normal;
        }

        private void Splash_MouseDown(object sender, MouseEventArgs e) => global.MoveForm(e, Handle);

        private async void Splash_Load(object sender, EventArgs e)
        {
            if (Map1 == null)
                Map1 = await UrlToImage($"{MapEndpoint}map.png");
            if (Map2 == null)
                Map2 = await UrlToImage($"{MapEndpoint}map_en.png");

            pictureBox1.Image = Map1;
        }

        private static async Task<Image> UrlToImage(string url)
        {
            Stream stream = await Program.httpClient.GetStreamAsync(url);
            var image = await Task.Run(() => Image.FromStream(stream));
            return image;
        }

        private void ExitButton_Click(object sender, EventArgs e) => Close();

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                pictureBox1.Image = Map2;
            else
                pictureBox1.Image = Map1;
        }
    }
}
