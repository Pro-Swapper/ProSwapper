using CUE4Parse.Encryption.Aes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System;
using MessagePack;
using System.IO;
namespace Pro_Swapper.API
{
    public static class api
    {
        private const string ProSwapperEndpoint = "https://pro-swapper.github.io/api/";
        public static APIRoot apidata = null;
        public const string FNAPIEndpoint = "https://fortnite-api.com/";
        public const string BenBotEndpoint = "https://benbot.app/api/";
        public static FAesKey fAesKey = null;
        public static FAesKey AESKey
        {
            get
            {
                try
                {
                    
                    switch (global.CurrentConfig.AESSource)
                    {
                        default:
                        case AESSource.FortniteAPIV1:
                            //Using msgpack_lz4 compression (faster)
                            string aesv1 = msgpack.MsgPacklz4($"{FNAPIEndpoint}v1/aes?responseFormat=msgpack_lz4").data.aes;
                            return new FAesKey(aesv1);
                          

                        case AESSource.FortniteAPIV2:
                            //Using msgpack_lz4 compression (faster)
                            string aesv2 = msgpack.MsgPacklz4($"{FNAPIEndpoint}v2/aes?responseFormat=msgpack_lz4").data.mainKey;
                            return new FAesKey(aesv2);

                        case AESSource.BenBot:

                            string json = new WebClient().DownloadString($"{BenBotEndpoint}v1/aes");
                            string benbotaes = ((dynamic)JObject.Parse(json)).mainKey;
                            return new FAesKey(benbotaes);
                        case AESSource.Manual:
                            return new FAesKey(global.CurrentConfig.ManualAESKey);

                    }
                }
                catch (Exception ex)
                {
                    return null;
                    throw new Exception($"Could not connect to {FNAPIEndpoint}: {ex.Message}");
                }

            }
        }

        public enum AESSource
        {
            FortniteAPIV1,
            FortniteAPIV2,
            BenBot,
            Manual
        }

        public static void UpdateAPI()
        {
            GlobalAPI.Root globalapi = null;
            #if DEBUG
            apidata = JsonConvert.DeserializeObject<APIRoot>(File.ReadAllText("api.json"));
            apidata.timestamp = global.GetEpochTime();
            globalapi = JsonConvert.DeserializeObject<GlobalAPI.Root>(File.ReadAllText("global.json"));
            string json = JsonConvert.SerializeObject(apidata, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore//Makes filesize smaller hehe
            });
            byte[] compressedapi = MessagePackSerializer.ConvertFromJson(json, MessagePackSerializerOptions.Standard);
            File.WriteAllBytes($"{global.version}.json", ByteCompression.Compress(compressedapi));

            #else
            try
            {
                using (WebClient web = new WebClient())
                {
                    byte[] apidatas = ByteCompression.Decompress(web.DownloadData($"{ProSwapperEndpoint}/{global.version}.json"));
                    string json = MessagePackSerializer.ConvertToJson(apidatas);
                    apidata = JsonConvert.DeserializeObject<APIRoot>(json);
                    globalapi = JsonConvert.DeserializeObject<GlobalAPI.Root>(web.DownloadString($"{ProSwapperEndpoint}/global.json"));
                }
            }
            catch (Exception ex)
            {
                Main.ThrowError($"Pro Swapper needs an internet connection to run, if you are already connected to the internet Pro Swapper's API may be blocked in your country, please use a VPN or try disabling your firewall, if you are already doing this please refer to this error: \n\n{ex.Message}");
            }
            #endif
            apidata.discordurl = globalapi.discordurl;
            apidata.version = globalapi.version;
            apidata.status[0] = globalapi.status[0];
        }

        public class Asset
        {
            public string[] Search { get; set; }
            public string[] Replace { get; set; }
            public string AssetPath { get; set; }
            public string UcasFile { get; set; }
        }

        public class Item
        {
            public string Type { get; set; }
            public string SwapsFrom { get; set; }
            public string SwapsTo { get; set; }
            public string FromImage { get; set; }
            public string ToImage { get; set; }
            public Asset[] Asset { get; set; }
            public string Note { get; set; } = null;
            public bool ShowMain { get; set; } = true;
            public bool Zlib { get; set; } = false;
        }
        public class OptionMenu
        {
            public string Type { get; set; }
            public string MainIcon { get; set; }
            public bool IsSwapOption { get; set; }
            public int[] ItemIndexs { get; set; }
            public string Title { get; set; }
        }


        public class Status
        {
            public bool IsUp { get; set; }
            public string DownMsg { get; set; }
        }

        public class APIRoot
        {
            public string newstext { get; set; }
            public string patchnotes { get; set; }
            public string version { get; set; }
            public string discordurl { get; set; }
            public long timestamp { get; set; }
            public Item[] items { get; set; }
            public Status[] status = new Status[1];
            public OptionMenu[] OptionMenu { get; set; }
        }


        public class GlobalAPI
        {
            public class Root
            {
                public Status[] status { get; set; }
                public string version { get; set; }
                public string discordurl { get; set; }
            }
        }
    }
}
