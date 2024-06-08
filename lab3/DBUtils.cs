using MySql.Data.MySqlClient;
using System.Data;

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

    ///
    /// Flights
    ///

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

    public static void RemoveFlight(MySqlConnection conn, int flight_num)
    {
        MySqlCommand cmd = new MySqlCommand(
            @"DELETE FROM flights
            WHERE flight_num = @flight_num;", conn);
        cmd.Parameters.AddWithValue("flight_num", flight_num);

        cmd.ExecuteNonQuery();
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
        foreach (int i in middle_points)
        {
            cmd.Parameters["point"].Value = i;

            cmd.ExecuteNonQuery();
        }
    }

    ///
    /// Tickets
    ///

    public static void RemoveTicket(MySqlConnection conn, int order_id)
    {
        MySqlCommand cmd = new MySqlCommand(
            @"DELETE FROM orders
            WHERE order_id = @order_id;", conn);
        cmd.Parameters.AddWithValue("order_id", order_id);

        cmd.ExecuteNonQuery();
    }

    public static void SaveTicket(MySqlConnection conn, TicketInfo ticket)
    {
        MySqlCommand cmd;

        if (ticket.order_id > 0)
        {
            cmd = new MySqlCommand(
                @"UPDATE orders
                SET
                    flight_num = @flight_num,
                    order_date = @order_date,
                    seat_type = @seat_type,
                    count = @count,
                    discount_type = @discount_type,
                    flight_date = @flight_date
                WHERE
                    order_id = @order_id", conn);
        }
        else
        {
            cmd = new MySqlCommand(
                @"INSERT INTO orders (flight_num, order_date, seat_type, count, discount_type, flight_date)
                VALUES (@flight_num, @order_date, @seat_type, @count, @discount_type, @flight_date)", conn);
        }

        cmd.Parameters.AddWithValue("flight_num", ticket.flight_num);
        cmd.Parameters.AddWithValue("order_date", ticket.order_date.Date);
        cmd.Parameters.AddWithValue("seat_type", ticket.seat_type);
        cmd.Parameters.AddWithValue("count", ticket.count);
        cmd.Parameters.AddWithValue("discount_type", ticket.discount_type == 0 ? null : ticket.discount_type);
        cmd.Parameters.AddWithValue("flight_date", ticket.flight_date.Date);
        cmd.ExecuteNonQuery();
    }

    public static TicketInfo GetTicket(MySqlConnection conn, int ticket_id)
    {
        return new TicketInfo(conn, ticket_id);
    }

    ///
    /// Get data
    /// 

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

    public static DataTable GetSeatTypes()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        dt.Columns.Add("name");

        DataRow dr = dt.NewRow();
        dr["id"] = "B";
        dr["name"] = "бізнес-клас";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = "P";
        dr["name"] = "І клас";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = "S";
        dr["name"] = "ІІ клас";
        dt.Rows.Add(dr);

        return dt;
    }

    public static DataTable GetDiscounts(MySqlConnection conn)
    {
        DataTable dt = new DataTable();

        MySqlDataAdapter adapter = new MySqlDataAdapter(
            @"SELECT
	            0 AS id,
                '-' AS name,
                0 AS perc

            UNION

            SELECT
	            disc_id AS id,
                CONCAT(disc_name, ' (-', disc_perc, '%)') AS name,
                disc_perc
            FROM discount_types;", conn);
        adapter.Fill(dt);

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

    public static DataTable GetFlights(MySqlConnection conn)
    {
        DataTable dt = new DataTable();

        MySqlDataAdapter adapter = new MySqlDataAdapter(
            @"SELECT
	            flight_num AS id,
	            CONCAT('№ ', flight_num,
		            ', ', pd.point_name, ' - ', pa.point_name
                ) AS name
            FROM flights AS f
            LEFT JOIN points AS pd ON pd.point_id = f.flight_departure
            LEFT JOIN points AS pa ON pa.point_id = f.flight_arrival", conn);
        adapter.Fill(dt);

        return dt;
    }

    public static FlightInfo GetFlightInfo(MySqlConnection conn, int flight_num, DateTime flight_date)
    {
        return new FlightInfo(conn, flight_num, flight_date);
    }
}

class FlightInfo
{
    public int flight_num { get; }
    public string avia_num { get; }
    public string avia_type { get; }
    public string avia_size { get; }
    public int total_count { get; }
    public int ordered { get; }
    public int b_count { get; }
    public int b_ordered { get; }
    public int b_free { get; }
    public decimal b_price { get; }
    public int p_count { get; }
    public int p_ordered { get; }
    public int p_free { get; }
    public decimal p_price { get; }
    public int s_count { get; }
    public int s_ordered { get; }
    public int s_free { get; }
    public decimal s_price { get; }
    public string flight_days { get; }
    public string days { get; }
    public int flight_departure { get; }
    public string departure { get; }
    public int flight_arrival { get; }
    public string arrival { get; }
    public int middlePointsCount { get; }
    public string middlePoints { get; }

    public FlightInfo(MySqlConnection conn, int flight_num, DateTime flight_date)
    {
        MySqlCommand cmd;
        MySqlDataReader reader;

        cmd = new MySqlCommand(
            @"SELECT
	            f.flight_num,
                f.avia_num,
                a.avia_type,
                CASE
		            WHEN a.avia_type = 'S' THEN 'маленький'
		            WHEN a.avia_type = 'M' THEN 'середній'
		            ELSE 'великий'
	            END AS avia_size,
                avia_business_count + avia_prime_count + avia_sec_count AS total_count,
                IFNULL(ordered_seats.ordered, 0) AS ordered,
                avia_business_count AS b_count,
                IFNULL(ordered_seats.b_ordered, 0) AS b_ordered,
                avia_business_count - IFNULL(ordered_seats.b_ordered, 0) AS b_free,
                avia_business_price AS b_price,
                avia_prime_count AS p_count,
                IFNULL(ordered_seats.p_ordered, 0) AS p_ordered,
                avia_prime_count - IFNULL(ordered_seats.p_ordered, 0) AS p_free,
                avia_prime_price AS p_price,
                avia_sec_count AS s_count,
                IFNULL(ordered_seats.s_ordered, 0) AS s_ordered,
                avia_sec_count - IFNULL(ordered_seats.s_ordered, 0) AS s_free,
                avia_sec_price AS s_price,
                f.flight_days,
                CASE
		            WHEN flight_days = 'E' THEN 'парні'
		            WHEN flight_days = 'O' THEN 'непарні'
		            ELSE 'щоденно'
	            END AS days,
                flight_departure,
                pd.point_name AS departure,
                flight_arrival,
                pa.point_name AS arrival,
                IFNULL(mp.middlePointsCount, 0) AS middlePointsCount,
                IFNULL(mp.middlePoints, '-') AS middlePoints
            FROM flights AS f
            LEFT JOIN points AS pd ON f.flight_departure = pd.point_id
            LEFT JOIN points AS pa ON f.flight_arrival = pa.point_id
            LEFT JOIN (
	            SELECT
		            flight_num,
		            COUNT(point) AS middlePointsCount,
                    GROUP_CONCAT(p.point_name SEPARATOR ', ') AS middlePoints
                FROM middle_points AS mp
                LEFT JOIN points AS p ON mp.point = p.point_id
                WHERE flight_num = @flight_num
            ) AS mp ON mp.flight_num = f.flight_num
            LEFT JOIN avialiners AS a ON a.avia_num = f.avia_num
            LEFT JOIN (
		            SELECT
			            flight_num,
                        SUM(count) AS ordered,
			            SUM(IF (seat_type = 'B', count, 0)) AS b_ordered,
			            SUM(IF (seat_type = 'P', count, 0)) AS p_ordered,
			            SUM(IF (seat_type = 'S', count, 0)) AS s_ordered
		            FROM orders
		            WHERE flight_num = @flight_num AND flight_date = @flight_date
            ) AS ordered_seats ON ordered_seats.flight_num = f.flight_num
            WHERE f.flight_num = @flight_num", conn);
        cmd.Parameters.AddWithValue("flight_num", flight_num);
        cmd.Parameters.AddWithValue("flight_date", flight_date.Date);

        reader = cmd.ExecuteReader();
        if (reader.HasRows && reader.Read())
        {
            this.flight_num = Convert.ToInt32(reader["flight_num"]);
            this.avia_num = Convert.ToString(reader["avia_num"]);
            this.avia_type = Convert.ToString(reader["avia_type"]);
            this.avia_size = Convert.ToString(reader["avia_size"]);
            this.total_count = Convert.ToInt32(reader["total_count"]);
            this.ordered = Convert.ToInt32(reader["ordered"]);
            this.b_count = Convert.ToInt32(reader["b_count"]);
            this.b_ordered = Convert.ToInt32(reader["b_ordered"]);
            this.b_free = Convert.ToInt32(reader["b_free"]);
            this.b_price = Convert.ToDecimal(reader["b_price"]);
            this.p_count = Convert.ToInt32(reader["p_count"]);
            this.p_ordered = Convert.ToInt32(reader["p_ordered"]);
            this.p_free = Convert.ToInt32(reader["p_free"]);
            this.p_price = Convert.ToDecimal(reader["p_price"]);
            this.s_count = Convert.ToInt32(reader["s_count"]);
            this.s_ordered = Convert.ToInt32(reader["s_ordered"]);
            this.s_free = Convert.ToInt32(reader["s_free"]);
            this.s_price = Convert.ToDecimal(reader["s_price"]);
            this.flight_days = Convert.ToString(reader["flight_days"]);
            this.days = Convert.ToString(reader["days"]);
            this.flight_departure = Convert.ToInt32(reader["flight_departure"]);
            this.departure = Convert.ToString(reader["departure"]);
            this.flight_arrival = Convert.ToInt32(reader["flight_arrival"]);
            this.arrival = Convert.ToString(reader["arrival"]);
            this.middlePointsCount = Convert.ToInt32(reader["middlePointsCount"]);
            this.middlePoints = Convert.ToString(reader["middlePoints"]);
        }

        reader.Close();
    }
}

class TicketInfo
{
    public int order_id { get; }
    public int flight_num { get; set; }
    public DateTime order_date { get; set; }
    public string seat_type { get; set; }
    public int count { get; set; }
    public int? discount_type { get; set; }
    public DateTime flight_date { get; set; }


    public TicketInfo()
    { }

    public TicketInfo(MySqlConnection conn, int ticket_id)
    {
        MySqlCommand cmd = new MySqlCommand(
            @"SELECT * FROM orders WHERE order_id = @order_id;", conn);
        cmd.Parameters.AddWithValue("order_id", ticket_id);

        MySqlDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows &&  reader.Read())
        {
            this.order_id = ticket_id;
            this.flight_num = Convert.ToInt32(reader["flight_num"]);
            this.order_date = Convert.ToDateTime(reader["order_date"]);
            this.seat_type = Convert.ToString(reader["seat_type"]);
            this.count = Convert.ToInt32(reader["count"]);
            this.discount_type = Convert.ToInt32(reader["discount_type"] is System.DBNull ? 0 : reader["discount_type"]);
            this.flight_date = Convert.ToDateTime(reader["flight_date"]);
        }

        reader.Close();
    }
}