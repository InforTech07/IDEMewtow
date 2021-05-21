using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEMewtow
{
    class LogError
    {
        public static List<string> ListError = new List<string>();


        public LogError()
        {

        }


        public static void AddError(string v_error)
        {
            ListError.Add(v_error);
        }

        public static List<string> GetError()
        {
            return ListError;
        }


    }
}
