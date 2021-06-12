using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Pro_Swapper.API;
using System.Threading.Tasks;
namespace Pro_Swapper
{
    public static class Swap
    {
        private static string PaksLocation = global.CurrentConfig.Paks;
        public static async Task SwapItem(api.Item item, bool Converting)
        {
            List<FinalPastes> finalPastes = new List<FinalPastes>();
            foreach (api.Asset asset in item.Asset)
            {
                string ucasfile = $"{PaksLocation}\\{asset.UcasFile}";

                //Checking if file is readonly coz we wouldn't be able to do shit with it
                File.SetAttributes(ucasfile, global.RemoveAttribute(File.GetAttributes(ucasfile), FileAttributes.ReadOnly));


                byte[] exportasset = Fortnite.FortniteExport.ExportAsset(asset.UcasFile, asset.AssetPath);
#if DEBUG
                Directory.CreateDirectory("Exports");

                string smallname = Path.GetFileName(asset.AssetPath);
                File.WriteAllBytes($"Exports\\Exported_{smallname}.pak", exportasset);//Just simple export
                File.WriteAllBytes($"Exports\\RawExport_{smallname}.pak", RawExported);//Uncompress exported by CUE4Parse
                
#endif
                //edit files and compress with oodle and replace
                byte[] edited = EditAsset(exportasset, asset, Converting, out bool Compress);//Compressed edited path

                if (!Compress)//File hasnt gotten any changes, no need to edit files that havent changed
                    continue;


                //byte[] towrite = MermaidCompress(edited);
                byte[] towrite = Oodle.OodleClass.Compress(edited);
#if DEBUG
                //Logging stuff for devs hehe
                File.WriteAllBytes($"Exports\\Edited_{smallname}.pak", edited);//Edited export
                File.WriteAllBytes($"Exports\\Compressed{smallname}.pak", towrite);//Compressed edited export

#endif
                finalPastes.Add(new FinalPastes(ucasfile, towrite, RawExported));
            }

            List<Task> tasklist = new List<Task>();
            //Actually put into game files:
            foreach (FinalPastes pastes in finalPastes)
                tasklist.Add(Task.Run(() => PasteInLocationBytes(pastes)));

            await Task.WhenAll(tasklist);
        }

        public class FinalPastes
        {
            public string ucasfile { get; set; }
            public byte[] towrite { get; set; }
            public byte[] RawExported { get; set; }
            public FinalPastes(string Ucasfile, byte[] ToWrite, byte[] rawExported)
            {
                ucasfile = Ucasfile;
                towrite = ToWrite;
                RawExported = rawExported;
            }
        }


        public static byte[] RawExported { get; set; }

        //Edits a byte array in memory
        private static byte[] EditAsset(byte[] file, api.Asset asset, bool Converting, out bool Compress)
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
                            RealReplace = SameLength(searchB, replaceB);
                            SearchIndex = IndexOfSequence(file, searchB);
                            if (SearchIndex == -1)//replace cannot be found so that means it is already swapped so skip it.
                                continue;
                        }
                        else
                        {
                        RealReplace = SameLength(replaceB, searchB);
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

        

        private static byte[] SameLength(byte[] search, byte[] replace)
        {
            List<byte> result = new List<byte>(replace);
            int difference = search.Length - replace.Length;
            for (int i = 0; i < difference; i++)
                result.Add(0);


            return result.ToArray();
        }
        //private static byte[] OodleChar = global.HexToByte("8C");
        private static void PasteInLocationBytes(FinalPastes finalpaste)
        {
            //Define our pak editor stream
            using (FileStream PakEditor = File.Open(finalpaste.ucasfile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {

                //Offset for "bothhave" for the general area of our bytes
                int offset = (int)Algorithms.BoyerMoore.IndexOf(PakEditor, finalpaste.RawExported);
                //Offset is where Œ char is

                PakEditor.Position = offset;
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
