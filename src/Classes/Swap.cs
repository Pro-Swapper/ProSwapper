using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Pro_Swapper.API;
using System.Threading.Tasks;
using CUE4Parse.FileProvider;
using System.Windows.Forms;
using File = System.IO.File;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.Compression;
using Ionic.Zlib;
using Pro_Swapper.src.Classes;

namespace Pro_Swapper
{
    public static class Swap
    {
        private static string PaksLocation = global.CurrentConfig.Paks;
        //private const string ProSwapperPakFolder = ".ProSwapper";
        public class ExportData
        {
            public ExportData(long offset, string fileName, CompressionMethod compressionMethod)
            {
                this.offset = offset;
                this.filePath = fileName;
                this.compressionMethod = compressionMethod;
            }
            public CompressionMethod compressionMethod;
            public long offset;
            public string filePath;
            public string fileName => Path.GetFileName(filePath);
            public byte[] compressedBuffer;
        }
        public static ExportData exportData;
        public static bool IsExporting = false;
        #region deprecated duplicate file
        //private static void DuplicateFile(string file)
        //{
        //    file = Path.GetFileNameWithoutExtension(file);
        //    string BaseFileName = $"{PaksLocation}\\{ProSwapperPakFolder}\\{file}";

        //    //Check if it may be old game version
        //    string OriginalSig = global.FileToMd5($"{PaksLocation}\\{file}.sig");
        //    string ModifiedSig = global.FileToMd5(BaseFileName + ".sig");
        //    if (OriginalSig != ModifiedSig)
        //    {
        //        global.DeleteFile(BaseFileName + ".sig");
        //        global.DeleteFile(BaseFileName + ".utoc");
        //        global.DeleteFile(BaseFileName + ".ucas");
        //        global.DeleteFile(BaseFileName + ".pak");
        //    }


        //    if (!File.Exists(BaseFileName + ".ucas"))
        //    {
        //        Directory.CreateDirectory(PaksLocation + $"\\{ProSwapperPakFolder}");
        //        File.Copy($"{PaksLocation}\\{file}.sig", BaseFileName + ".sig", true);
        //        File.Copy($"{PaksLocation}\\{file}.utoc", BaseFileName + ".utoc", true);
        //        File.Copy($"{PaksLocation}\\{file}.ucas", BaseFileName + ".ucas", true);
        //        File.Copy($"{PaksLocation}\\{file}.pak", BaseFileName + ".pak", true);
        //    }
        //}
        #endregion
        public static DefaultFileProvider Provider = null;

        public static DefaultFileProvider GetProvider()
        {
            if (Provider == null)
            {
                Provider = new DefaultFileProvider($"{PaksLocation}", SearchOption.TopDirectoryOnly, false, new CUE4Parse.UE4.Versions.VersionContainer(CUE4Parse.UE4.Versions.EGame.GAME_UE5_LATEST));
                Provider.Initialize();

            }
            
            return Provider;
        }

        public static bool SwapItem(api.Item item, bool Converting)
        {
            DefaultFileProvider Provider = GetProvider();


            //Load all aes keys for required files
            foreach (var vfs in Provider.UnloadedVfs)
            {
                if (!vfs.Name.Contains("optional"))
                {
                    Provider.SubmitKey(vfs.EncryptionKeyGuid, api.AESKey);
                }
            }

            List<FinalPastes> finalPastes = new List<FinalPastes>();
            foreach (api.Asset asset in item.Asset)
            {
                byte[] exportasset = Fortnite.FortniteExport.ExportAsset(Provider, asset.AssetPath);
                // Directory.CreateDirectory("Exports");
                //IF DuplicateFile

                //string ucasfile = $"{PaksLocation}\\{ProSwapperPakFolder}\\{exportData.fileName}";
                //ENDIF
                string ucasfile = exportData.filePath;
                string smallname = Path.GetFileName(asset.AssetPath);
#if DEBUG
                Directory.CreateDirectory("Exports");
                File.WriteAllBytes($"Exports\\{smallname}_Raw.pak", exportData.compressedBuffer);//Just simple export
                File.WriteAllBytes($"Exports\\{smallname}_Decompressed.pak", exportasset);//Just simple export
#endif
                if (EditAsset(ref exportasset, asset, Converting))
                {
#if DEBUG
                    File.WriteAllBytes($"Exports\\{smallname}_Edited.pak", exportasset);//Edited export
#endif

                    switch (exportData.compressionMethod)
                    {
                        case CompressionMethod.None:
                            break;
                        case CompressionMethod.Oodle:
                            exportasset = Oodle.Compress(exportasset);
                            break;
                        case CompressionMethod.Zlib:
                            ByteCompression.Compress(exportasset);
                            break;
                    }
#if DEBUG
                    File.WriteAllBytes($"Exports\\{smallname}_Compress_Edited_{exportData.compressionMethod}.pak", exportasset);//Compressed edited export
#endif
                    //DuplicateFile(ucasfile);
                    File.SetAttributes(ucasfile, global.RemoveAttribute(File.GetAttributes(ucasfile), FileAttributes.ReadOnly));
                    if (exportasset.Length <= exportData.compressedBuffer.Length)
                    {
                        RevertEngine.CreateRevertItem(new RevertItem(exportData.offset, exportData.compressedBuffer, exportData.fileName, smallname));
                        finalPastes.Add(new FinalPastes(ucasfile, exportasset, exportData.offset));
                    }
                    else
                    {
                        MessageBox.Show("The edited asset is larger than the original one");
                    }

                }
            }
            //Provider.Dispose();, we don't need to dispose it anymore because CUE4Parse is modified with FileShare.ReadWrite
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

        public static bool EditAsset(ref byte[] file, api.Asset Asset, bool Converting)
        {
            if (Asset.Search.Length != Asset.Replace.Length)
                return false;
            using (MemoryStream stream = new MemoryStream(file))
            {
                for (int i = 0; i < Asset.Search.Length; i++)
                {
                    byte[] searchB = ParseByteArray(Asset.Search[i]);
                    byte[] replaceB = ParseByteArray(Asset.Replace[i]);
                    FillEnd(ref replaceB, searchB.Length);
                    if (Converting)
                    {
                        int SearchOffset = IndexOfSequence(file, searchB);
                        stream.Position = SearchOffset;
                        FillEnd(ref replaceB, searchB.Length);
                        stream.Write(replaceB, 0, replaceB.Length);
                    }
                    else
                    {
                        int ReplaceOffset = IndexOfSequence(file, replaceB);
                        stream.Position = ReplaceOffset;
                        stream.Write(searchB, 0, searchB.Length);
                    }
                }
                file = stream.ToArray();
                return true;
            }
        }

        public static void FillEnd(ref byte[] buffer, int len) => Array.Resize(ref buffer, len);
        public static void PasteInLocationBytes(FinalPastes finalpaste)
        {
            using (FileStream PakEditor = File.Open(finalpaste.ucasfile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
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
