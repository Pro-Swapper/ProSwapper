using Newtonsoft.Json;
using System;
using System.Net;

namespace Pro_Swapper
{
    public static class api
    {
        private static string endpoint = $"/{global.version}.json";
        private static readonly string[] hosturls = {"" };
        public class APIRoot
        {
            public string newstext { get; set; }
            public string patchnotes { get; set; }
            public string version { get; set; }
            public string discordurl { get; set; }
            public string items { get; set; }
            public string timestamp { get; set; }
            public string fnver { get; set; }
        }
        public static APIRoot apidata;
        public static void UpdateAPI()
        {
                    for (int i = 0; i < hosturls.Length; i++)
                    {
                        try
                        {
                            using (WebClient web = new WebClient())
                            apidata = JsonConvert.DeserializeObject<APIRoot>(web.DownloadString($"{hosturls[i]}{endpoint}"));
                            //apidata = JsonConvert.DeserializeObject<APIRoot>(System.IO.File.ReadAllText(@"C:\Users\ProMa\source\repos\OffsetDumper\bin\Debug\latest.json"));
                        break;
                        }
                        catch (Exception ex)
                        {
                            if (i != hosturls.Length)
                                continue;
                            else
                                Main.ThrowError("Pro Swapper needs an internet connection to run, if you are already connected to the internet Pro Swapper severs may be blocked in your country, please use a VPN or try disabling your firewall, if you are already doing this please refer to this error: \n\n" + ex);
                        }
                    }
                global.items = JsonConvert.DeserializeObject<Items.Root>(global.Decompress(apidata.items));
                //global.items = JsonConvert.DeserializeObject<Items.Root>(System.IO.File.ReadAllText(@"C:\Users\ProMa\source\repos\OffsetDumper\bin\Debug\itemtemplate.json"));
        }
        }
    }
