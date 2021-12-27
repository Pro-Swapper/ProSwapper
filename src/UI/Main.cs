using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using DiscordRPC;
using Bunifu.Framework.UI;
using Pro_Swapper.API;

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
                RPC.InitializeRPC();

                Icon = appIcon;
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
                versionlabel.Text = global.version;
                
                panelContainer.Controls.Add(Dashboard.Instance);
                Control.ControlCollection MenuButtons = Controls["panel1"].Controls;
                for (int i = 0; i < MenuButtons.Count; i++)
                {
                    if (MenuButtons[i].GetType().Name == "BunifuFlatButton" && MenuButtons[i].Tag.Equals("TabButton"))
                    {
                        BunifuFlatButton btn = (BunifuFlatButton)MenuButtons[i];
                        btn.BackColor = btn.Normalcolor = btn.OnHovercolor = btn.Activecolor = global.Button;
                        btn.Textcolor = btn.OnHoverTextColor = global.TextColor;
                        if (btn.Text == "Settings")
                            continue;
                        
                        btn.Click += delegate
                        {
                            NewPanel(btn.Text);
                        };
                    }
                }
                
                BackColor = global.MainMenu;
                panel1.BackColor = global.MainMenu;
                versionlabel.ForeColor = global.TextColor;
                global.SaveConfig();
                splash.Invoke(new Action(() => { splash.Close(); }));
            }
            catch (Exception ex)
            {
                Program.logger.LogError(ex.Message);
                ThrowError($"Source: {ex.Source} | Message: {ex.Message} | Stack Trace: {ex.StackTrace}", true);
            }
        }
        #region RoundedCorners
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse
        );
        #endregion
        
        private void Main_MouseDown(object sender, MouseEventArgs e)=> global.MoveForm(e, Handle);
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
        public static void Cleanup() 
        {
            Program.logger.Log("Closing Pro Swapper");
            Program.logger.Dispose();
            RPC.client.Dispose();
            Process.GetCurrentProcess().Kill();
        } 
        private void Main_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.TopMost = false;
        }
        private void ExitButton_Click(object sender, EventArgs e)=> Cleanup();
        private void Main_FormClosing(object sender, FormClosingEventArgs e)=> Cleanup();
        private void button2_Click(object sender, EventArgs e)=> WindowState = FormWindowState.Minimized;

        private void bunifuFlatButton6_Click(object sender, EventArgs e)=> new Settings().Show();

    }
}
