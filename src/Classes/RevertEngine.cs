using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Pro_Swapper.src.Classes
{
    public static class RevertEngine
    {

        public static void CreateRevertItem(RevertItem item)
        {
            CreateDirectory();
            string file = JsonConvert.SerializeObject(item);
            File.WriteAllText($"{RevertFolderDirectory}\\{Path.GetFileNameWithoutExtension(item.AssetPath)}.json", file);
        }
        public static void RevertAll()
        {
            if (Swap.Provider.MountedVfs.Count > 0)
                Swap.Provider.Dispose();


            string[] files = Directory.GetFiles(RevertFolderDirectory);
            foreach (string file in files)
            {
                RevertItem(file);
            }

        }

        public static bool RevertItem(API.api.Item item)
        {
            List<bool> Result = new();
            foreach (var asset in item.Asset)
            {
                Result.Add(RevertItem($"{RevertFolderDirectory}\\{Path.GetFileNameWithoutExtension(asset.AssetPath)}.json"));
            }
            return Result.All(x => x == true);
        }

        public static bool RevertItem(string filepath)
        {
            //filepath is the location of the revert item file.
            if (File.Exists(filepath))
            {
                string fileContent = File.ReadAllText(filepath);
                RevertItem revertItem = JsonConvert.DeserializeObject<RevertItem>(fileContent);
                return RevertItem(revertItem);
            }

            return false;
        }
        public static bool RevertItem(RevertItem revertItem)
        {
            try
            {
                if (revertItem.FortniteVersion == EpicGamesLauncher.GetCurrentInstalledFortniteVersion())
                {
                    using (FileStream fileEditor = File.Open(Path.Combine(global.CurrentConfig.Paks, revertItem.File), FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        fileEditor.Position = revertItem.offset;
                        fileEditor.Write(revertItem.compressedBuffer, 0, revertItem.compressedBuffer.Length);
                    }
                }
                return true;
            }
            catch { }
            return false;
        }

        public static void CreateDirectory()
        {
            if (!Directory.Exists(RevertFolderDirectory))
                Directory.CreateDirectory(RevertFolderDirectory);
        }

        public static string RevertFolderDirectory
        {
            get
            {
                string FortniteVersion = EpicGamesLauncher.GetCurrentInstalledFortniteVersion();
                return Path.Combine(global.ProSwapperFolder, "Reverts", FortniteVersion);
            }
        }

    }


    public sealed class RevertItem
    {
        public long offset { get; set; } // The offset of the compressedBuffer
        public byte[] compressedBuffer { get; set; } //The compressed bytes unchanged from Fortnite
        public string File { get; set; } //The file in the 'Paks' folder which the compressed buffer was copied from.
        public string AssetPath { get; set; } //The virtual file path of the asset within Fortnite
        public string FortniteVersion { get; set; } //Fortnite's version when the asset was fetched.
        public RevertItem(long offset, byte[] compressedBuffer, string file, string assetPath)
        {
            this.offset = offset;
            this.compressedBuffer = compressedBuffer;
            this.File = file;
            this.AssetPath = assetPath;
            //When creating a revert item we are going to create it on the current fortnite version
            this.FortniteVersion = EpicGamesLauncher.GetCurrentInstalledFortniteVersion();
        }
    }

}
