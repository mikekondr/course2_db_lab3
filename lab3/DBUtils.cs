using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System.Data;
using System.Security.Cryptography;
using System.Security.Policy;

namespace lab3;

internal class DBUtils
{
    public static MySqlConnection GetDBConnection()
    {
        string host = "10.211.55.2";
        int port = 3306;
        string database = "avia_sales";
        string username = "avia";
        string password = "aviapass";

        string connString = $"Server={host};Database={database};Port={port};" +
            $"User Id={username};Password={password}";

        return new MySqlConnection(connString);
    }

    public static void FillTables(MySqlConnection conn, out DataTable shedule, out DataTable tickets)
    {
        MySqlDataAdapter adapter;
        //shedule
        adapter = new MySqlDataAdapter(
            @"SELECT flight_num AS 'Номер рейсу',
                   avia_num AS 'Бортовий номер',
                   CASE
                       WHEN avia_type = 'S' THEN 'маленький'
                       WHEN avia_type = 'M' THEN 'середній'
                       ELSE 'великий'
                   END AS 'Тип літака',
                   avia_business_count + avia_prime_count + avia_sec_count AS 'Кількість місць',
                   pd.point_name AS 'Відправлення',
                   pa.point_name AS 'Прибуття',
                   CASE
                       WHEN flight_days = 'E' THEN 'парні'
                       WHEN flight_days = 'O' THEN 'непарні'
                       ELSE 'щоденно'
                   END AS 'Дні вильоту',
                   ifnull(mp.points, '-') AS 'Зупинки'
            FROM flights AS f
            LEFT JOIN points AS pd ON f.flight_departure = pd.point_id
            LEFT JOIN points AS pa ON f.flight_arrival = pa.point_id
            LEFT JOIN (
               SELECT flight_num,
                       group_concat(p.point_name SEPARATOR ', ') AS points
               FROM middle_points AS mp
               LEFT JOIN points AS p ON mp.point = p.point_id
               GROUP BY flight_num
            ) AS mp USING(flight_num)
            LEFT JOIN avialiners USING (avia_num)", conn);

        shedule = new DataTable();
        adapter.Fill(shedule);

        //tickets
        adapter = new MySqlDataAdapter(
            @"SELECT order_id AS 'Замовлення',
                order_date AS 'Дата замовлення',
                flight_num AS 'Номер рейсу',
                flight_date AS 'Дата вильоту',
                pd.point_name AS 'Відправлення',
                pa.point_name AS 'Прибуття',
                CASE
	                WHEN seat_type = 'B' THEN 'Бізнес'
	                WHEN seat_type = 'P' THEN 'Перший'
	                ELSE 'Другий'
                END AS 'Клас',
                count AS 'Кількість місць',
                CASE
	                WHEN seat_type = 'B' THEN a.avia_business_price
	                WHEN seat_type = 'P' THEN a.avia_prime_price
	                ELSE a.avia_sec_price
                END AS 'Ціна',
                ifnull(dt.disc_name, '-') AS 'Категорія знижки',
                ifnull(dt.disc_perc, 0) AS 'Знижка, %',
                ROUND(count * CASE
		            WHEN seat_type = 'B' THEN a.avia_business_price
		            WHEN seat_type = 'P' THEN a.avia_prime_price
		            ELSE a.avia_sec_price
    	            END * (100 - ifnull(dt.disc_perc, 0) ) / 100 , 2) AS 'Сума зі знижкою'
            FROM orders as o
	            LEFT JOIN flights AS f USING(flight_num)
	            LEFT JOIN points AS pd ON f.flight_departure = pd.point_id
	            LEFT JOIN points AS pa ON f.flight_arrival = pa.point_id
	            LEFT JOIN avialiners AS a ON f.avia_num = a.avia_num
	            LEFT JOIN discount_types AS dt ON o.discount_type = dt.disc_id
            ", conn);

        tickets = new DataTable();
        adapter.Fill(tickets);
    }

    public static int CheckTicketsCount(MySqlConnection conn, int flight_num)
    {
        int result = 0;

        MySqlCommand cmd = new MySqlCommand(
            @"SELECT count(order_id) as count
            FROM orders
            WHERE flight_num = @flight_num", conn);
        cmd.Parameters.AddWithValue("flight_num", flight_num);

        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            if (reader.HasRows && reader.Read())
            {
                result = reader.GetInt32(0);
            }
        }

        return result;
    }

    public static void RemoveFlight(MySqlConnection conn, int flight_num)
    {
        MySqlCommand cmd = new MySqlCommand(
            @"DELETE FROM flights
            WHERE flight_num = @flight_num;", conn);
        cmd.Parameters.AddWithValue("flight_num", flight_num);

        cmd.ExecuteNonQuery();
    }

    public static DataTable GetFlightDays()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        dt.Columns.Add("name");

        DataRow dr = dt.NewRow();
        dr["id"] = "A";
        dr["name"] = "щодня";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = "E";
        dr["name"] = "парні";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = "O";
        dr["name"] = "непарні";
        dt.Rows.Add(dr);

        return dt;
    }

    public static DataTable GetPoints(MySqlConnection conn)
    {
        DataTable dt = new DataTable();

        MySqlDataAdapter adapter = new MySqlDataAdapter(
            @"SELECT point_id AS id, point_name AS name
                FROM points", conn);
        adapter.Fill(dt);
        dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };

        return dt;
    }

    public static DataTable GetAvialiners(MySqlConnection conn)
    {
        DataTable dt = new DataTable();

        MySqlDataAdapter adapter = new MySqlDataAdapter(
            @"SELECT
	            avia_num AS id,
	            CONCAT(
	               avia_num,
                   ' (',
                   CASE
		               WHEN avia_type = 'S' THEN 'маленький'
		               WHEN avia_type = 'M' THEN 'середній'
		               ELSE 'великий'
	               END, ', ', 
                   avia_business_count + avia_prime_count + avia_sec_count,
                   ' місць)') as name
            FROM avialiners", conn);
        adapter.Fill(dt);

        return dt;
    }

    public static (DataTable data, DataTable middle_points) GetFlightData(MySqlConnection conn, int flight_num)
    {
        DataTable dt = new DataTable();
        DataTable po = new DataTable();

        MySqlCommand cmd = new MySqlCommand(
            @"SELECT * FROM flights WHERE flight_num = @flight_num", conn);
        cmd.Parameters.AddWithValue("flight_num", flight_num);

        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

        adapter.Fill(dt);

        cmd.CommandText =
            @"SELECT point FROM middle_points WHERE flight_num = @flight_num";
        adapter = new MySqlDataAdapter(cmd);
        adapter.Fill(po);
        po.PrimaryKey = new DataColumn[] { po.Columns["point"] };

        return (data: dt, middle_points: po);
    }

    public static void SaveFlight(MySqlConnection conn,
        string avia_num, string flight_days,
        int flight_departure, int flight_arrival,
        int[] middle_points, int flight_num = 0)
    {
        MySqlCommand cmd;

        if (flight_num > 0)
        {
            cmd = new MySqlCommand(
                @"UPDATE flights
                SET
                    avia_num = @avia_num,
                    flight_days = @flight_days,
                    flight_departure = @flight_departure,
                    flight_arrival = @flight_arrival
                WHERE
                    flight_num = @flight_num", conn);
        }
        else
        {
            cmd = new MySqlCommand(
                @"INSERT INTO flights (avia_num, flight_days, flight_departure, flight_arrival)
                VALUES (@avia_num, @flight_days, @flight_departure, @flight_arrival)", conn);
        }

        cmd.Parameters.AddWithValue("flight_num", flight_num);
        cmd.Parameters.AddWithValue("avia_num", avia_num);
        cmd.Parameters.AddWithValue("flight_days", flight_days);
        cmd.Parameters.AddWithValue("flight_departure", flight_departure);
        cmd.Parameters.AddWithValue("flight_arrival", flight_arrival);

        cmd.ExecuteNonQuery();

        if (flight_num == 0)
            flight_num = (int)cmd.LastInsertedId;

        cmd = new MySqlCommand(
            @"delete from middle_points
            where flight_num = @flight_num", conn);
        cmd.Parameters.AddWithValue("flight_num", flight_num);
        cmd.ExecuteNonQuery();

        cmd = new MySqlCommand(
            @"INSERT INTO middle_points (flight_num, point)
            VALUES (@flight_num, @point)", conn);
        cmd.Parameters.AddWithValue("flight_num", flight_num);
        cmd.Parameters.Add("point", MySqlDbType.Int32);
        foreach(int i in middle_points)
        {
            cmd.Parameters["point"].Value = i;

            cmd.ExecuteNonQuery();
        }
    }
}