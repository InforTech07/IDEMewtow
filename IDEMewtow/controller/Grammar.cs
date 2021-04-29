using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace IDEMewtow
{
    class Grammar
    {

        private string Mgrammar;
        private string Mtype;
        public Grammar()
        {
            Mgrammar = string.Empty;
            Mtype = string.Empty;
        }

        public Grammar(string vgrammar,string vtype)
        {
            Mgrammar = vgrammar;
            Mtype = vtype;
        }

        public String mgrammar
        {
            get { return Mgrammar; }
            set { Mgrammar = value; }
        }
        public String mtype
        {
            get { return Mtype; }
            set { Mtype = value; }
        }

        public bool LoadGrammar()
        {
            //char[] delimiterChars = { ' ', ',', '.', ':', '(', ')', '{', '}', '\t', '\n' };
            try
            {
                Console.WriteLine("iniciando carga de Gramatica");
                char[] delimiterSentece = { '\n' };
                char[] delimiterWord = { ';' };
                string PathFile = Helpers.OpenFile();
                Console.WriteLine("cargando file");
                string ContentFile = Helpers.ReadFile(PathFile);
                string[] ArrayGrammar = ContentFile.Split(delimiterSentece);


                foreach (var w in ArrayGrammar)
                {
                    Console.WriteLine(w);
                    string[] Grammar = w.Split(delimiterWord);
                    RequestDB.NewGrammar(Grammar[0], Grammar[1]);
                }

                MessageBox.Show(ContentFile, "File Content at path: " + PathFile, MessageBoxButtons.OK);

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

            
            return true;
        }
 
    }
}
