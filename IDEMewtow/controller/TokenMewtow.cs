using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;

namespace IDEMewtow
{
    class TokenMewtow
    {
        private string MToken;
        private int Mx;
        private int My;
        private string Mtype;
        private bool MLexico;
        private bool MSintactico;
        private bool MSemantico;
        public static List<TokenMewtow> ListToken = new List<TokenMewtow>();
        public static List<string> ListSentence = new List<string>();
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

        

        public bool CompilerFile(string ContentFile)
        {
            ClearListToken();
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
                if (result == 1)
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

                }

                x = 0;

                
            }

            return true;
        }

        public string[] CreateSentence(string Content)
        {
            char[] delimiterChars = { '\n' };
            string[] mysentence = Content.Split(delimiterChars);

            return mysentence;
        }

        public List<TokenMewtow> GetTokens()
        {
            return ListToken;
        }

        public void ClearListToken()
        {
            ListToken.Clear();
        }
       

    }
    

}
