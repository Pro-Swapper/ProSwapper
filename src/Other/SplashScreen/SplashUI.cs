using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Newtonsoft.Json;
using System.Net;
namespace Pro_Swapper.Splash
{
    public partial class SplashUI : Form
    {
        SplashSettings splashSettings1 = new SplashSettings();
        private bool IsOnSettings = false;
        public static readonly string SplashPath = global.CurrentConfig.Paks.Replace(@"Content\Paks", @"Binaries\Win64\EasyAntiCheat\Launcher\");
        public SplashUI()
        {
            InitializeComponent();
            Icon = Main.appIcon;
            Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            Controls.Add(splashSettings1);
            splashSettings1.Size = items.Size;
            splashSettings1.Location = items.Location;
            splashSettings1.SendToBack();
            BackColor = global.MainMenu;
            button2.BackColor = global.Button;
            button3.BackColor = global.Button;
            foreach (string image in api.GetImgurUrls())
            {
                PictureBox picturebox = new PictureBox();
                picturebox.Cursor = Cursors.Hand;
                new Thread(() =>
                {
                    picturebox.Image = global.ItemIcon(image);
                }).Start();
                picturebox.Location = new Point(3, 3);
                picturebox.Size = new Size(196, 129);
                picturebox.SizeMode = PictureBoxSizeMode.Zoom;
                picturebox.TabIndex = 1;
                picturebox.TabStop = false;
                picturebox.Click += delegate
                {
                    try
                    {
                        File.Copy(global.ProSwapperFolder + @"Images\" + image, SplashPath + "SplashScreen.png", true);
                        MessageBox.Show("Set Splash Screen!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Your Fortnite game folder could not be found! Please contact support and give them this info: {Directory.Exists(SplashPath)} | {SplashPath} | {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
                items.Controls.Add(picturebox);
            }
        }
        private void ThemeCreator_MouseDown(object sender, MouseEventArgs e)=> global.MoveForm(e, Handle);
        private void button1_Click(object sender, EventArgs e) => Close();
        private void CustomSplash(object sender, EventArgs e)
        {
            using (OpenFileDialog a = new OpenFileDialog())
            {
                if (a.ShowDialog() == DialogResult.OK)
                {
                    Image splash = ResizeImage(Image.FromFile(a.FileName), 640,360);
                    splash.Save(SplashPath + "SplashScreen.png");
                    MessageBox.Show("Set Splash Screen!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #region ResizeImage
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        #endregion
      


        private class api
        {
            public class imgapi
            {
                public string[] url { get; set; }
            }
            private static readonly string[] hosturls = { "https://pro-swapper.github.io/api/splashscreen.json", "https://raw.githubusercontent.com/Pro-Swapper/api/main/splashscreen.json" };
            public static string[] GetImgurUrls()
            {
                int url = 0;
                string data = string.Empty;
            redo: try
                {
                    data = new WebClient().DownloadString(hosturls[url]);
                }
                catch
                {
                    url++;
                    goto redo;
                }
                return JsonConvert.DeserializeObject<imgapi>(data).url;
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (IsOnSettings)
            {
                IsOnSettings = false;
                items.BringToFront();
                button2.Text = "Edit Splash Screen Settings";
            }
            else
            {
                IsOnSettings = true;
                splashSettings1.BringToFront();
                button2.Text = "Choose Splash Screen";
            }
        }

        private void SplashUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            splashSettings1.Save();
        }
    }
}
