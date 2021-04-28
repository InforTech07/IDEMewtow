using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEMewtow
{
    public class Environment
    {
        public const string rootDir = @"C:\Users\Techp\source\repos\IDEMewtow\proyect\";
        public const string DirDB = @"C:\Users\Techp\source\repos\IDEMewtow\MewtowDB.s3db";
        public const string PathDB = "Data Source=C:\\Users\\Techp\\source\\repos\\IDEMewtow\\MewtowDB.s3db";
        public const string NameDB = "MewtowDB.s3db";
        public const string PathScriptSQL = @"C:\Users\Techp\source\repos\IDEMewtow\scriptdb.sql";
        
    }
    public class ScriptSQl
    {
        public const string SqlGetProyects = "SELECT * FROM proyecto";
        public const string SqlNewProyect = "INSERT INTO proyecto(NOMBRE)VALUES(@name)";
        public const string SqlGetProyectID = "SELECT NOMBRE FROM proyecto WHERE id = @id";
        public const string SqlCountProyects= "SELECT ID FROM proyecto WHERE ID = (SELECT MAX(id) FROM proyecto)";
    }
}
