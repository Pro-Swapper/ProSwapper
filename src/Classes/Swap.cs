using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Pro_Swapper.API;
using System.Threading.Tasks;
using CUE4Parse.FileProvider;
using System.Windows.Forms;
namespace Pro_Swapper
{
    public static class Swap
    {
        private static string PaksLocation = global.CurrentConfig.Paks;
        public static bool SwapItem(api.Item item, bool Converting)
        {
            const string ProSwapperPakFolder = ".ProSwapper";
            List<string> UsingFiles = new List<string>();
            UsingFiles.AddRange(item.Asset.Select(x => Path.GetFileNameWithoutExtension(x.UcasFile)).Distinct());
            if (!global.CanSwap(UsingFiles))
                return false;

            foreach (string file in UsingFiles)
            {
                string BaseFileName = $"{PaksLocation}\\{ProSwapperPakFolder}\\{file}";

                //Check if it may be old game version
                string OriginalSig = global.FileToMd5($"{PaksLocation}\\{file}.sig");
                string ModifiedSig = global.FileToMd5(BaseFileName + ".sig");
                if (OriginalSig != ModifiedSig)
                {
                    global.DeleteFile(BaseFileName + ".sig");
                    global.DeleteFile(BaseFileName + ".utoc");
                    global.DeleteFile(BaseFileName + ".ucas");
                    global.DeleteFile(BaseFileName + ".pak");
                }


                if (!File.Exists(BaseFileName + ".ucas"))
                {
                    Directory.CreateDirectory(PaksLocation + $"\\{ProSwapperPakFolder}");
                    File.Copy($"{PaksLocation}\\{file}.sig", BaseFileName + ".sig", true);
                    File.Copy($"{PaksLocation}\\{file}.utoc", BaseFileName + ".utoc", true);
                    File.Copy($"{PaksLocation}\\{file}.ucas", BaseFileName + ".ucas", true);
                    File.Copy($"{PaksLocation}\\{file}.pak", BaseFileName + ".pak", true);
                }
            }

            var Provider = new DefaultFileProvider($"{PaksLocation}\\{ProSwapperPakFolder}", SearchOption.TopDirectoryOnly, false, new CUE4Parse.UE4.Versions.VersionContainer(CUE4Parse.UE4.Versions.EGame.GAME_UE5_LATEST));
            Provider.Initialize();

            //Load all aes keys for required files, cleaner in linq than doing a loop
            Provider.UnloadedVfs.All(x => { Provider.SubmitKey(x.EncryptionKeyGuid, api.AESKey); return true; });

            List<FinalPastes> finalPastes = new List<FinalPastes>();
            foreach (api.Asset asset in item.Asset)
            {
                string ucasfile = $"{PaksLocation}\\{ProSwapperPakFolder}\\{asset.UcasFile}";
                File.SetAttributes(ucasfile, global.RemoveAttribute(File.GetAttributes(ucasfile), FileAttributes.ReadOnly));
                byte[] exportasset = Fortnite.FortniteExport.ExportAsset(Provider, asset.UcasFile, asset.AssetPath);
                // Directory.CreateDirectory("Exports");

                string smallname = Path.GetFileName(asset.AssetPath);
#if DEBUG
                File.WriteAllBytes($"Exports\\Exported_{smallname}.pak", exportasset);//Just simple export
#endif
                if (EditAsset(ref exportasset, asset, Converting))
                {
#if DEBUG
                    File.WriteAllBytes($"Exports\\Edited_{smallname}.pak", exportasset);//Edited export
#endif
                    exportasset = Oodle.OodleClass.Compress(exportasset);
                    //Logging stuff for devs hehe
                    // File.WriteAllBytes($"Exports\\Compressed{smallname}.pak", exportasset);//Compressed edited export
                    finalPastes.Add(new FinalPastes(ucasfile, exportasset, Fortnite.FortniteExport.Offset));
                }
            }
            Provider.Dispose();
            foreach (FinalPastes pastes in finalPastes)
                PasteInLocationBytes(pastes);

            return true;
        }


        public class FinalPastes
        {
            public string ucasfile { get; private set; }
            public byte[] towrite { get; private set; }
            public long Offset { get; private set; }
            public FinalPastes(string Ucasfile, byte[] ToWrite, long offset)
            {
                ucasfile = Ucasfile;
                towrite = ToWrite;
                Offset = offset;
            }
        }

        public static byte[] RawExport { get; set; }

        //Edits a byte array in memory
        public static byte[] EditAsset(byte[] file, api.Asset asset, bool Converting, out bool Compress)
        {
            Compress = false;
            using (MemoryStream stream = new MemoryStream(file))
            {
                int NumberOfReplaces = asset.Search.Length;
                for (int i = 0; i < NumberOfReplaces; i++)
                {
                    byte[] searchB = ParseByteArray(asset.Search[i]);
                    byte[] replaceB = ParseByteArray(asset.Replace[i]);
                    byte[] RealReplace;
                    int ReplaceIndex = 0, SearchIndex = 0;


                    if (Converting)
                    {
                        //Search is in the byte array
                        RealReplace = FillEnd(replaceB, searchB.Length);
                        SearchIndex = IndexOfSequence(file, searchB);
                        if (SearchIndex == -1)//replace cannot be found so that means it is already swapped so skip it.
                            continue;
                    }
                    else
                    {
                        RealReplace = FillEnd(searchB, replaceB.Length);
                        ReplaceIndex = IndexOfSequence(file, replaceB);
                        if (ReplaceIndex == -1)//search cannot be found so that means it is already swapped so skip it.
                            continue;
                    }
                    Compress = true;//Change has been made so compress it

                    stream.Position = Math.Max(SearchIndex, ReplaceIndex);
                    stream.Write(RealReplace, 0, RealReplace.Length);
                }
                return stream.ToArray();
            }
        }



