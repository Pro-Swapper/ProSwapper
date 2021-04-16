using System;
using System.Windows.Forms;
using System.IO;
using Pro_Swapper.Properties;
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
            WriteDll(dlls[0], Resources.Bunifu_UI_v1_52, hashes[0]);
            WriteDll(dlls[1], Resources.DiscordRPC, hashes[1]);
            WriteDll(dlls[2], Resources.Newtonsoft_Json, hashes[2]);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
        private static readonly string[] dlls = { "Bunifu_UI_v1.52", "DiscordRPC", "Newtonsoft.Json" };
        private static readonly string[] hashes = { "3764580d568e4fc506048e04db90562c", "ad463f573775c43a561ade842c41b0e8","6815034209687816d8cf401877ec8133"};
        private static void WriteDll(string filename, byte[] resource, string hash)
        {
            filename += ".dll";
            if (global.FileToMd5(filename) != hash)
            {
                File.WriteAllBytes(filename, global.Decompress(resource));
            }
        }
    }
}