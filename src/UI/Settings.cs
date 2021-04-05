using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;
namespace Pro_Swapper
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            RPC.SetState("Settings", true);
            Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
            Icon = Main.appIcon;

            new Thread(() =>
            {
                CheckForIllegalCrossThreadCalls = false;
                Thread.CurrentThread.IsBackground = true;
                Thread.CurrentThread.Priority = ThreadPriority.Highest;

                pictureBox7.Image = global.ItemIcon("0G7O3O2.png");
                pictureBox1.Image = global.ItemIcon("8Z9xRUU.png");
                pictureBox2.Image = global.ItemIcon("EHbHFjp.png");
                discord.Image = global.ItemIcon("ECo8w6F.png");
            }).Start();
        }
        private void button1_Click(object sender, EventArgs e) => Close();
        private void SettingsForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Main.FormMove(Handle);
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog paks = new FolderBrowserDialog())
            {
                paks.RootFolder = Environment.SpecialFolder.MyComputer;
                paks.Description = "Select your Fortnite paks folder. By default it's located in C:\\Program Files\\Epic Games\\Fortnite\\FortniteGame\\Content\\Paks";
                paks.ShowNewFolderButton = false;
                paks.ShowDialog();
                paksBox.Text = paks.SelectedPath;
                global.WriteSetting(paks.SelectedPath, global.Setting.Paks);
            }
        }
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            paksBox.Text = global.ReadSetting(global.Setting.Paks);
            BackColor = global.MainMenu;
            button2.BackColor = global.Button;
            button2.ForeColor = global.TextColor;
            
            button3.BackColor = global.Button;
            button3.ForeColor = global.TextColor;
            button4.BackColor = global.Button;
            button4.ForeColor = global.TextColor;


            button10.BackColor = global.Button;
            button10.ForeColor = global.TextColor;

            button9.BackColor = global.Button;
            button9.ForeColor = global.TextColor;

            button7.BackColor = global.Button;
            button7.ForeColor = global.TextColor;
            Restart.BackColor = global.Button;
            Restart.ForeColor = global.TextColor;
            label13.ForeColor = global.TextColor;
            label1.ForeColor = global.TextColor;
        }

        private void button2_Click(object sender, EventArgs e) => Process.Start(paksBox.Text);
        private void Restart_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.FriendlyName);
            Main.Cleanup();
        }
        private const string epicfnpath = "com.epicgames.launcher://apps/Fortnite?action=";
        private void button9_Click(object sender, EventArgs e)
        {
            Process.Start(epicfnpath + "launch");
            Main.Cleanup();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to verify Fortnite and revert your files to how they were before you used the swapper?", "Fortnite Verification", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Process.Start(epicfnpath + "verify");
                global.WriteSetting("", global.Setting.swaplogs);
                Main.Cleanup();
            }            
        }

        void CleanupFiles()
        {
            string pakslocation = global.ReadSetting(global.Setting.Paks) + "\\";
            string[] fileext = { "ucas", "pak", "utoc", "sig" };
            foreach (Items.Item item in global.items.Items)
            {
                if (item.New > 0)
                {
                    foreach (string ext in fileext)
                    {
                        File.Delete(pakslocation + $"pakchunk10_s{item.New}-WindowsClient.{ext}");
                    }
                }
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            CleanupFiles();
            global.WriteSetting("", global.Setting.swaplogs);
            MessageBox.Show("All configs for item reset! Now all items will show as OFF (This button should be used after verifying Fortnite)", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button10_Click(object sender, EventArgs e) => new ThemeCreator().ShowDialog();
        private void pictureBox1_Click(object sender, EventArgs e)=> Process.Start("https://youtube.com/proswapperofficial");
        private void pictureBox2_Click(object sender, EventArgs e)=> Process.Start("https://twitter.com/Pro_Swapper");
        private void ConvertedItemsList(object sender, EventArgs e)
        {
            int converteditemno = global.ReadSetting(global.Setting.swaplogs).Length - global.ReadSetting(global.Setting.swaplogs).Replace(",", "").Length;
            if (converteditemno > 0)
                MessageBox.Show("You currently have " + converteditemno + " item(s) converted. The items you have converted are: " + global.ReadSetting(global.Setting.swaplogs), "Converted Items List", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("You have no items converted!", "Converted Items List", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void discord_Click(object sender, EventArgs e)=> Process.Start(Convert.ToString(api.apidata.discordurl));
        private void button5_Click(object sender, EventArgs e)
        {
            new Message("Credits And About", $"Pro Swapper made by Kye#5000. https://github.com/kyeondiscord. Credit to Tamely & Smoonthie for new Fortnite Swapping Method(s) \n\n\n\nProduct Information:\nLicense: MIT\nVersion: {global.version}\nMD5: {global.FileToMd5(AppDomain.CurrentDomain.FriendlyName)}\nLast Update: {CalculateTimeSpan(UnixTimeStampToDateTime(long.Parse(api.apidata.timestamp)))}", false).ShowDialog();
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp).ToUniversalTime();
        }

        private static string CalculateTimeSpan(DateTime dt)
        {
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - dt.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 60)
            {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }
            if (delta < 60 * 2)
            {
                return "a minute ago";
            }
            if (delta < 45 * 60)
            {
                return ts.Minutes + " minutes ago";
            }
            if (delta < 90 * 60)
            {
                return "an hour ago";
            }
            if (delta < 24 * 60 * 60)
            {
                return ts.Hours + " hours ago";
            }
            if (delta < 48 * 60 * 60)
            {
                return "yesterday";
            }
            if (delta < 30 * 24 * 60 * 60)
            {
                return ts.Days + " days ago";
            }
            if (delta < 12 * 30 * 24 * 60 * 60)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }
    }
}