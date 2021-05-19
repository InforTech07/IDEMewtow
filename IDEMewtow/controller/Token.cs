using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace IDEMewtow
{
    public class Token
    {
        private string word;
        private string typeword;
        private int indice;

        public Token()
        {
            word = string.Empty;
            typeword = string.Empty;
            indice = 0;

        }

        public Token(string vword, string vtype, int ind)
        {
            word = vword;
            typeword = vtype;
            indice = ind;

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

        public int Indice
        {
            get { return indice; }
            set { indice = value; }
        }

    }

    public class TokenGenerator
    {
        public TokenGenerator()
        {

        }
        

        public static List<Token> CreateTokens(string content)
        {
            //char[] delimiterChars = { ' ', ',', '.', ':', '(', ')', '{', '}', '\t', '\n' };
            char[] delimiterChars = { ' ','\n', '\t', };
            string[] tokens = content.Split(delimiterChars);

            List<Token> ListTokens = new List<Token>();

            foreach (var pword in tokens)
            {
                int indx = WordClasification(pword);
                string description = TokenDescription(indx);
                Token NewToken = new Token(pword,description,indx);
                ListTokens.Add(NewToken);
            }

            return ListTokens;
        }





        private static int  WordClasification(string vword)
        {
            int indice = 0;
            switch (vword)
            {
                case var vwords when Regex.IsMatch(vword, @"mfunction"):
                    indice = 1;
                    break;

                case var vwords when Regex.IsMatch(vword, @"mvoid"):
                    indice = 1;
                    break;
                case var vwords when Regex.IsMatch(vword, @"mmain"):
                    indice = 1;
                    break;
                case var vwords when Regex.IsMatch(vword, @"mif"):
                    indice = 1;
                    break;
                case var vwords when Regex.IsMatch(vword, @"melse"):
                    indice = 1;
                    break;
                case var vwords when Regex.IsMatch(vword, @"mvar"):
                    indice = 1;
                    break;
                case var vwords when Regex.IsMatch(vword, @"^[a-zA-Z0-9\s,]*$"):
                    indice = 2;
                    break;
                case var vwords when Regex.IsMatch(vword, @"([+]|[-]|[\/]|[*])"):
                    indice = 3;
                    break;
                case var vwords when Regex.IsMatch(vword, @":="):
                    indice = 4;
                    break;
                case var vwords when Regex.IsMatch(vword, @"{"):
                    indice = 5;
                    break;
                case var vwords when Regex.IsMatch(vword, @"}"):
                    indice = 6;
                    break;
                case var vwords when Regex.IsMatch(vword, @"\("):
                    indice = 7;
                    break;
                case var vwords when Regex.IsMatch(vword, @"\)"):
                    indice = 8;
                    break;
                case var vwords when Regex.IsMatch(vword, @"\n"):
                    indice = 9;
                    break;
                case var vwords when Regex.IsMatch(vword, @" "):
                    indice = 10;
                    break;
                default:
                    indice=11;
                    break;
            }
            return indice;

        }


       private static string TokenDescription(int i)
        {
            var des = string.Empty;
            switch (i)
            {
                case 1:
                    des = "Reservada";
                    break;
                case 2: des = "identificador";
                    break;
                case 3:
                    des = "operador";
                    break;
                case 4:
                    des = "asignacion";
                    break;
                case 5:
                    des = "Abre_llave";
                    break;
                case 6:
                    des = "Cierra_llave";
                    break;
                case 7:
                    des = "Abre_parant";
                    break;
                case 8:
                    des = "cierra_parent";
                    break;
                case 9:
                    des = "saltolinea";
                    break;
                case 10:
                    des = "espacio";
                    break;
                case 11: des ="desconocido";
                    break;
                default:
                    des = "desconocido";
                    break;

            }

            return des;
        }


    }




}
