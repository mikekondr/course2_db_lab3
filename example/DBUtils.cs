using MySql.Data.MySqlClient;

namespace example
{
    public class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "world";
            string username = "root";
            string password = "RootKit5";

            return DBMysqlUtils.GetDBConnection(host, port, database, username, password);
        }
    }
}