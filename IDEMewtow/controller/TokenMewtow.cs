﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

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
        List<KeyWord> MyKeyWord = new List<KeyWord>();
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

        List<TokenMewtow> ListToken = new List<TokenMewtow>();

        public void LexiconPhase(string ContentFile)
        {
            int x = 0;
            int y = 0;
            string[] DataSent = CreateSentence(ContentFile);
            char[] delimiterChars = { ' ' };
          //  List<TokenMewtow> ListToken = new List<TokenMewtow>();

            foreach(var mSent in DataSent)
            {
                x += 1;
                y += 1;
                string[] mSen = mSent.Split(delimiterChars);
                foreach(var mtok in mSen)
                {
                    KeyWord kw = new KeyWord();
                    string typ = kw.IsValidToken(mtok);
                    
                    TokenMewtow NewToken = new TokenMewtow(mtok, x, y, typ, true, false, false);
                    ListToken.Add(NewToken);
                    x += 1;
                }
                x = 0;
            }
            
        }

        


        public string[] CreateSentence(string Content)
        {
            char[] delimiterChars = { '\n' };
            string[] mysentence = Content.Split(delimiterChars);
            return mysentence;
        }


        public List<TokenMewtow> GetLexicon()
        {
            return ListToken;
        }


        public void SynthacticPhase()
        {
            foreach(var Tk in ListToken)
            {
                string t = Tk.MToken.ToString();
                IsValid(t);
            }
            
            
           
        }

        public void IsValid(string v)
        {
            foreach(var ky in MyKeyWord)
            {
                
            }

        }



    }
    

}
