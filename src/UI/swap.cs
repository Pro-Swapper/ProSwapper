using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Linq;
using System.ComponentModel;

namespace Pro_Swapper
{
    public partial class swap : Form
    {
        private static Items.Item ThisItem { get; set; }
        public swap(int item)
        {
            InitializeComponent();
            ThisItem = global.items.Items[item];
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
            if (global.ReadSetting(global.Setting.swaplogs).Contains(ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + ","))
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
                    string pakslocation = global.ReadSetting(global.Setting.Paks) + "\\";
                        foreach (Items.Swap swap in ThisItem.Swaps)
                        {
                        string filepath = pakslocation + swap.File;

                        //Checking if file is readonly coz we wouldn't be able to do shit with it
                        File.SetAttributes(filepath, global.RemoveAttribute(File.GetAttributes(filepath), FileAttributes.ReadOnly));
                        
                        //Make it support hex as well coz fuck it
                        byte[] searchbyte, replacebyte;
                        if (swap.Search.StartsWith("hex=") && swap.Replace.StartsWith("hex="))
                        {
                            searchbyte = global.HexToByte(swap.Search.Replace("hex=", ""));
                            replacebyte = global.HexToByte(swap.Replace.Replace("hex=", ""));
                        }
                        else//eww using text but atleast we can read it
                        {
                            searchbyte = Encoding.Default.GetBytes(swap.Search);
                            replacebyte = Encoding.Default.GetBytes(swap.Replace);
                        }
                        

                        //Checks if the ucas file is at right offset
                        byte[] currentfile = global.ReadBytes(filepath, searchbyte.Length, swap.Offset);
                       
                    
                    //Checks to see if the next bytes are the same as api
                    /* bool rightoffset = false;
                        if (currentfile.SequenceEqual(searchbyte))
                            rightoffset = true;
                        else if (currentfile.SequenceEqual(replacebyte))
                            rightoffset = true;
                        else
                            rightoffset = false;

                        if (rightoffset == false)
                        {
                            MessageBox.Show("Error! Pro Swapper has not been updated for the most recent update. Please wait for Kye to update it! Join the Discord server for more information. You can also try verifying Fortnite if you were using another swapper or modifying Fortnite beforehand", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }*/

                        //Decides to either Convert/Revert
                        byte[] towrite = searchbyte;
                        if (Converting) towrite = replacebyte;


                        if (!currentfile.SequenceEqual(towrite))//If already swapped skip coz we don't want the 9yo cpu to blow for unnessary shit
                            ReplaceBytes(filepath, swap.Offset, towrite);
                    }
                    s.Stop();
                    logbox.Clear();
                    Log("====");
                    if (Converting)
                    {
                        Log($"[+] Converted item in {s.Elapsed.TotalMilliseconds}ms");
                        label3.Text = "ON";
                        label3.ForeColor = Color.Lime;
                        s.Stop();
                        global.WriteSetting(global.ReadSetting(global.Setting.swaplogs) + ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + ",", global.Setting.swaplogs);
                    }
                    else
                    {
                        Log($"[+] Reverted item in {s.Elapsed.TotalMilliseconds}ms");
                        label3.Text = "OFF";
                        label3.ForeColor = Color.Red;
                        global.WriteSetting(global.ReadSetting(global.Setting.swaplogs).Replace(ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + ",", ""), global.Setting.swaplogs);
                    }
                    Log("====");
                }
                catch (Exception ex)
                {
                    Log($"Restart the swapper or refer to this error: {ex.Message}");
                }
        }
        private void Log(string text) => logbox.Text += $"{text}{Environment.NewLine}";
        private void SwapButton_Click(object sender, EventArgs e)
        {
            string path = global.ReadSetting(global.Setting.Paks) + @"\pakchunk0-WindowsClient.sig";
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
        public void ReplaceBytes(string file, long Offset, byte[] towrite)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(file, FileMode.Open, FileAccess.ReadWrite)))
            {
                writer.BaseStream.Seek(Offset, SeekOrigin.Begin);
                writer.Write(towrite);
                writer.Close();
            }
        }

        private void ExitButton_Click(object sender, EventArgs e) => Close();

        private void button2_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void swap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) Main.FormMove(Handle);

            button2.Focus();
        }
    }
}