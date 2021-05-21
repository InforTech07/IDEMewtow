using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEMewtow
{
    class ScriptSQLdb
    {
        public const string SqlGetProyects = "SELECT ID, NOMBRE, FECHA_CREACION AS CREADO FROM PROYECTO";
        public const string SqlNewProyect = "INSERT INTO PROYECTO(NOMBRE)VALUES(@name)";
        public const string SqlInsertKeyWord = "INSERT INTO PALABRACLAVE(PALABRA,TIPO_PALABRA,PALABRACS)VALUES(@word,@typeword,@wordcs)";
        public const string SqlInsertGrammar = "INSERT INTO GRAMATICA(GRAMATICA_SENTENCIA,TIPO_GRAMATICA)VALUES(@grammar,@typegrammar)";
        public const string SqlGetProyectID = "SELECT NOMBRE FROM proyecto WHERE id = @id";
        public const string SqlCountProyects = "SELECT ID FROM proyecto WHERE ID = (SELECT MAX(id) FROM proyecto)";
        public const string SqlCountKeyWord = "SELECT COUNT(PALABRA) FROM PALABRACLAVE";
        public const string SqlCountGrammar = "SELECT COUNT(GRAMATICA_SENTENCIA) FROM GRAMATICA";
        public const string SqlDeleteKeyWord = "DELETE FROM PALABRACLAVE";
        public const string SqlDeleteGrammar = "DELETE FROM GRAMATICA";
        public const string SqlGetKeyWord = "SELECT PALABRA,TIPO_PALABRA,PALABRACS FROM PALABRACLAVE";
        public const string SqlGetGrammar = "SELECT GRAMATICA_SENTENCIA FROM GRAMATICA";
    }
}
