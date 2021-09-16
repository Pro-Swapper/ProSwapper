using System;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net;
namespace Pro_Swapper
{
    static class Program
    {
        public const string oodledll = "oo2core_5_win64.dll";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Directory.CreateDirectory(global.ProSwapperFolder + "\\Config");
            Directory.CreateDirectory(global.ProSwapperFolder + "\\Images");
            global.InitConfig();
            
            UI.Splash splash = new UI.Splash();
            Task.Run(() => Application.Run(splash));

            if (!File.Exists(global.CurrentConfig.Paks + @"\pakchunk0-WindowsClient.sig"))
                EpicGamesLauncher.FindPakFiles();

            if (!File.Exists(oodledll))
            {
                if (EpicGamesLauncher.GetOodleDll(out string oodleFilePath))
                    File.Copy(oodleFilePath, oodledll);
                else
                    File.WriteAllBytes(oodledll, new WebClient().DownloadData("https://cdn.proswapper.xyz/oo2core_5_win64.dll"));
            }
            Application.Run(new Main(splash));
        }
    }
}