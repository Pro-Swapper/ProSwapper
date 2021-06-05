using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel;
using Pro_Swapper.API;
namespace Pro_Swapper
{
    public partial class OodleSwap : Form
    {
        private static api.Item ThisItem { get; set; }
        public OodleSwap(int item)
        {
            InitializeComponent();
            ThisItem = api.apidata.items[item];
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
                if (ThisItem.Note != null) MessageBox.Show("Warning for " + ThisItem.SwapsTo + ": "+ ThisItem.Note, ThisItem.SwapsFrom + " - " + ThisItem.SwapsTo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
             }
            string swaptext = ThisItem.SwapsFrom + " --> " + ThisItem.SwapsTo;
            Text = swaptext;
            label1.Text = swaptext;
            image.Image = global.ItemIcon(ThisItem.FromImage);
            swapsfrom.Image = global.ItemIcon(ThisItem.ToImage);
            Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }
        private bool Converting { get; set; }
        private void SwapWork(object sender, DoWorkEventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            try
                {
                    Stopwatch s = new Stopwatch();
                    s.Start();
                
                    Swap.SwapItem(ThisItem, out bool Converting);
                    s.Stop();
                    logbox.Clear();
                    Log("====");
                    string swaplogs = global.CurrentConfig.swaplogs;
                    if (Converting)
                    {
                        Log($"[+] Converted item in {s.Elapsed.TotalMilliseconds}ms");
                        label3.Text = "ON";
                        label3.ForeColor = Color.Lime;
                        s.Stop();
                        global.CurrentConfig.swaplogs += ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + ",";
                    }
                    else
                    {
                        Log($"[+] Reverted item in {s.Elapsed.TotalMilliseconds}ms");
                        label3.Text = "OFF";
                        label3.ForeColor = Color.Red;
                        global.CurrentConfig.swaplogs = swaplogs.Replace(ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + ",", "");
                    }
                    Log("====");
                    global.SaveConfig();
                }
                catch (Exception ex)
                {
                    Log($"Restart the swapper or refer to this error: {ex.Message} | {ex.StackTrace}");
                }
            
        }
        private void Log(string text)
        {
            logbox.Text += $"{text}{Environment.NewLine}";
            logbox.ScrollToCaret();
        }
        private void SwapButton_Click(object sender, EventArgs e)
        {
            string path = global.CurrentConfig.Paks + @"\pakchunk0-WindowsClient.sig";
            if (!File.Exists(path))
            {
                MessageBox.Show("Select your paks folder in Settings", "Pro Swapper");
                return;
            }
            logbox.Clear();
            Log("Loading...");

            

            if (((Bunifu.Framework.UI.BunifuFlatButton)sender).Text == "Convert")
                Converting = true;
            else //Revert
                Converting = false;

            using (BackgroundWorker swapbg = new BackgroundWorker())
            {
                swapbg.DoWork += new DoWorkEventHandler(SwapWork);
                swapbg.RunWorkerAsync();
            }
        }
        private void ExitButton_Click(object sender, EventArgs e) => Close();
        private void button2_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
        private void swap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) global.FormMove(Handle);
        }
    }
}