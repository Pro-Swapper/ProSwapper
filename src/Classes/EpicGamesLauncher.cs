using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
namespace Pro_Swapper
{
    public class EpicGamesLauncher
    {
        private static readonly string LauncherJson = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Epic\UnrealEngineLauncher\LauncherInstalled.dat";
        public class InstallationList
        {
            public string InstallLocation { get; set; }
            public string NamespaceId { get; set; }
            public string ItemId { get; set; }
            public string ArtifactId { get; set; }
            public string AppVersion { get; set; }
            public string AppName { get; set; }
        }

        public class Root
        {
            public List<InstallationList> InstallationList { get; set; }
        }
        public static bool FindPakFiles()
        {
            try
            {
                if (File.Exists(LauncherJson))
                {
                    Root launcherdata = JsonConvert.DeserializeObject<Root>(File.ReadAllText(LauncherJson));
                    string InstallLocation = launcherdata.InstallationList.First(x => x.AppName == "Fortnite").InstallLocation;
                    global.CurrentConfig.Paks = InstallLocation + @"\FortniteGame\Content\Paks";
                    global.SaveConfig();
                    return true;
                }
            }
            catch { }
            return false;
        }

        public static string GetCurrentInstalledFortniteVersion()
        {
            try
            {
                if (File.Exists(LauncherJson))
                {
                    Root launcherdata = JsonConvert.DeserializeObject<Root>(File.ReadAllText(LauncherJson));
                    if (launcherdata.InstallationList.Count > 0)
                    {
                        string fortniteVersion = launcherdata.InstallationList.First(x => x.AppName == "Fortnite").AppVersion;
                        global.CurrentConfig.FortniteVersion = fortniteVersion;
                        global.SaveConfig();
                        return fortniteVersion;
                    }
                }
            }
            catch { }
            return null;
        }


        public static bool CloseFNPrompt()
        {
            Process.GetProcessesByName("EpicGamesLauncher").All(x => { x.Kill(); return true; });
            Process[] fnproc = Process.GetProcessesByName("FortniteClient-Win64-Shipping.exe");
            if (fnproc.Length > 0)
            {
                MessageBox.Show($@"Fortnite running! Please close this before swapping anything!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
    }
}
