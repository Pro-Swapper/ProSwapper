using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using Pro_Swapper.API;
using Bunifu.Framework.UI;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CUE4Parse.FileProvider;
using CUE4Parse.UE4.Vfs;
using static Pro_Swapper.Swap;
using Ionic.Zlib;

namespace Pro_Swapper
{
    public partial class ZlibSwap : Form
    {
        private api.Item ThisItem;

        public ZlibSwap(api.Item item)
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

                await Task.Run(() => SwapAsync(ThisItem, Converting));

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


        public static string SearchString = null;

        public static ZlibBlock zlibblock;

        public class ZlibBlock
        {
            public long BlockStart { get; set; }
            public long BlockEnd { get; set; }
            public byte[] decompressed { get; set; }
            public byte[] compressed { get; set; }
            public ZlibBlock(long start, long end, byte[] decomp, byte[] comp)
            {
                BlockStart = start;
                BlockEnd = end;
                decompressed = decomp;
                compressed = comp;
            }

        }



        public static async Task SwapAsync(api.Item item, bool Converting)
        {
                //Load the exporter
                List<string> thesefiles = new List<string>();
                foreach (var Asset in item.Asset)
                    thesefiles.Add(Path.GetFileNameWithoutExtension(Asset.UcasFile));

                List<string> UsingFiles = thesefiles.Distinct().ToList();


                var Provider = new DefaultFileProvider(global.CurrentConfig.Paks, SearchOption.TopDirectoryOnly);
                Provider.Initialize(UsingFiles);

                //Load all aes keys for required files, cleaner in linq than doing a loop
                Provider.UnloadedVfs.All(x => { Provider.SubmitKey(x.EncryptionKeyGuid, api.fAesKey); return true; });


                List<FinalPastes> finalPastes = new List<FinalPastes>();

                int assetnum = 0;

                for (int i = 0; i < item.Asset.Length; i++)
                {
                    api.Asset asset = item.Asset[i];

                string ucasfile = $"{global.CurrentConfig.Paks}\\{asset.UcasFile}";

                //Checking if file is readonly coz we wouldn't be able to do shit with it
                File.SetAttributes(ucasfile, global.RemoveAttribute(File.GetAttributes(ucasfile), FileAttributes.ReadOnly));

                if (Converting)
                SearchString = asset.Search[assetnum];
                else
                SearchString = asset.Replace[assetnum];
                //Use this to define zlibblock var
                Fortnite.FortniteExport.ExportAsset(Provider, asset.UcasFile, asset.AssetPath);

#if DEBUG
                Directory.CreateDirectory("Exports");

                string smallname = Path.GetFileName(asset.AssetPath);
                File.WriteAllBytes($"Exports\\Exported_{smallname}.pak", zlibblock.decompressed);//Just simple export
                                                                                                   // File.WriteAllBytes($"Exports\\RawExport_{smallname}.pak", RawExported);//Uncompress exported by CUE4Parse

#endif
                //edit files and compress with oodle and replace
                byte[] edited = EditAsset(zlibblock.decompressed, asset, Converting, out bool Compress);//Compressed edited path
                assetnum++;
                if (!Compress)//File hasnt gotten any changes, no need to edit files that havent changed
                    continue;


                byte[] towrite = ZlibCompress(edited);

                towrite = SetLength(towrite, zlibblock.compressed);

#if DEBUG
                //Logging stuff for devs hehe
                File.WriteAllBytes($"Exports\\Edited_{smallname}.pak", edited);//Edited export
                File.WriteAllBytes($"Exports\\Compressed{smallname}.pak", towrite);//Compressed edited export

#endif
                finalPastes.Add(new FinalPastes(ucasfile, towrite, zlibblock.BlockStart));

            }

            Provider.Dispose();
            List<Task> tasklist = new List<Task>();
            //Actually put into game files:
            foreach (FinalPastes pastes in finalPastes)
                tasklist.Add(Task.Run(() => PasteInLocationBytes(pastes)));

            await Task.WhenAll(tasklist);
        }


        public static byte[] ZlibCompress(byte[] input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (ZlibStream zls = new ZlibStream(ms, CompressionMode.Compress, CompressionLevel.Level7))
                    zls.Write(input, 0, input.Length);
                
                return ms.ToArray();
            }
        }

    }
}