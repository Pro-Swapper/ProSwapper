using System;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using Pro_Swapper.Properties;
using System.Net;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            UI.Splash splash = new UI.Splash();
            Task.Run(() => Application.Run(splash));
            global.web = new WebClient();
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
            
            Application.Run(new Main(splash));
        }
    }
}