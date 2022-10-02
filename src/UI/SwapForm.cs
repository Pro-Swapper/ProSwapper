using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using Pro_Swapper.API;
using System.Threading.Tasks;
using Pro_Swapper.src.Classes;

namespace Pro_Swapper
{
    public partial class SwapForm : Form
    {
        private api.Item ThisItem;

        public SwapForm(api.Item item)
        {
            InitializeComponent();
            this.Icon = Main.appIcon;
            RPC.SetState(item.SwapsFrom + " To " + item.SwapsTo, true);
            ThisItem = item;
            string swaptext = ThisItem.SwapsFrom + " --> " + ThisItem.SwapsTo;
            Text = swaptext;
            label1.Text = swaptext;
            image.Image = global.ItemIcon(ThisItem.FromImage);
            swapsfrom.Image = global.ItemIcon(ThisItem.ToImage);
            Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
            Icon = Main.appIcon;
            BackColor = global.MainMenu;
            logbox.BackColor = global.MainMenu;

            ConvertB.ForeColor = global.TextColor;
            ConvertB.BackColor = global.Button;


            RevertB.ForeColor = global.TextColor;
            RevertB.BackColor = global.Button;

            logbox.ForeColor = global.TextColor;
            if (global.CurrentConfig.swaplogs.Contains(ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + ","))
            {
                label3.ForeColor = Color.Lime;
                label3.Text = "ON";
            }
            else
            {
                label3.ForeColor = Color.Red;
                label3.Text = "OFF";
                if (ThisItem.Note != null) MessageBox.Show("Warning for " + ThisItem.SwapsTo + ": " + ThisItem.Note, ThisItem.SwapsFrom + " - " + ThisItem.SwapsTo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Log(string text)
        {
            logbox.Invoke(new Action(() => { logbox.Text += $"{text}{Environment.NewLine}"; logbox.ScrollToCaret(); }));
            Program.logger.Log(text);
        }

        private async void ButtonbgWorker(bool Converting)
        {
            try
            {
                ConvertB.Invoke(new Action(() => { ConvertB.Enabled = false; }));
                RevertB.Invoke(new Action(() => { RevertB.Enabled = false; }));
                label3.Invoke(new Action(() => { label3.Text = "Loading..."; label3.ForeColor = Color.White; }));

                Stopwatch s = Stopwatch.StartNew();
                Program.logger.Log($"(SwapForm.cs) (Converting = {Converting}) Starting to convert {this.Text}");
                bool Swapped = false;
                if (Converting)
                {
                    //Converting
                    Swapped = Task.Run(() => Swap.SwapItem(ThisItem, Converting)).Result;
                }
                else
                {
                    //Reverting
                    Swapped = RevertEngine.RevertItem(ThisItem);
                }

                if (!Swapped)
                {
                    ConvertB.Invoke(new Action(() => { ConvertB.Enabled = true; }));
                    RevertB.Invoke(new Action(() => { RevertB.Enabled = true; }));
                    label3.Invoke(new Action(() => { label3.Text = "OFF"; label3.ForeColor = Color.Red; }));
                    Log("[/] Canceled Swap");
                    s.Stop();
                    return;
                }

                s.Stop();
                ConvertB.Invoke(new Action(() => { ConvertB.Enabled = true; }));
                RevertB.Invoke(new Action(() => { RevertB.Enabled = true; }));
                logbox.Invoke(new Action(() => { logbox.Clear(); }));
                string LogMsg = string.Empty;
                if (Converting)
                {
                    LogMsg = $"[+] Converted {ThisItem.SwapsFrom} to {ThisItem.SwapsTo} in {s.ElapsedMilliseconds}ms";
                    label3.Invoke(new Action(() => { label3.Text = "ON"; label3.ForeColor = Color.Lime; }));
                    global.CurrentConfig.swaplogs += ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + ",";
                }
                else
                {
                    LogMsg = $"[-] Reverted {ThisItem.SwapsFrom} to {ThisItem.SwapsTo} in {s.ElapsedMilliseconds}ms";
                    label3.Invoke(new Action(() => { label3.Text = "OFF"; label3.ForeColor = Color.Red; }));
                    global.CurrentConfig.swaplogs = global.CurrentConfig.swaplogs.Replace(ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + ",", "");
                }
                Log(LogMsg);
                global.SaveConfig();
            }
            catch (Exception ex)
            {
                Program.logger.LogError(ex.Message);
                Log($"Please send this error in #help on the Pro Swapper Discord: {ex.Message} | {ex.StackTrace}");
            }
        }

        public bool PrepareToSwap()
        {
            bool CorrectPaksFolder = File.Exists($"{global.CurrentConfig.Paks}\\global.utoc") || File.Exists($"{global.CurrentConfig.Paks}\\global.ucas") || File.Exists($"{global.CurrentConfig.Paks}\\pakchunk0-WindowsClient.pak");
            if (!CorrectPaksFolder)
            {
                MessageBox.Show("Select your paks folder in Settings", "Pro Swapper");
                return false;
            }
            logbox.Clear();
            Log("Loading...");

            return EpicGamesLauncher.CloseFNPrompt();
        }

        private void ConvertB_Click(object sender, EventArgs e)
        {
            if (PrepareToSwap())
            {
                Task.Run(() => ButtonbgWorker(true));
            }

        }

        private void RevertB_Click(object sender, EventArgs e)
        {
            if (PrepareToSwap())
            {
                Task.Run(() => ButtonbgWorker(false));
            }
        }
        private void ExitButton_Click(object sender, EventArgs e) => Close();
        private void button2_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
        private void swap_MouseDown(object sender, MouseEventArgs e) => global.MoveForm(e, Handle);


    }
}