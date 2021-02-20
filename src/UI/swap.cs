using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Threading;
namespace Pro_Swapper
{
    public partial class swap : Form
    {
        private static global.Item ThisItem { get; set; }
        public swap(int selecteditem)
        {
            InitializeComponent();
            BackColor = global.ItemsBG;
            logbox.BackColor = global.ItemsBG;

            ConvertB.ForeColor = global.TextColor;
            ConvertB.BackColor = global.Button;
            ConvertB.Activecolor = global.Button;
            ConvertB.Normalcolor = global.Button;


            RevertB.ForeColor = global.TextColor;
            RevertB.BackColor = global.Button;
            RevertB.Activecolor = global.Button;
            RevertB.Normalcolor = global.Button;

            logbox.ForeColor = global.TextColor;

            button1.BackColor = global.Button;
            button1.ForeColor = global.TextColor;



            ThisItem = global.ItemList[selecteditem - 1];
            if (global.ReadSetting(global.Setting.swaplogs).Contains(ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + ","))
            {
             label3.ForeColor = Color.Lime;
             label3.Text = "ON";
                RevertB.Enabled = true;
                ConvertB.Enabled = false;
                ConvertB.Cursor = Cursors.No;
                RevertB.Cursor = Cursors.Hand;
            }
             else
             {
             label3.ForeColor = Color.Red;
             label3.Text = "OFF";
                RevertB.Enabled = false;
                ConvertB.Enabled = true;
                RevertB.Cursor = Cursors.No;
                ConvertB.Cursor = Cursors.Hand;


                if (ThisItem.Note.Length > 3)
                    MessageBox.Show("Warning for " + ThisItem.SwapsTo + ": "+ ThisItem.Note, ThisItem.SwapsFrom + " - " + ThisItem.SwapsTo, MessageBoxButtons.OK, MessageBoxIcon.Warning);

             }
            Text = ThisItem.SwapsFrom + " --> " + ThisItem.SwapsTo;
            image.Image = global.ItemIcon(ThisItem.SwapsFromIcon);
        }
        
        private void SwapWork(bool Converting)
        {
            new Thread(() =>
            {
                CheckForIllegalCrossThreadCalls = false;
                Thread.CurrentThread.IsBackground = true;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            try
                {
                    logbox.Clear();
                    Main.CloseFN();
                    Stopwatch s = new Stopwatch();
                    s.Start();
                    string pakslocation = global.ReadSetting(global.Setting.Paks) + "\\";


                    if (Converting)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (ThisItem.Swap.File[i].Length > 3)
                            {
                                ReplaceBytes(pakslocation + ThisItem.Swap.File[i], ThisItem.Swap.Search[i], ThisItem.Swap.Replace[i], ThisItem.Swap.Offset[i]);
                                Log("Replaced " + ThisItem.Swap.Search[i] + " in " + ThisItem.Swap.File[i].Replace("-WindowsClient.ucas", ""));
                            }
                        }

                        label3.Text = "ON";
                        label3.ForeColor = Color.Lime;
                        s.Stop();
                        Log("====");
                        Log("[+] Converted item in " + s.Elapsed.TotalMilliseconds + "ms");
                        global.WriteSetting(global.ReadSetting(global.Setting.swaplogs) + ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + ",", global.Setting.swaplogs);
                    }
                    else
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (ThisItem.Swap.File[i].Length > 3)
                            {
                                ReplaceBytes(pakslocation + ThisItem.Swap.File[i], ThisItem.Swap.Replace[i], ThisItem.Swap.Search[i], ThisItem.Swap.Offset[i]);
                                Log("Replaced " + ThisItem.Swap.Search[i] + " in " + ThisItem.Swap.File[i].Replace("-WindowsClient.ucas", ""));
                            }  
                        }
                        label3.Text = "OFF";
                        label3.ForeColor = Color.Red;
                        s.Stop();
                        Log("====");
                        Log("[-] Reverted item in " + s.Elapsed.TotalMilliseconds + "ms");
                        global.WriteSetting(global.ReadSetting(global.Setting.swaplogs).Replace(ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + ",", ""), global.Setting.swaplogs);
                    }
                }
                catch (Exception ex)
                {
                    Log("Restart the swapper or refer to this error:" + ex.Message);
                }
            }).Start();

        }
        private void Log(string text) => logbox.Text += text + Environment.NewLine;
        private void SwapButton_Click(object sender, EventArgs e)
        {
            string path = global.ReadSetting(global.Setting.Paks) + @"\pakchunk0-WindowsClient.sig";
            if (!File.Exists(path))
            {
                MessageBox.Show("Select your paks folder in Settings", "Pro Swapper");
                return;
            }
            logbox.Clear();
            Log("Starting...");
            if (((Bunifu.Framework.UI.BunifuFlatButton)sender).Text == "Convert")
            {
                RevertB.Enabled = true;
                ConvertB.Enabled = false;
                ConvertB.Cursor = Cursors.No;
                RevertB.Cursor = Cursors.Hand;
                SwapWork(true);
            }
            else //Revert
            {
                RevertB.Enabled = false;
                ConvertB.Enabled = true;
                RevertB.Cursor = Cursors.No;
                ConvertB.Cursor = Cursors.Hand;
                SwapWork(false);
            }
        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            label3.Text = "???";
            label3.ForeColor = Color.White;
            ConvertB.Enabled = true;
            RevertB.Enabled = true;
            RevertB.Cursor = Cursors.Hand;
            ConvertB.Cursor = Cursors.Hand;
            logbox.Clear();
            MessageBox.Show("Config Reset! (Now you can either convert or revert for this item)", "Pro Swapper");
        }
        public void ReplaceBytes(string path, string searchtxt, string replacetxt, long startoffset)
        {
            byte[] search = Encoding.UTF8.GetBytes(searchtxt);
            byte[] replace = Encoding.UTF8.GetBytes(replacetxt);
            Stream stream = File.OpenRead(path);
            long offset = global.FindPosition(stream, 0, startoffset, search);
            stream.Dispose();
            using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(path, FileMode.Open, FileAccess.ReadWrite)))
            {
                    binaryWriter.BaseStream.Seek(offset, SeekOrigin.Begin);
                    binaryWriter.Write(replace);
                    binaryWriter.Close();
            }
            
        }
    }
}