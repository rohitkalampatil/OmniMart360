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
    public partial class ReceiiptsPage : Form
    {
        MySqlConnection c1;
        MySqlDataAdapter da;
        DataTable t;
        String q="";
        public ReceiiptsPage()
        {
            InitializeComponent();
        }

        private void ReceiiptsPage_Load(object sender, EventArgs e)
        {
            
            c1 = DBConnection.GetConnection();;
            refreshFunction();
        }
        private void refreshFunction()
        {
            c1.Open();
            try
            {
                q = "select * from receipts order by invoiceno desc";
                da = new MySqlDataAdapter(q, c1);
                t = new DataTable();

                da.Fill(t);
                PopulateDataGridView(t);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message, "OmniMart360", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (c1.State == ConnectionState.Open)
                    c1.Close();
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string filterText = textSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(filterText))
            {
                PopulateDataGridView(t); // Reset to original data if search text is empty
            }
            else
            {
                // Apply filter to the DataTable
                DataView dv = new DataView(t);
                dv.RowFilter = string.Format("CONVERT(cust_name, System.String) LIKE '%{0}%' OR CONVERT(cust_mobile, System.String) LIKE '%{0}%' OR CONVERT(date, System.String) LIKE '%{0}%' OR CONVERT(totalamount, System.String) LIKE '%{0}%' OR CONVERT(invoiceno, System.String) LIKE '%{0}%'", filterText);
                PopulateDataGridView(dv.ToTable());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the click is on a button column
            if (e.ColumnIndex == 5 && e.RowIndex >= 0)
            {
                // Get the name from the Name column
                string name = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                int invNo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                
                ReceiptUpdate obj = new ReceiptUpdate(invNo);
                obj.ShowDialog();   

            }
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshFunction();
        }

        private void PopulateDataGridView(DataTable table)
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow row in table.Rows)
            {
                int rowIndex = dataGridView1.Rows.Add();

                dataGridView1.Rows[rowIndex].Cells["customerName"].Value = row["cust_name"].ToString().ToUpper();
                dataGridView1.Rows[rowIndex].Cells["mobileNo"].Value = row["cust_mobile"];
                dataGridView1.Rows[rowIndex].Cells["date"].Value = row["date"];
                dataGridView1.Rows[rowIndex].Cells["billAmount"].Value = row["totalamount"];
                dataGridView1.Rows[rowIndex].Cells["invno"].Value = row["invoiceno"];

            }
        }
    }
}
