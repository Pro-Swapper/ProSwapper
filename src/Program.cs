using System;
using System.IO;
using System.Windows.Forms;
using Pro_Swapper.Properties;
namespace Pro_Swapper
{
    static class Program
    {

        const string oodle6 = "oo2core_6_win64.dll";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
                if (!File.Exists(oodle6))
                File.WriteAllBytes(oodle6, Resources.oo2core_6_win64);//Writes both so we can break
                else if(new FileInfo(oodle6).Length != 950248)
                File.WriteAllBytes(oodle6, Resources.oo2core_6_win64);


            global.InitConfig();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }            
        
    }
}