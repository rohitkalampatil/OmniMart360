using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RMS
{
    public partial class ReceivableDetailscs : Form
    {
        long mobile = 0;
        decimal rem=0;
        string cname, add;
        MySqlConnection c1;
        DataTable t;
     
        public ReceivableDetailscs(string cname, long mobile,string caddress,decimal rem)
        {
            InitializeComponent();
            this.mobile = mobile;
            this.rem = rem;
            this.cname = cname;
            this.add = caddress;
        }

        private void ReceivableDetailscs_Load(object sender, EventArgs e)
        {
            c1 = DBConnection.GetConnection();
            refreshFunction();
        }
        private void refreshFunction()
        {
            combYear.Focus();
            try
            {
                c1.Open();

                string q = @"SELECT 

                                c.cmobile,
                                c.caddress,
                                r.invoiceno,
                                r.billamount,
                                r.paid,
                                r.remaining,
                                rc.date
                                
                            FROM customers c
                            JOIN receivable r ON c.cmobile = r.cmobile
                            JOIN receipts rc ON r.invoiceno = rc.invoiceno
                            WHERE c.cmobile = @mobile;";

                MySqlDataAdapter da = new MySqlDataAdapter(q, c1);
                da.SelectCommand.Parameters.AddWithValue("@mobile", mobile);

                t = new DataTable();
                da.Fill(t);
                lblRemaining.Text = rem.ToString();
                lblName.Text = cname;
                lblMobile.Text = mobile.ToString();
                lblAdd.Text = add;

                PopulateDataGridView(t); // Your method to bind data to grid
                PopulateYearMonthFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
            finally
            {
                if (c1.State == ConnectionState.Open)
                    c1.Close();
            }
            //MessageBox.Show(""+mobile);
        }
        private void PopulateYearMonthFilters()
        {
            // Populate years from the data
            var years = t.AsEnumerable()
                         .Select(row => Convert.ToDateTime(row["date"]).Year)
                         .Distinct()
                         .OrderByDescending(y => y)
                         .ToList();

            combYear.Items.Clear();
            combYear.Items.Add("All");
            foreach (var year in years)
                combYear.Items.Add(year.ToString());
            combYear.SelectedIndex = 0;

            // Populate months
            combMonth.Items.Clear();
            combMonth.Items.Add("All");
            for (int i = 1; i <= 12; i++)
                combMonth.Items.Add(new DateTime(2000, i, 1).ToString("MMMM"));
            combMonth.SelectedIndex = 0;
        }

        private void PopulateDataGridView(DataTable table)
        {
            dataGridView1.Rows.Clear(); // Clear existing rows

            foreach (DataRow row in table.Rows)
            {
                int rowIndex = dataGridView1.Rows.Add();

                dataGridView1.Rows[rowIndex].Cells["invoiceno"].Value = row["invoiceno"];
                dataGridView1.Rows[rowIndex].Cells["date"].Value = row["date"];
                dataGridView1.Rows[rowIndex].Cells["billAmount"].Value = row["billamount"];
                dataGridView1.Rows[rowIndex].Cells["paid"].Value = row["paid"];
                dataGridView1.Rows[rowIndex].Cells["remaining"].Value = row["remaining"];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FilterByYearMonth();
        }
        private void FilterByYearMonth()
        {
            string selectedYear = combYear.SelectedItem.ToString();
            string selectedMonth = combMonth.SelectedItem.ToString();

            int year = selectedYear != "All" ? Convert.ToInt32(selectedYear) : -1;
            int month = selectedMonth != "All" ? DateTime.ParseExact(selectedMonth, "MMMM", null).Month : -1;

            DataTable filteredTable = t.Clone(); // Clone structure

            foreach (DataRow row in t.Rows)
            {
                DateTime date = Convert.ToDateTime(row["date"]);
                bool matchYear = (year == -1 || date.Year == year);
                bool matchMonth = (month == -1 || date.Month == month);

                if (matchYear && matchMonth)
                {
                    filteredTable.ImportRow(row);
                }
            }

            PopulateDataGridView(filteredTable);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex >= 0)
            {
                // Get the name from the Name column
                
                int invNo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                ReceiptUpdate obj = new ReceiptUpdate(invNo);
                obj.ShowDialog();

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            refreshFunction();
        }
    }
}
