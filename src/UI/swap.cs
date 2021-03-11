using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Linq;

namespace Pro_Swapper
{
    public partial class swap : Form
    {
        private static Items.Item ThisItem { get; set; }
        public swap(int item)
        {
            InitializeComponent();
            ThisItem = global.items.Items[item];
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
            Text = ThisItem.SwapsFrom + " --> " + ThisItem.SwapsTo;
            image.Image = global.ItemIcon(ThisItem.FromImage);
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
                    foreach (Items.Swap swap in ThisItem.Swaps)
                    {
                        //Checking if file is readonly coz we wouldn't be able to do shit with it
                        string filepath = pakslocation + swap.File;
                        bool IsReadOnly = File.GetAttributes(filepath).ToString().Contains("ReadOnly");
                        if (IsReadOnly)
                            File.SetAttributes(filepath, FileAttributes.Normal);
                        
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
                        bool rightoffset = false;
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
                        }

                        //Decides to either Convert/Revert
                        byte[] towrite = searchbyte;
                        if (Converting) towrite = replacebyte;


                        if (!currentfile.SequenceEqual(towrite))//If already swapped skip coz we don't want the 9yo cpu to blow for unnessary shit
                            ReplaceBytes(filepath, swap.Offset, towrite);
                    }
                    s.Stop();
                    string logtext = "+] Converted";
                    if (Converting) logtext = "-] Reverted";
                    if (Converting)
                    {
                        label3.Text = "ON";
                        label3.ForeColor = Color.Lime;
                        s.Stop();
                        global.WriteSetting(global.ReadSetting(global.Setting.swaplogs) + ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + ",", global.Setting.swaplogs);
                    }
                    else
                    {
                        label3.Text = "OFF";
                        label3.ForeColor = Color.Red;
                        global.WriteSetting(global.ReadSetting(global.Setting.swaplogs).Replace(ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + ",", ""), global.Setting.swaplogs);
                    }
                    Log("====");
                    Log($"[{logtext} item in " + s.Elapsed.TotalMilliseconds + "ms");
                    Log("====");
                }
                catch (Exception ex)
                {
                    Log("Restart the swapper or refer to this error:" + ex.Message);
                }
            }).Start();

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
            Log("Starting...");
            if (((Bunifu.Framework.UI.BunifuFlatButton)sender).Text == "Convert")
                SwapWork(true);
            else //Revert
                SwapWork(false);
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
    }
}