using System;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net;
namespace Pro_Swapper
{
    static class Program
    {
        public static Logger.Logger logger;
        public const string oodledll = "oo2core_5_win64.dll";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Directory.CreateDirectory(global.ProSwapperFolder + "\\Config");
                Directory.CreateDirectory(global.ProSwapperFolder + "\\Images");
                global.InitConfig();
                logger = new Logger.Logger(global.ProSwapperFolder + "Pro_Swapper.log");
                logger.Log($"Pro Swapper Version: {global.version}");
                UI.Splash splash = new UI.Splash();
                Task.Run(() => Application.Run(splash));
                logger.Log("Started Splash Screen");

                if (!File.Exists(global.CurrentConfig.Paks + @"\pakchunk0-WindowsClient.sig"))
                {
                    logger.Log("Fortnite paks have not been found! Searching for paks now");
                    EpicGamesLauncher.FindPakFiles();
                    if (global.CurrentConfig.Paks.Contains("Paks"))
                    {
                        logger.Log($"Found paks folder -> {global.CurrentConfig.Paks}");
                    }
                    else
                    {
                        logger.Log($"ERROR -> Paks folder could not be found!");
                    }
                }
                logger.Log(global.GetPaksList);
                    

                if (!File.Exists(oodledll))
                {
                    if (EpicGamesLauncher.GetOodleDll(out string oodleFilePath))
                    {
                        File.Copy(oodleFilePath, oodledll);
                        logger.Log($"Copied {oodledll} from {oodleFilePath}!");
                    }
                    else
                    {
                        File.WriteAllBytes(oodledll, new WebClient().DownloadData("https://cdn.proswapper.xyz/oo2core_5_win64.dll"));
                        logger.Log($"Downloaded {oodledll} from Pro Swapper cdn. ??? Not found in user's game files");
                    }
                }
                else
                {
                    logger.Log($"{oodledll} already exists so no need to fetch it!");
                }
                logger.Log("Running main form");
                Application.Run(new Main(splash));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            
        }
    }
}