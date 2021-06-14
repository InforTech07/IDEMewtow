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
    /// <summary>
    /// Grammar: clase que se encarga de contener, y disponer la gramatica que se utilizara en el lenguaje.
    /// </summary>
    class Grammar
    {
        /// <summary>
        /// atributos de la clase.
        /// </summary>
        private string Mgrammar;
        private string Mtype;
        public string Mwordcs;

        public static string MNamespace;
        public static string MClass;
        public static string MStatic;
        public static string MVoid;
        public static string MMain;
        public static string MInt;
        public static string MString;
        public static string MConsole;
        public static string MReadline;
        public static string MSwitch;
        public static string MCase;
        public static string MBreak;
        public static string MDefault;
        public static string MIf;
        public static string MDo;
        public static string MWhile;
        /// <summary>
        /// listado encargado de almacenar las gramaticas del lenguaje.
        /// </summary>
        public static List<Grammar> mygrammar = new List<Grammar>();
        /// <summary>
        /// constructor de la clase sin parametros
        /// </summary>
        public Grammar()
        {
            Mgrammar = string.Empty;
            Mtype = string.Empty;
            Mwordcs = string.Empty;
        }

        /// <summary>
        /// constructor con parametros
        /// </summary>
        /// <param name="vgrammar">sentencia de gramatica</param>
        /// <param name="vtype">tipo de la gramatica;metodo principal, funcion, asignacion</param>
        /// <param name="vwordc"></param>
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
          //  var dat = RequestDB.GetKeyWords();

          //  mygrammar = dat.Tables[0].AsEnumerable().Select(
          //      dataRow => new Grammar
           //     {
           //         Mgrammar = dataRow.Field<string>("palabra"),
            //        Mtype = dataRow.Field<string>("tipo_palabra"),
             //       Mwordcs = dataRow.Field<string>("palabracs"),

               // }).ToList();


            var data = KeyWord.GetKeywordList();
            foreach(var d in data)
            {

                // string wordc = d.mkeyword.ToString();
                string wordc = d.mwordCs.ToString();
               // string wordc = d.Mwordcs.ToString();
                switch (wordc)
                {
                   
                    case var gw when  Regex.IsMatch(wordc,@"namespace"):
                        MNamespace = d.mkeyword.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"class"):
                        MClass = d.mkeyword.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"static"):
                        MStatic = d.mkeyword.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"void"):
                        MVoid = d.mkeyword.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"main"):
                        MMain = d.mkeyword.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"int"):
                        MInt = d.mkeyword.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"string"):
                        MString = d.mkeyword.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"consolewriteline"):
                        MConsole = d.mkeyword.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"consolereadline"):
                        MReadline = d.mkeyword.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"switch"):
                        MSwitch = d.mkeyword.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"case"):
                        MCase = d.mkeyword.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"break"):
                        MBreak = d.mkeyword.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"default"):
                        MDefault = d.mkeyword.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"if"):
                        MIf = d.mkeyword.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"do"):
                        MDo = d.mkeyword.ToString();
                        break;
                    case var gw when Regex.IsMatch(wordc, @"while"):
                        MWhile = d.mkeyword.ToString();
                        break;
                    default:
                        Console.WriteLine("error" + wordc);
                        ProcessLog.AddProcess("Advertencia.!.El: [" + wordc + "] No es valido para el lenguaje");
                        break;
                }
            }

        }
        public int ValidSentence(string Sentence, int line)
        {
            int res = 0;
            string Mgrammarsentence = string.Empty;
            Regex regexComment = new Regex(@"//\s\b[a-zA-z]\w+");
            Regex regexNamespace = new Regex(@"" + MNamespace + @"\s\w+$");
            Regex regexClass = new Regex(@"" + MClass + @"\s\w+$");
            Regex regexStatic = new Regex(@"" + MStatic + @"\s" + MVoid + @"\s" + MMain + "$");
            Regex regexDeclareVariable = new Regex(@"" + MInt + @"|"+ MString + @"\s\w+$");
            Regex regexIntAsig = new Regex(@"\w+\s+(\:\=)+\s+\d$");
            Regex regexStringAsig = new Regex(@"\w+\s+(\:\=)+\s+(\'[\w\s\:\-\,]*\')$");
            Regex regexConsole = new Regex(@""+ MConsole + @"(\([\w\s\:\-\,]*\))$"); ;
            Regex regexReadLine = new Regex(@""+ MReadline +@"|(\w+\s+(\:\=)\s" + MReadline + @")");
            Regex regexSwitch = new Regex(@"" + MSwitch + @"+(\(\w+\))$");
            Regex regexCase = new Regex(@"" + MCase + @"\s(\'\w+\'|\d)\:$");
            Regex regexBreak = new Regex(@"" + MBreak +"");
            Regex regexDefault = new Regex(@"" + MDefault + @"\:$");
            Regex rgexif = new Regex(@""+MIf+@"\s+(\w+|\d)+\s+(\=\=|\!\=|\>|\<)+\s+(\w+|\d)\:$");
            Regex regexDo = new Regex(@"" + MDo + "");
            Regex regexWhile = new Regex(@"" + MWhile + @"[\(][a-zA-z]\w+[\)]");
            Regex regexBraketopen = new Regex(@"\{");
            Regex regexBraketclose = new Regex(@"\}");
            Regex regexOperationAsig = new Regex(@"\w+\s+(\:\=)+\s+(\d|\w+)+\s+(\*|-|\/|\+)+\s+(\d|\w+)$");
            switch (Sentence)
            {
                case var vv when regexBraketopen.IsMatch(Sentence):
                    Mgrammarsentence = "[Abre llave]";
                    res = 3;
                    break;
                case var vv when regexBraketclose.IsMatch(Sentence):
                    Mgrammarsentence = "[Cierra llave]";
                    res = 4;
                    break;
                case var vv when regexComment.IsMatch(Sentence):
                    Mgrammarsentence = "[un comentario]";
                    res = 2;
                    break;
                case var vv when regexNamespace.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion del mnamespace]";
                    res = 1;
                    break;
                case var vv when regexClass.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion de mclase]";
                    res = 1;
                    break;
                case var vv when regexStatic.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion de metodo principal mstatic]";
                    res = 1;
                    break;
                case var vv when regexDeclareVariable.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion de variable]";
                    res = 1;
                    break;
                case var vv when regexIntAsig.IsMatch(Sentence):
                    Mgrammarsentence = "[Asignacion de variable entero]";
                   
                    res = 1;
                    break;
                case var vv when regexStringAsig.IsMatch(Sentence):
                    Mgrammarsentence = "[Asignacion de variable string]";
                    res = 1;
                    break;
                case var vv when regexOperationAsig.IsMatch(Sentence):
                    Mgrammarsentence = "[Asignacion y operacion de varibles,Enteros]";
                    res = 1;
                    break;
                case var vv when regexConsole.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion del metodo ->" + Sentence + "]";
                    res = 1;
                    break;
                case var vv when regexSwitch.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion del control ->" + Sentence +"]";
                    res = 1;
                    break;
                case var vv when regexCase.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion del opcion ->" + Sentence + "]";
                    res = 1;
                    break;
                case var vv when regexDefault.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion del opcion ->" + Sentence + "]";
                    res = 1;
                    break;
                case var vv when regexBreak.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion del interruptor ->" + Sentence + "]";
                    res = 1;
                    break;
                case var vv when regexReadLine.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion la sentencia ReadLine]";
                    res = 1;
                    break;
                case var vv when rgexif.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion del controlador " + Sentence + "]";
                    res = 1;
                    break;
                case var vv when regexDo.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion del incio de bucle -> " + Sentence + "]";
                    res = 1;
                    break;
                case var vv when regexWhile.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion del bucle ->" + Sentence + "]";
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

        public void validAsig(string sentence)
        {


        }

    
    }

}
