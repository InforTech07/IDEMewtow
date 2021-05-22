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
        public string Mwordcs;

        public static string CNamespace;
        public static string CClass;
        public static string CStatic;
        public static string CVoid;
        public static string CMain;
        public static string CInt;
        public static string CString;
        public static string CConsoleWriteline;
        public static string CDo;
        public static string CSwitch;
        public static string CCase;
        public static string CWhile;
        public static string CBreak;
        public static string CIf;
        //----------------------

        public static List<Grammar> mygrammar = new List<Grammar>();
        
        public Grammar()
        {
            Mgrammar = string.Empty;
            Mtype = string.Empty;
            Mwordcs = string.Empty;
        }

        public Grammar(string vgrammar,string vtype, string vwordc)
        {
            Mgrammar = vgrammar;
            Mtype = vtype;
            Mwordcs = vwordc;
            
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

        public String mwordcs
        {
            get { return Mwordcs; }
            set { Mwordcs = value; }
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

        




        public  void CreateWordCsharp()
        {
            var dat = RequestDB.GetKeyWords();

            mygrammar = dat.Tables[0].AsEnumerable().Select(
                dataRow => new Grammar
                {
                    Mgrammar = dataRow.Field<string>("palabra"),
                    Mtype = dataRow.Field<string>("tipo_palabra"),
                    Mwordcs = dataRow.Field<string>("palabracs"),

                }).ToList();


           // var data = KeyWord.GetKeywordList();
            foreach(var d in mygrammar)
            {
                
                string wordc = d.Mwordcs.ToString();
                switch (wordc)
                {
                   
                    case var gw when  Regex.IsMatch(wordc,@"namespace"):
                        CNamespace = d.Mgrammar.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"class"):
                        CClass = d.Mgrammar.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"static"):
                        CStatic = d.Mgrammar.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"void"):
                        CVoid = d.Mgrammar.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"main"):
                        CMain = d.Mgrammar.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"int"):
                        CInt = d.Mgrammar.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"string"):
                        CString = d.Mgrammar.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"consoleWriteline"):
                        CConsoleWriteline = d.Mgrammar.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"do"):
                        CDo = d.Mgrammar.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"switch"):
                        CSwitch = d.Mgrammar.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"case"):
                        CCase = d.Mgrammar.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"while"):
                        CWhile = d.Mgrammar.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"if"):
                        CIf = d.Mgrammar.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"break"):
                        CBreak = d.Mgrammar.ToString();
                        break;
                    default:
                        Console.WriteLine("error" + wordc);
                        ProcessLog.AddProcess("El: [" + wordc + "] No es valido para el lenguaje");
                        break;
                }
            }
        }


        public int ValidSentence(string Sentence, int line)
        {
            int res = 0;
            string Mgrammarsentence = string.Empty;
            Regex rgexcomment = new Regex(@"//\s\b[a-zA-z]\w+");
            Regex rgexnamespace = new Regex(@"^" + CNamespace + @"\s\b[a-zA-z]\w+");
            Regex rgexclass = new Regex(@"^" + CClass + @"\s\b[a-zA-z]\w+");
            Regex rgexstatic = new Regex(@"" + CStatic + @"\s" + CVoid + @"\s" + CMain + "");
            Regex rgexint = new Regex(@"^" + CInt + @"\s\b[a-zA-z]");
            Regex rgexintasig = new Regex(@"^" + CInt + @"\s\b[a-zA-z]\w+\s\:=\s\d");
            Regex rgexstringasig = new Regex(@"^" + CString + @"\s\b[a-zA-z]\w+\s\:=\s\'\b[a-z0-9A-z,\s]\w+'$");
            Regex rgexstring = new Regex(@"^" + CString + @"\s\b[a-zA-z]");
            Regex rgexwriteline = new Regex(@"^" + CConsoleWriteline + @"\(\b[a-z0-9A-z,\s]\w+\)$");
            Regex rgexswitch = new Regex(@"^" + CSwitch + @"\([a-zA-Z]\w+\)");
            Regex rgexbreak = new Regex(@"^" + CBreak + "");
            Regex rgexdo = new Regex(@"^" + CDo + "");
            Regex rgexwhile = new Regex(@"" + CWhile+ @"[\(][a-zA-z]\w+[\)]");
            Regex rgexbraketopen = new Regex(@"\{");
            Regex rgexbraketclose = new Regex(@"\}");
            Regex rgexif = new Regex(@""+CIf+ @"\s\b[a-zA-z]\w+\s[=]\s\d");

            switch (Sentence)
            {
                case var vv when rgexbraketopen.IsMatch(Sentence):
                    Mgrammarsentence = "[Abre llave]";
                    res = 3;
                    break;
                case var vv when rgexbraketclose.IsMatch(Sentence):
                    Mgrammarsentence = "[Cierra llave]";
                    res = 4;
                    break;
                case var vv when rgexcomment.IsMatch(Sentence):
                    Mgrammarsentence = "[un comentario]";
                    res = 2;
                    break;
                case var vv when rgexnamespace.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion del mnamespace]";
                    res = 1;
                    break;
                case var vv when rgexclass.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion de mclase]";
                    res = 1;
                    break;
                case var vv when rgexstatic.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion de metodo principal mstatic]";
                    res = 1;
                    break;
                case var vv when rgexint.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion de variable Entero]";
                    res = 1;
                    break;
                case var vv when rgexintasig.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion y asignacion de variable entero]";
                    res = 1;
                    break;
                case var vv when rgexstring.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion de variable typo mstring]";
                    res = 1;
                    break;
                case var vv when rgexstringasig.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion y asignacion de variable mstring]";
                    res = 1;
                    break;
                case var vv when rgexswitch.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion del control mcase]";
                    res = 1;
                    break;
                case var vv when rgexwriteline.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion del metodo mWriteline]";
                    res = 1;
                    break;
                case var vv when rgexbreak.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion del interruptor mbreak]";
                    res = 1;
                    break;
                case var vv when rgexif.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion del controlador mIF]";
                    res = 1;
                    break;
                case var vv when rgexdo.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion del incio de bucle mDo]";
                    res = 1;
                    break;
                case var vv when rgexwhile.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion del bucle mwhile]";
                    res = 1;
                    break;
                default:
                    ErrorLog.AddError("-> Error en: [" + Sentence + "] ->linea: " + line);
                    Mgrammarsentence = "[Declaracion desconocida del lenguaje.!]";
                    res = 0;
                    break;
            }

            TokenMewtow.AddListSentence(line+ ";"+ Mgrammarsentence+";"+Sentence);
            return res;
        }







    }





}
