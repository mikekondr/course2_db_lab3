using MySql.Data.MySqlClient;

namespace example
{
    public class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "10.211.55.2";
            int port = 3306;
            string database = "world";
            string username = "root";
            string password = "RootKit5";

            return DBMysqlUtils.GetDBConnection(host, port, database, username, password);
        }
    }
}