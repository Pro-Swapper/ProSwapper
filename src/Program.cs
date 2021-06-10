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
            global.InitConfig();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}