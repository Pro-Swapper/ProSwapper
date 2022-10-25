using System;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using Pro_Swapper.API;

namespace Pro_Swapper
{
    static class Program
    {
        public static Logger.Logger logger;
        public static HttpClient httpClient;
        public const string Oodledll = "oo2core_5_win64.dll";
        public const string Oodledll9 = "oo2core_9_win64.dll";


        private static void InitializeHttpClient(ref HttpClient httpClient)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Utils.KillDuplicateProcesses();
                InitializeHttpClient(ref Program.httpClient);



                Directory.CreateDirectory(global.ProSwapperFolder);
                Directory.CreateDirectory(global.ProSwapperFolder + "\\Config");
                Directory.CreateDirectory(global.ProSwapperFolder + "\\Images");
                Native.SetDllDirectory(global.ProSwapperFolder);
                global.InitConfig();

                logger = new Logger.Logger(global.ProSwapperFolder + "Pro_Swapper.log");
                logger.Log($"Pro Swapper Version: {global.version}");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

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

                if (!File.Exists(Oodledll) || !File.Exists(Oodledll9))
                {

                    string OodlePath = Path.Combine(global.ProSwapperFolder, Oodledll);
                    File.WriteAllBytes(OodlePath, Properties.Resources.oo2core_5_win64);
                    logger.Log($"Wrote {Oodledll} to {OodlePath}");

                    string OodlePath9 = Path.Combine(global.ProSwapperFolder, Oodledll9);
                    File.WriteAllBytes(OodlePath9, Properties.Resources.oo2core_9_win64);
                    logger.Log($"Wrote {Oodledll9} to {OodlePath9}");
                }
                else
                {
                    logger.Log($"{Oodledll} already exists so no need to fetch it!");
                }


                logger.Log($"Fetching latest data from API");
                api.UpdateAPI();
                string apiversion = api.apidata.version;
                double TimeNow = global.GetEpochTime();

                if (global.CurrentConfig.lastopened + 7200 < TimeNow)
                {
                    global.OpenUrl(api.apidata.discordurl);
                    global.CurrentConfig.lastopened = TimeNow;
                }

                int thisVer = int.Parse(global.version.Replace(".", ""));
                int apiVer = int.Parse(api.apidata.version.Replace(".", ""));

                const string NewDownload = "https://linkvertise.com/86737/proswapper";

                if (apiVer > thisVer)
                {
                    MessageBox.Show("New Pro Swapper Update found! Redirecting you to the new download!", "Pro Swapper Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    global.OpenUrl(NewDownload);
                    Process.GetCurrentProcess().Kill();
                }

                if (global.IsNameModified())
                {
                    Pro_Swapper.Main.ThrowError($"This Pro Swapper version has been renamed, this means you have not downloaded it from the official source. Please redownload it on the Discord server at {api.apidata.discordurl}");
                    global.OpenUrl(NewDownload);
                }

                if (api.apidata.status[0].IsUp == false)
                    Pro_Swapper.Main.ThrowError(api.apidata.status[0].DownMsg);

                logger.Log("Running main form");
                RPC.InitializeRPC();
                Application.Run(new Main(splash));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.LogError(ex.Message);
            }

        }
    }
}