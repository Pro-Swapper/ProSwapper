using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Pro_Swapper
{
    public class global
    {
        public static string version = Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(2, 5);

        private const string ImgurCDN = "https://i.imgur.com/";
        public static Image ItemIcon(string url)
        {
            if (url.StartsWith("https://fortnite-api.com/"))
            {
                //Fetch with fortnite api
                string path = ProSwapperFolder + @"Images\" + url.Replace("https:", "").Replace("/", "");
                //Downloads image if doesnt exists
                if (!File.Exists(path))
                    return SaveImage(url, path, ImageFormat.Png);
                else
                {
                    return Image.FromFile(path);
                }


            }
            else
            {
                //Fetch from imgur
                string ActualUrl = url.Substring(url.LastIndexOf('/') + 1);//If not full url returns original which is what we want :) https://stackoverflow.com/a/5327562/12897035
                string path = ProSwapperFolder + @"Images\" + ActualUrl;
                string imageurl = ImgurCDN + ActualUrl;


                //Downloads image if doesnt exists
                if (!File.Exists(path))
                    return SaveImage(imageurl, path, ImageFormat.Png);
                else
                    return Image.FromFile(path);
            }
        }

        public static void DeleteFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(filepath);
            }
        }


        public static string GetPaksList
        {
            get
            {
                List<string> PaksInfo = new List<string>(Directory.GetFiles(CurrentConfig.Paks, "*", SearchOption.AllDirectories));
                PaksInfo = PaksInfo.Select(x => x.Substring(x.IndexOf("FortniteGame"))).ToList();
                PaksInfo.Insert(0, $"Paks Information ({PaksInfo.Count}) Files: ");
                return string.Join("\n", PaksInfo);
            }
        }

        private static Image SaveImage(string imageUrl, string filename, ImageFormat format)
        {
            using (WebClient web = new WebClient())
            {
                Stream stream = web.OpenRead(imageUrl);
                Bitmap bitmap = new Bitmap(stream);

                if (bitmap != null)
                    bitmap.Save(filename, format);

                stream.Flush();
                stream.Close();
                return bitmap;
            }
        }



        public static string IntToBool(string number)
        {
            if (number == "0")
                return "false";
            else if (number == "1")
                return "true";

            return null;
        }

        public static string BoolToInt(string boolean)
        {
            switch (boolean)
            {
                case "false": return "0";
                case "true": return "1";
                default: return null;
            }
        }


        public static long GetEpochTime() => DateTimeOffset.Now.ToUnixTimeSeconds();
        public static bool IsNameModified()
        {
            if (Process.GetCurrentProcess().ProcessName.Contains("Pro_Swapper"))
                return false;
            else
                return true;
        }

        public static Color MainMenu, Button, TextColor, ItemsBG;

        public static void OpenUrl(string url)
        {
            Program.logger.Log($"Opened link \"{url}\"");
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C start {url}",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            };
            Process.Start(psi);
        }

        public static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove) => attributes & ~attributesToRemove;
        public static string ProSwapperFolder => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Pro_Swapper\";
        #region Config Handler
        private static string ConfigPath => ProSwapperFolder + @"Config\" + version + "_config.json";

        public static ConfigObj CurrentConfig;
        public static void InitConfig()
        {
            if (!File.Exists(ConfigPath))
                File.WriteAllText(ConfigPath, ToJson(new ConfigObj()));
            CurrentConfig = FromJSON<ConfigObj>(File.ReadAllText(ConfigPath));
        }
        public static void SaveConfig() => File.WriteAllText(ConfigPath, ToJson(CurrentConfig));
        public static T FromJSON<T>(string json) => JsonConvert.DeserializeObject<T>(json);
        public static string ToJson(Object config) => JsonConvert.SerializeObject(config);

        public class ConfigObj
        {
            public string Paks { get; set; } = "";
            public string ConfigIni { get; set; } = "";
            public Color[] theme { get; set; } = new Color[4] { Color.FromArgb(0, 33, 113), Color.FromArgb(64, 85, 170), Color.FromArgb(65, 105, 255), Color.FromArgb(255, 255, 255) };//0,33,113;    64,85,170;    65,105,255;   255,255,255
            public double lastopened { get; set; }
            public double LastOpenedAPI { get; set; }
            public double LobbyLastOpened { get; set; }
            public string swaplogs { get; set; } = "";
            public string ManualAESKey { get; set; } = "";
            public bool AntiKick { get; set; } = true;
            public API.api.AESSource AESSource { get; set; } = API.api.AESSource.FortniteCentral;
        }
        #endregion


        public static byte[] HexToByte(string hex)
        {
            hex = hex.Replace(" ", string.Empty).Replace("hex=", string.Empty);
            return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }
        public static string FileToMd5(string filename)
        {
            if (File.Exists(filename))
                return BitConverter.ToString(MD5.Create().ComputeHash(File.OpenRead(filename))).Replace("-", string.Empty).ToUpperInvariant();
            else
                return string.Empty;
        }

        #region FormMoveable
        private const string User32dll = "user32.dll";
        [DllImport(User32dll)]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport(User32dll)]
        public static extern bool ReleaseCapture();
        public static void FormMove(IntPtr Handle)
        {
            ReleaseCapture();
            SendMessage(Handle, 0xA1, 0x2, 0);
        }
        #endregion

        public static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = color.R;
            float green = color.G;
            float blue = color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }
            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }


        public static void MoveForm(MouseEventArgs e, IntPtr Handle)
        {
            if (e.Button == MouseButtons.Left)
                FormMove(Handle);
        }

        public static string CalculateTimeSpan(DateTime dt)
        {
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - dt.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 60)
            {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }
            if (delta < 60 * 2)
            {
                return "a minute ago";
            }
            if (delta < 45 * 60)
            {
                return ts.Minutes + " minutes ago";
            }
            if (delta < 90 * 60)
            {
                return "an hour ago";
            }
            if (delta < 24 * 60 * 60)
            {
                return ts.Hours + " hours ago";
            }
            if (delta < 48 * 60 * 60)
            {
                return "yesterday";
            }
            if (delta < 30 * 24 * 60 * 60)
            {
                return ts.Days + " days ago";
            }
            if (delta < 12 * 30 * 24 * 60 * 60)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp) => DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).DateTime;
    }
}
