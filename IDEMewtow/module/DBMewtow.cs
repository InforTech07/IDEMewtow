using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;

namespace IDEMewtow
{
    
    class DBMewtow
    {
        private const string DBName = Environment.NameDB;
        private const string pathDb = Environment.DirDB;
        private const string SQLScript = Environment.PathScriptSQL;
        private static bool IsDbRecentlyCreated = false;

        public static void CreateDataBase()
        {
            // Crea la base de datos 
            if (!File.Exists(pathDb))
            {
                SQLiteConnection.CreateFile(DBName);
                IsDbRecentlyCreated = true;
            }
            using (var ctx = ConnectionDB.instanceDB())
            {
                if (IsDbRecentlyCreated)
                {
                    using (var reader = new StreamReader(Path.GetFullPath(SQLScript)))
                    {
                        Console.WriteLine(reader);
                        var query = "";
                        var line = "";
                        while ((line = reader.ReadLine()) != null)
                        {
                            query += line;
                        }

                        using (var command = new SQLiteCommand(query, ctx))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }

    class ConnectionDB
    {
        private static ConnectionInstance instance = null;

        public static SQLiteConnection instanceDB()
        {
            if (instance == null)
            {
                instance = new ConnectionInstance();
            }
            return instance.conn;
        }
    }

    class ConnectionInstance
    {
        public SQLiteConnection conn = null;

        public ConnectionInstance()
        {
            conn = new SQLiteConnection(Environment.PathDB);
            conn.Open();
        }
        //  ~ConnectionInstance()
        //  {
        //      conn.Close();
        //  }

    }

}
