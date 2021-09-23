using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro_Swapper.Logger
{
    public class Logger
    {
        StreamWriter writer;
        public Logger(string Path) 
        {
            global.DeleteFile(Path);//We only want the latest logs yk

            using (FileStream fs = new FileStream(Path, FileMode.Append, FileAccess.Write))
            {
                writer = new StreamWriter(fs);
            }
            writer = new StreamWriter(Path, true);
            writer.WriteLine($"[{DateTime.Now}] Initialized Logger for {Process.GetCurrentProcess().ProcessName}");
            writer.Flush();//Actually writes it
        }
        public void Dispose()
        {
            writer.Flush();
            writer.Close();
            writer.Dispose();
        }
        public void Log(string content)
        {
            writer.WriteLine($"[{DateTime.Now}] {content}");
            writer.Flush();//Actually writes it
        }
        public void LogError(string content)
        {
            writer.WriteLine("====================================");
            writer.WriteLine($"ERROR: [{DateTime.Now}] {content}");
            writer.WriteLine("====================================");
            writer.Flush();//Actually writes it
        }

    }
}
