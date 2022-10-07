using System;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;

namespace Pro_Swapper
{
    static class Program
    {
        public static Logger.Logger logger;
        public static HttpClient httpClient;
        public const string Oodledll = "oo2core_5_win64.dll";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                //Init the global httpClient
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                httpClient = new HttpClient(clientHandler);
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");

                //Kill duplicate Pro Swapper's
                Process CurrentProc = Process.GetCurrentProcess();
                foreach (Process proc in Process.GetProcessesByName(CurrentProc.ProcessName))
                    if (proc.Id != CurrentProc.Id)
                        proc.Kill();


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
                    if (EpicGamesLauncher.FindPakFiles() && global.CurrentConfig.Paks.Contains("Paks"))
                    {
                        logger.Log($"Found paks folder -> {global.CurrentConfig.Paks}");
                    }
                    else
                    {
                        logger.Log($"ERROR -> Paks folder could not be found!");
                        MessageBox.Show(@"Your Fortnite install location could not be found! Please make sure you have Fortnite installed!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                logger.Log(global.GetPaksList);

                if (!File.Exists(Oodledll))
                {
                    File.WriteAllBytes(Oodledll, httpClient.GetByteArrayAsync($"https://cdn.proswapper.xyz/{Oodledll}").Result);
                    logger.Log($"Downloaded {Oodledll} from Pro Swapper cdn.");
                }
                else
                {
                    logger.Log($"{Oodledll} already exists so no need to fetch it!");
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