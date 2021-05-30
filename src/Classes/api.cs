using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
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
                return ((dynamic)JObject.Parse(new WebClient().DownloadString($"{FNAPIEndpoint}aes"))).data.mainKey;
            }
        }

        public static void UpdateAPI()
        {
            
            #if DEBUG
            apidata = JsonConvert.DeserializeObject<APIRoot>(File.ReadAllText("api.json"));
            File.WriteAllText($"{global.version}.json", StringCompression.Compress(File.ReadAllText("api.json")));
            Console.WriteLine(StringCompression.Decompress(File.ReadAllText($"{global.version}.json")));
            #else
            Exception exception = null;
            WebClient web = new WebClient();
            foreach (string url in hosturls)
            {
                try
                {
                    string apidatas = StringCompression.Decompress(web.DownloadString($"{url}{endpoint}"));
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
            public Asset() { }
            public string[] Search { get; set; }
            public string[] Replace { get; set; }
            public string AssetPath { get; set; }
            public string UcasFile { get; set; }
            public string BothHave { get; set; }
        }

        public class Item
        {
            public string Type { get; set; }
            public string SwapsFrom { get; set; }
            public string SwapsTo { get; set; }
            public string FromImage { get; set; }
            public string ToImage { get; set; }
            public Asset[] Asset { get; set; }
            public string Note { get; set; }
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
            public string timestamp { get; set; }
            public string fnver { get; set; }
            public string aes { get; set; }
            public Item[] items { get; set; }
            public Status[] status { get; set; }
        }
    }
}
