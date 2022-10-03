using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using static Pro_Swapper.API.api;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using Pro_Swapper.src.Classes;

namespace Pro_Swapper
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            RPC.SetState("Settings", true);
            Icon = Main.appIcon;
#if DEBUG
            OodleCompressBtn.Visible = true;
#endif
            checkBox1_CheckedChanged(null, new EventArgs());
            this.Paint += (sender, e) =>
            {
                Graphics g = e.Graphics;
                GraphicsPath GP = new GraphicsPath();
                GP.AddRectangle(Region.GetBounds(g));
                g.DrawPath(new Pen(global.ChangeColorBrightness(BackColor, 0.15f)) { Width = 10f }, GP);
            };
        }
        private void button1_Click(object sender, EventArgs e)
        {
            global.SaveConfig();
            Close();
        }
        private void SettingsForm_MouseDown(object sender, MouseEventArgs e) => global.MoveForm(e, Handle);
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog paks = new FolderBrowserDialog())
            {
                paks.UseDescriptionForTitle = true;
                paks.Description = "Select your Fortnite Paks folder";
                paks.ShowNewFolderButton = false;
                if (paks.ShowDialog() == DialogResult.OK)
                {
                    paksBox.Text = paks.SelectedPath;
                    global.CurrentConfig.Paks = paks.SelectedPath;
                    global.SaveConfig();
                }
            }
        }


        private void ApplyTheme(Control.ControlCollection Controls)
        {
            foreach (Control ctrl in Controls)
            {
                string type = ctrl.GetType().Name;

                switch (type)
                {
                    case "Button":
                        ctrl.BackColor = global.Button;
                        ctrl.ForeColor = global.TextColor;
                        break;
                    case "Label":
                        ctrl.ForeColor = global.TextColor;
                        break;
                    case "CheckBox":
                        ctrl.ForeColor = global.TextColor;
                        break;
                    case "GroupBox":
                        ctrl.ForeColor = global.TextColor;
                        break;
                    case "ComboBox":
                        ctrl.ForeColor = global.TextColor;
                        ctrl.BackColor = global.Button;
                        break;
                }
            }
        }
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            paksBox.Text = global.CurrentConfig.Paks;
            ApplyTheme(this.Controls);
            ApplyTheme(groupBox1.Controls);
            this.BackColor = global.MainMenu;
            button1.BackColor = this.BackColor;
            AesKeySourceComboBox.DataSource = Enum.GetNames(typeof(AESSource));
            AesKeySourceComboBox.Text = global.CurrentConfig.AESSource.ToString();
            anitkickbox.Checked = global.CurrentConfig.AntiKick;
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
            if (Swap.Provider != null)
                Swap.Provider.Dispose();


            Swap.Provider = null;
            //Dispose the provider because we don't want anything accessing the Fortnite files

            notifyIcon1.Visible = true;
            notifyIcon1.Icon = Main.appIcon;
            notifyIcon1.Text = "Pro Swapper";
            notifyIcon1.Click += NotifyIcon1_Click;
            global.OpenUrl($"com.epicgames.launcher://apps/fn%3A4fe75bbc5a674f4f9b356b5c90567da5%3AFortnite?action=launch&silent=true");
            this.Hide();
            Action safeClose = delegate { Main.Mainform.Hide(); };
            Main.Mainform.Invoke(safeClose);
            Process fngame = null;

            //Basically define fngame proc "searcher"
            while (fngame == null)
            {
                Task.Delay(1000);
                Process[] thisproc = Process.GetProcessesByName("FortniteClient-Win64-Shipping");
                if (thisproc.Length > 0)
                    fngame = thisproc[0];
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
            MessageBox.Show(@"Pro Swapper is currently running while playing Fortnite so it closes Epic Games Launcher when you finish playing. If you want to close Pro Swapper end the process from task manager :'(", "Pro Swapper Notify Icon", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void KillEpic() => Process.GetProcessesByName("EpicGamesLauncher").All(x => { x.Kill(); return true; });
        private void button7_Click(object sender, EventArgs e)
        {
            bool reverted = RevertEngine.RevertAll();
            RevertAllSwaps();
            global.CurrentConfig.swaplogs = "";
            global.SaveConfig();
            //global.OpenUrl($"{epicfnpath}verify");
            //Main.Cleanup();
            if (reverted)
            {
                MessageBox.Show("Fortnite successfully verified.", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Fortnite could not be verified, Try verifying from Epic Games Launcher instead", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }
        private void button10_Click(object sender, EventArgs e) => new ThemeCreator().ShowDialog();
        private void button5_Click(object sender, EventArgs e) => new UI.About().ShowDialog();
        private void ConvertedItemsList(object sender, EventArgs e)
        {
            string swaplogs = global.CurrentConfig.swaplogs;
            int converteditemno = swaplogs.Length - swaplogs.Replace(",", "").Length;
            if (converteditemno > 0)
                MessageBox.Show("You currently have " + converteditemno + " item(s) converted. The items you have converted are: " + swaplogs.Remove(swaplogs.Length - 1), "Converted Items List", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(@"You have no items converted!", "Converted Items List", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Enum.TryParse(AesKeySourceComboBox.Text, out AESSource aesSource);
                global.CurrentConfig.AESSource = aesSource;

                if (global.CurrentConfig.AESSource == AESSource.Manual)
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
        private void manualAES_TextChanged(object sender, EventArgs e) => global.CurrentConfig.ManualAESKey = manualAES.Text;
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

                case AESSource.FortniteCentral:
                    url = "fortnitecentral.gmatrixgames.ga";
                    break;
            }

            PingReply pingreply = ping.Send(url, 5000);
            List<long> listtimes = new List<long>();
            for (int i = 0; i < 5; i++)
                listtimes.Add(ping.Send(url, 5000).RoundtripTime);

            MessageBox.Show($@"Sent request to {pingreply.Address} ({url})
Status: {pingreply.Status}
Ping (Average): {listtimes.Average()} / Min: {listtimes.Min()} / Max: {listtimes.Max()}", "Pro Swapper AES", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void button8_Click_1(object sender, EventArgs e)
        {
#if DEBUG//Oodle compressor only in debug mode for dev(s) hehe
            using (OpenFileDialog a = new OpenFileDialog())
            {
                if (a.ShowDialog() == DialogResult.OK)
                {
                    byte[] file = File.ReadAllBytes(a.FileName);
                    //  string Path1 = "/Game/Characters/Player/Female/Medium/Bodies/F_MED_Renegade_Skull/Materials/F_MED_Renegade_Skull.F_MED_Renegade_Skull";
                    // string Path2 = "/Game/Characters/Player/Female/Medium/Heads/F_MED_ASN_Sarah_Head_01/Materials/F_MED_ASN_Sarah_Head_02.F_MED_ASN_Sarah_Head_02";
                    //  Swap.ReplaceAnyLength(file, System.Text.Encoding.Default.GetBytes(Path1), System.Text.Encoding.Default.GetBytes(Path2));
                    byte[] newbyte = Oodle.Compress(file, OodleFormat.Leviathan);
                    File.WriteAllBytes(a.FileName + "_compressed.uasset", newbyte);
                }
            }
#endif
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
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.Invalidate(Region);//https://stackoverflow.com/a/4124655/12897035
            bool ShowAdvanced = checkBox1.Checked;
            groupBox1.Visible = ShowAdvanced;
            if (ShowAdvanced)
                this.Height = 431;
            else
                this.Height = 245;
            Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", global.ProSwapperFolder);
        }

        private void anitkickbox_CheckedChanged(object sender, EventArgs e)
        {
            global.CurrentConfig.AntiKick = anitkickbox.Checked;
            global.SaveConfig();
        }
    }
}