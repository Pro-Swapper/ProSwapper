using System;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Web.Script.Serialization;
namespace Pro_Swapper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            string[] dlls = { "Bunifu_UI_v1.52.dll", "DiscordRPC.dll" };
            string discordurll = "https://cdn.discordapp.com/attachments/777053390009925643/";
            string[] urls = { discordurll + "797387747769450537/Bunifu_UI_v1.52.dll_compress", discordurll + "797368599937155072/DiscordRPC.dll_compress" };
            if (global.md5(dlls[0]) + global.md5(dlls[1]) != "3764580D568E4FC506048E04DB90562CA1C35901AD26A30C5B7836771B6BADFF")
            {
                using (WebClient web = new WebClient())
                {
                    File.WriteAllBytes(dlls[0], global.Decompress(web.DownloadData(urls[0])));
                    File.WriteAllBytes(dlls[1], global.Decompress(web.DownloadData(urls[1])));
                }
            }
            global.CreateDir(global.ProSwapperFolder);
            string filename = AppDomain.CurrentDomain.FriendlyName;
            if (!filename.Contains("Pro") && !filename.Contains("Swapper"))
                ThrowError("This version of Pro Swapper has been modified (renamed) " + filename + " , please download the original Pro Swapper at https://proswapper.xyz/download");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //with args(user open file with the program)
            if (args.Length > 0 && File.Exists(args[0]) && args[0].Contains(".pro"))
            {
                    string fileName = args[0];
                    Application.Run(new Plugins(fileName));
            }
            //without args
            else
            {
                #region Checks
                try
                {
                    using (WebClient web = new WebClient())
                    {
                        string endpoint = "/latest.json";
                        try
                        {
                            apidata = new JavaScriptSerializer().Deserialize<api>(web.DownloadString("https://pro-swapper.github.io/api" + endpoint));
                        }
                        catch
                        {
                            apidata = new JavaScriptSerializer().Deserialize<api>(web.DownloadString("https://raw.githubusercontent.com/Pro-Swapper/api/main" + endpoint));
                        }
                        
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

                    if (apiversion.Contains("OFFLINE"))
                        ThrowError("Pro Swapper is currently not working. Take a look at our Discord for any announcments");
                #endregion
                Application.Run(new Main());
            }
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