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
namespace Pro_Swapper
{
    public partial class Main : Form
    {
        public static void ThrowError(string ex) => new Message("Error!", ex, true).ShowDialog();
        public static Icon appIcon = Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule.FileName);
        public Main()
        {
            InitializeComponent();
        }
        #region RoundedCorners
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse
        );
        #endregion
        #region LoadIcons
        private void LoadIcons()
        {
            CheckForIllegalCrossThreadCalls = false;
            pictureBox1.Image = global.ItemIcon("RKZPOXV.png");
            bunifuFlatButton1.Iconimage = global.ItemIcon("JCYJliG.png");
            bunifuFlatButton2.Iconimage = global.ItemIcon("0YAShwW.png");
            bunifuFlatButton3.Iconimage = global.ItemIcon("3kJgylm.png");
            bunifuFlatButton4.Iconimage = global.ItemIcon("s1uLZkY.png");
            bunifuFlatButton5.Iconimage = global.ItemIcon("frbu3Qj.png");
            bunifuFlatButton6.Iconimage = global.ItemIcon("CueE0Wg.png");
        }
        #endregion
        #region CloseFN
        private static void CloseFN()
        {
            bool asked = false;
            for(;;)
            {
                try
                {
                    Process[] procs = Process.GetProcesses();
                    string[] killdeez = { "easyanticheat", "fortnite", "epicgameslauncher", "unrealcefsubprocess", "fmodel"};
                    if (asked == false)
                    {
                        asked = true;
                        new Thread(() =>
                        {
                            Thread.CurrentThread.IsBackground = true;
                            foreach (Process a in procs)
                            {
                                if (a.ProcessName == "FortniteClient-Win64-Shipping")
                                    MessageBox.Show("Fortnite needs to be closed to use Pro Swapper!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (a.ProcessName == "EpicGamesLauncher")
                                    MessageBox.Show("Epic Games Launcher cannot be opened with Pro Swapper for safety measures. Close Pro Swapper to access Epic Games Launcher", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }).Start();
                    }
                    procs.Where(x => killdeez.Any(x.ProcessName.ToLower().StartsWith)).All(a => { a.Kill(); return true; });
                }
                catch
                { }
                Thread.Sleep(3000);
            }
        }
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
                panelContainer.Controls.Add(new UserControl(tab));//If not other tab use this one
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RPC.SetState("Dashboard", true);
            if (!panelContainer.Controls.Contains(Dashboard.Instance))
                panelContainer.Controls.Add(Dashboard.Instance);
            Dashboard.Instance.BringToFront();
        }
        public static void Cleanup() => Process.GetCurrentProcess().Kill();
        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                
                global.CreateDir(global.ProSwapperFolder);
                api.UpdateAPI();

                Console.WriteLine(Process.GetCurrentProcess().ProcessName);
                Console.WriteLine(Process.GetCurrentProcess().MainModule);
                string apiversion = api.apidata.version;
                string thishr = DateTime.Now.ToString("MMddHH");

                if (global.CurrentConfig.lastopened != thishr)
                {
                    global.OpenUrl(api.apidata.discordurl);
                    global.CurrentConfig.lastopened = thishr;
                }

                if (!apiversion.Contains(global.version)) //if outdated
                {
                    MessageBox.Show("New Pro Swapper Update found! Redirecting you to the new download!", "Pro Swapper Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    global.OpenUrl("https://linkvertise.com/86737/proswapper");
                    Cleanup();
                }


                if (global.IsNameModified())
                {
                    ThrowError($"This Pro Swapper version has been renamed, this means you have not downloaded it from the official source. Please redownload it on the Discord server at {API.api.apidata.discordurl}");
                    global.OpenUrl("https://linkvertise.com/86737/proswapper");
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
                new Thread(LoadIcons).Start();
                new Thread(CloseFN).Start();
                Icon = appIcon;
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
                SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                versionlabel.Text = global.version;
                if (!File.Exists(global.CurrentConfig.Paks + @"\pakchunk0-WindowsClient.sig"))
                    EpicGamesLauncher.FindPakFiles();

                panelContainer.Controls.Add(Dashboard.Instance);
                #region ThemeColors
                Control.ControlCollection panel1buttons = Controls["panel1"].Controls;
                foreach (BunifuFlatButton c in panel1buttons.OfType<BunifuFlatButton>().Where(c => c.Tag.Equals("TabButton")).Distinct())
                {
                        c.BackColor = global.Button;
                        // ((BunifuFlatButton)c).IconZoom = 85;
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
                
                /* Offsets aren't static so it doesnt matter about the user's Fortnite version
                if (!EpicGamesLauncher.InstalledFortniteVersion().Contains(API.api.apidata.fnver))
                    new Message("Hold Up!", $"Looks like there has recently been a Fortnite update and Pro Swapper hasn't been updated for that new version. Please check again later, also don't delete this program because it'll auto update :)\n\nDebug Info:\nInstalled Version: {EpicGamesLauncher.InstalledFortniteVersion()}\nAPI Version:{API.api.apidata.fnver}", true).ShowDialog();
                */
                global.SaveConfig();
            }
            catch (Exception ex)
            {
                ThrowError($"Source: {ex.Source} | Message: {ex.Message} | Stack Trace: {ex.StackTrace}");
            }
        }
    }
}
