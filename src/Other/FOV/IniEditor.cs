using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
namespace Pro_Swapper.FOV
{
    public static class IniEditor
    {
        public static string fndir
        {
            get
            {
                string defaultpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\FortniteGame\Saved\Config\WindowsClient\GameUserSettings.ini";
                if (File.Exists(defaultpath))
                {
                    return defaultpath;
                }
                else
                {
                    using (OpenFileDialog o = new OpenFileDialog())
                    {
                        o.Title = "Select your Fortnite GameUserSettings.ini";
                        o.Filter = "GameUserSettings.ini (*.ini*)|*.ini";
                        o.ShowDialog();

                        if (o.ShowDialog() == DialogResult.OK)
                        {
                            return o.FileName;
                        }
                        return "null";
                    }
                }
            }
        }
        private static string GameFile { get; set; }


        /*
         * 
         * 
         * LastUserConfirmedDesiredScreenWidth=1920
            LastUserConfirmedDesiredScreenHeight=1080
            LastRecommendedScreenWidth=-1.000000
            LastRecommendedScreenHeight=-1.000000
         * 
         * DesiredScreenWidth=1920
DesiredScreenHeight=1080
         * 
         */
        public static void SetRes(string YRes, string XRes, int fullscreenmode)
        {
            try
            {
                File.SetAttributes(global.CurrentConfig.ConfigIni, FileAttributes.Normal);
                GameFile = string.Empty;
                string line;
                using (StreamReader file = new StreamReader(global.CurrentConfig.ConfigIni))
                {
                    while ((line = file.ReadLine()) != null)//Reads line one by one
                    {
                        if (line.StartsWith("ResolutionSizeY="))
                        {
                            GameFile += "ResolutionSizeY=" + YRes + Environment.NewLine;
                            continue;
                        }
                        if (line.StartsWith("LastUserConfirmedResolutionSizeY="))
                        {
                            GameFile += "LastUserConfirmedResolutionSizeY=" + YRes + Environment.NewLine;
                            continue;
                        }
                        if (line.StartsWith("LastUserConfirmedDesiredScreenHeight="))
                        {
                            GameFile += "LastUserConfirmedDesiredScreenHeight=" + YRes + Environment.NewLine;
                            continue;
                        }
                        if (line.StartsWith("DesiredScreenHeight="))
                        {
                            GameFile += "DesiredScreenHeight=" + YRes + Environment.NewLine;
                            continue;
                        }

                        if (line.StartsWith("ResolutionSizeX="))
                        {
                            GameFile += "ResolutionSizeX=" + XRes + Environment.NewLine;
                            continue;
                        }
                        if (line.StartsWith("LastUserConfirmedResolutionSizeX="))
                        {
                            GameFile += "LastUserConfirmedResolutionSizeX=" + XRes + Environment.NewLine;
                            continue;
                        }

                        if (line.StartsWith("LastUserConfirmedDesiredScreenWidth="))
                        {
                            GameFile += "LastUserConfirmedDesiredScreenWidth=" + XRes + Environment.NewLine;
                            continue;
                        }
                        if (line.StartsWith("DesiredScreenWidth="))
                        {
                            GameFile += "DesiredScreenWidth=" + XRes + Environment.NewLine;
                            continue;
                        }

                        if (line.StartsWith("LastConfirmedFullscreenMode="))
                        {
                            GameFile += "LastConfirmedFullscreenMode=" + fullscreenmode + Environment.NewLine;
                            continue;
                        }
                        if (line.StartsWith("FullscreenMode="))
                        {
                            GameFile += "FullscreenMode=" + fullscreenmode + Environment.NewLine;
                            continue;
                        }



                        GameFile += line + Environment.NewLine;
                    }
                }
                File.WriteAllText(global.CurrentConfig.ConfigIni, GameFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pro Swapper FOV Changer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
