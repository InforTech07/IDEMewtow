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
        public static List<string> codecsharp = new List<string>();
        public static List<string> declareVariable = new List<string>();
        public static List<string> asigVariable = new List<string>();
        public static List<string> method = new List<string>();
        public static List<string> listmclass = new List<string>();
        public static int numVaribles = 0;
        public static int numMethod = 0;
        public static int numNamespace = 0;
        public static int numclass = 0;

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

            var data = KeyWord.GetKeywordList();
            foreach(var d in data)
            {

                string wordc = d.mwordCs.ToString();
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
            // Regex regexReadLine = new Regex(@""+ MReadline +@"|(\w+\s+(\=)\s" + MReadline + @")");
            int res = 0;
            string Mgrammarsentence = string.Empty;
            Regex regexComment = new Regex(@"//\s+\b[a-zA-z]\w+");
            Regex regexNamespace = new Regex(@"" + MNamespace + @"\s\w+$");
            Regex regexClass = new Regex(@"" + MClass + @"\s\w+$");
            Regex regexStatic = new Regex(@"" + MStatic + @"\s" + MVoid + @"\s" + MMain + "$");
            Regex regexDeclareVariable = new Regex(@"((" + MInt + @")|("+ MString + @"))+\s[a-z]\w+$");
            Regex regexIntAsig = new Regex(@"\w+\s+(\=)+\s+(\d|" + MInt + @"\(" + MReadline + @"\))$");
            Regex regexStringAsig = new Regex(@"\w+\s+(\=)+\s+(\'[\w\s\:\-\,]*\')$");
            Regex regexConsole = new Regex(@""+ MConsole + @"\s(\(\'[\w\s\:\-\,]*\'\))|(\(\'[\w\s\:\-\,]*\'\,\s\w+\))$");
            Regex regexReadLine = new Regex(@""+ MReadline +"");
          //  Regex regexReadLineAsig = new Regex(@"(\w+\s+(\=)\s" + MInt + @"\(" + MReadline + @"\))");
            Regex regexSwitch = new Regex(@"" + MSwitch + @"+(\(\w+\))$");
            Regex regexCase = new Regex(@"" + MCase + @"\s(\'\w+\'|\d)\:$");
            Regex regexBreak = new Regex(@"" + MBreak +"");
            Regex regexDefault = new Regex(@"" + MDefault + @"\:$");
            Regex rgexif = new Regex(@""+MIf+@"\s+(\w+|\d)+\s+(\=\=|\!\=|\>|\<)+\s+(\w+|\d)\:$");
            Regex regexDo = new Regex(@"" + MDo + "");
            Regex regexWhile = new Regex(@"" + MWhile + @"[\(][a-zA-z]\w+[\)]");
            Regex regexBraketopen = new Regex(@"\{");
            Regex regexBraketclose = new Regex(@"\}");
            Regex regexOperationAsig = new Regex(@"\w+\s+(\=)+\s+(\d|\w+)+\s+(\*|-|\/|\+)+\s+(\d|\w+)$");
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
                case var vv when regexReadLine.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion la sentencia ReadLine]";
                    res = 1;
                    break;
                case var vv when regexNamespace.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion del mnamespace]";
                    numNamespace += 1;
                    res = 1;
                    break;
                case var vv when regexClass.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion de mclase]";
                    listmclass.Add(Sentence);
                    numclass += 1;
                    res = 1;
                    break;
                case var vv when regexStatic.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion de metodo principal mstatic]";
                    method.Add(Sentence);
                    numMethod += 1;
                    res = 1;
                    break;
                case var vv when regexDeclareVariable.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion de variable]";
                    declareVariable.Add(Sentence);
                    numVaribles += 1;
                    res = 1;
                    break;
                case var vv when regexIntAsig.IsMatch(Sentence):
                    Mgrammarsentence = "[Asignacion de variable entero]";
                    string[] mvar1 = Sentence.Split(' ');
                    asigVariable.Add(mvar1[0]);
                    res = 1;
                    break;
                case var vv when regexStringAsig.IsMatch(Sentence):
                    Mgrammarsentence = "[Asignacion de variable string]";
                    string[] mvar2 = Sentence.Split(' ');
                    asigVariable.Add(mvar2[0]);
                    res = 1;
                    break;
                case var vv when regexOperationAsig.IsMatch(Sentence):
                    Mgrammarsentence = "[Asignacion y operacion de varibles,Enteros]";
                    string[] mvar3 = Sentence.Split(' ');
                    asigVariable.Add(mvar3[0]);
                    res = 1;
                    break;
             //   case var vv when regexReadLineAsig.IsMatch(Sentence):
               //     Mgrammarsentence = "[Asignacion y convertir a int]";
                //    string[] mvar4 = Sentence.Split(' ');
                 //   asigVariable.Add(mvar4[0]);
                 //   res = 1;
                   // break;
                case var vv when regexConsole.IsMatch(Sentence):
                    Mgrammarsentence = "[Declaracion de la sentencia ->" + Sentence + "]";
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

        /// <summary>
        /// semnatica
        /// </summary>
        public static void clearListsemantic()
        {
            asigVariable.Clear();
            method.Clear();
            listmclass.Clear();
            declareVariable.Clear();
            numclass = 0;
            numNamespace = 0;
            numVaribles = 0;
            numMethod = 0;
        }

        public static void Semantic()
        {
            foreach (var sen in asigVariable)
            {

              //  var resultsearch = declareVariable.Find(x => x.Contains(sen));
              //  if(resultsearch == null)
              //  {
               //      ErrorLog.AddError("-! Error la variable: " + sen + "No esta declarada");

               // }

                Console.WriteLine("asigvariable",sen);
            }
            foreach (var sen1 in declareVariable)
            {

                //  var resultsearch = declareVariable.Find(x => x.Contains(sen));
                //  if(resultsearch == null)
                //  {
                //      ErrorLog.AddError("-! Error la variable: " + sen + "No esta declarada");

                // }

                Console.WriteLine("declar vari", sen1);
            }

            if (numNamespace != 1)
            {
                ErrorLog.AddError("-! Error Namespace unico no declarado..");
            }
            if(numclass < 1)
            {
                ErrorLog.AddError("-! Error Clase no declarada");
            }
            if(numMethod < 1)
            {
                ErrorLog.AddError("-! Metodo principal no declarada");
            }
        }

        public static TreeNode GenerateTreeVar()
        {
            TreeNode Nod = new TreeNode("Variables Declaradas :(" + numVaribles+")");
            foreach (var Wn in declareVariable)
            {
                TreeNode Nnond = new TreeNode(Wn);
                Nod.Nodes.Add(Nnond);
            }

            return Nod;
        }
        public static TreeNode GenerateTreeMethod()
        {
            TreeNode Nodd = new TreeNode("Metodos Declarados :(" + numMethod+")");
            foreach (var Wn in method)
            {
                TreeNode Nnondd = new TreeNode(Wn);
                Nodd.Nodes.Add(Nnondd);
            }

            return Nodd;
        }

        public static TreeNode GenerateTreeClass()
        {
            TreeNode Nodc = new TreeNode("Mclass declarados :(" + numclass+")");
            foreach (var Wnc in listmclass)
            {
                TreeNode Nnondc = new TreeNode(Wnc);
                Nodc.Nodes.Add(Nnondc);
            }

            return Nodc;
        }






        public static void clearCodeSharp(){
            codecsharp.Clear();
        }
        
        public static string TranslatorToCsharp(string Msentence)
        {
            string sentenceSharp = string.Empty;
            Regex regexComment = new Regex(@"//\s+\b[a-zA-z]\w+");
            Regex regexNamespace = new Regex(@"" + MNamespace + @"\s\w+$");
            Regex regexClass = new Regex(@"" + MClass + @"\s\w+$");
            Regex regexStatic = new Regex(@"" + MStatic + @"\s" + MVoid + @"\s" + MMain + "$");
            Regex regexDeclareVariable = new Regex(@"((" + MInt + @")|(" + MString + @"))+\s[a-z]\w+$");
            Regex regexIntAsig = new Regex(@"\w+\s+(\=)+\s+(\d|" + MInt + @"\(" + MReadline + @"\))$");
            Regex regexStringAsig = new Regex(@"\w+\s+(\=)+\s+(\'[\w\s\:\-\,]*\')$");
            Regex regexConsole = new Regex(@"" + MConsole + @"\s(\(\'[\w\s\:\-\,]*\'\))|(\(\'[\w\s\:\-\,]*\'\,\s\w+\))$");
            Regex regexReadLine = new Regex(@"" + MReadline + "");
           // Regex regexReadLineAsig = new Regex(@"(\w+\s+(\=)\s" + MInt + @"\(" + MReadline + @"\))");
            Regex regexSwitch = new Regex(@"" + MSwitch + @"+(\(\w+\))$");
            Regex regexCase = new Regex(@"" + MCase + @"\s(\'\w+\'|\d)\:$");
            Regex regexBreak = new Regex(@"" + MBreak + "");
            Regex regexDefault = new Regex(@"" + MDefault + @"\:$");
            Regex rgexif = new Regex(@"" + MIf + @"\s+(\w+|\d)+\s+(\=\=|\!\=|\>|\<)+\s+(\w+|\d)\:$");
            Regex regexDo = new Regex(@"" + MDo + "");
            Regex regexWhile = new Regex(@"" + MWhile + @"[\(][a-zA-z]\w+[\)]");
            Regex regexBraketopen = new Regex(@"\{");
            Regex regexBraketclose = new Regex(@"\}");
            Regex regexOperationAsig = new Regex(@"\w+\s+(\=)+\s+(\d|\w+)+\s+(\*|-|\/|\+)+\s+(\d|\w+)$");
            
            switch (Msentence)
            {
                case var vv when regexBraketopen.IsMatch(Msentence):
                    sentenceSharp = "{";
                    break;
                case var vv when regexBraketclose.IsMatch(Msentence):
                    sentenceSharp = "}";
                    break;
                case var vv when regexComment.IsMatch(Msentence):
                    sentenceSharp = Msentence;
                    break;
                case var vv when regexNamespace.IsMatch(Msentence):
                    sentenceSharp = Regex.Replace(Msentence,@"" + MNamespace+"","namespace");
                    break;
                case var vv when regexClass.IsMatch(Msentence):
                    sentenceSharp = Regex.Replace(Msentence,@""+ MClass +"","class");
                    break;
                case var vv when regexStatic.IsMatch(Msentence):
                    sentenceSharp = Regex.Replace(Msentence, @"" + MStatic + @"\s" + MVoid + @"\s" + MMain + "$","static void Main()");
                    break;
                case var vv when regexDeclareVariable.IsMatch(Msentence):
  
                    if (Regex.IsMatch(Msentence, @"" + MInt + @"\s\w+$"))
                    {
                        sentenceSharp = Regex.Replace(Msentence, @"" + MInt + "", "int") + ";";
                    }
                    else
                    {
                        sentenceSharp = Regex.Replace(Msentence, @"" + MString + "", "string") + ";";
                    }
                    break;
                case var vv when regexIntAsig.IsMatch(Msentence):
                    sentenceSharp = Regex.Replace(Msentence, @"" + MInt + @"\(" + MReadline + @"\)", "int.Parse(Console.ReadLine)") + ";";
                    break;
                case var vv when regexStringAsig.IsMatch(Msentence):
                    sentenceSharp = Regex.Replace(Msentence, @"\'", "\"");
                    break;
                case var vv when regexOperationAsig.IsMatch(Msentence):
                    sentenceSharp = Msentence + ";";
                    break;
                case var vv when regexConsole.IsMatch(Msentence):
                    string r = Regex.Replace(Msentence, @"" + MConsole + @"\s\(\'", "Console.WriteLine(\"");
                    sentenceSharp = Regex.Replace(r, @"\'", "\"") + ";";
                    break;
                case var vv when regexSwitch.IsMatch(Msentence):
                    sentenceSharp = Regex.Replace(Msentence,@"" + MSwitch + "","switch ");
                    break;
                case var vv when regexCase.IsMatch(Msentence):
                    string c = Regex.Replace(Msentence, @"" + MCase + "", "case");
                    sentenceSharp = Regex.Replace(c,@"\'","\"");
                    Console.WriteLine("");
                    break;
                case var vv when regexDefault.IsMatch(Msentence):
                    sentenceSharp = Regex.Replace(Msentence,@"" + MDefault + "","default");
                    break;
                case var vv when regexBreak.IsMatch(Msentence):
                    sentenceSharp = Regex.Replace(Msentence,@"" +MBreak + "","break")+";";
                    break;
             //   case var vv when regexReadLineAsig.IsMatch(Msentence):
             //       sentenceSharp = Regex.Replace(Msentence, @"" + MInt + @"\(" + MReadline + @"\)", "int.Parse(Console.ReadLine)") + ";";
              //      break;
                case var vv when regexReadLine.IsMatch(Msentence):
                    sentenceSharp = Regex.Replace(Msentence,@"" + MReadline+ "", "Console.ReadLine();");
                    break;
                case var vv when rgexif.IsMatch(Msentence):
                    string rif = Regex.Replace(Msentence, @"" + MIf + "", "if (");
                    sentenceSharp = Regex.Replace(rif,@"\:",")");
                    break;
                case var vv when regexDo.IsMatch(Msentence):
                    sentenceSharp = Regex.Replace(Msentence,@"" + MDo+ "","do");
                    break;
                case var vv when regexWhile.IsMatch(Msentence):
                    sentenceSharp = Regex.Replace(Msentence,@"" + MWhile+ "","while ")+";";
                    break;
                default:
                    sentenceSharp = "//fallo de traduccion: " + Msentence;
                    break;
            }
            return sentenceSharp;
        }









    }
}
