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
            this.Icon = Main.appIcon;
            RPC.SetState(item.SwapsFrom + " To " + item.SwapsTo, true);
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
            if (global.CurrentConfig.swaplogs.Contains(ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + " (Lobby),"))
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
                Program.logger.Log($"(ZlibSwap.cs) (Converting = {Converting}) Starting to convert {this.Text}");
                bool Swapped = Task.Run(() => SwapZlib(ThisItem, Converting)).Result;

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
                    global.CurrentConfig.swaplogs += ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + " (Lobby),";
                }
                else
                {
                    LogMsg = $"[-] Reverted {ThisItem.SwapsFrom} to {ThisItem.SwapsTo} in {s.ElapsedMilliseconds}ms";
                    label3.Invoke(new Action(() => { label3.Text = "OFF"; label3.ForeColor = Color.Red; }));
                    global.CurrentConfig.swaplogs = global.CurrentConfig.swaplogs.Replace(ThisItem.SwapsFrom + " To " + ThisItem.SwapsTo + " (Lobby),", "");
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

        private void SwapButton_Click(object sender, EventArgs e)
        {
            string path = global.CurrentConfig.Paks + @"\pakchunk0-WindowsClient.sig";
            if (!File.Exists(path))
            {
                MessageBox.Show("Select your paks folder in Settings", "Pro Swapper");
                return;
            }
            if (!EpicGamesLauncher.CloseFNPrompt())
                return;
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
            logbox.Clear();
            Log("Loading...");
            bool isconverting = ((BunifuFlatButton)(sender)).Text == "Convert";
            Task.Run(() => ButtonbgWorker(isconverting));
        }
        private void ExitButton_Click(object sender, EventArgs e) => Close();
        private void button2_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
        private void swap_MouseDown(object sender, MouseEventArgs e) => global.MoveForm(e, Handle);


        public static string SearchString = null;

        public static ZlibBlock zlibblock = null;

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



        public static bool SwapZlib(api.Item item, bool Converting)
        {
            const string ProSwapperPakFolder = "Pro Swapper Lobby";
            string PaksLocation = global.CurrentConfig.Paks;
            //Load the exporter
            List<string> thesefiles = new List<string>();
            foreach (var Asset in item.Asset)
                thesefiles.Add(Path.GetFileNameWithoutExtension(Asset.UcasFile));

            List<string> UsingFiles = thesefiles.Distinct().ToList();
            if (!global.CanSwap(UsingFiles))
                return false;

            foreach (string file in UsingFiles)
            {
                string BaseFileName = $"{PaksLocation}\\{ProSwapperPakFolder}\\{file}";


                //Check if it may be old game version
                string OriginalSig = global.FileToMd5($"{PaksLocation}\\{file}.sig");
                string ModifiedSig = global.FileToMd5(BaseFileName + ".sig");
                if (OriginalSig != ModifiedSig)
                    Lobby.RevertAllLobbySwaps();


                if (!File.Exists(BaseFileName + ".pak"))
                {
                    Directory.CreateDirectory(PaksLocation + $"\\{ProSwapperPakFolder}");
                    File.Copy($"{PaksLocation}\\{file}.sig", BaseFileName + ".sig", true);
                    File.Copy($"{PaksLocation}\\{file}.utoc", BaseFileName + ".utoc", true);
                    File.Copy($"{PaksLocation}\\{file}.ucas", BaseFileName + ".ucas", true);
                    File.Copy($"{PaksLocation}\\{file}.pak", BaseFileName + ".pak", true);
                }


            }

            var Provider = new DefaultFileProvider($"{PaksLocation}\\{ProSwapperPakFolder}", SearchOption.TopDirectoryOnly);
            Provider.Initialize(UsingFiles);


            //Load all aes keys for required files, cleaner in linq than doing a loop
            Provider.UnloadedVfs.All(x => { Provider.SubmitKey(x.EncryptionKeyGuid, api.AESKey); return true; });


            List<FinalPastes> finalPastes = new List<FinalPastes>();
            foreach (api.Asset asset in item.Asset)
            {
                string ucasfile = $"{PaksLocation}\\{ProSwapperPakFolder}\\{asset.UcasFile}";

                //Checking if file is readonly coz we wouldn't be able to do shit with it
                File.SetAttributes(ucasfile, global.RemoveAttribute(File.GetAttributes(ucasfile), FileAttributes.ReadOnly));

                foreach (string srch in asset.Search)
                {
                    SearchString = srch;

                    Fortnite.FortniteExport.ExportAsset(Provider, asset.UcasFile, asset.AssetPath);

                    byte[] edited = EditAsset(zlibblock.decompressed, asset, Converting, out bool Compress);//Compressed edited path

                    byte[] towrite = ByteCompression.Compress(edited);//Compress to zlib

                    towrite = FillEnd(towrite, zlibblock.compressed.Length);

                    finalPastes.Add(new FinalPastes(ucasfile, towrite, zlibblock.BlockStart));
                }

                //                if (Converting)
                //                    SearchString = asset.Search[0];
                //                else
                //                    SearchString = asset.Replace[0];
                //                //Use this to define zlibblock var
                //                Fortnite.FortniteExport.ExportAsset(Provider, asset.UcasFile, asset.AssetPath);
                //#if DEBUG
                //                Directory.CreateDirectory("Exports");

                //                string smallname = Path.GetFileName(asset.AssetPath);
                //                File.WriteAllBytes($"Exports\\Exported_{smallname}.pak", zlibblock.decompressed);//Just simple export
                //                                                                                                 // File.WriteAllBytes($"Exports\\RawExport_{smallname}.pak", RawExported);//Uncompress exported by CUE4Parse

                //#endif
                //                //edit files and compress with oodle and replace
                //                byte[] edited = EditAsset(zlibblock.decompressed, asset, Converting, out bool Compress);//Compressed edited path
                //                if (!Compress)//File hasnt gotten any changes, no need to edit files that havent changed
                //                    continue;


                //                byte[] towrite = ByteCompression.Compress(edited);//Compress to zlib

                //                towrite = FillEnd(towrite, zlibblock.compressed.Length);
                //#if DEBUG
                //                //Logging stuff for devs hehe
                //                File.WriteAllBytes($"Exports\\Edited_{smallname}.pak", edited);//Edited export
                //                File.WriteAllBytes($"Exports\\Compressed{smallname}.pak", towrite);//Compressed edited export

                //#endif
                //                finalPastes.Add(new FinalPastes(ucasfile, towrite, zlibblock.BlockStart));

            }

            Provider.Dispose();
            foreach (FinalPastes pastes in finalPastes)
                PasteInLocationBytes(pastes);

            return true;
        }
    }
}