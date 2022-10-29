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
        private static FAesKey tmpAes = null;
        public static FAesKey AESKey
        {
            get
            {
                if (tmpAes == null)//Get aes key if it's not assigned, if it is just return tmpAes
                {
                    try
                    {

                        switch (global.CurrentConfig.AESSource)
                        {
                            default:
                            case AESSource.FortniteAPIV1:
                                //Using msgpack_lz4 compression (faster)
                                tmpAes = new FAesKey((string)msgpack.MsgPacklz4($"{FNAPIEndpoint}v1/aes?responseFormat=msgpack_lz4").data.aes);
                                break;

                            case AESSource.FortniteAPIV2:
                                //Using msgpack_lz4 compression (faster)
                                tmpAes = new FAesKey((string)msgpack.MsgPacklz4($"{FNAPIEndpoint}v2/aes?responseFormat=msgpack_lz4").data.mainKey);
                                break;

                            case AESSource.FortniteCentral:
                                string json = Program.httpClient.GetStringAsync("https://fortnitecentral.gmatrixgames.ga/api/v1/aes").GetAwaiter().GetResult();
                                tmpAes = new FAesKey((string)((dynamic)JObject.Parse(json)).mainKey);
                                break;
                            case AESSource.Manual:
                                tmpAes = new FAesKey(global.CurrentConfig.ManualAESKey);
                                break;

                        }
                        return tmpAes;
                    }
                    catch (Exception ex)
                    {
                        Program.logger.LogError(ex.Message);
                        throw new Exception($"Could not connect to {FNAPIEndpoint}: {ex.Message}");
                    }
                }
                else
                {
                    return tmpAes;
                }
            }
        }

        public enum AESSource
        {
            FortniteAPIV1,
            FortniteAPIV2,
            FortniteCentral,
            Manual
        }

        public static bool UpdateAPI()
        {
            string rawAPIFile = global.ProSwapperFolder + "api.ProSwapper";
            string rawGlobalFile = global.ProSwapperFolder + "global.ProSwapper";
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
            apidata.discordurl = globalapi.discordurl;
            apidata.version = globalapi.version;
            apidata.status[0] = globalapi.status[0];
            return true;
#else

            try
            {
            download: double TimeNow = global.GetEpochTime();
                if (global.CurrentConfig.LastOpenedAPI + 1800 < TimeNow)
                {
                    //Get api coz it wasnt fetched more than 30 mins ago


                    //Download api & global
                    byte[] rawAPI = Program.httpClient.GetByteArrayAsync($"{ProSwapperEndpoint}/{global.version}.json").GetAwaiter().GetResult();
                    string rawGlobal = Program.httpClient.GetStringAsync($"{ProSwapperEndpoint}/global.json").GetAwaiter().GetResult();

                    //Decompress api & set
                    apidata = JsonConvert.DeserializeObject<APIRoot>(MessagePackSerializer.ConvertToJson(ByteCompression.Decompress(rawAPI)));
                    globalapi = JsonConvert.DeserializeObject<GlobalAPI.Root>(rawGlobal);


                    File.WriteAllBytes(rawAPIFile, rawAPI);
                    File.WriteAllText(rawGlobalFile, rawGlobal);

                    global.CurrentConfig.LastOpenedAPI = TimeNow;
                }
                else
                {
                    if (!File.Exists(rawAPIFile) || !File.Exists(rawGlobalFile))
                    {
                        global.CurrentConfig.LastOpenedAPI = 0;
                        goto download;
                    }
                    //Was fetched within the hour
                    //Download api & global
                    byte[] rawAPI = File.ReadAllBytes(rawAPIFile);
                    string rawGlobal = File.ReadAllText(rawGlobalFile);

                    //Decompress api & set
                    apidata = JsonConvert.DeserializeObject<APIRoot>(MessagePackSerializer.ConvertToJson(ByteCompression.Decompress(rawAPI)));
                    globalapi = JsonConvert.DeserializeObject<GlobalAPI.Root>(rawGlobal);
                }
                apidata.discordurl = globalapi.discordurl;
                apidata.version = globalapi.version;
                apidata.status[0] = globalapi.status[0];
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Pro Swapper needs an Internet connection to run, if you are already connected to the Internet Pro Swapper's API may be blocked in your country, please use a VPN or try disabling your firewall, if you are already doing this please refer to this error: \n\n{ex.Message}");
                Program.logger.LogError(ex.Message);
                return false;
            }
#endif

        }

        public class Asset
        {
            public string[] Search { get; set; }
            public string[] Replace { get; set; }
            public string AssetPath { get; set; }
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
