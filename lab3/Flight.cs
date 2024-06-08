using MySql.Data.MySqlClient;
using System.Data;

namespace lab3
{
    public partial class flight : Form
    {
        private MySqlConnection conn;
        private DataTable middle_points;

        private int flight_num;

        private void fillControlsData()
        {
            comboDeparture.DataSource = DBUtils.GetPoints(conn);
            comboDeparture.ValueMember = "id";
            comboDeparture.DisplayMember = "name";

            comboArrival.DataSource = DBUtils.GetPoints(conn);
            comboArrival.ValueMember = "id";
            comboArrival.DisplayMember = "name";


            comboDays.DataSource = DBUtils.GetFlightDays();
            comboDays.ValueMember = "id";
            comboDays.DisplayMember = "name";

            comboAvialiner.DataSource = DBUtils.GetAvialiners(conn);
            comboAvialiner.ValueMember = "id";
            comboAvialiner.DisplayMember = "name";

            listMiddlePoints.DataSource = DBUtils.GetPoints(conn);
            listMiddlePoints.ValueMember = "id";
            listMiddlePoints.DisplayMember = "name";
        }

        private void fillFlight()
        {
            (DataTable data, DataTable middle_points) = DBUtils.GetFlightData(conn, flight_num);
            if (data.Rows.Count > 0)
            {
                DataRow d = data.Rows[0];
                comboArrival.SelectedValue = d["flight_arrival"];
                comboDeparture.SelectedValue = d["flight_departure"];
                comboDays.SelectedValue = d["flight_days"];
                comboAvialiner.SelectedValue = d["avia_num"];
            }

            if (middle_points.Rows.Count > 0)
            {
                for (int i = 0; i < listMiddlePoints.Items.Count; i++)
                {
                    if (middle_points.Rows.Find(((System.Data.DataRowView)listMiddlePoints.Items[i]).Row["id"]) is not null)
                        listMiddlePoints.SetItemChecked(i, true);
                }
            }
        }

        public flight(MySqlConnection conn, int flight_num = 0)
        {
            InitializeComponent();

            this.conn = conn;
            this.fillControlsData();

            middle_points = new DataTable();
            middle_points.Columns.Add("id");
            middle_points.Columns.Add("name");
            this.flight_num = flight_num;
            if (flight_num != 0)
            {
                labelHead.Text = $"Рейс № {flight_num}";
                fillFlight();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < listMiddlePoints.Items.Count; i++)
            {
                if (listMiddlePoints.GetItemChecked(i))
                    list.Add((int)((System.Data.DataRowView)listMiddlePoints.Items[i]).Row["id"]);
            }

            DBUtils.SaveFlight(conn, comboAvialiner.SelectedValue.ToString(), comboDays.SelectedValue.ToString(),
                (int)comboDeparture.SelectedValue, (int)comboArrival.SelectedValue, list.ToArray(), flight_num);
        }
    }
}
