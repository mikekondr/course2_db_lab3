namespace lab3
{
    partial class flight
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
            components = new System.ComponentModel.Container();
            comboDeparture = new ComboBox();
            comboArrival = new ComboBox();
            comboDays = new ComboBox();
            comboAvialiner = new ComboBox();
            dataTableBindingSource = new BindingSource(components);
            listMiddlePoints = new CheckedListBox();
            btnOk = new Button();
            btnCancel = new Button();
            labelHead = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dataTableBindingSource).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // comboDeparture
            // 
            comboDeparture.FormattingEnabled = true;
            comboDeparture.Items.AddRange(new object[] { "1;Test1", "2;Test2" });
            comboDeparture.Location = new Point(103, 110);
            comboDeparture.Name = "comboDeparture";
            comboDeparture.Size = new Size(193, 23);
            comboDeparture.TabIndex = 2;
            // 
            // comboArrival
            // 
            comboArrival.FormattingEnabled = true;
            comboArrival.Items.AddRange(new object[] { "1;Test1", "2;Test2" });
            comboArrival.Location = new Point(103, 139);
            comboArrival.Name = "comboArrival";
            comboArrival.Size = new Size(193, 23);
            comboArrival.TabIndex = 3;
            // 
            // comboDays
            // 
            comboDays.FormattingEnabled = true;
            comboDays.Location = new Point(103, 81);
            comboDays.Name = "comboDays";
            comboDays.Size = new Size(193, 23);
            comboDays.TabIndex = 1;
            // 
            // comboAvialiner
            // 
            comboAvialiner.FormattingEnabled = true;
            comboAvialiner.Location = new Point(103, 52);
            comboAvialiner.Name = "comboAvialiner";
            comboAvialiner.Size = new Size(193, 23);
            comboAvialiner.TabIndex = 0;
            // 
            // dataTableBindingSource
            // 
            dataTableBindingSource.DataSource = typeof(System.Data.DataTable);
            // 
            // listMiddlePoints
            // 
            listMiddlePoints.CheckOnClick = true;
            listMiddlePoints.Dock = DockStyle.Fill;
            listMiddlePoints.FormattingEnabled = true;
            listMiddlePoints.Location = new Point(3, 19);
            listMiddlePoints.Name = "listMiddlePoints";
            listMiddlePoints.Size = new Size(278, 210);
            listMiddlePoints.TabIndex = 6;
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(75, 406);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 7;
            btnOk.Text = "Зберегти";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(156, 406);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Скасувати";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // labelHead
            // 
            labelHead.AutoSize = true;
            labelHead.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelHead.Location = new Point(12, 9);
            labelHead.Name = "labelHead";
            labelHead.Size = new Size(116, 25);
            labelHead.TabIndex = 9;
            labelHead.Text = "Новий рейс";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 113);
            label1.Name = "label1";
            label1.Size = new Size(85, 15);
            label1.TabIndex = 10;
            label1.Text = "Відправлення:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 142);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 11;
            label2.Text = "Прибуття:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 84);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 12;
            label3.Text = "Дні вильоту:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 55);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 13;
            label4.Text = "Літак:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(listMiddlePoints);
            groupBox1.Location = new Point(12, 168);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(284, 232);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Проміжні зупинки";
            // 
            // flight
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(310, 441);
            Controls.Add(groupBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(labelHead);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(comboAvialiner);
            Controls.Add(comboDays);
            Controls.Add(comboArrival);
            Controls.Add(comboDeparture);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "flight";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Рейс";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)dataTableBindingSource).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboDeparture;
        private ComboBox comboArrival;
        private ComboBox comboDays;
        private ComboBox comboAvialiner;
        private BindingSource dataTableBindingSource;
        private CheckedListBox listMiddlePoints;
        private Button btnOk;
        private Button btnCancel;
        private Label labelHead;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private GroupBox groupBox1;
    }
}