namespace lab3
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabShedule = new TabPage();
            dgvShedule = new DataGridView();
            mShedule = new MenuStrip();
            addSheduleMenuItem = new ToolStripMenuItem();
            removeSheduleMenuItem = new ToolStripMenuItem();
            tabTickets = new TabPage();
            dgvTickets = new DataGridView();
            mTickets = new MenuStrip();
            addTicketMenuItem = new ToolStripMenuItem();
            removeTicketMenuItem = new ToolStripMenuItem();
            tabControl1.SuspendLayout();
            tabShedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvShedule).BeginInit();
            mShedule.SuspendLayout();
            tabTickets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTickets).BeginInit();
            mTickets.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabShedule);
            tabControl1.Controls.Add(tabTickets);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 450);
            tabControl1.TabIndex = 0;
            // 
            // tabShedule
            // 
            tabShedule.Controls.Add(dgvShedule);
            tabShedule.Controls.Add(mShedule);
            tabShedule.Location = new Point(4, 24);
            tabShedule.Name = "tabShedule";
            tabShedule.Padding = new Padding(3);
            tabShedule.Size = new Size(792, 422);
            tabShedule.TabIndex = 0;
            tabShedule.Text = "Розклад рейсів";
            tabShedule.UseVisualStyleBackColor = true;
            // 
            // dgvShedule
            // 
            dgvShedule.AllowUserToAddRows = false;
            dgvShedule.AllowUserToDeleteRows = false;
            dgvShedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvShedule.Dock = DockStyle.Fill;
            dgvShedule.Location = new Point(3, 27);
            dgvShedule.Name = "dgvShedule";
            dgvShedule.ReadOnly = true;
            dgvShedule.Size = new Size(786, 392);
            dgvShedule.TabIndex = 0;
            dgvShedule.MouseDoubleClick += dgvShedule_MouseDoubleClick;
            // 
            // mShedule
            // 
            mShedule.Items.AddRange(new ToolStripItem[] { addSheduleMenuItem, removeSheduleMenuItem });
            mShedule.Location = new Point(3, 3);
            mShedule.Name = "mShedule";
            mShedule.Size = new Size(786, 24);
            mShedule.TabIndex = 2;
            mShedule.Text = "menuStrip1";
            // 
            // addSheduleMenuItem
            // 
            addSheduleMenuItem.Name = "addSheduleMenuItem";
            addSheduleMenuItem.Size = new Size(58, 20);
            addSheduleMenuItem.Text = "Додати";
            addSheduleMenuItem.Click += addSheduleMenuItem_Click;
            // 
            // removeSheduleMenuItem
            // 
            removeSheduleMenuItem.Name = "removeSheduleMenuItem";
            removeSheduleMenuItem.Size = new Size(71, 20);
            removeSheduleMenuItem.Text = "Видалити";
            removeSheduleMenuItem.Click += removeSheduleMenuItem_Click;
            // 
            // tabTickets
            // 
            tabTickets.Controls.Add(dgvTickets);
            tabTickets.Controls.Add(mTickets);
            tabTickets.Location = new Point(4, 24);
            tabTickets.Name = "tabTickets";
            tabTickets.Padding = new Padding(3);
            tabTickets.Size = new Size(792, 422);
            tabTickets.TabIndex = 1;
            tabTickets.Text = "Квитки";
            tabTickets.UseVisualStyleBackColor = true;
            // 
            // dgvTickets
            // 
            dgvTickets.AllowUserToAddRows = false;
            dgvTickets.AllowUserToDeleteRows = false;
            dgvTickets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTickets.Dock = DockStyle.Fill;
            dgvTickets.Location = new Point(3, 27);
            dgvTickets.Name = "dgvTickets";
            dgvTickets.ReadOnly = true;
            dgvTickets.Size = new Size(786, 392);
            dgvTickets.TabIndex = 1;
            dgvTickets.MouseDoubleClick += dgvTickets_MouseDoubleClick;
            // 
            // mTickets
            // 
            mTickets.Items.AddRange(new ToolStripItem[] { addTicketMenuItem, removeTicketMenuItem });
            mTickets.Location = new Point(3, 3);
            mTickets.Name = "mTickets";
            mTickets.Size = new Size(786, 24);
            mTickets.TabIndex = 0;
            mTickets.Text = "menuStrip2";
            // 
            // addTicketMenuItem
            // 
            addTicketMenuItem.Name = "addTicketMenuItem";
            addTicketMenuItem.Size = new Size(58, 20);
            addTicketMenuItem.Text = "Додати";
            addTicketMenuItem.Click += addTicketMenuItem_Click;
            // 
            // removeTicketMenuItem
            // 
            removeTicketMenuItem.Name = "removeTicketMenuItem";
            removeTicketMenuItem.Size = new Size(71, 20);
            removeTicketMenuItem.Text = "Видалити";
            removeTicketMenuItem.Click += removeTicketMenuItem_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            MainMenuStrip = mShedule;
            Name = "Main";
            Text = "Авіаквитки";
            FormClosed += Form1_FormClosed;
            Shown += Form1_Shown;
            tabControl1.ResumeLayout(false);
            tabShedule.ResumeLayout(false);
            tabShedule.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvShedule).EndInit();
            mShedule.ResumeLayout(false);
            mShedule.PerformLayout();
            tabTickets.ResumeLayout(false);
            tabTickets.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTickets).EndInit();
            mTickets.ResumeLayout(false);
            mTickets.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabControl1;
        private TabPage tabShedule;
        private TabPage tabTickets;
        private DataGridView dgvShedule;
        private MenuStrip mShedule;
        private DataGridView dgvTickets;
        private MenuStrip mTickets;
        private ToolStripMenuItem addSheduleMenuItem;
        private ToolStripMenuItem removeSheduleMenuItem;
        private ToolStripMenuItem addTicketMenuItem;
        private ToolStripMenuItem removeTicketMenuItem;
    }
}
