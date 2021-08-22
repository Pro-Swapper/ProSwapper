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
        public static void FindPakFiles()
        {
            if (File.Exists(LauncherJson))
            {
                try
                {
                    Root launcherdata = JsonConvert.DeserializeObject<Root>(File.ReadAllText(LauncherJson));
                    InstallationList fortnite = launcherdata.InstallationList.Where(x => x.AppName == "Fortnite").FirstOrDefault();
                    global.CurrentConfig.Paks = fortnite.InstallLocation + @"\FortniteGame\Content\Paks";
                    global.SaveConfig();
                }
                catch
                {
                    MessageBox.Show("Could not find your pak files! Please select them manually!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public static string GetOodleDll()
        {
            Root launcherdata = JsonConvert.DeserializeObject<Root>(File.ReadAllText(LauncherJson));
            InstallationList fortnite = launcherdata.InstallationList.Where(x => x.AppName == "Fortnite").First();

            string oodlefile = fortnite.InstallLocation + @"\FortniteGame\Binaries\Win64\oo2core_5_win64.dll";
            if (File.Exists(oodlefile))
                return oodlefile;
            else
                return null;
        }

        public static bool CloseFNPrompt()
        {
            Process[] procs = Process.GetProcesses();
            string[] blacklistedprocs = { "easyanticheat", "FortniteClient-Win64-Shipping", "epicgameslauncher"};
            foreach (var proc in procs)
            {
                if (blacklistedprocs.Any(proc.ProcessName.ToLower().Contains))
                {
                    try
                    {
                        proc.Kill();
                    }
                    catch
                    {
                        MessageBox.Show($"{proc.ProcessName} ({proc.Id}) is running! Please close this before swapping anything!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
