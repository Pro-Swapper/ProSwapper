using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
namespace Pro_Swapper
{
    public partial class Plugins : Form
    {
        private static string file { get; set; }
        public Plugins(string filepath)
        {
            InitializeComponent();
            this.Icon = Main.appIcon;
            
            try
            {
                file = filepath;
                string pluginfile = file;
                swapsfrom = GetLine(pluginfile, 3).Replace("Swaps From=", "");
                plugincreatortxt.Text = "This plugin is made by " + GetLine(pluginfile, 7).Replace("Plugin Creator=", "");
                swapsto = GetLine(pluginfile, 4).Replace("Swaps To=", "");
                string swapfromico = GetLine(pluginfile, 5).Replace("Swaps From Icon=", "");
                string swaptoico = GetLine(pluginfile, 6).Replace("Swaps To Icon=", "");
                this.Text = "Pro Swapper | " + swapsfrom + " - " + swapsto;
                try
                {
                    pictureBox4.ImageLocation = swapfromico;
                }
                catch
                {
                    MessageBox.Show("Could not get image (Swaps From Icon), either plugin wasn't made properly or the image url does not exist!");
                }
                try
                {
                    pictureBox1.ImageLocation = swaptoico;
                }
                catch
                {
                    MessageBox.Show("Could not get image (Swaps From Icon), either plugin wasn't made properly or the image url does not exist!");
                }
                string warning = GetLine(pluginfile, 8).Replace("Warning=", "");
                if (warning != "")
                {
                    MessageBox.Show(warning, "Pro Swapper Plugin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch
            {
                MessageBox.Show("Invalid Plugin! That's not a valid plugin, please contact support if you do not know how to fix this!");
            }
            if (global.ReadSetting(global.Setting.swaplogs).Contains(swapsfrom + " To " + swapsto + ","))
            {
                label3.ForeColor = Color.Lime;
                label3.Text = "ON";
                ConvertB.Enabled = false;
                RevertB.Enabled = true;
            }
            else
            {
                label3.ForeColor = Color.Red;
                label3.Text = "OFF";
                ConvertB.Enabled = true;
                RevertB.Enabled = false;
            }
        }

        public static string getSearch(string filename, int line)
        {
            return GetLine(filename, line).Replace("search=", "");
        }
        public static string getReplace(string filename, int line)
        {
            return GetLine(filename, line).Replace("replace=", "");
        }


        public void DoReplace(int pakfileline, string log)
        {
            string filename = file;
            int firstpak = pakfileline;
            byte[] searchbytes1 = Encoding.UTF8.GetBytes(getSearch(filename, firstpak + 2));
            byte[] replacebytes1 = Encoding.UTF8.GetBytes(getReplace(filename, firstpak + 3));
            int replace1fill = getSearch(filename, firstpak + 2).Length - getReplace(filename, firstpak + 3).Length;

            while (replace1fill > 0)
            {
                byte[] fill = { 0 };
                replacebytes1 = Combine(replacebytes1, fill);
                replace1fill = replace1fill - 1;
            }
            ReplaceBytes(getpak(filename, firstpak), searchbytes1, replacebytes1, getoffset(filename, firstpak + 1), log + getType(filename, firstpak + 4));
          
        }

        public void DoRevert(int pakfileline, string log)
        {
            string filename = file;
            int firstpak = pakfileline;
            byte[] searchbytes1 = Encoding.UTF8.GetBytes(getSearch(filename, firstpak + 2));
            byte[] replacebytes1 = Encoding.UTF8.GetBytes(getReplace(filename, firstpak + 3));
            int replace1fill = getSearch(filename, firstpak + 2).Length - getReplace(filename, firstpak + 3).Length;

            while (replace1fill > 0)
            {
                byte[] fill = { 0 };
                replacebytes1 = Combine(replacebytes1, fill);
                replace1fill = replace1fill - 1;
            }
            ReplaceBytes(getpak(filename, firstpak), replacebytes1, searchbytes1, getoffset(filename, firstpak + 1), log + getType(filename, firstpak + 4));
        }


        public int replacenumber(int replacenumber)
        {
            int pak = 10;
            while (replacenumber > 0)
            {
                pak = pak + 7;
                replacenumber = replacenumber - 1;
            }
            return pak;
        }
        public void ReplaceBytes(object sender, DoWorkEventArgs e)
        {
            Main.CloseFN();
            string filename = file;
            Control.CheckForIllegalCrossThreadCalls = false;
            Stopwatch s = new Stopwatch();
            s.Start();
            int pak = 10;
            try
            {


                if (GetLine(filename, replacenumber(0)).Contains("pak"))
                {
                    DoReplace(pak, "[1/20] Converted ");
                }

                if (GetLine(filename, replacenumber(1)).Contains("pak"))
                {
                    DoReplace(replacenumber(1), "[2/20] Converted ");
                }

                if (GetLine(filename, replacenumber(2)).Contains("pak"))
                {
                    DoReplace(replacenumber(2), "[3/20] Converted ");
                }


                if (GetLine(filename, replacenumber(3)).Contains("pak"))
                {
                    DoReplace(replacenumber(3), "[4/20] Converted ");
                }

                if (GetLine(filename, replacenumber(4)).Contains("pak"))
                {
                    DoReplace(replacenumber(4), "[5/20] Converted ");
                }

                if (GetLine(filename, replacenumber(5)).Contains("pak"))
                {
                    DoReplace(replacenumber(5), "[6/20] Converted ");
                }

                if (GetLine(filename, replacenumber(6)).Contains("pak"))
                {
                    DoReplace(replacenumber(6), "[7/20] Converted ");
                }

                if (GetLine(filename, replacenumber(7)).Contains("pak"))
                {
                    DoReplace(replacenumber(7), "[8/20] Converted ");
                }

                if (GetLine(filename, replacenumber(8)).Contains("pak"))
                {
                    DoReplace(replacenumber(8), "[9/20] Converted ");
                }

                if (GetLine(filename, replacenumber(9)).Contains("pak"))
                {
                    DoReplace(replacenumber(9), "[10/20] Converted ");
                }

                if (GetLine(filename, replacenumber(10)).Contains("pak"))
                {
                    DoReplace(replacenumber(10), "[11/20] Converted ");
                }

                if (GetLine(filename, replacenumber(11)).Contains("pak"))
                {
                    DoReplace(replacenumber(11), "[12/20] Converted ");
                }

                if (GetLine(filename, replacenumber(12)).Contains("pak"))
                {
                    DoReplace(replacenumber(12), "[13/20] Converted ");
                }


                if (GetLine(filename, replacenumber(13)).Contains("pak"))
                {
                    DoReplace(replacenumber(13), "[14/20] Converted ");
                }

                if (GetLine(filename, replacenumber(14)).Contains("pak"))
                {
                    DoReplace(replacenumber(14), "[15/20] Converted ");
                }

                if (GetLine(filename, replacenumber(15)).Contains("pak"))
                {
                    DoReplace(replacenumber(15), "[16/20] Converted ");
                }

                if (GetLine(filename, replacenumber(16)).Contains("pak"))
                {
                    DoReplace(replacenumber(16), "[17/20] Converted ");
                }
                if (GetLine(filename, replacenumber(17)).Contains("pak"))
                {
                    DoReplace(replacenumber(17), "[18/20] Converted ");
                }
                if (GetLine(filename, replacenumber(18)).Contains("pak"))
                {
                    DoReplace(replacenumber(18), "[19/20] Converted ");
                }
                if (GetLine(filename, replacenumber(19)).Contains("pak"))
                {
                    DoReplace(replacenumber(19), "[20/20] Converted ");
                }
            }
            catch
            {
            }

            label3.Text = "ON";
            label3.ForeColor = Color.Lime;
            s.Stop();
            Log("[+] Converted item in " + s.Elapsed.TotalMilliseconds + "ms");
            global.WriteSetting(global.ReadSetting(global.Setting.swaplogs) + swapsfrom + " To " + swapsto + ",", global.Setting.swaplogs);
        }

        public void RevertBytes(object sender, DoWorkEventArgs e)
        {
            Main.CloseFN();
            string filename = file;
            CheckForIllegalCrossThreadCalls = false;
            Stopwatch s = new Stopwatch();
            s.Start();
            int pak = 10;
            try
            {
                if (GetLine(filename, replacenumber(0)).Contains("pak"))
                {
                    DoRevert(pak, "[1/20] Reverted ");
                }

                if (GetLine(filename, replacenumber(1)).Contains("pak"))
                {
                    DoRevert(replacenumber(1), "[2/20] Reverted ");
                }

                if (GetLine(filename, replacenumber(2)).Contains("pak"))
                {
                    DoRevert(replacenumber(2), "[3/20] Reverted ");
                }


                if (GetLine(filename, replacenumber(3)).Contains("pak"))
                {
                    DoRevert(replacenumber(3), "[4/20] Reverted ");
                }

                if (GetLine(filename, replacenumber(4)).Contains("pak"))
                {
                    DoRevert(replacenumber(4), "[5/20] Reverted ");
                }

                if (GetLine(filename, replacenumber(5)).Contains("pak"))
                {
                    DoRevert(replacenumber(5), "[6/20] Reverted ");
                }

                if (GetLine(filename, replacenumber(6)).Contains("pak"))
                {
                    DoRevert(replacenumber(6), "[7/20] Reverted ");
                }

                if (GetLine(filename, replacenumber(7)).Contains("pak"))
                {
                    DoRevert(replacenumber(7), "[8/20] Reverted ");
                }

                if (GetLine(filename, replacenumber(8)).Contains("pak"))
                {
                    DoRevert(replacenumber(8), "[9/20] Reverted ");
                }
                if (GetLine(filename, replacenumber(9)).Contains("pak"))
                {
                    DoRevert(replacenumber(9), "[10/20] Reverted ");
                }
                if (GetLine(filename, replacenumber(10)).Contains("pak"))
                {
                    DoRevert(replacenumber(10), "[11/20] Reverted ");
                }
                if (GetLine(filename, replacenumber(11)).Contains("pak"))
                {
                    DoRevert(replacenumber(11), "[12/20] Reverted ");
                }
                if (GetLine(filename, replacenumber(12)).Contains("pak"))
                {
                    DoRevert(replacenumber(12), "[13/20] Reverted ");
                }
                if (GetLine(filename, replacenumber(13)).Contains("pak"))
                {
                    DoRevert(replacenumber(13), "[14/20] Reverted ");
                }

                if (GetLine(filename, replacenumber(14)).Contains("pak"))
                {
                    DoRevert(replacenumber(14), "[15/20] Reverted ");
                }

                if (GetLine(filename, replacenumber(15)).Contains("pak"))
                {
                    DoRevert(replacenumber(15), "[16/20] Reverted ");
                }

                if (GetLine(filename, replacenumber(16)).Contains("pak"))
                {
                    DoRevert(replacenumber(16), "[17/20] Reverted ");
                }
                if (GetLine(filename, replacenumber(17)).Contains("pak"))
                {
                    DoRevert(replacenumber(17), "[18/20] Reverted ");
                }
                if (GetLine(filename, replacenumber(18)).Contains("pak"))
                {
                    DoRevert(replacenumber(18), "[19/20] Reverted ");
                }
                if (GetLine(filename, replacenumber(19)).Contains("pak"))
                {
                    DoRevert(replacenumber(19), "[20/20] Reverted ");
                }
            }
            catch
            {
            }
            label3.Text = "OFF";
            label3.ForeColor = Color.Red;
            s.Stop();
            Log("[-] Reverted item in " + s.Elapsed.TotalMilliseconds + "ms");
            global.WriteSetting(global.ReadSetting(global.Setting.swaplogs).Replace(swapsfrom + " To " + swapsto + ",", ""), global.Setting.swaplogs);
        }

        public static string GetLine(string fileName, int line)
        {
            using (var sr = new StreamReader(fileName))
            {
                for (int i = 1; i < line; i++)
                    sr.ReadLine();
                return sr.ReadLine();
            }
        }

        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        public static string getpak(string filename, int line)
        {
            return global.ReadSetting(global.Setting.Paks) + @"\" + GetLine(filename, line).Replace("pak=", "");
        }

        public static long getoffset(string filename, int line)
        {
            return Convert.ToInt64(GetLine(filename, line).Replace("offset=", ""));
        }
        public static string getType(string filename, int line)
        {
            return GetLine(filename, line).Replace("type=", "");
        }
        private static string swapsfrom { get; set; }
        private static string swapsto { get; set; }
        public void Log(string text)
        {
           title.Text += "[LOG] " + text + Environment.NewLine;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string path = global.ReadSetting(global.Setting.Paks) + @"\pakchunk10_s2-WindowsClient.pak";
            if (File.Exists(path))
            {
                RevertB.Enabled = true;
                ConvertB.Enabled = false;
                this.Swap.RunWorkerAsync();
                title.Clear();
                Log("Starting...");
            }
            else
            {
                MessageBox.Show("Choose Your Pak File Location!");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string path = global.ReadSetting(global.Setting.Paks) + @"\pakchunk10_s2-WindowsClient.pak";
            if (File.Exists(path))
            {
                ConvertB.Enabled = true;
                RevertB.Enabled = false;
                this.Revert.RunWorkerAsync();
                title.Clear();
                Log("Starting...");
            }
            else
            {
                MessageBox.Show("Choose Your Pak File Location!");
            }
        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            label3.Text = "???";
            label3.ForeColor = Color.White;
            ConvertB.Enabled = true;
            RevertB.Enabled = true;
            title.Clear();
            MessageBox.Show("Config Reset! (Now you can either convert or revert for this item)");
        }
        public void ReplaceBytes(string path, byte[] search, byte[] replace, long startoffset, string log)
        {
            Stream stream = File.OpenRead(path);
            long offset = global.FindPosition(stream, 0, startoffset, search);
            stream.Dispose();
            using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(path, FileMode.Open, FileAccess.ReadWrite)))
            {
                binaryWriter.BaseStream.Seek(offset, SeekOrigin.Begin);
                binaryWriter.Write(replace);
                binaryWriter.Close();
            }
            Log(log);
        }
    }
}