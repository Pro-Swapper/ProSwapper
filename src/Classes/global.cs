using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
namespace Pro_Swapper
{
    public class global
    {
        public static string version = Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(2, 5);
        public static Image ItemIcon(string url)
        {
                string rawpath = ProSwapperFolder + @"Images\";
                CreateDir(rawpath);
                string path = rawpath + url;
                string imageurl = "https://i.imgur.com/" + url;
                //Downloads image if doesnt exists
                start:  if (!File.Exists(path))
                    new WebClient().DownloadFile(imageurl, path);
            try
            {
                Image img;
                using (Bitmap bmpTemp = new Bitmap(path))
                {
                    img = new Bitmap(bmpTemp);
                }
                    if (IsImage(img))
                    {
                        return img;
                    }
                    else
                    {
                        img.Dispose();
                        throw new Exception();
                    }
            }
            catch
            {
                File.Delete(path);
                goto start;
            }
        }
        public static bool IsNameModified()=> FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).OriginalFilename.Replace(".dll", "") != Process.GetCurrentProcess().ProcessName;
        public static Color MainMenu, Button, TextColor, ItemsBG;

        public static void OpenUrl(string url)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        public static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove) => attributes & ~attributesToRemove;

        private static bool IsImage(Image imagevar)
        {
            try
            {
                Image imgInput = imagevar;
                Graphics gInput = Graphics.FromImage(imgInput);
                ImageFormat thisFormat = imgInput.RawFormat;
                return true;
            }
            catch
            {
                return false;
            }
        }


        
        /* Deprecated
        public static byte[] ReadBytes(string filename, int numberOfBytes, long offset)
        {
            using (Stream stream = File.Open(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                List<byte> array = new List<byte>();
                stream.Position = offset;
                for (int i = 0; i < numberOfBytes; i++)
                    array.Add((byte)stream.ReadByte());

                stream.Close();
                stream.Dispose();
                return array.ToArray();
            }
        }*/

        public static string ProSwapperFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Pro_Swapper\";
        public static void CreateDir(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
        #region Config Handler
        private static string ConfigPath
            {
                get
                {
                    string path = ProSwapperFolder + @"Config\" + version + "_config.txt";
                    CreateDir(ProSwapperFolder + @"Config\");
                    return path;
                }
            }

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
            public string lastopened { get; set; } = "";
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
        #region BoyerMoore
        public class BoyerMoore
        {
            readonly byte[] needle;
            readonly int[] charTable;
            readonly int[] offsetTable;
            ArrayList foundAL;
            int[] foundIdxArray;
            bool found = false;

            public BoyerMoore(byte[] needle)
            {
                this.needle = needle;
                this.charTable = makeByteTable(needle);
                this.offsetTable = makeOffsetTable(needle);
                this.foundAL = new ArrayList();
                this.found = false;
            }

            public int[] Search(byte[] haystack, bool onlyFirst = false)
            {
                if (needle.Length == 0)
                    return new int[0];

                for (int i = needle.Length - 1; i < haystack.Length;)
                {
                    int j;

                    for (j = needle.Length - 1; needle[j] == haystack[i]; --i, --j)
                    {
                        if (j != 0)
                            continue;
                        foundAL.Add(new FoundIndex(i));
                        found = true;
                        i += needle.Length - 1;
                        break;
                    }

                    i += Math.Max(offsetTable[needle.Length - 1 - j], charTable[haystack[i]]);
                    if (onlyFirst && found)
                    { break; }
                }

                foundIdxArray = new int[foundAL.Count];
                for (int i = 0; i < foundAL.Count; i++)
                {
                    foundIdxArray[i] = ((FoundIndex)foundAL[i]).Idx;
                }
                return foundIdxArray;
            }

            static int[] makeByteTable(byte[] needle)
            {
                const int ALPHABET_SIZE = 256;
                int[] table = new int[ALPHABET_SIZE];

                for (int i = 0; i < table.Length; ++i)
                    table[i] = needle.Length;

                for (int i = 0; i < needle.Length - 1; ++i)
                    table[needle[i]] = needle.Length - 1 - i;

                return table;
            }

            static int[] makeOffsetTable(byte[] needle)
            {
                int[] table = new int[needle.Length];
                int lastPrefixPosition = needle.Length;

                for (int i = needle.Length - 1; i >= 0; --i)
                {
                    if (isPrefix(needle, i + 1))
                        lastPrefixPosition = i + 1;

                    table[needle.Length - 1 - i] = lastPrefixPosition - i + needle.Length - 1;
                }

                for (int i = 0; i < needle.Length - 1; ++i)
                {
                    int slen = suffixLength(needle, i);
                    table[slen] = needle.Length - 1 - i + slen;
                }

                return table;
            }

            static bool isPrefix(byte[] needle, int p)
            {
                for (int i = p, j = 0; i < needle.Length; ++i, ++j)
                    if (needle[i] != needle[j])
                        return false;

                return true;
            }

            static int suffixLength(byte[] needle, int p)
            {
                int len = 0;

                for (int i = p, j = needle.Length - 1; i >= 0 && needle[i] == needle[j]; --i, --j)
                    ++len;

                return len;
            }
        }
        public class FoundIndex
        {
            public int Idx { get; private set; }
            public FoundIndex(int idx)
            {
                this.Idx = idx;
            }
        }
        #endregion
    }
}
