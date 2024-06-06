namespace example;

using MySql.Data.MySqlClient;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Getting connection...");

        MySqlConnection conn = DBUtils.GetDBConnection();

        try
        {
            Console.WriteLine("Opening connection....");

            conn.Open();

            QueryEmployee(conn);
            Console.WriteLine("\n\n");
            QueryCities(conn);

            Console.WriteLine("Connection succesfull!");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }

    }

    private static void QueryEmployee(MySqlConnection conn)
    {
        string id, name, country;
        string sql = "select Code, Name, Continent from country;";

        MySqlCommand cmd = new MySqlCommand();

        cmd.Connection = conn;
        cmd.CommandText = sql;

        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    id = reader["Code"].ToString();
                    name = reader["Name"].ToString();
                    country = reader["Continent"].ToString();
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine($"Код: {id} Назва: {name} Континент: {country}");
                    Console.WriteLine("--------------------------------------");
                }
            }
        }
    }

    private static void QueryCities(MySqlConnection conn)
    {
        string? id, name, country;
        string sql = "select ID, Name, CountryCode from city;";

        MySqlCommand cmd = new MySqlCommand();

        cmd.Connection = conn;
        cmd.CommandText = sql;

        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    id = reader["ID"].ToString();
                    name = reader["Name"].ToString();
                    country = reader["CountryCode"].ToString();
                    Console.WriteLine($"Код: {id}\nНазва: {name}\nКод країни: {country}");
                    Console.WriteLine("--------------------------------------");
                }
            }
        }
    }
}