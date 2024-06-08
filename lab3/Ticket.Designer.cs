namespace lab3
{
    partial class Ticket
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboFlight = new ComboBox();
            dateTicket = new DateTimePicker();
            comboSeatType = new ComboBox();
            numCount = new NumericUpDown();
            comboDiscount = new ComboBox();
            dateFlight = new DateTimePicker();
            btnOk = new Button();
            btnCancel = new Button();
            labelFlightInfo = new Label();
            labelDay = new Label();
            labelSeatPrice = new Label();
            labelSeatCount = new Label();
            labelDiscount = new Label();
            labelTotal = new Label();
            labelHead = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)numCount).BeginInit();
            SuspendLayout();
            // 
            // comboFlight
            // 
            comboFlight.FormattingEnabled = true;
            comboFlight.Location = new Point(155, 48);
            comboFlight.Name = "comboFlight";
            comboFlight.Size = new Size(249, 23);
            comboFlight.TabIndex = 0;
            // 
            // dateTicket
            // 
            dateTicket.Location = new Point(155, 268);
            dateTicket.Name = "dateTicket";
            dateTicket.Size = new Size(249, 23);
            dateTicket.TabIndex = 5;
            // 
            // comboSeatType
            // 
            comboSeatType.FormattingEnabled = true;
            comboSeatType.Location = new Point(155, 92);
            comboSeatType.Name = "comboSeatType";
            comboSeatType.Size = new Size(249, 23);
            comboSeatType.TabIndex = 1;
            // 
            // numCount
            // 
            numCount.Location = new Point(155, 136);
            numCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCount.Name = "numCount";
            numCount.Size = new Size(248, 23);
            numCount.TabIndex = 2;
            numCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // comboDiscount
            // 
            comboDiscount.FormattingEnabled = true;
            comboDiscount.Location = new Point(155, 180);
            comboDiscount.Name = "comboDiscount";
            comboDiscount.Size = new Size(249, 23);
            comboDiscount.TabIndex = 3;
            // 
            // dateFlight
            // 
            dateFlight.Location = new Point(155, 224);
            dateFlight.Name = "dateFlight";
            dateFlight.Size = new Size(250, 23);
            dateFlight.TabIndex = 4;
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(110, 345);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 6;
            btnOk.Text = "Зберегти";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(214, 345);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Скасувати";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // labelFlightInfo
            // 
            labelFlightInfo.AutoSize = true;
            labelFlightInfo.Location = new Point(156, 74);
            labelFlightInfo.Name = "labelFlightInfo";
            labelFlightInfo.Size = new Size(38, 15);
            labelFlightInfo.TabIndex = 8;
            labelFlightInfo.Text = "label1";
            // 
            // labelDay
            // 
            labelDay.AutoSize = true;
            labelDay.ForeColor = Color.Red;
            labelDay.Location = new Point(156, 250);
            labelDay.Name = "labelDay";
            labelDay.Size = new Size(38, 15);
            labelDay.TabIndex = 9;
            labelDay.Text = "label1";
            // 
            // labelSeatPrice
            // 
            labelSeatPrice.AutoSize = true;
            labelSeatPrice.Location = new Point(156, 118);
            labelSeatPrice.Name = "labelSeatPrice";
            labelSeatPrice.Size = new Size(38, 15);
            labelSeatPrice.TabIndex = 10;
            labelSeatPrice.Text = "label1";
            // 
            // labelSeatCount
            // 
            labelSeatCount.AutoSize = true;
            labelSeatCount.Location = new Point(155, 162);
            labelSeatCount.Name = "labelSeatCount";
            labelSeatCount.Size = new Size(38, 15);
            labelSeatCount.TabIndex = 11;
            labelSeatCount.Text = "label1";
            // 
            // labelDiscount
            // 
            labelDiscount.AutoSize = true;
            labelDiscount.Location = new Point(156, 206);
            labelDiscount.Name = "labelDiscount";
            labelDiscount.Size = new Size(38, 15);
            labelDiscount.TabIndex = 12;
            labelDiscount.Text = "label1";
            // 
            // labelTotal
            // 
            labelTotal.AutoSize = true;
            labelTotal.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelTotal.Location = new Point(12, 316);
            labelTotal.Name = "labelTotal";
            labelTotal.Size = new Size(51, 21);
            labelTotal.TabIndex = 13;
            labelTotal.Text = "label1";
            // 
            // labelHead
            // 
            labelHead.AutoSize = true;
            labelHead.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelHead.Location = new Point(12, 9);
            labelHead.Name = "labelHead";
            labelHead.Size = new Size(136, 25);
            labelHead.TabIndex = 14;
            labelHead.Text = "Новий квиток";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 51);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 15;
            label1.Text = "Рейс:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 95);
            label2.Name = "label2";
            label2.Size = new Size(78, 15);
            label2.TabIndex = 16;
            label2.Text = "Клас салону:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 138);
            label3.Name = "label3";
            label3.Size = new Size(93, 15);
            label3.TabIndex = 17;
            label3.Text = "Кількість місць:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 183);
            label4.Name = "label4";
            label4.Size = new Size(106, 15);
            label4.TabIndex = 18;
            label4.Text = "Категорія знижки:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 230);
            label5.Name = "label5";
            label5.Size = new Size(82, 15);
            label5.TabIndex = 19;
            label5.Text = "Дата вильоту:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 274);
            label6.Name = "label6";
            label6.Size = new Size(137, 15);
            label6.TabIndex = 20;
            label6.Text = "Дата придбання квитка:";
            // 
            // Ticket
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(416, 380);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(labelHead);
            Controls.Add(labelTotal);
            Controls.Add(labelDiscount);
            Controls.Add(labelSeatCount);
            Controls.Add(labelSeatPrice);
            Controls.Add(labelDay);
            Controls.Add(labelFlightInfo);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(dateFlight);
            Controls.Add(comboDiscount);
            Controls.Add(numCount);
            Controls.Add(comboSeatType);
            Controls.Add(dateTicket);
            Controls.Add(comboFlight);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Ticket";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Квиток";
            TopMost = true;
            Shown += Ticket_Shown;
            ((System.ComponentModel.ISupportInitialize)numCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboFlight;
        private DateTimePicker dateTicket;
        private ComboBox comboSeatType;
        private NumericUpDown numCount;
        private ComboBox comboDiscount;
        private DateTimePicker dateFlight;
        private Button btnOk;
        private Button btnCancel;
        private Label labelFlightInfo;
        private Label labelDay;
        private Label labelSeatPrice;
        private Label labelSeatCount;
        private Label labelDiscount;
        private Label labelTotal;
        private Label labelHead;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
    }
}