using MySql.Data.MySqlClient;

namespace example
{
    public class DBMysqlUtils
    {
        public static MySqlConnection GetDBConnection(
            string host,
            int port,
            string database,
            string username,
            string password)
        {
            string connString = $"Server={host};Database={database};Port={port};" +
                $"User Id={username};Password={password}";
            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }

    }
}