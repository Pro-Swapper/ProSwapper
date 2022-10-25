using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro_Swapper
{
    internal class Utils
    {
        //Kill duplicate Pro Swapper's
        internal static void KillDuplicateProcesses()
        {
            Process CurrentProc = Process.GetCurrentProcess();
            foreach (Process proc in Process.GetProcessesByName(CurrentProc.ProcessName))
                if (proc.Id != CurrentProc.Id)
                    proc.Kill();
        }
    }
}
