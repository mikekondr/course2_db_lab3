using MySql.Data.MySqlClient;

namespace lab3;

public partial class Ticket : Form
{
    private MySqlConnection conn;

    private int ticked_id;
    private FlightInfo flight_data;
    private TicketInfo ticket_data;

    public Ticket(MySqlConnection conn, int ticket_id = 0)
    {
        InitializeComponent();
        this.conn = conn;
        this.ticked_id = ticket_id;

        FillControls();

        comboFlight.SelectedValueChanged += this.ControlChanged_UpdateInfo;
        comboSeatType.SelectedValueChanged += this.ControlChanged;
        numCount.ValueChanged += this.ControlChanged;
        comboDiscount.SelectedValueChanged += this.ControlChanged;
        dateFlight.ValueChanged += this.ControlChanged_UpdateInfo;

        if (this.ticked_id != 0)
        {
            labelHead.Text = $"Квиток № {this.ticked_id}";
            FillTicket();
        }
        else
            ticket_data = new TicketInfo();
    }

    private void FillTicket()
    {
        ticket_data = DBUtils.GetTicket(conn, ticked_id);

        comboFlight.SelectedValue = ticket_data.flight_num;
        comboDiscount.SelectedValue = ticket_data.discount_type;
        comboSeatType.SelectedValue = ticket_data.seat_type;
        numCount.Value = ticket_data.count;
        dateFlight.Value = ticket_data.flight_date;
        dateTicket.Value = ticket_data.order_date;
    }

    private void FillControls()
    {
        comboFlight.ValueMember = "id";
        comboFlight.DisplayMember = "name";
        comboFlight.DataSource = DBUtils.GetFlights(conn);

        comboSeatType.DataSource = DBUtils.GetSeatTypes();
        comboSeatType.ValueMember = "id";
        comboSeatType.DisplayMember = "name";

        comboDiscount.DataSource = DBUtils.GetDiscounts(conn);
        comboDiscount.ValueMember = "id";
        comboDiscount.DisplayMember = "name";

        flight_data = DBUtils.GetFlightInfo(conn, (int)comboFlight.SelectedValue, dateFlight.Value);
    }

    private void ControlChanged_UpdateInfo(object sender, EventArgs e)
    {
        flight_data = DBUtils.GetFlightInfo(conn, (int)comboFlight.SelectedValue, dateFlight.Value);
        UpdateFlightInfo();
    }

    private void UpdateFlightInfo()
    {
        decimal price = 0;
        decimal discount = (decimal)((System.Data.DataRowView)comboDiscount.SelectedItem).Row["perc"];
        int count = (int)numCount.Value;
        if ((string)comboSeatType.SelectedValue == "B")
            price = flight_data.b_price;
        else if ((string)comboSeatType.SelectedValue == "P")
            price = flight_data.p_price;
        else
            price = flight_data.s_price;

        if (flight_data.middlePointsCount == 0)
            labelFlightInfo.Text = "";
        else if (flight_data.middlePointsCount == 1)
            labelFlightInfo.Text = $"через {flight_data.middlePointsCount} проміжну зупинку: {flight_data.middlePoints}";
        else
            labelFlightInfo.Text = $"через {flight_data.middlePointsCount} проміжні зупинки: {flight_data.middlePoints}";


        labelSeatPrice.Text = $"Вартість: {price}";


        if ((string)comboSeatType.SelectedValue == "B")
            labelSeatCount.Text = $"Вільно {flight_data.b_free} з {flight_data.b_count}";
        else if ((string)comboSeatType.SelectedValue == "P")
            labelSeatCount.Text = $"Вільно {flight_data.p_free} з {flight_data.p_count}";
        else
            labelSeatCount.Text = $"Вільно {flight_data.s_free} з {flight_data.s_count}";


        if (discount == 0)
            labelDiscount.Text = "";
        else
            labelDiscount.Text = $"Знижка {Math.Round(price * discount / 100, 2)}";


        if (flight_data.flight_days == "O" && dateFlight.Value.Day % 2 == 0
            || flight_data.flight_days == "E" && dateFlight.Value.Day % 2 == 1)

            labelDay.Text = $"Цей рейс вилітає тільки у {flight_data.days} дні!";
        else
            labelDay.Text = "";


        labelTotal.Text = $"Разом до сплати: {price * count - Math.Round(price * count * discount / 100, 2)}";
    }

    private void Ticket_Shown(object sender, EventArgs e)
    {
        UpdateFlightInfo();
    }

    private void ControlChanged(object sender, EventArgs e)
    {
        UpdateFlightInfo();
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        DBUtils.SaveTicket(conn, ticket_data);
    }
}