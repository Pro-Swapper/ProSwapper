using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using Pro_Swapper.API;

namespace Pro_Swapper
{
    public class Umodel
    {
        private static string PaksLocation = global.CurrentConfig.Paks;
        private static string launchargs = $"-path=\"{PaksLocation}\" -game=ue4.26 -aes=0x{api.apidata.aes}";
        private static string umodelcfg = "SavePackages= { SavePath=\"" + PaksLocation + "\" KeepDirectoryStructure=false }";


        private static void IsolateFileDo(string ucasfile)
        {
            //Rip https://github.com/gildor2/UEViewer/issues/215 so we gotta isolate by changing filename
            List<string> files = Directory.GetFiles(PaksLocation, "*.pak").ToList();
            files.Remove(ucasfile.Replace(".ucas", ".pak"));
            foreach (string file in files)
                File.Move(file, Path.ChangeExtension(file, ".proswapper"));

        }
        private static void IsolateFileUndo(string ucasfile)
        {
            List<string> files = Directory.GetFiles(PaksLocation, "*.proswapper").ToList();
            files.Remove(ucasfile.Replace(".ucas", ".pak"));
            foreach (string file in files)
                File.Move(file, Path.ChangeExtension(file,".pak"));
        }


        public static void SwapItem(api.Item item, bool Converting)
        {
            string path = AppContext.BaseDirectory + @"Umodel\";
            if (!File.Exists(path + "umodel.cfg"))
                File.WriteAllText(path + "umodel.cfg", umodelcfg);

            foreach (api.Asset asset in item.Asset)
            {
                string ucasfile = $"{PaksLocation}\\{asset.UcasFile}";
                IsolateFileDo(ucasfile);
                //Checking if file is readonly coz we wouldn't be able to do shit with it
                File.SetAttributes(ucasfile, global.RemoveAttribute(File.GetAttributes(ucasfile), FileAttributes.ReadOnly));

                string savedpath = $"{PaksLocation}\\{Path.GetFileName(asset.AssetPath)}.uasset";
                if (File.Exists(savedpath))
                    File.Delete(savedpath);
                Process p = new Process();
                p.StartInfo.CreateNoWindow = false;
                p.StartInfo.FileName = path + "umodel.exe";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.Arguments = $" {launchargs} -save {asset.AssetPath}.uasset";
                p.StartInfo.WorkingDirectory = AppContext.BaseDirectory + "\\Umodel\\";
                Console.WriteLine("Filename = " + p.StartInfo.FileName);
                Console.WriteLine("Args = " + p.StartInfo.Arguments);
                p.Start();
                p.WaitForExit();

                IsolateFileUndo(ucasfile);

                FileInfo fileinfoexported = new FileInfo(savedpath);

                bool fileexists = File.Exists(savedpath);
                if (fileinfoexported.Length == 0 && fileexists)
                {
                    File.Delete(savedpath);
                    throw new Exception($"Umodel did not export the specified asset {Path.GetFileName(savedpath)}");
                }



                Console.WriteLine(savedpath);
                Console.WriteLine("Saved file exists = " + fileexists);

                //edit files and compress with oodle and replace
                byte[] compressed = EditAsset(savedpath, item, Converting);//Compressed edited path
                File.WriteAllBytes("a.pak", compressed);//why not log it
                PasteInLocationBytes(ucasfile, compressed, asset.BothHave);
            }
        }


        /// <summary>
        /// Edits the exported file with the "swaps" var and returns the oodle compressed byte array
        /// </summary>
        /// <param name="path"></param>
        /// <param name="swaps"></param>
        /// <returns>Compressed oodle bytes</returns>
        private static byte[] EditAsset(string path, api.Item item, bool Converting)
        {
            byte[] file = File.ReadAllBytes(path);
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Open, FileAccess.ReadWrite)))
            {

                foreach (api.Asset asset in item.Asset)
                {
                    int NumberOfReplaces = asset.Search.Length;
                    for (int i = 0; i < NumberOfReplaces; i++)
                    {
                        byte[] searchB, replaceB;
                        if (Converting)
                        {
                            searchB = ParseByteArray(asset.Search[i]);
                            replaceB = ParseByteArray(asset.Replace[i]);
                        }
                        else
                        {
                            searchB = ParseByteArray(asset.Replace[i]);
                            replaceB = ParseByteArray(asset.Search[i]);
                        }

                        writer.BaseStream.Seek(IndexOfSequence(file, searchB), SeekOrigin.Begin);
                        writer.Write(SameLength(searchB, replaceB));
                    }

                }
                writer.Close();
            }


            byte[] compressed = Oodle.OodleClass.Compress(path);
            if (File.Exists(path))
                File.Delete(path);
            return compressed;
        }

        private static byte[] OodleChar = global.HexToByte("80C6");

        private static byte[] SameLength(byte[] search, byte[] replace)
        {
            List<byte> result = new List<byte>(replace);
            int difference = search.Length - replace.Length;
            for (int i = 0; i < difference; i++)
                result.Add(0);
            return result.ToArray();
        }

        private static void PasteInLocationBytes(string path, byte[] towrite, string bothhave)
        {
            int compressedchar = IndexOfSequence(towrite, OodleChar);//compressedoodlecharindex
            towrite = towrite.Skip(compressedchar).ToArray();
            //Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length);
            byte[] file = File.ReadAllBytes(path);

            global.BoyerMoore a = new global.BoyerMoore(ParseByteArray(bothhave));
            int offset = a.Search(file, true)[0];//Gets offset of "bothhave" aka get the whereabouts of the bytes;
            Console.WriteLine($"{bothhave} offset = {offset}");

            int offset2 = IndexOfSequence(file, OodleChar, offset - 400);//-400 so its before the start and get the offset
            Console.WriteLine($"{bothhave}'s 80C6 offset = {offset2}");
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Open, FileAccess.ReadWrite)))
            {
                writer.BaseStream.Seek(offset2, SeekOrigin.Begin);
                writer.Write(towrite);
                writer.Close();
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
    }
}
