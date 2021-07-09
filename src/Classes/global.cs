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
namespace Pro_Swapper
{
    public class global
    {
        public static string version = Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(2, 5);

        public static WebClient web;
        
        
        public static Image ItemIcon(string url)
        {
                string path = ProSwapperFolder + @"Images\" + url;
                string imageurl = "https://i.imgur.com/" + url;
            //Downloads image if doesnt exists
            if (!File.Exists(path))
                return SaveImage(imageurl, path, ImageFormat.Png);
            else
                return Image.FromFile(path);
        }

        private static Image SaveImage(string imageUrl, string filename, ImageFormat format)
        {
            WebClient client = new WebClient();
            client.Proxy = null;
            Stream stream = client.OpenRead(imageUrl);
            Bitmap bitmap; bitmap = new Bitmap(stream);

            if (bitmap != null)
            {
                bitmap.Save(filename, format);
            }
            
            stream.Flush();
            stream.Close();
            client.Dispose();
            return bitmap;
        }

        public static double GetEpochTime() => (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;


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
        public static void CreateDir(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
        #region Config Handler
        private static string ConfigPath => ProSwapperFolder + @"Config\" + version + "_config.txt";

        public static ConfigObj CurrentConfig;
            public static void InitConfig()
            {
                if (!File.Exists(ConfigPath))
                    File.WriteAllText(ConfigPath, ToJson(new ConfigObj()));

                CurrentConfig = FromJSON<ConfigObj>(File.ReadAllText(ConfigPath));
            }
            public static void SaveConfig()
            {
                File.WriteAllText(ConfigPath, ToJson(CurrentConfig));
            }

            public static T FromJSON<T>(string json)//Make a json string to obj
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            public static string ToJson(Object config)//Make obj to json string
            {
                return JsonConvert.SerializeObject(config);
            }

            public class ConfigObj
            {
            public string Paks { get; set; } = "";
            public Color[] theme { get; set; } = new Color[4] { Color.FromArgb(0, 33, 113), Color.FromArgb(64, 85, 170), Color.FromArgb(65,105,255), Color.FromArgb(255,255,255) };//0,33,113;    64,85,170;    65,105,255;   255,255,255
            public double lastopened { get; set; }
            public string swaplogs { get; set; } = "";
            }
        #endregion


        public static byte[] HexToByte(string hex)
        {
            hex = hex.Replace(" ", "").Replace("hex=", "");
            return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }
        public static string FileToMd5(string filename)
        {
            if (File.Exists(filename)) return BitConverter.ToString(MD5.Create().ComputeHash(File.OpenRead(filename))).Replace("-", "").ToLowerInvariant();
            else return string.Empty;
        }
        #region FormMoveable
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public static void FormMove(IntPtr Handle)
        {
            ReleaseCapture();
            SendMessage(Handle, 0xA1, 0x2, 0);
        }
        #endregion
    }
}
