using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace IDEMewtow
{
    public class RequestDB
    {
        private const string SqlGetProyects = ScriptSQLdb.SqlGetProyects;
        private const string SqlNewProyect = ScriptSQLdb.SqlNewProyect;
        private const string SqlGetProyectId = ScriptSQLdb.SqlGetProyectID;
        private const string SqlCountProyects = ScriptSQLdb.SqlCountProyects;
        private const string SqlNewWord = ScriptSQLdb.SqlInsertKeyWord;
        private const string SqlCountWord = ScriptSQLdb.SqlCountKeyWord;
        private const string SqlNewGrammar = ScriptSQLdb.SqlInsertGrammar;
        private const string SqlCountGrammar = ScriptSQLdb.SqlCountGrammar;
        private const string SqlDeleteKeyword = ScriptSQLdb.SqlDeleteKeyWord;
        private const string SqlDeleteGrammar = ScriptSQLdb.SqlDeleteGrammar;
        private const string SqlGetKeyWords = ScriptSQLdb.SqlGetKeyWord;


        public static  DataTable GetProyects()
        {
            DataTable dt = new DataTable();
            SQLiteDataAdapter da = new SQLiteDataAdapter(SqlGetProyects, ConnectionDB.instanceDB());
            da.Fill(dt);
            return dt;
        }

        public static void CreateNewProyect(string nameproyect,string namefile)
        {
            SQLiteCommand cmd = new SQLiteCommand(SqlNewProyect, ConnectionDB.instanceDB());
            cmd.Parameters.Add(new SQLiteParameter("@name", nameproyect));
            cmd.ExecuteNonQuery();
        }

        public static DataSet GetProyectId(int id)
        {
            SQLiteCommand ProyectId = new SQLiteCommand(SqlGetProyectId, ConnectionDB.instanceDB());
            ProyectId.Parameters.Add(new SQLiteParameter("@id", id));
            ProyectId.ExecuteNonQuery();
            DataSet dataproyect = new DataSet();
            SQLiteDataAdapter proyectdata = new SQLiteDataAdapter(ProyectId);
            proyectdata.Fill(dataproyect);
            return dataproyect;
        }


        private static DataSet GetProyect(string sql)
        {
            SQLiteCommand LastProyect = new SQLiteCommand(sql, ConnectionDB.instanceDB());
            LastProyect.ExecuteScalar();
            DataSet dt = new DataSet();
            SQLiteDataAdapter ad = new SQLiteDataAdapter(LastProyect);
            ad.Fill(dt);
            return dt;
        }

        public static int GetLastId()
        {
            var Data = GetProyect(SqlCountProyects);
            var LastId = Convert.ToInt32(Data.Tables[0].Rows[0][0]);
            return LastId;
        }

        public static void NewKeyWord(string vword,string vtype,string vwordcs)
        {
            SQLiteCommand cmd = new SQLiteCommand(SqlNewWord, ConnectionDB.instanceDB());
            cmd.Parameters.Add(new SQLiteParameter("@word",vword));
            cmd.Parameters.Add(new SQLiteParameter("@typeword",vtype));
            cmd.Parameters.Add(new SQLiteParameter("@wordcs", vwordcs));
            cmd.ExecuteNonQuery();
        }

        public static int GetCountKeyWord()
        {
            SQLiteCommand CountKeyWord = new SQLiteCommand(SqlCountWord, ConnectionDB.instanceDB());
            CountKeyWord.ExecuteScalar();
            DataSet dskw = new DataSet();
            SQLiteDataAdapter dsw = new SQLiteDataAdapter(CountKeyWord);
            dsw.Fill(dskw);
            int num = Convert.ToInt32(dskw.Tables[0].Rows[0][0]);
            return num;
        }

        public static void NewGrammar(string vgrammar, string vtype)
        {
            SQLiteCommand cmd = new SQLiteCommand(SqlNewGrammar, ConnectionDB.instanceDB());
            cmd.Parameters.Add(new SQLiteParameter("@grammar", vgrammar));
            cmd.Parameters.Add(new SQLiteParameter("@typegrammar", vtype));
            cmd.ExecuteNonQuery();
        }

        public static int GetCountGrammar()
        {
            SQLiteCommand CountGrammar = new SQLiteCommand(SqlCountGrammar, ConnectionDB.instanceDB());
            CountGrammar.ExecuteScalar();
            DataSet dg = new DataSet();
            SQLiteDataAdapter dgr = new SQLiteDataAdapter(CountGrammar);
            dgr.Fill(dg);
            int num = Convert.ToInt32(dg.Tables[0].Rows[0][0]);
            return num;
        }


        public static void DeleteKeyWord()
        {
            SQLiteCommand DeleteKeyWord = new SQLiteCommand(SqlDeleteKeyword, ConnectionDB.instanceDB());
            DeleteKeyWord.ExecuteNonQuery();

        }

        public static void DeleteGrammar()
        {
            SQLiteCommand DeleteGrammar = new SQLiteCommand(SqlDeleteGrammar, ConnectionDB.instanceDB());
            DeleteGrammar.ExecuteNonQuery();

        }


        public static DataSet GetKeyWords()
        {
            DataSet dg = new DataSet();
            SQLiteDataAdapter dgr = new SQLiteDataAdapter(SqlGetKeyWords, ConnectionDB.instanceDB());
            dgr.Fill(dg);
            return dg;

        }


    }
}
