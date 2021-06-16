using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace IDEMewtow
{
    /// <summary>
    /// clase TokenMewtow crea la plantilla para evaluar las fases de un compilador 
    /// </summary>
    class TokenMewtow
    {
        /// <summary>
        /// Declaracion de variables privadas de la clase en donde se alamacen los tokens
        /// listToken: alamacen los tokes ya procesados con sus resultados.
        /// </summary>
        private string MToken;
        private int Mx;
        private int My;
        private string Mtype;
        private bool MLexico;
        private bool MSintactico;
        private bool MSemantico;
        public static List<TokenMewtow> ListToken = new List<TokenMewtow>();
        public static List<string[]> ListSentence = new List<string[]>();
        //identificadores que alamacenan los estados de las llaves
        public static int Mbracketopen = 0;
        public static int Mbracketclose = 0;

        /// <summary>
        /// TokenMewtow: metodo constructor sin parametros.
        /// </summary>
        public TokenMewtow()
        {
            MToken = string.Empty;
            Mx =0;
            My = 0;
            Mtype = string.Empty;
            MLexico = false;
            MSintactico = false;
            MSemantico = false;
        }

        /// <summary>
        /// constructor de la clase que acepta parametros.
        /// </summary>
        /// <param name="vToken"></param> recibe el token que se evalua
        /// <param name="vX"></param> recibe la posicion en x del token
        /// <param name="vY"></param> recibe la posicion o linea en que se encuentra el token
        /// <param name="vType"></param> recibe el tipo de token 
        /// <param name="vLexico"></param> recibe true si paso la gase lexico o false en caso de un error.
        /// <param name="vSintactico"></param> true si paso  la fase sintactico o false en caso de error.
        /// <param name="vSemantico"></param> true si paso la fase semantica o false en caso de error.
        public TokenMewtow(string vToken, int vX, int vY,string vType,bool vLexico, bool vSintactico,bool vSemantico)
        {
            MToken = vToken;
            Mx = vX;
            My = vY;
            Mtype = vType;
            MLexico = vLexico;
            MSintactico = vSintactico;
            MSemantico = vSemantico;
        }

        /// <summary>
        /// declaracion de get y set de los atributos de la clase.
        /// </summary>
        public String mToken
        {
            get { return MToken; }
            set { MToken = value; }
        }

        public int mx
        {
            get { return Mx; }
            set { Mx = value; }
        }

        public int my
        {
            get { return My; }
            set { My = value; }
        }

        public String mType
        {
            get { return Mtype; }
            set { Mtype = value; }
        }
        public bool mLexico
        {
            get { return MLexico; }
            set { MLexico = value; }
        }

        public bool mSintactico
        {
            get { return MSintactico; }
            set { MSintactico = value; }
        }

        public bool mSemantico
        {
            get { return MSemantico; }
            set { MSemantico = value; }
        }

        
        /// <summary>
        /// metodo compiler: se encarga de ejecutar todo el proceso de compilar el lenguaje.
        /// </summary>
        /// <param name="ContentFile"></param> recibe el string del archivo de texto.
        /// <returns></returns> retorna un true despues de haber culminado todo el proceso.
        public bool CompilerFile(string ContentFile)
        {
            ClearListToken();
            ClearListSentence();
            Grammar.clearListsemantic();
            Mbracketclose = 0;
            Mbracketopen = 0;
            int x = 0;
            int y = 0;
            string[] DataSent = CreateSentence(ContentFile);
            char[] delimiterChar = { ' ' };
     
            foreach(var mSent in DataSent)
            {
                x += 1;
                y += 1;

                var MSentence = mSent.Trim();
                Grammar gra = new Grammar();
                int result = gra.ValidSentence(MSentence, y);
                if (result == 1 || result == 0)
                {
                    string[] mSen = MSentence.Split(delimiterChar);
                    foreach (var mtok in mSen)
                    {
                        KeyWord kw = new KeyWord();
                        string typ = kw.IsValidToken(mtok);
                        TokenMewtow NewToken = new TokenMewtow(mtok, x, y, typ, true, false, false);
                        ListToken.Add(NewToken);
                        x += 1;
                    }

                }
                else if (result == 2)
                {
                    TokenMewtow TokenComment = new TokenMewtow(mSent, x, y, "comentario", true, false, false);
                    ListToken.Add(TokenComment);

                }else if (result == 3)
                {
                    TokenMewtow TokenComment = new TokenMewtow(mSent, x, y, "Delimitador", true, false, false);
                    ListToken.Add(TokenComment);
                    Mbracketopen += 1;
                }
                else if (result == 4)
                {
                    TokenMewtow TokenComment = new TokenMewtow(mSent, x, y, "Delimitador", true, false, false);
                    ListToken.Add(TokenComment);
                    Mbracketclose += 1;
                }

                x = 0;
            }

            int resultbracket = Mbracketopen - Mbracketclose;
            if (resultbracket > 0)
            {
                ErrorLog.AddError("-> Tienes (" + resultbracket +") Delimitador abierto");
            }else if(resultbracket < 0)
            {
                ErrorLog.AddError("-> Tienes (" + resultbracket * -1 + ") Delimitador cerrando");
            }

            Grammar.Semantic();
            return true;
        }

        /// <summary>
        /// metodo encargado de dividir el contenido del archivo en lineas de sentencias.
        /// </summary>
        /// <param name="Content">todo el contenido del archivo  a compilar</param>
        /// <returns>lineas de sentencia</returns>
        public string[] CreateSentence(string Content)
        {
            char[] delimiterChars = { '\n' };
            string[] mysentence = Content.Split(delimiterChars);

            return mysentence;
        }

        /// <summary>
        /// metodo encargado de enviar el listado de los tokens
        /// </summary>
        /// <returns>listado de tokens</returns>
        public List<TokenMewtow> GetTokens()
        {
            return ListToken;
        }

        /// <summary>
        /// metodo encargado de limpiar la lista de tokens procesesados.
        /// </summary>
        public void ClearListToken()
        {
            ListToken.Clear();
        }
        /// <summary>
        /// metodo encargado de limpiear la lista de setencias de tokens
        /// </summary>
        public void ClearListSentence()
        {
            ListSentence.Clear();
        }

        public static List<string[]> GetListSentence()
        {
            return ListSentence;
        }

        /// <summary>
        /// metodo encargado de agregar a la lista los tokens procesados....
        /// </summary>
        /// <param name="vgramar"> resultado el token procesado</param>
        public static void AddListSentence(string vgramar)
        {
            char[] delim = { ';' };
            string[] sentencetoken = vgramar.Split(delim);
            ListSentence.Add(sentencetoken);
        }

        /// <summary>
        /// metodo encargado de crear un arbol de los resultados de la fase sintactica.
        /// </summary>
        /// <returns> un nodo con sus subnodos creados.</returns>
        public static TreeNode GenerateTreeSyntactic()
        {
            TreeNode Nod = new TreeNode("Lenguaje Mewtow");
            foreach (var Wn in ListSentence)
            {
                TreeNode Nnond = new TreeNode(Wn[0] + "-" + Wn[1]);
                TreeNode Nond = new TreeNode(Wn[2]);
                Nnond.Nodes.Add(Nond);
                Nod.Nodes.Add(Nnond);   
            }

            return Nod;
        }

    }
    

}
