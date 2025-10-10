using System;

using System.Data;

using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace RMS
{
    public partial class SupplierPage : Form
    {
        MySqlConnection c1;
        MySqlCommand cmd;
        string q1 = "";
        MySqlDataAdapter da;
        DataTable t;
        string supName = "", supAdd = "", supEmail = "";
        long supMob = 0;
        public SupplierPage()
        {
            InitializeComponent();
        }

        private void SupplierPage_Load(object sender, EventArgs e)
        {
            textSearch.Focus();
            c1 = DBConnection.GetConnection();
            textName.Focus();
            refreshFunction();
        }

        private void refreshFunction()
        {
            c1.Open();
            try
            {
                q1 = "select * from supplier";
                da = new MySqlDataAdapter(q1, c1);
                t = new DataTable();
                da.Fill(t);
                PopulateDataGridView(t);
                
            }
            finally
            {
                if (c1.State == ConnectionState.Open)
                    c1.Close();
            }
        }
        private void PopulateDataGridView(DataTable table)
        {
            dataSupplier.Rows.Clear(); // Clear existing rows

            foreach (DataRow row in table.Rows)
            {
                int rowIndex = dataSupplier.Rows.Add();

                dataSupplier.Rows[rowIndex].Cells["supplierName"].Value = row["supplier_name"].ToString().ToUpper();
                dataSupplier.Rows[rowIndex].Cells["supplierMobile"].Value = row["mobile"];
                dataSupplier.Rows[rowIndex].Cells["supplierEmail"].Value = row["email"];

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();

        }

        private void ClearAll()
        {
            textName.Text = "";
            textMobile.Text = "";
            textEmail.Text = "";
            textAddress.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }


        private void textName_TextChanged(object sender, EventArgs e)
        {
            textName.Text = Regex.Replace(textName.Text, "[^a-zA-Z ]", "");
        }

        private void textMobile_TextChanged(object sender, EventArgs e)
        {
            textMobile.Text = Regex.Replace(textMobile.Text, "[^0-9]", "");
        }
        //refresh button
        private void button1_Click(object sender, EventArgs e)
        {
            refreshFunction();
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
                dv.RowFilter = string.Format("CONVERT(supplier_name, System.String) LIKE '%{0}%' OR CONVERT(mobile, System.String) LIKE '%{0}%' OR CONVERT(email, System.String) LIKE '%{0}%' ", filterText);
                PopulateDataGridView(dv.ToTable());
            }
        }


        
        private void textEmail_TextChanged(object sender, EventArgs e)
        {
            textEmail.Text = Regex.Replace(textEmail.Text,"[^a-zA-Z @._ ]","");
        }

        private void textAddress_TextChanged(object sender, EventArgs e)
        {
            textAddress.Text = Regex.Replace(textAddress.Text, "[^a-zA-Z ]", "");
        }


    }
}
