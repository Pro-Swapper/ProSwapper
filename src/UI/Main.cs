using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Pro_Swapper.API;
using System.Collections.Generic;
using System.Linq;
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
                Color[] theme = global.CurrentConfig.theme;
                global.MainMenu = theme[0];
                global.ItemsBG = theme[1];
                global.Button = theme[2];
                global.TextColor = theme[3];

                Icon = appIcon;
                Region = Native.RoundedFormRegion(Width, Height);
                versionlabel.Text = global.version;

                panelContainer.Controls.Add(Dashboard.Instance);
                Control.ControlCollection MenuButtons = Controls["panel1"].Controls;
                for (int i = 0; i < MenuButtons.Count; i++)
                {
                    if (MenuButtons[i].GetType().Name == "Button" && MenuButtons[i].Tag.Equals("TabButton"))
                    {
                        Button btn = (Button)MenuButtons[i];
                        btn.BackColor = global.Button;
                        btn.ForeColor = Color.White;
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


        private void Main_MouseDown(object sender, MouseEventArgs e) => global.MoveForm(e, Handle);

        public static List<ItemTab> tabs = new();
        public static OtherTab otherTab = new OtherTab();
        private void NewPanel(string tab)
        {
            RPC.SetState(tab, true);
            panelContainer.Controls.Clear();

            if (!tabs.Any(x => x.Tab.Contains(tab)))//Uses the 'Tab' value in each ItemTab to check if this tab has been added or not.
            {
                if (tab != "Other")
                    tabs.Add(new ItemTab(tab));
            }


            if (tab == "Other")
                panelContainer.Controls.Add(otherTab);
            else
                panelContainer.Controls.Add(tabs.First(x => x.Tab == tab));

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
        private void ExitButton_Click(object sender, EventArgs e) => Cleanup();
        private void Main_FormClosing(object sender, FormClosingEventArgs e) => Cleanup();
        private void button2_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
        private void button6_Click(object sender, EventArgs e) => new Settings().Show();
    }
}
