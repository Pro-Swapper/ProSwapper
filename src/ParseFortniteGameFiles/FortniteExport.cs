using System;
using System.Collections.Generic;
using CUE4Parse.UE4.Versions;
using CUE4Parse.FileProvider;
using System.IO;
using CUE4Parse.Encryption.Aes;
using CUE4Parse.UE4.Vfs;
using Newtonsoft.Json;

namespace Pro_Swapper.Fortnite
{
    public static class FortniteExport
    {

        public static byte[] ExportAsset(string filename, string Asset)
        {
            string basefilename = Path.GetFileNameWithoutExtension(filename);
            var Provider = new DefaultFileProvider(global.CurrentConfig.Paks, SearchOption.TopDirectoryOnly, false, EGame.GAME_UE4_LATEST, UE4Version.VER_UE4_DETERMINE_BY_GAME);
            Provider.Initialize(basefilename);
            string aes = API.api.apidata.aes;
            IReadOnlyCollection<IAesVfsReader> vfs = Provider.UnloadedVfs;
            foreach (IAesVfsReader file in vfs)
                    Provider.SubmitKey(file.EncryptionKeyGuid, new FAesKey(aes));


            if (Provider.TrySaveAsset(Asset, out byte[] asset))
            {
                Provider.Dispose();
                return asset;
            }
            else
            {
                Provider.Dispose();
                throw new Exception($"Asset {asset} in {filename} could not be exported");
            }
        }
    }
}
