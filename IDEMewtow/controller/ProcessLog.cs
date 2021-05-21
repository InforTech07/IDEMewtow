using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEMewtow
{
    class ProcessLog
    {
        
        public static List<string> ListProcess = new List<string>();

        public ProcessLog()
        {
            
        }

        public static void AddProcess(string process)
        {
            ListProcess.Add(process);
        }

        public static List<string> Getprocess()
        {
            return ListProcess;
        }

    }
}
