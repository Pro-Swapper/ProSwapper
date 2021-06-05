using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Pro_Swapper.API;

namespace Pro_Swapper
{
    public static class Swap
    {
        private static string PaksLocation = global.CurrentConfig.Paks;
        public static void SwapItem(api.Item item , out bool Output)
        {
            Output = false;//It'll get assigned to a different var
            foreach (api.Asset asset in item.Asset)
            {
                string ucasfile = $"{PaksLocation}\\{asset.UcasFile}";
               
                //Checking if file is readonly coz we wouldn't be able to do shit with it
                File.SetAttributes(ucasfile, global.RemoveAttribute(File.GetAttributes(ucasfile), FileAttributes.ReadOnly));


                byte[] exportasset = Fortnite.FortniteExport.ExportAsset(asset.UcasFile, asset.AssetPath);

                //edit files and compress with oodle and replace
                byte[] edited = EditAsset(exportasset, asset, out bool Converting);//Compressed edited path
                byte[] towrite = Oodle.OodleClass.Compress(edited);
                #if DEBUG
                //Logging stuff for devs hehe
                File.WriteAllBytes("Exported.pak", exportasset);//Just simple export
                File.WriteAllBytes("Edited.pak", edited);//Edited export
                File.WriteAllBytes("Compressed.pak", towrite);//Compressed edited export
                File.WriteAllBytes("RawExported.pak", RawExported);//Uncompress exported by CUE4Parse
                #endif
                //handle new and original have byte searches
                Output = Converting;
                PasteInLocationBytes(ucasfile, towrite, RawExported);
            }
        }


        public static byte[] RawExported { get; set; }

        //Edits a byte array in memory
        private static byte[] EditAsset(byte[] file, api.Asset asset, out bool Converting)
        {
            Converting = true;//Have to define this or error
            using (MemoryStream stream = new MemoryStream(file))
            {
                    int NumberOfReplaces = asset.Search.Length;
                    for (int i = 0; i < NumberOfReplaces; i++)
                    {
                        byte[] searchB = ParseByteArray(asset.Search[i]);
                        byte[] replaceB = ParseByteArray(asset.Replace[i]);
                        byte[] RealReplace;
                        
                        //Check which one exists
                        int SearchIndex = IndexOfSequence(file, searchB);
                        int ReplaceIndex = IndexOfSequence(file, replaceB);


                        //The if the search index is found that means we're converting
                        Converting = SearchIndex > ReplaceIndex;


                        if (Converting)
                        {
                            //Search is in the byte array
                            RealReplace = replaceB;
                            RealReplace = SameLength(searchB, replaceB);//Search is going to be longer than replace
                        }
                        else
                        {
                        RealReplace = searchB;
                        RealReplace = SameLength(replaceB, searchB);
                        }
                            

                        stream.Position = Math.Max(SearchIndex, ReplaceIndex);
                        stream.Write(RealReplace, 0, RealReplace.Length);
                    }
                return stream.ToArray();
            }
        }

        private static byte[] OodleChar = global.HexToByte("8C");
        private static byte[] SameLength(byte[] search, byte[] replace)
        {
            byte[] nullbyte = { 0 };
            int difference = search.Length - replace.Length;
            for (int i = 0; i < difference; i++)
                replace = Combine(replace, nullbyte);


            return replace;

        }

        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        private static void PasteInLocationBytes(string path, byte[] towrite, byte[] bothhave)
        {
            //Define our pak editor stream
            using (FileStream PakEditor = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {

                //Offset for "bothhave" for the general area of our bytes
                int offset = (int)Algorithms.BoyerMoore.IndexOf(PakEditor, bothhave);
                //Our actual offset of our file for Œ char
                int actualoffset = (int)FindPosition(PakEditor,0, offset - 400, OodleChar);

                
                PakEditor.Position = actualoffset;
                PakEditor.Write(towrite, 0, towrite.Length);
            }
        }
        private static byte[] ParseByteArray(string encodedtxt)
            {
            if (encodedtxt.StartsWith("hex="))
                return global.HexToByte(encodedtxt);
            else//eww using text but atleast we can read it
                return Encoding.ASCII.GetBytes(encodedtxt);
        }
        //Originally: https://stackoverflow.com/a/332667/12897035
        private static int IndexOfSequence(byte[] buffer, byte[] pattern, int startIndex = 0)
        {
            int i = Array.IndexOf(buffer, pattern[0], startIndex);
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
        //Researcher
        public static long FindPosition(Stream stream, int searchPosition, long startIndex, byte[] searchPattern)
        {
            long searchResults = 0;
            stream.Position = startIndex;
            while (true)
            {
                if (stream.Position == stream.Length)
                    return searchResults;

                var latestbyte = stream.ReadByte();
                if (latestbyte == -1)
                    break;

                if (latestbyte == searchPattern[searchPosition])
                {
                    searchPosition++;
                    if (searchPosition == searchPattern.Length)
                        return stream.Position - searchPattern.Length;
                }
                else
                    searchPosition = 0;
            }

            return searchResults;
        }
    }
}
