using MySql.Data.MySqlClient;
using System.Data;
using System.Runtime.CompilerServices;

namespace lab3;

public partial class Main : Form
{
    private MySqlConnection? conn;

    private DataTable shedule;
    private DataTable tickets;

    public Main()
    {
        InitializeComponent();
    }

    private void RefreshTables()
    {
        if (conn is not null && conn.State != ConnectionState.Closed)
        {
            DBUtils.FillTables(conn, out shedule, out tickets);

            dgvShedule.DataSource = shedule;
            dgvTickets.DataSource = tickets;
        }
    }

    private void Form1_Shown(object sender, EventArgs e)
    {
        conn = DBUtils.GetDBConnection();
        try
        {
            conn.Open();
            RefreshTables();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Помилка підключення до бази даних!\n\n" + ex.Message, "ПОМИЛКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }
    }

    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
        if (conn is not null && conn.State == System.Data.ConnectionState.Open)
        {
            conn.Close();
            conn.Dispose();
        }
    }

    private void addSheduleMenuItem_Click(object sender, EventArgs e)
    {
        Form frm = new flight(conn);
        if (frm.ShowDialog() == DialogResult.OK)
            RefreshTables();
    }

    private void removeSheduleMenuItem_Click(object sender, EventArgs e)
    {
        int flight_num = 0, ticket_count = 0;
        List<int> for_remove = new List<int>();
        List<int> for_check = new List<int>();

        List<DataGridViewRow> rows = new List<DataGridViewRow>();

        if (dgvShedule.SelectedRows.Count > 0)
            foreach (DataGridViewRow row in dgvShedule.SelectedRows)
                rows.Add(row);
        else if (dgvShedule.SelectedCells.Count > 0)
            foreach (DataGridViewCell cell in dgvShedule.SelectedCells)
                if (!rows.Contains(cell.OwningRow))
                    rows.Add(cell.OwningRow);

        if (rows.Count() > 0)
        {
            foreach (DataGridViewRow row in rows)
            {
                flight_num = (int)row.Cells[0].Value;
                int count = DBUtils.CheckTicketsCount(conn, flight_num);
                if (count > 0)
                    for_check.Add(flight_num);
                else
                    for_remove.Add(flight_num);
                ticket_count += count;
            }
        }
        else
            return;

        DialogResult answer = DialogResult.Yes;
        if (ticket_count > 0)
            answer = MessageBox.Show($"Для обраних рейсів ({for_check.Count()} із {for_remove.Count() + for_check.Count()} шт) вже є придбані квитки ({ticket_count} шт)\n\nВсеодно видалити?",
                "УВАГА!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        else
            answer = MessageBox.Show($"Обрані рейси ({for_remove.Count()} шт) буде остаточно видалено.\n\nПродовжити?",
                "УВАГА!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

        if (answer == DialogResult.Cancel)
            return;

        foreach (int i in for_remove)
            DBUtils.RemoveFlight(conn, i);

        if (answer == DialogResult.Yes)
            foreach (int i in for_check)
                DBUtils.RemoveFlight(conn, i);

        RefreshTables();
    }

    private void dgvShedule_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        int flight_num = (int)dgvShedule.CurrentCell.OwningRow.Cells[0].Value;
        Form frm = new flight(conn, flight_num);
        if (frm.ShowDialog() == DialogResult.OK)
            RefreshTables();
    }
}
