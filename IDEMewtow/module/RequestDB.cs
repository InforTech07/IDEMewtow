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
        private const string SqlGetProyects = ScriptSQl.SqlGetProyects;
        private const string SqlNewProyect = ScriptSQl.SqlNewProyect;
        private const string SqlGetProyectId = ScriptSQl.SqlGetProyectID;
        private const string SqlCountProyects = ScriptSQl.SqlCountProyects;
        
        public static  DataTable GetProyects()
        {
            DataTable dt = new DataTable();
            SQLiteDataAdapter da = new SQLiteDataAdapter(SqlGetProyects, ConnectionDB.instanceDB());
            da.Fill(dt);
            return dt;
        }

        public static void CreateNewProyect(string nameproyect)
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

    }
}
