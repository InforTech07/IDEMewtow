using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEMewtow
{
    class ErrorLog
    {

        public static List<string> ListError = new List<string>();

        public static int ContError = 0;

        public ErrorLog()
        {

        }


        public static void AddError(string v_error)
        {
            ContError += 1;
            ListError.Add(v_error);
        }

        public static List<string> GetError()
        {
            return ListError;
        }

        public static void ResetErrorList()
        {
            ListError.Clear();
            ContError = 0;
        }
    }
}
