using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.Diagnostics;
using DiscordRPC;
using System.Linq;
using Bunifu.Framework.UI;
using Pro_Swapper.API;
using System.Threading.Tasks;

namespace Pro_Swapper
{
    public partial class Main : Form
    {
        public static void ThrowError(string ex, bool close = true) => new Message("Error!", ex, close).ShowDialog();
        public static Icon appIcon = Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule.FileName);

        public static Form Mainform;
        
        public Main(UI.Splash splash)
        {
            InitializeComponent();
            Mainform = this;
            try
            {
                api.UpdateAPI();
                string apiversion = api.apidata.version;

                double TimeNow = global.GetEpochTime();

                if (global.CurrentConfig.lastopened + 7200 < TimeNow)
                {
                    global.OpenUrl(api.apidata.discordurl);
                    global.CurrentConfig.lastopened = TimeNow;
                }

                int thisVer = int.Parse(global.version.Replace(".", ""));
                int apiVer = int.Parse(api.apidata.version.Replace(".", ""));

                const string NewDownload = "https://linkvertise.com/86737/proswapper";

                if (apiVer > thisVer)
                {
                    MessageBox.Show("New Pro Swapper Update found! Redirecting you to the new download!", "Pro Swapper Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    global.OpenUrl(NewDownload);
                    Cleanup();
                }

                if (global.IsNameModified())
                {
                    ThrowError($"This Pro Swapper version has been renamed, this means you have not downloaded it from the official source. Please redownload it on the Discord server at {api.apidata.discordurl}");
                    global.OpenUrl(NewDownload);
                }

                if (api.apidata.status[0].IsUp == false)
                    ThrowError(api.apidata.status[0].DownMsg);

                Color[] theme = global.CurrentConfig.theme;
                global.MainMenu = theme[0];
                global.ItemsBG = theme[1];
                global.Button = theme[2];
                global.TextColor = theme[3];
                RPC.rpctimestamp = Timestamps.Now;
                RPC.InitializeRPC();

                Icon = appIcon;
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
                versionlabel.Text = global.version;
                


                panelContainer.Controls.Add(Dashboard.Instance);
                #region ThemeColors
                Control.ControlCollection panel1buttons = Controls["panel1"].Controls;
                foreach (BunifuFlatButton c in panel1buttons.OfType<BunifuFlatButton>().Where(c => c.Tag.Equals("TabButton")).Distinct())
                {
                    c.BackColor = global.Button;
                    c.Normalcolor = global.Button;
                    c.OnHovercolor = global.Button;
                    c.Activecolor = global.Button;
                    c.Textcolor = global.TextColor;
                    c.OnHoverTextColor = global.TextColor;
                    if (c.Text == "Settings")
                        c.Click += delegate
                        {
                            new Settings().Show();
                        };
                    else
                        c.Click += delegate
                        {
                            NewPanel(c.Text);
                        };
                }
                FormClosing += delegate
                {
                    Cleanup();
                };
                ExitButton.Click += delegate
                {
                    Cleanup();
                };
                button2.Click += delegate
                {
                    WindowState = FormWindowState.Minimized;
                };

                BackColor = global.MainMenu;
                panel1.BackColor = global.MainMenu;
                versionlabel.ForeColor = global.TextColor;
                #endregion
                global.SaveConfig();
                
            }
            catch (Exception ex)
            {
                ThrowError($"Source: {ex.Source} | Message: {ex.Message} | Stack Trace: {ex.StackTrace}");
            }

            Action safeClose = delegate { splash.Close(); };
            splash.Invoke(safeClose);
            
        }
        #region RoundedCorners
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse
        );
        #endregion
        
        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) global.FormMove(Handle);
        }
        private void NewPanel(string tab)
        {
            RPC.SetState(tab, true);
            panelContainer.Controls.Clear();
            if (tab == "Other")
                panelContainer.Controls.Add(new OtherTab());
            else 
                panelContainer.Controls.Add(new ItemTab(tab));//If not other tab use this one
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RPC.SetState("Dashboard", true);
            if (!panelContainer.Controls.Contains(Dashboard.Instance))
                panelContainer.Controls.Add(Dashboard.Instance);
            Dashboard.Instance.BringToFront();
        }
        public static void Cleanup() => Process.GetCurrentProcess().Kill();
        private void Main_Load(object sender, EventArgs e)=> Activate();//Bring forward
    }
}
