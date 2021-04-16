using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Collections.Generic;

namespace Pro_Swapper
{
    public partial class cp : Form
    {
        private static Items.CPSwap ThisItem { get; set; }
        public cp(Items.CPSwap item)
        {
            InitializeComponent();
            ThisItem = item;
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
            if (global.ReadSetting(global.Setting.swaplogs).Contains("Default To " + ThisItem.SwapsTo + ","))
            {
             label3.ForeColor = Color.Lime;
             label3.Text = "ON";
            }
             else
             {
             label3.ForeColor = Color.Red;
             label3.Text = "OFF";
             }
            string swaptext = "Default --> " + ThisItem.SwapsTo;
            Text = swaptext;
            label1.Text = swaptext;
            image.Image = global.ItemIcon("nQ05Fa0.png");
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
                    string filepath = pakslocation + global.items.CPPak;
                    File.SetAttributes(filepath, global.RemoveAttribute(File.GetAttributes(filepath), FileAttributes.ReadOnly));
                    byte[] searchbyte = global.HexToByte(global.items.CPSearch);
                    byte[] replacebyte = global.HexToByte(ThisItem.Replace);
                    byte[] currentfile = global.ReadBytes(filepath, searchbyte.Length, global.items.CPOffset);
                    //Decides to either Convert/Revert
                    byte[] towrite = searchbyte;
                    if (Converting)
                    {
                    towrite = replacebyte;
                    ChangeDefault(true);
                    }
                    else
                    {
                    ChangeDefault(false);
                    }
                

                        if (!currentfile.SequenceEqual(towrite))//If already swapped skip coz we don't want the 9yo cpu to blow for unnessary shit
                            ReplaceBytes(filepath, global.items.CPOffset, towrite);


                    
                    s.Stop();
                    logbox.Clear();
                    Log("====");
                    if (Converting)
                    {
                        Log($"[+] Converted item in {s.Elapsed.TotalMilliseconds}ms");
                        label3.Text = "ON";
                        label3.ForeColor = Color.Lime;
                        s.Stop();
                        global.WriteSetting(global.ReadSetting(global.Setting.swaplogs) + "Default To " + ThisItem.SwapsTo + ",", global.Setting.swaplogs);
                    }
                    else
                    {
                        Log($"[+] Reverted item in {s.Elapsed.TotalMilliseconds}ms");
                        label3.Text = "OFF";
                        label3.ForeColor = Color.Red;
                        global.WriteSetting(global.ReadSetting(global.Setting.swaplogs).Replace("Default To " + ThisItem.SwapsTo + ",", ""), global.Setting.swaplogs);
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
        public void ChangeDefault(bool Invalidate)
        {
            string filepath = global.ReadSetting(global.Setting.Paks) + "\\" + global.items.CPPak;
            if (Invalidate)
            {
                foreach (Items.InvalidSwap item in global.items.InvalidSwaps)
                {
                    ReplaceBytes(filepath, item.Offset, global.HexToByte(item.Replace));
                }
               
               // Items.InvalidSwap item = global.items.InvalidSwaps[0];
                //ReplaceBytes(filepath, item.Offset, global.HexToByte(item.Replace));
            }
            else
            {
                foreach (Items.InvalidSwap item in global.items.InvalidSwaps)
                {
                    ReplaceBytes(filepath, item.Offset, global.HexToByte(item.Search));
                }
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