using System;
using System.IO;
using System.Windows.Forms;
using Pro_Swapper.Properties;
namespace Pro_Swapper
{
    static class Program
    {
        const string oodledll = "oo2core_5_win64.dll";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            global.web = new System.Net.WebClient();
            global.web.Proxy = null;
            string FortniteOodleDLL = EpicGamesLauncher.GetOodleDll();
            if (!File.Exists(oodledll))
            {
                if (EpicGamesLauncher.GetOodleDll() == "")
                {
                    if (!File.Exists(oodledll))
                        File.WriteAllBytes(oodledll, Resources.oo2core_5_win64);
                }
                else
                    File.Copy(FortniteOodleDLL, oodledll);
            }
            global.CreateDir(global.ProSwapperFolder);
            global.CreateDir(global.ProSwapperFolder + "\\Config");
            global.CreateDir(global.ProSwapperFolder + "\\Images");
            global.InitConfig();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}