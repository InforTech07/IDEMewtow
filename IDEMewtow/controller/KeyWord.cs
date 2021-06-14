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
    
    class KeyWord
    {
        
        private string Mkeyword;
        private string Mtype;
        private string MwordCs;
        public static List<KeyWord> KeyWordList = new List<KeyWord>();
        public static List<string> ListVariable = new List<string>();

        public KeyWord()
        {
            Mkeyword = string.Empty;
            Mtype = string.Empty;
            MwordCs = string.Empty;
        }

        public KeyWord(string vkeyword, string vtype,string vwordcs)
        {
            Mkeyword = vkeyword;
            Mtype = vtype;
            MwordCs = vwordcs;
        }
        public String mkeyword
        {
            get { return Mkeyword; }
            set { Mkeyword = value; }
        }
        public String mtype
        {
            get { return Mtype; }
            set { Mtype = value; }
        }
        public String mwordCs
        {
            get { return MwordCs; }
            set { MwordCs = value; }
        }

        public static bool LoadKeyword()
        {
            //char[] delimiterChars = { ' ', ',', '.', ':', '(', ')', '{', '}', '\t', '\n' };
            char[] delimiterSentece = { '\n' };
            char[] delimiterWord = { '|' };
            string PathFile = Helpers.OpenFile();
            string ContentFile = Helpers.ReadFile(PathFile);
            string[] ArrayKeywords = ContentFile.Split(delimiterSentece);


            foreach (var w in ArrayKeywords)
            {
                string[] kw = w.Split(delimiterWord);
                RequestDB.NewKeyWord(kw[0], kw[1], kw[2]);
           }

            MessageBox.Show(ContentFile, "File Content at path: " + PathFile, MessageBoxButtons.OK);
            return true;
        }

        
        public void CreateKeyWords()
        {
             var DataKeyWords = RequestDB.GetKeyWords();

            KeyWordList = DataKeyWords.Tables[0].AsEnumerable().Select(
                            dataRow => new KeyWord
                            {
                                Mkeyword = dataRow.Field<string>("palabra"),
                                Mtype = dataRow.Field<string>("tipo_palabra"),
                                MwordCs = dataRow.Field<string>("palabracs"),
                            }).ToList();
        }
      
        public string IsValidToken(string word)
        {
            Console.WriteLine("ejecutantdo.. Is Valid");
            var typeword =string.Empty;
            string keywordexpresion = string.Empty;
            foreach(KeyWord t in KeyWordList)
            {
                Console.WriteLine("linea...codigo");
                Console.WriteLine(t.Mkeyword.ToString());
                keywordexpresion = t.Mkeyword.ToString();
                
                Regex rgexKeyWord = new Regex(@"^" + keywordexpresion + "");
                Regex rgexVariable= new Regex(@"\b[a-zA-z]\w+");
                Regex rgexNumber = new Regex(@"\d");
                Regex rgexSpace = new Regex(@"\s");
                Regex rgexString = new Regex(@"[""][\w\s]*[""]");
                Regex rgexOperator = new Regex(@"([+]|[-]|[\/]|[*])");
                Regex rgexAsignment = new Regex(@":=");
                //  Regex rgexAssignament = new Regex(@":=");

                if (rgexKeyWord.IsMatch(word.Trim()))
                {
                    typeword=t.Mtype.ToString();
                    break;
                }
                else
                {
                    switch (word)
                    {
                        case var v when rgexNumber.IsMatch(word.Trim()):
                            typeword = "numero";                            
                            break;
                        case var v when rgexOperator.IsMatch(word.Trim()):
                            typeword = "operador";
                            break;
                      case var v when rgexVariable.IsMatch(word.Trim()):
                          typeword = "variable";
                            ListVariable.Add(word.Trim());
                          break;
                        case var v when rgexAsignment.IsMatch(word.Trim()):
                            typeword = "asignacion";
                            break;
                        case var v when rgexSpace.IsMatch(word.Trim()):
                            typeword = "espacio";
                            break;
                        default:
                            typeword = "nodefindo";
                            break;
                    }
                }
               
            }

            return typeword;
        }

        public static List<KeyWord> GetKeywordList()
        {
            return KeyWordList;
        }


    }
}
