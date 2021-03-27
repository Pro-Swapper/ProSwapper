using System;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;
namespace Pro_Swapper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        private static void Main()
        {
            for (int i = 0; i < dlls.Length;i++)
            {
                if (global.FileToMd5(dlls[i] + ".dll") != hashes[i])
                {
                    ExtractDlls();
                    break;
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }

        private static readonly string[] dlls = { "Bunifu_UI_v1.52", "DiscordRPC", "Newtonsoft.Json" };
        private static readonly string[] hashes = { "3764580d568e4fc506048e04db90562c", "ad463f573775c43a561ade842c41b0e8", "6815034209687816d8cf401877ec8133" };

        private static void ExtractDlls()
        {
            foreach (string dll in dlls)
            {
                string dllfile = dll + ".dll";

                if (File.Exists(dllfile))
                    File.Delete(dllfile);
            }
            string zipfilename = "dlls.zip";
            File.WriteAllBytes(zipfilename, Properties.Resources.Ref);
            ZipFile.ExtractToDirectory(zipfilename, AppDomain.CurrentDomain.BaseDirectory);
            File.Delete(zipfilename);
        }
    }
}