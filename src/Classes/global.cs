using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Reflection;
using System.IO.Compression;
using System.Drawing;
using System.Drawing.Imaging;
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
        public static string md5(string filepath)
        {
            if (File.Exists(filepath))
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filepath))
                    {
                        return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                    }
                }
            }
            else
            {
                return "0";
            }
        }
        public class Swap
        {
            public  string[] Search { get; set; }
            public string[] Replace { get; set; }
            public  string[] File { get; set; }
            public long[] Offset { get; set; }

            public Swap(string[] search, string[] replace, string[] file, long[] offset)
            {
                Search = search;
                Replace = replace;
                File = file;
                Offset = offset;
            }
        }
        public static List<Item> ItemList = new List<Item>();
        public class Item
        {
            public int ID { get; set; }
            public string SwapsFrom { get; set; }
            public string SwapsTo { get; set; }
            public string SwapsFromIcon { get; set; }
            public string SwapsToIcon { get; set; }
            public string Note { get; set; }
            public Swap Swap { get; set; }
            public Item(int id, string swapsFrom, string swapsTo, string fromicon, string toicon, Swap swap, string note = "")
            {
                ID = id;
                SwapsFrom = swapsFrom;
                SwapsTo = swapsTo;

                SwapsFromIcon = fromicon;
                SwapsToIcon = toicon;

                Swap = swap;
                Note = note;

            }
        }



        //Researcher
        public static long FindPosition(Stream stream, int searchPosition, long startIndex, byte[] searchPattern)
        {
            long searchResults = 0;
            stream.Position = startIndex;
            while (true)
            {
                if (stream.Position == stream.Length)
                    return searchResults;

                var latestbyte = stream.ReadByte();
                if (latestbyte == -1)
                    break;

                if (latestbyte == searchPattern[searchPosition])
                {
                    searchPosition++;
                    if (searchPosition == searchPattern.Length)
                        return stream.Position - searchPattern.Length;
                }
                else
                    searchPosition = 0;
            }

            return searchResults;
        }
    }
}
