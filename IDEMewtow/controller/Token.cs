using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEMewtow
{
    public class Token
    {
        private string word;
        private string typeword;

        public Token()
        {
            word = string.Empty;
            typeword = string.Empty;

        }

        public Token(string vword, string vtype)
        {
            word = vword;
            typeword = vtype;

        }
        public String Word
        {
            get { return word; }
            set { word = value; }
        }
        public String Typewrod
        {
            get { return typeword; }
            set { typeword = value; }
        }


    }

    public class TokenGenerator
    {
        public static string[] CreateTokens(string content)
        {
            //char[] delimiterChars = { ' ', ',', '.', ':', '(', ')', '{', '}', '\t', '\n' };
            char[] delimiterChars = { ' ','\n' };
            string[] tokens = content.Split(delimiterChars);
            return tokens;
        }
    }


}