        public static bool EditAsset(ref byte[] file, api.Asset Asset, bool Converting)
        {
            using (MemoryStream stream = new MemoryStream(file))
            {
                bool Ready = false;
                int ReplaceCount = Asset.Search.Length;
                for (int i = 0; i < ReplaceCount; i++)
                {
                    byte[] searchB = ParseByteArray(Asset.Search[i]);
                    byte[] replaceB = ParseByteArray(Asset.Replace[i]);

                    int SearchOffset = IndexOfSequence(file, searchB);
                    int ReplaceOffset = IndexOfSequence(file, replaceB);

                    if (SearchOffset != -1 || ReplaceOffset != -1)
                    {
                        Ready = true;

                        if (searchB.Length >= replaceB.Length)
                        {
                            replaceB = FillEnd(replaceB, searchB.Length);

                            if (Converting)
                            {
                                stream.Position = SearchOffset;
                                stream.Write(replaceB, 0, replaceB.Length);
                                continue;
                            }
                            else
                            {
                                stream.Position = ReplaceOffset;
                                stream.Write(searchB, 0, searchB.Length);
                                continue;
                            }
                        }
                        else
                        {
                            if (Converting)
                            {

                                stream.Position = SearchOffset;
                                byte[] ConvertCheck = stream.ToArray();
                                if (ConvertCheck[SearchOffset + 2] == Convert.ToByte(0))
                                {
                                    int SearchLengthWeHave = SearchOffset;
                                    while (true)
                                    {
                                        if (Convert.ToByte(stream.ReadByte()) == 0)
                                        {
                                            SearchLengthWeHave++;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    SearchLengthWeHave--;//Last char is to represent end of string in uasset
                                    if (SearchLengthWeHave > replaceB.Length)
                                    {
                                        stream.Position = SearchOffset;
                                        replaceB = FillEnd(replaceB, SearchLengthWeHave);
                                        stream.Write(replaceB, 0, replaceB.Length);
                                        continue;
                                    }
                                    else
                                    {
                                        searchB = FillEnd(searchB, SearchLengthWeHave);
                                        byte[] tmp = ReplaceAnyLength(stream.ToArray(), searchB, replaceB);
                                        stream.Position = 0;
                                        stream.Write(tmp, 0, tmp.Length);
                                        continue;
                                    }
                                }
                                else
                                {
                                    byte[] tmp = ReplaceAnyLength(stream.ToArray(), searchB, replaceB);
                                    stream.Position = 0;
                                    stream.Write(tmp, 0, tmp.Length);
                                    continue;
                                }



                            }
                            else
                            {
                                //Just paste in the replace
                                if (ReplaceOffset != -1)
                                {
                                    stream.Position = ReplaceOffset;
                                    stream.Write(FillEnd(searchB, replaceB.Length), 0, replaceB.Length);
                                }
                                continue;
                            }
                        }
                    }
                }
                file = stream.ToArray();
                return Ready;
            }
        }

        private static readonly byte[] FileEnd = new byte[] { 248, 112 };
        public static byte[] ReplaceAnyLength(byte[] file, byte[] search, byte[] replace)
        {
            List<byte> File = new List<byte>(file);

            int SearchOffset = IndexOfSequence(file, search);//Get our search offset
            File.RemoveRange(SearchOffset, search.Length);//Delete our search string
            File.InsertRange(SearchOffset, replace);//Insert our new replace string
            File[SearchOffset - 1] = Convert.ToByte(replace.Length);//Change the 1 byte int of the search length
            File.RemoveRange(IndexOfSequence(File.ToArray(), FileEnd) - 245, replace.Length - search.Length);//Remove the difference in length

            return File.ToArray();
        }

        public static byte[] FillEnd(byte[] buffer, int len)
        {
            List<byte> result = new List<byte>(buffer);
            result.AddRange(Enumerable.Repeat((byte)0, len - buffer.Length));
            return result.ToArray();
        }
        public static void PasteInLocationBytes(FinalPastes finalpaste)
        {
            //Define our pak editor stream
            using (FileStream PakEditor = File.Open(finalpaste.ucasfile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {

                //Offset for "bothhave" for the general area of our bytes
                //int offset = (int)Algorithms.BoyerMoore.IndexOf(PakEditor, finalpaste.RawExported);
                //Offset is where Œ char is
                PakEditor.Position = finalpaste.Offset;
                PakEditor.Write(finalpaste.towrite, 0, finalpaste.towrite.Length);
            }
        }
        private static byte[] ParseByteArray(string encodedtxt)
        {
            if (encodedtxt.StartsWith("hex="))
                return global.HexToByte(encodedtxt);
            else//eww using text but atleast we can read it
                return Encoding.Default.GetBytes(encodedtxt);
        }
        //Originally: https://stackoverflow.com/a/332667/12897035
        private static int IndexOfSequence(byte[] buffer, byte[] pattern)
        {
            int i = Array.IndexOf(buffer, pattern[0], 0);
            while (i >= 0 && i <= buffer.Length - pattern.Length)
            {
                byte[] segment = new byte[pattern.Length];
                Buffer.BlockCopy(buffer, i, segment, 0, pattern.Length);
                if (segment.SequenceEqual(pattern))
                    return i;
                i = Array.IndexOf(buffer, pattern[0], i + 1);
            }
            return -1;
        }
    }
}
