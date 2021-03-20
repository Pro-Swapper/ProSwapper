using System;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net.Cache;
namespace Pro_Swapper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            global.CreateDir(global.ProSwapperFolder);
                #region Checks
                try
                {
                    using (WebClient web = new WebClient())
                    {
                        web.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
                        string endpoint = "/cool.json";
                        string apidownloaded = string.Empty;
                        try
                        {
                            apidownloaded = web.DownloadString("https://pro-swapper.github.io/api" + endpoint);
                        }
                        catch
                        {
                            apidownloaded = web.DownloadString("https://raw.githubusercontent.com/Pro-Swapper/api/main" + endpoint);
                        }
                        apidata = JsonConvert.DeserializeObject<api>(apidownloaded);
                    }
                }
                catch (Exception ex)
                {
                    ThrowError("Pro Swapper needs an internet connection to run, if you are already connected to the internet Pro Swapper severs may be blocked in your country, please use a VPN or try disabling your firewall, if you are already doing this please refer to this error: \n\n" + ex);
                }
                string apiversion = apidata.version;
                decompresseditems = global.Decompress(apidata.items);
                   
                    string thishr = DateTime.Now.ToString("MMddHH");

                    if (global.ReadSetting(global.Setting.lastopened) != thishr)
                    {
                        string discordurl = apidata.discordurl;
                        Process.Start(discordurl);//opens discord
                        global.WriteSetting(thishr, global.Setting.lastopened);
                    }

                    if (!apiversion.Contains(global.version)) //if outdated
                    {
                        //Auto updater
                        MessageBox.Show("New Pro Swapper Update found! Redirecting you to the new download!", "Pro Swapper Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start("https://linkvertise.com/86737/proswapper");
                        Environment.Exit(0);
                    }

                    string filename = AppDomain.CurrentDomain.FriendlyName;
                    if (!filename.Contains("Pro") && !filename.Contains("Swapper"))
                        ThrowError("This version of Pro Swapper has been modified (renamed) " + filename + " , please download the original Pro Swapper on the Discord server");


            if (apiversion.Contains("OFFLINE"))
                        ThrowError("Pro Swapper is currently not working. Take a look at our Discord for any announcments");
            #endregion
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }


        // https://json2csharp.com/
        public class api
        {
            public string newstext { get; set; }
            public string patchnotes { get; set; }
            public string version { get; set; }
            public string discordurl { get; set; }
            public string items { get; set; }
        }
        public static api apidata;
        public static string decompresseditems { get; set; }
        public static void ThrowError(string ex)=> new Error(ex).ShowDialog();
    }
}