using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;
namespace Pro_Swapper
{
    public class EpicGamesLauncher
    {

        private class InstallationList
        {
            public string InstallLocation { get; set; }
            public string AppName { get; set; }
        }

        private class Root
        {
            public List<InstallationList> InstallationList { get; set; }
        }
        public static void FindPakFiles()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Epic\UnrealEngineLauncher\LauncherInstalled.dat";
            if (File.Exists(path))
            {
                try
                {
                    Root launcherdata = JsonConvert.DeserializeObject<Root>(File.ReadAllText(path));
                    foreach (var d in launcherdata.InstallationList)
                    {
                        if (d.AppName == "Fortnite")
                        {
                            global.WriteSetting(d.InstallLocation + @"\FortniteGame\Content\Paks", global.Setting.Paks);
                            return;
                        }
                    }
                }
                catch
                {
                    goto error;
                }
            }
            error: MessageBox.Show("Could not find your pak files! Please select them manually!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
