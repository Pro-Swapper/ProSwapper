using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using DiscordRPC;
using Bunifu.Framework.UI;
namespace Pro_Swapper
{
    public partial class Main : Form
    {
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
        #region FormMoveable
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public static void FormMove(IntPtr Handle)
        {
            ReleaseCapture();
            SendMessage(Handle, 0xA1, 0x2, 0);
        }
        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                FormMove(Handle);
        }
        #endregion
        #region CloseFN
        public static void CloseFN()
        {
            while (true)
            {
                try
                {
                    foreach (Process a in Process.GetProcesses())
                    {
                        string b = a.ProcessName.ToLower();
                        if (b.StartsWith("easyanticheat") | b.StartsWith("fortnite") |  b.StartsWith("epicgameslauncher") |b.Contains("unrealcefsubprocess") | b.Equals("umodel") | b.Equals("fmodel"))
                            a.Kill();
                        if (a.ProcessName == "FortniteClient-Win64-Shipping")
                            MessageBox.Show("Fortnite needs to be closed to use Pro Swapper!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (a.ProcessName == "EpicGamesLauncher")
                            MessageBox.Show("Epic Games Launcher cannot be opened with Pro Swapper for safety measures. Close Pro Swapper to access Epic Games Launcher", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch
                { }
                Thread.Sleep(1500);
            }
               
        }
        #endregion
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
        public static void Cleanup()
        {
            RPC.client.Dispose();
            Process.GetCurrentProcess().Kill();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                global.CreateDir(global.ProSwapperFolder);
                api.UpdateAPI();
                string apiversion = api.apidata.version;
                string thishr = DateTime.Now.ToString("MMddHH");

                if (global.ReadSetting(global.Setting.lastopened) != thishr)
                {
                    string discordurl = api.apidata.discordurl;
                    Process.Start(discordurl);
                    global.WriteSetting(thishr, global.Setting.lastopened);
                }

                if (!apiversion.Contains(global.version)) //if outdated
                {
                    MessageBox.Show("New Pro Swapper Update found! Redirecting you to the new download!", "Pro Swapper Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start("https://linkvertise.com/86737/proswapper");
                    Cleanup();
                }
                
                string filename = AppDomain.CurrentDomain.FriendlyName;
                if (!filename.Contains("Pro") && !filename.Contains("Swapper"))
                    ThrowError("This version of Pro Swapper has been modified (renamed) " + filename + " , please download the original Pro Swapper on the Discord server");


                if (apiversion.Contains("OFFLINE"))
                    ThrowError("Pro Swapper is currently not working. Take a look at our Discord for any announcments");

                
                string[] data = global.ReadSetting(global.Setting.theme).Split(';');
                string[] panel1d = data[0].Split(',');
                string[] panel2d = data[1].Split(',');
                string[] panel3d = data[2].Split(',');
                string[] panel4d = data[3].Split(',');
                global.MainMenu = Color.FromArgb(255, int.Parse(panel1d[0]), int.Parse(panel1d[1]), int.Parse(panel1d[2]));
                global.ItemsBG = Color.FromArgb(255, int.Parse(panel2d[0]), int.Parse(panel2d[1]), int.Parse(panel2d[2]));
                global.Button = Color.FromArgb(255, int.Parse(panel3d[0]), int.Parse(panel3d[1]), int.Parse(panel3d[2]));
                global.TextColor = Color.FromArgb(255, int.Parse(panel4d[0]), int.Parse(panel4d[1]), int.Parse(panel4d[2]));

                RPC.rpctimestamp = Timestamps.Now;
                RPC.InitializeRPC();
                new Thread(LoadIcons).Start();
                new Thread(CloseFN).Start();
                Icon = appIcon;
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
                SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                versionlabel.Text = global.version;
                if (!File.Exists(global.ReadSetting(global.Setting.Paks) + @"\pakchunk0-WindowsClient.sig"))
                    EpicGamesLauncher.FindPakFiles();

                panelContainer.Controls.Add(Dashboard.Instance);
                #region ThemeColors
                BackColor = global.MainMenu;
                panel1.BackColor = global.MainMenu;
                bunifuFlatButton1.BackColor = global.Button;
                bunifuFlatButton1.IconZoom = 85;
                bunifuFlatButton1.Normalcolor = global.Button;
                bunifuFlatButton1.OnHovercolor = global.Button;
                bunifuFlatButton1.Activecolor = global.Button;
                bunifuFlatButton1.Textcolor = global.TextColor;
                bunifuFlatButton1.OnHoverTextColor = global.TextColor;
                bunifuFlatButton2.BackColor = global.Button;
                bunifuFlatButton2.Normalcolor = global.Button;
                bunifuFlatButton2.OnHovercolor = global.Button;
                bunifuFlatButton2.Activecolor = global.Button;
                bunifuFlatButton2.Textcolor = global.TextColor;
                bunifuFlatButton2.OnHoverTextColor = global.TextColor;
                bunifuFlatButton2.IconZoom = 90;
                bunifuFlatButton3.BackColor = global.Button;
                bunifuFlatButton3.Normalcolor = global.Button;
                bunifuFlatButton3.OnHovercolor = global.Button;
                bunifuFlatButton3.Activecolor = global.Button;
                bunifuFlatButton3.Textcolor = global.TextColor;
                bunifuFlatButton3.OnHoverTextColor = global.TextColor;
                bunifuFlatButton3.IconZoom = 90;
                bunifuFlatButton4.BackColor = global.Button;
                bunifuFlatButton4.Normalcolor = global.Button;
                bunifuFlatButton4.OnHovercolor = global.Button;
                bunifuFlatButton4.Activecolor = global.Button;
                bunifuFlatButton4.Textcolor = global.TextColor;
                bunifuFlatButton4.OnHoverTextColor = global.TextColor;
                bunifuFlatButton4.IconZoom = 100;
                bunifuFlatButton5.BackColor = global.Button;
                bunifuFlatButton5.Normalcolor = global.Button;
                bunifuFlatButton5.OnHovercolor = global.Button;
                bunifuFlatButton5.Activecolor = global.Button;
                bunifuFlatButton5.Textcolor = global.TextColor;
                bunifuFlatButton5.OnHoverTextColor = global.TextColor;
                bunifuFlatButton5.IconZoom = 90;
                bunifuFlatButton6.BackColor = global.Button;
                bunifuFlatButton6.Normalcolor = global.Button;
                bunifuFlatButton6.OnHovercolor = global.Button;
                bunifuFlatButton6.Activecolor = global.Button;
                bunifuFlatButton6.Textcolor = global.TextColor;
                bunifuFlatButton6.OnHoverTextColor = global.TextColor;
                bunifuFlatButton6.IconZoom = 85;
                versionlabel.ForeColor = global.TextColor;
                #endregion
                if (!EpicGamesLauncher.InstalledFortniteVersion().Contains(api.apidata.fnver))
                {
                    new Message("Hold Up!", $"Looks like there has recently been a Fortnite update and Pro Swapper hasn't been updated for that new version. Please check again later, also don't delete this program because it'll auto update :)\n\nDebug Info:\nInstalled Version: {EpicGamesLauncher.InstalledFortniteVersion()}\nAPI Version:{api.apidata.fnver}", true).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ThrowError(ex.Message);
            }
        }
        public static void ThrowError(string ex) => new Message("Error!", ex, true).ShowDialog();
        private void Main_FormClosing(object sender, FormClosingEventArgs e) => Cleanup();
        private void UserControlPanelClick(object sender, EventArgs e) => NewPanel(((BunifuFlatButton)sender).Text);//Uses textbox name for usercontrol
        private void bunifuFlatButton6_Click(object sender, EventArgs e) => new Settings().Show();
        private void button2_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
        public static Icon appIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
        private void ExitButton_Click(object sender, EventArgs e) => Cleanup();
    }
}
