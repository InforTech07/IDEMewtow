using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Text.RegularExpressions;

namespace IDEMewtow
{
    class Grammar
    {

        private string Mgrammar;
        private string Mtype;

        public string CNamespace = "mnamespace";
        public string CClass = "mclass";
        public string CStatic = "mstatic";
        public string CVoid = "mvoid";
        public string CMain = "mmain";
        public string CInt = "mint";
        public string CString = "mstring";
        public string CConsoleWriteline = "mprint";
        public string CDo = "mdo";
        public string CSwitch = "mswitch";
        public string CCase = "mcase";
        public string CWhile = "mwhile";
        public string CBreak = "mbreak";
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

        




        public void CreateWordCsharp()
        {
            var data = KeyWord.GetKeywordList();
            foreach(var d in data)
            {
                string wordc = d.mwordCs.ToString();
                string wordm = d.mkeyword.ToString();
                switch (wordc)
                {
                    case "namespace": CNamespace = wordm;
                        break;
                    case "class":
                        CClass = wordm;
                        break;
                    case "static":
                        CStatic = wordm;
                        break;
                    case "void": CVoid = wordm;
                        break;
                    case "main": CMain = wordm;
                        break;
                    case "int":
                        CInt = wordm;
                        break;
                    case "string":
                        CString = wordm;
                        break;
                    case "ConsoleWriteline":
                        CConsoleWriteline = wordm;
                        break;
                    case "do":
                        CDo = wordm;
                        break;
                    case "switch":
                        CSwitch = wordm;
                        break;
                    case "case":
                        CCase = wordm;
                        break;
                    case "while":
                        CWhile = wordm;
                        break;
                    case "break":
                        CBreak = wordm;
                        break;
                    default:
                        ProcessLog.AddProcess("El: [" + wordc + "] No es valido para el lenguaje");
                        break;
                }
            }
        }


        /*
         * public string CNamespace = "mnamespace";
        public string CClass = "mclass";
        public string CStatic = "mstatic";
        public string CVoid = "mvoid";
        public string CMain = "mmain";
        public string CInt = "mint";
        public string CString = "mstring";
        public string CConsoleWriteline = "mprint";
        public string CDo = "mdo";
        public string CSwitch = "mswitch";
        public string CCase = "mcase";
        public string CWhile = "mwhile";
        public string CBreak = "mbreak";
         */





        public int ValidSentence(string Sentence, int line)
        {
            int res = 0;
            string asig = ":=";
            Regex rgexcomment = new Regex(@"//\s\b[a-zA-z]\w+");
            Regex rgexnamespace = new Regex(@"" + CNamespace + @"\s\b[a-zA-z]\w+");
            Regex rgexclass = new Regex(@"" + CClass + @"\s\b[a-zA-z]\w+");
            Regex rgexstatic = new Regex(@"" + CStatic + @"\s" + CVoid + @"\s" + CMain + @"\(\)");
            Regex rgexint = new Regex(@"" + CInt + @"\s\b[a-zA-z]");
            Regex rgexintasig = new Regex(@"" + CInt + @"\s\b[a-zA-z]\w+\s" + asig + @"\s\d");
            Regex rgexstringasig = new Regex(@"" + CString + @"\s\b[a-zA-z]\w+\s" + asig + @"\s\'\b[a-z0-9A-z,\s]\w+'$");
            Regex rgexstring = new Regex(@"" + CString + @"\s\b[a-zA-z]");
            Regex rgexwriteline = new Regex(@"" + CConsoleWriteline + @"\(\b[a-z0-9A-z,\s]\w+\)$");
            Regex rgexswitch = new Regex(@"" + CSwitch + @"\([a-zA-Z]\w+\)");
            Regex rgexbreak = new Regex(@"" + CBreak + "");
            switch (Sentence)
            {
                case var vv when rgexcomment.IsMatch(Sentence):
                    res = 2;
                    break;
                case var vv when rgexnamespace.IsMatch(Sentence):
                    res = 1;
                    break;
                case var vv when rgexclass.IsMatch(Sentence):
                    res = 1;
                    break;
                case var vv when rgexstatic.IsMatch(Sentence):
                    res = 1;
                    break;
                case var vv when rgexint.IsMatch(Sentence):
                    res = 1;
                    break;
                case var vv when rgexintasig.IsMatch(Sentence):
                    res = 1;
                    break;
                case var vv when rgexstring.IsMatch(Sentence):
                    res = 1;
                    break;
                case var vv when rgexstringasig.IsMatch(Sentence):
                    res = 1;
                    break;
                case var vv when rgexswitch.IsMatch(Sentence):
                    res = 1;
                    break;
                case var vv when rgexwriteline.IsMatch(Sentence):
                    res = 1;
                    break;
                case var vv when rgexbreak.IsMatch(Sentence):
                    res = 1;
                    break;
                default:
                    ErrorLog.AddError("-> Error en: [" + Sentence + "] ->linea: " + line);
                    res = 0;
                    break;
            }            

            return res;
        }

    }
}
