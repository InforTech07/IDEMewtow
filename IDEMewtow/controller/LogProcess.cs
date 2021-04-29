using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEMewtow
{
    class LogProcess
    {
        private string Process;
      //  public static List<LogProcess> listprocess = new List<LogProcess>();
        public static List<string> ListProcess = new List<string>();

        public LogProcess()
        {
            Process = string.Empty;
        }

        public LogProcess(string vprocess)
        {
            Process = vprocess;
        }

        public String process
        {
            get { return Process; }
            set { Process = value; }
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
