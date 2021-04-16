using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Reflection;
using System.IO.Compression;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Collections;

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
        public static Color MainMenu, Button, TextColor, ItemsBG;


        public static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

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
        }

        //Settings Writer

        public static string ProSwapperFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Pro_Swapper\";
        public static string settingspath()
        {
            string path = ProSwapperFolder + @"Config\" + version + "_config.txt";
            CreateDir(ProSwapperFolder + @"Config\");
            if (!File.Exists(path))
            {
                using (StreamWriter a = new StreamWriter(path))
                {
                    foreach (Setting foo in Enum.GetValues(typeof(Setting)))
                    {
                        a.WriteLine(foo + "=");
                    }
                }
                WriteSetting("0,33,113;64,85,170;65,105,255;255,255,255", Setting.theme);
            }
            return path;
        }

        public static void CreateDir(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
        public static void WriteSetting(string newText, Setting value)
        {
            string line;
            int counter = 1;
            string text = File.ReadAllText(settingspath());
            using (StringReader reader = new StringReader(text))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith(value + "="))
                    {
                        lineChanger(value + "=" + newText, settingspath(), counter);
                        break;
                    }
                    counter++;
                }
            }
        }
        private static void lineChanger(string newText, string fileName, int line_to_edit)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit - 1] = newText;
            File.WriteAllLines(fileName, arrLine);
        }
        public static string ReadSetting(Setting value)
        {
            string line;
            using (StreamReader file = new StreamReader(settingspath()))
            {
                for (int counter = 0; (line = file.ReadLine()) != null; counter++)
                {
                    if (line.StartsWith(value + "="))
                        return line.Replace(value + "=", "");
                }
                return null;
            }
        }
        public enum Setting
        {
            Paks,
            theme,
            lastopened,
            swaplogs
        }
        public static string Decompress(string input)
        {
            return Encoding.UTF8.GetString(Decompress(Convert.FromBase64String(input)));
        }
        public static byte[] Decompress(byte[] input)
        {
            using (var source = new MemoryStream(input))
            {
                byte[] lengthBytes = new byte[4];
                source.Read(lengthBytes, 0, 4);

                var length = BitConverter.ToInt32(lengthBytes, 0);
                using (var decompressionStream = new GZipStream(source,
                    CompressionMode.Decompress))
                {
                    var result = new byte[length];
                    decompressionStream.Read(result, 0, length);
                    return result;
                }
            }
        }

        public static Items.Root items { get; set; }
        public static byte[] HexToByte(string hex)
        {
            hex = hex.Replace(" ", "").Replace("hex=", "");
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public static string FileToMd5(string filename)
        {
            if (File.Exists(filename))
                        return BitConverter.ToString(MD5.Create().ComputeHash(File.OpenRead(filename))).Replace("-", "").ToLowerInvariant();
            else
                return string.Empty;
        }

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
    }
}
