using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using Pro_Swapper.API;
using Bunifu.Framework.UI;
using System.Threading.Tasks;
namespace Pro_Swapper
{
    public partial class OodleSwap : Form
    {
        private api.Item ThisItem;

        public OodleSwap(api.Item item)
        {
            InitializeComponent();
            ThisItem = item;
            string swaptext = ThisItem.SwapsFrom + " --> " + ThisItem.SwapsTo;
            Text = swaptext;
            label1.Text = swaptext;
            image.Image = global.ItemIcon(ThisItem.FromImage);
            swapsfrom.Image = global.ItemIcon(ThisItem.ToImage);
            Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, Width, Height, 30, 30));

            BackColor = global.MainMenu;
            logbox.BackColor = global.MainMenu;

            ConvertB.ForeColor = global.TextColor;
            ConvertB.BackColor = global.Button;
            ConvertB.Activecolor = global.Button;
            ConvertB.Normalcolor = global.Button;


            RevertB.ForeColor = global.TextColor;
            RevertB.BackColor = global.Button;
            RevertB.Activecolor = global.Button;
            RevertB.Normalcolor = global.Button;

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
            CheckForIllegalCrossThreadCalls = false;
            logbox.Text += $"{text}{Environment.NewLine}";
            logbox.ScrollToCaret();
        }

        private async void ButtonbgWorker(bool Converting)
        {
            CheckForIllegalCrossThreadCalls = false;
            try
            {
                string path = global.CurrentConfig.Paks + @"\pakchunk0-WindowsClient.sig";
                if (!File.Exists(path))
                {
                    MessageBox.Show("Select your paks folder in Settings", "Pro Swapper");
                    return;
                }
                
                
                ConvertB.Enabled = false;
                RevertB.Enabled = false;
                label3.Text = "Loading...";
                label3.ForeColor = Color.White;
                Stopwatch s = new Stopwatch();
                s.Start();
                foreach (api.Asset asset in ThisItem.Asset)
                {
                    //Check if replace is longer
                    for (int i = 0; i < asset.Search.Length; i++)
                    {
                        if (asset.Search[i].Length < asset.Replace[i].Length)
                        {
                            string error = "The replace length is longer than the search, pleaes make sure the search is greater than or equal to the replace length";
                            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Log(error);
                            return;
                        }
                    }
                }
                await Swap.SwapItem(ThisItem, Converting);
                ConvertB.Enabled = true;
                RevertB.Enabled = true;
                s.Stop();
                logbox.Clear();
                string swaplogs = global.CurrentConfig.swaplogs;
                if (Converting)
                {
                    Log($"[+] Converted item in {s.Elapsed.Milliseconds}ms");
                    label3.Text = "ON";
                    label3.ForeColor = Color.Lime;
                    s.Stop();
                    global.CurrentConfig.swaplogs += ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + ",";
                }
                else
                {
                    Log($"[-] Reverted item in {s.Elapsed.Milliseconds}ms");
                    label3.Text = "OFF";
                    label3.ForeColor = Color.Red;
                    global.CurrentConfig.swaplogs = swaplogs.Replace(ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + ",", "");
                }
                global.SaveConfig();
            }
            catch (Exception ex)
            {
                Log($"Restart the swapper or refer to this error: {ex.Message} | {ex.StackTrace}");
            }
        }

        private void SwapButton_Click(object sender, EventArgs e)
        {
            logbox.Clear();
            Log("Loading...");
            bool isconverting = ((BunifuFlatButton)(sender)).Text == "Convert";
            Task.Run(() => ButtonbgWorker(isconverting));
        }
        private void ExitButton_Click(object sender, EventArgs e) => Close();
        private void button2_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
        private void swap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                global.FormMove(Handle);
        }
    }
}