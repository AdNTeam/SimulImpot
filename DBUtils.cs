using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulImpot
{
    class DBUtils
    {
        string database = "impot";
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string username = "root";
            string password = "";
            String database = "Impot";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
    }
}
