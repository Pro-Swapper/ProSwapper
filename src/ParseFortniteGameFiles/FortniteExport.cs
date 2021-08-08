using System;
using System.Collections.Generic;
using CUE4Parse.UE4.Versions;
using CUE4Parse.FileProvider;
using System.IO;
using CUE4Parse.Encryption.Aes;
using CUE4Parse.UE4.Vfs;
using Newtonsoft.Json;
using Pro_Swapper.API;
namespace Pro_Swapper.Fortnite
{
    public static class FortniteExport
    {
        public static byte[] ExportAsset(DefaultFileProvider Provider, string filename, string Asset)
        {
            try
            {
                return Provider.SaveAsset(Asset);
            }
            catch (Exception ex)
            {
                throw new Exception($"Asset {Asset} in {filename} could not be exported: {ex.Message}");
            }
        }

        public static long Offset = 0;
    }
}
