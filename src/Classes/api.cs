using Newtonsoft.Json;
namespace Pro_Swapper
{
    public static class api
    {
        private static string endpoint = $"/{global.version}.json";
        private static readonly string[] hosturls = {""};
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

        #if DEBUG
        apidata = JsonConvert.DeserializeObject<APIRoot>(System.IO.File.ReadAllText("api.json"));
        global.items = JsonConvert.DeserializeObject<Items.Root>(System.IO.File.ReadAllText("items.json"));
            
        #else
            for (int i = 0; i < hosturls.Length; i++)
            {
                try
                {
                    using (System.Net.WebClient web = new System.Net.WebClient())
                        apidata = JsonConvert.DeserializeObject<APIRoot>(web.DownloadString($"{hosturls[i]}{endpoint}"));
                    break;
                }
                catch (System.Exception ex)
                {
                    if (i != hosturls.Length)
                        continue;
                    else
                        Main.ThrowError("Pro Swapper needs an internet connection to run, if you are already connected to the internet Pro Swapper severs may be blocked in your country, please use a VPN or try disabling your firewall, if you are already doing this please refer to this error: \n\n" + ex);
                }
            }
            global.items = JsonConvert.DeserializeObject<Items.Root>(global.Decompress(apidata.items));
        #endif
        }

    }
}
