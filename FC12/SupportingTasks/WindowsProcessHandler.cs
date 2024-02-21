using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FC12.SupportingTasks
{
    public static class WindowsProcessHandler
    {
        public static readonly string[] _processNamesToKill = 
        {
            "ContentCapture", 
            "FlexiBR", 
            "StationMonitor"
        };

        public static void KillWindowsProcess()
        {

            foreach (Process process in GetProcesses())
            {
                //MessageBox.Show(process.ProcessName);
                process.Kill();
            }
        }

        private static List<Process> GetProcesses()
        {
            List<Process> result = new List<Process>();

            foreach (string processName in _processNamesToKill)
            {
                var existingProcesses = Process.GetProcessesByName(processName).ToList();

                //MessageBox.Show(processName + " " + existingProcesses.Count);

                if (existingProcesses != null)
                {
                    result = result.Concat(existingProcesses).ToList();
                }
                else
                {
                    continue;
                }
            }

            return result;
        }
    }
}
