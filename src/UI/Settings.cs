using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Pro_Swapper.API;
using System.Net.NetworkInformation;
using static Pro_Swapper.API.api;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
            #if DEBUG
            OodleCompressBtn.Visible = true;
            #endif
        }
        private void button1_Click(object sender, EventArgs e)
        {
            global.SaveConfig();
            Close();
        }
        private void SettingsForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) global.FormMove(Handle);
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
                global.CurrentConfig.Paks = paks.SelectedPath;
                global.SaveConfig();
            }
        }
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            paksBox.Text = global.CurrentConfig.Paks;
            BackColor = global.MainMenu;

            button1.BackColor = global.MainMenu;
            button1.ForeColor = global.TextColor;

            button2.BackColor = global.Button;
            button2.ForeColor = global.TextColor;
            

            button3.BackColor = global.Button;
            button3.ForeColor = global.TextColor;
            button4.BackColor = global.Button;
            button4.ForeColor = global.TextColor;

            button6.BackColor = global.Button;
            button6.ForeColor = global.TextColor;

            button10.BackColor = global.Button;
            button10.ForeColor = global.TextColor;

            button9.BackColor = global.Button;
            button9.ForeColor = global.TextColor;

            button5.BackColor = global.Button;
            button5.ForeColor = global.TextColor;

            button7.BackColor = global.Button;
            button7.ForeColor = global.TextColor;
            Restart.BackColor = global.Button;
            Restart.ForeColor = global.TextColor;
            label13.ForeColor = global.TextColor;
            label1.ForeColor = global.TextColor;
            checkPing.BackColor = global.Button;
            AesKeySourceComboBox.BackColor = global.Button;
            AesKeySourceComboBox.ForeColor = global.TextColor;
            AesKeySourceComboBox.DataSource = Enum.GetNames(typeof(api.AESSource));
            AesKeySourceComboBox.Text = global.CurrentConfig.AESSource.ToString();

            if (global.CurrentConfig.AESSource == AESSource.Manual)
            {
                
                manualAES.Visible = true;
                manualAESLabel.Visible = true;
                checkPing.Visible = false;
                manualAES.Text = global.CurrentConfig.ManualAESKey;
            }
        }

        private void button2_Click(object sender, EventArgs e) => Process.Start("explorer.exe", paksBox.Text);
        private void Restart_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.FriendlyName);
            Main.Cleanup();
        }
        private const string epicfnpath = "com.epicgames.launcher://apps/Fortnite?action=";
        private void button9_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            notifyIcon1.Icon = Main.appIcon;
            notifyIcon1.Text = "Pro Swapper";
            notifyIcon1.Click += NotifyIcon1_Click;
            global.OpenUrl($"{epicfnpath}launch");
            this.Hide();
            Action safeClose = delegate { Main.Mainform.Hide(); };
            Main.Mainform.Invoke(safeClose);
            Process fngame = null;
            
            //Basically define fngame proc "searcher"
            while (fngame == null)
            {
                Process[] thisproc = Process.GetProcessesByName("FortniteClient-Win64-Shipping");
                if (thisproc.Length > 0)
                        fngame = thisproc[0];

                Thread.Sleep(1000);//Person probs gonna have the game opened for over 10 seconds yk yk
            }

            fngame.WaitForExit();
            Task.Run(() => KillEpic());
            Action safeShow = delegate { Main.Mainform.Show(); };
            Main.Mainform.Invoke(safeShow);
            this.Show();
            this.BringToFront();
        }

        private void NotifyIcon1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pro Swapper is currently running while playing Fortnite so it closes Epic Games Launcher when you finish playing. If you want to close Pro Swapper end the process from task manager :'(", "Pro Swapper Notify Icon", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void KillEpic()=> Process.GetProcessesByName("EpicGamesLauncher").All(x => { x.Kill(); return true; });
        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to verify Fortnite and revert your files to how they were before you used the swapper?", "Fortnite Verification", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Lobby.RevertAllLobbySwaps(true);
                RevertAllSwaps();
                global.CurrentConfig.swaplogs = "";
                global.SaveConfig();
                global.OpenUrl($"{epicfnpath}verify");
                Main.Cleanup();
            }            
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            global.CurrentConfig.swaplogs = "";
            global.SaveConfig();
            MessageBox.Show("All configs for item reset! Now all items will show as OFF (This button should be used after verifying Fortnite)", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button10_Click(object sender, EventArgs e) => new ThemeCreator().ShowDialog();
        private void button5_Click(object sender, EventArgs e)
        {
            List<string> PaksInfo = new List<string>(Directory.GetFiles(global.CurrentConfig.Paks, "*", SearchOption.AllDirectories));
            for (int i = 0; i < PaksInfo.Count; i++)
            {
                try
                {
                    PaksInfo[i] = PaksInfo[i].Substring(PaksInfo[i].IndexOf("FortniteGame"));
                }
                catch {}
            }
            PaksInfo.Insert(0, $"Paks Information ({PaksInfo.Count}) Files: ");
            string paksinfo = string.Join("\n", PaksInfo);
            new Message("Credits And About", $"Pro Swapper made by Kye#5000. https://github.com/kyeondiscord. \nSource Code: https://github.com/Pro-Swapper/ProSwapper \nCredit to Tamely & Smoonthie for new Fortnite Swapping Method(s) \n\n\n\nProduct Information:\nLicense: MIT\nCopyright (©) 2019 - {DateTime.Now.ToString("yyyy")} Pro Swapper\nVersion: {global.version}\nMD5: {global.FileToMd5(Process.GetCurrentProcess().MainModule.FileName)}\nLast Update: {CalculateTimeSpan(UnixTimeStampToDateTime(apidata.timestamp))}\nNumber of swappable items: {apidata.items.Length}\n\n\n{paksinfo}", false).ShowDialog();

        }
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp) => DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).DateTime;
        private void ConvertedItemsList(object sender, EventArgs e)
        {
            string swaplogs = global.CurrentConfig.swaplogs;
            int converteditemno = swaplogs.Length - swaplogs.Replace(",", "").Length;
            if (converteditemno > 0)
                MessageBox.Show("You currently have " + converteditemno + " item(s) converted. The items you have converted are: " + swaplogs.Remove(swaplogs.Length - 1), "Converted Items List", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("You have no items converted!", "Converted Items List", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static string CalculateTimeSpan(DateTime dt)
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
        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to reset Pro Swapper to it's original settings? This option also deletes any cached images and older settings", "Delete Pro Swapper Settings?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string dir = global.ProSwapperFolder;

                string[] files = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);

                int del = 0;
                foreach (string file in files)
                {
                    try
                    {
                        if (File.Exists(file))
                        {
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            File.Delete(file);
                            del++;
                        }
                    }
                    catch { }//Used by proc
                }
                MessageBox.Show("Cleaned " + del + " files in " + dir, "Done", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                MessageBox.Show("Pro Swapper will now restart...");
                Process.Start(AppDomain.CurrentDomain.FriendlyName);
                Main.Cleanup();
            }
        }
        private void AesKeySourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AesKeySourceComboBox.Focused)//When form loads we dont want it to fire this event
            {
                Enum.TryParse(AesKeySourceComboBox.Text, out api.AESSource aesSource);
                global.CurrentConfig.AESSource = aesSource;

                if (global.CurrentConfig.AESSource == api.AESSource.Manual)
                {
                    manualAES.Visible = true;
                    manualAESLabel.Visible = true;
                    checkPing.Visible = false;
                }
                else
                {
                    manualAES.Visible = false;
                    manualAESLabel.Visible = false;
                    checkPing.Visible = true;
                }
                global.SaveConfig();
            }
        }
        private void manualAES_TextChanged(object sender, EventArgs e)=> global.CurrentConfig.ManualAESKey = manualAES.Text;
        private void button8_Click(object sender, EventArgs e)
        {
            Ping ping = new Ping();
            string url = string.Empty;
            switch (global.CurrentConfig.AESSource)
            {
                case AESSource.FortniteAPIV1:
                case AESSource.FortniteAPIV2:
                    url = "fortnite-api.com";
                    break;

                case AESSource.BenBot:
                    url = "benbot.app";
                    break;
            }

            PingReply pingreply = ping.Send(url, 5000);
            List<long> listtimes = new List<long>();
            for (int i = 0; i < 5; i++)
                listtimes.Add(ping.Send(url, 5000).RoundtripTime);

            MessageBox.Show($"Sent request to {pingreply.Address} ({url})\nStatus: {pingreply.Status}\nPing (Average): {listtimes.Average()} / Min: {listtimes.Min()} / Max: {listtimes.Max()}", "Pro Swapper AES", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog a = new OpenFileDialog())
            {
                if (a.ShowDialog() == DialogResult.OK)
                {
                    
                    byte[] newbyte = Oodle.OodleClass.Compress(File.ReadAllBytes(a.FileName));
                    File.WriteAllBytes(a.FileName + "_compressed.uasset", newbyte);
                }
            }
        }

        public static void RevertAllSwaps()
        {
            string ProSwapperDupePath = $"{global.CurrentConfig.Paks}\\.ProSwapper";
            if (Directory.Exists(ProSwapperDupePath))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Directory.Delete(ProSwapperDupePath, true);
            }
        }
    }
}