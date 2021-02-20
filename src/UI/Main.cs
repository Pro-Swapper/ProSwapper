using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Pro_Swapper
{
    public partial class Main : Form
    {
        private static DiscordRpc.RichPresence presence;

        public static DiscordRpc.EventHandlers handlers;

        #region RoundedCorners
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        #endregion
        public static Icon appIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
        private static long opened { get; set; }
        public Main()
        {
            InitializeComponent();
            string[] data = global.ReadSetting(global.Setting.theme).Split(';');
            string[] panel1d = data[0].Split(',');
            string[] panel2d = data[1].Split(',');
            string[] panel3d = data[2].Split(',');
            string[] panel4d = data[3].Split(',');
            global.MainMenu = Color.FromArgb(255, int.Parse(panel1d[0]), int.Parse(panel1d[1]), int.Parse(panel1d[2]));
            global.ItemsBG = Color.FromArgb(255, int.Parse(panel2d[0]), int.Parse(panel2d[1]), int.Parse(panel2d[2]));
            global.Button = Color.FromArgb(255, int.Parse(panel3d[0]), int.Parse(panel3d[1]), int.Parse(panel3d[2]));
            global.TextColor = Color.FromArgb(255, int.Parse(panel4d[0]), int.Parse(panel4d[1]), int.Parse(panel4d[2]));

            InitializeDiscord("697579712653819985");
            new Thread(LoadIcons).Start();
            new Thread(detectfn).Start();
            new Thread(CloseFN).Start();
            Icon = appIcon;
            opened = DateTimeToTimestamp(DateTime.UtcNow);
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));            
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            versionlabel.Text = global.version;
            if (!File.Exists(global.ReadSetting(global.Setting.Paks) + @"\pakchunk0-WindowsClient.sig"))
                FindPakFiles();


            RPC("Dashboard");
            panelContainer.Controls.Add(Dashboard.Instance);
            panelContainer.Controls.Add(OtherTab.Instance);
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
                      
        }
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
            try
            {
                foreach (Process a in Process.GetProcesses())
                {
                    string b = a.ProcessName.ToLower();
                    if (b.Contains("easyanticheat") | b.StartsWith("fortniteclient") | b.StartsWith("epicgameslauncher") | a.ProcessName.Contains("UnrealCEFSubProcess") | a.ProcessName.Equals("umodel") | a.ProcessName.Equals("FModel"))
                    {
                        if (a.ProcessName == "FortniteClient-Win64-Shipping")
                        {
                            MessageBox.Show("Closed Fortnite (Fortnite needs to be closed to use Pro Swapper)!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        a.Kill();
                    }
                }
            }
            catch
            {}    
        }
        #endregion
        #region DiscordRPC
        private static void InitializeDiscord(string clientid)
        {
            handlers = new DiscordRpc.EventHandlers();
            DiscordRpc.Initialize(clientid, ref handlers, true, null);
        }
        public static void RPC(string Tab)
        {
            presence.details = "Pro Swapper " + global.version;
            presence.state = Convert.ToString(Program.apidata.discordurl).Replace("https://", "")   + "| Watching " + Tab;
            presence.startTimestamp = opened;
            presence.largeImageKey = "logo";
            presence.largeImageText = "Pro Swapper";
            presence.smallImageKey = "kyenew";
            presence.smallImageText = "Made by Kye#5000";

            DiscordRpc.UpdatePresence(ref presence);
        }

        private long DateTimeToTimestamp(DateTime dt)
        {
            return (dt.Ticks - 621355968000000000) / 10000000;
        }
        #endregion
        private void NewPanel(string tab)
        {
            RPC(tab);
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(new UserControl(tab));
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e) => Cleanup();
        private void bunifuFlatButton1_Click(object sender, EventArgs e) => NewPanel("Skins");
        private void bunifuFlatButton2_Click(object sender, EventArgs e) => NewPanel("Backblings");
        private void bunifuFlatButton3_Click(object sender, EventArgs e)=> NewPanel("Pickaxes");
        private void bunifuFlatButton4_Click(object sender, EventArgs e)=> NewPanel("Emotes");

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            RPC("Other");
            panelContainer.Controls.Clear();
            if (!panelContainer.Controls.Contains(OtherTab.Instance))
                panelContainer.Controls.Add(OtherTab.Instance);

            OtherTab.Instance.BringToFront();
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e) => new Settings().Show();
        private void button1_Click(object sender, EventArgs e) => Cleanup();
        private void button2_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;


        public class InstallationList
        {
            public string InstallLocation { get; set; }
            public string AppName { get; set; }
         //   public string AppVersion { get; set; }
        }

        public class Root
        {
            public List<InstallationList> InstallationList { get; set; }
        }
        private void FindPakFiles()
        {
            
            string datfile = "";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Epic\UnrealEngineLauncher\LauncherInstalled.dat";
            if (File.Exists(path))
                datfile = path;
            else
                datfile = "";

            string error = "Could not find your pak files! Please select them manually!";
            if (datfile != "")
            {
                try
                {
                    foreach (var d in new JavaScriptSerializer().Deserialize<Root>(File.ReadAllText(datfile)).InstallationList)
                    {
                        if (d.AppName == "Fortnite")
                            global.WriteSetting(d.InstallLocation + @"\FortniteGame\Content\Paks", global.Setting.Paks);
                    }
                }
                catch
                {
                    
                    MessageBox.Show(error, "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(error, "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RPC("Dashboard");
            if (!panelContainer.Controls.Contains(Dashboard.Instance))
                panelContainer.Controls.Add(Dashboard.Instance);

            Dashboard.Instance.BringToFront();
        }
        public static void Cleanup()
        {
            DiscordRpc.Shutdown();
            Application.Exit();
            Environment.Exit(0);
        }
        private void detectfn()
        {
            while (true)
            {
                Thread.Sleep(10000);
                foreach (Process a in Process.GetProcessesByName("FortniteClient-Win64-Shipping"))
                {
                    if (a.ProcessName == "FortniteClient-Win64-Shipping")
                    {
                        MessageBox.Show("Fortnite has been detected as opened! Closing Pro Swapper as it can be closed while playing Fortnite!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Cleanup();
                        break;
                    }
                }
            }
        }
    }
}
