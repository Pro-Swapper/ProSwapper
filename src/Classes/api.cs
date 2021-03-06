﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
namespace Pro_Swapper.API
{
    public static class api
    {
        private static readonly string endpoint = $"/{global.version}.json";
        private static readonly string[] hosturls = {"https://pro-swapper.github.io/api","https://raw.githubusercontent.com/Pro-Swapper/api/main" };
        public static APIRoot apidata = null;
        public const string FNAPIEndpoint = "https://fortnite-api.com/v2/";
        private static string AESKey
        {
            get
            {
                try
                {
                    return ((dynamic)JObject.Parse(global.web.DownloadString($"{FNAPIEndpoint}aes"))).data.mainKey;
                }
                catch (Exception ex)
                {
                    return "";
                    throw new Exception($"Could not connect to {FNAPIEndpoint}: {ex.Message}");
                }
                
            }
        }

        public static void UpdateAPI()
        {

            #if DEBUG
            apidata = JsonConvert.DeserializeObject<APIRoot>(File.ReadAllText("api.json"));
            apidata.timestamp = global.GetEpochTime();

            string json = JsonConvert.SerializeObject(apidata, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore//Makes filesize smaller hehe
            });

            File.WriteAllText($"{global.version}.json", StringCompression.Compress(json));
            #else
            Exception exception = null;
            foreach (string url in hosturls)
            {
                try
                {
                    string apidatas = StringCompression.Decompress(global.web.DownloadString($"{url}{endpoint}"));
                    apidata = JsonConvert.DeserializeObject<APIRoot>(apidatas);
                    Console.WriteLine(apidatas);
                }
                catch (Exception ex)
                {
                    exception = ex;
                    continue;
                }
            }
            if (exception != null)
                Main.ThrowError($"Pro Swapper needs an internet connection to run, if you are already connected to the internet Pro Swapper's API may be blocked in your country, please use a VPN or try disabling your firewall, if you are already doing this please refer to this error: \n\n{exception.Message}");
            #endif
            apidata.aes = AESKey;
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
            public double timestamp { get; set; }
            public string fnver { get; set; }
            public string aes { get; set; }
            public Item[] items { get; set; }
            public Status[] status { get; set; }
            public OptionMenu[] OptionMenu { get; set; }
        }
    }
}
