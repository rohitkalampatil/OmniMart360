using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Linq;

using BusinessLayer;
using Models;
using System.Collections.Generic;

namespace RMS
{
    public partial class SupplierPage : Form
    {
        MySqlConnection c1;

        SupplierBLL bl = new SupplierBLL();

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

        //refresh button
        private void button1_Click(object sender, EventArgs e)
        {
            refreshFunction();
        }
        // Add Supplier
        private void btnAdd_Click(object sender, EventArgs e)
        {
            SupplierModel supplier = new SupplierModel
            {
                SupplierName = textName.Text,
                Mobile = Convert.ToInt64(textMobile.Text),
                Email = textEmail.Text,
                Address = textAddress.Text,
                BankAcc = textBankAccount.Text,
                IFSC = textIFSC.Text,
                PAN = textPAN.Text,
                GSTIN = textGSTIN.Text
                
            };

            string msg = bl.AddSupplier(supplier);
            MessageBox.Show(msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearAll();
            refreshFunction();
        }
        // Clear Button
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
            textBankAccount.Text = "";
            textIFSC.Text = "";
            textPAN.Text = "";
            textGSTIN.Text = "";
        }

        


        private void dataSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {

                string supplierEmail = dataSupplier.Rows[e.RowIndex].Cells[2].Value.ToString();
                SupplierUpdate obj = new SupplierUpdate(supplierEmail);
                obj.ShowDialog();

            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            string filterText = textSearch.Text.Trim().ToLower();

            var allSuppliers = bl.getAllSuppliers();

            if (string.IsNullOrEmpty(filterText))
            {
                PopulateDataGridView(allSuppliers); // Show all if no filter
            }
            else
            {
                var filteredSuppliers = allSuppliers.Where(s =>
                    (s.SupplierName != null && s.SupplierName.ToLower().Contains(filterText)) ||
                    (s.Mobile.ToString().Contains(filterText)) ||
                    (s.Email != null && s.Email.ToLower().Contains(filterText))
                ).ToList();

                PopulateDataGridView(filteredSuppliers);
            }
        }

        private void PopulateDataGridView(List<SupplierModel> suppliers)
        {
            dataSupplier.Rows.Clear(); // Clear existing rows

            foreach (var supplier in suppliers)
            {
                int rowIndex = dataSupplier.Rows.Add();

                dataSupplier.Rows[rowIndex].Cells["supplierName"].Value = supplier.SupplierName.ToUpper();
                dataSupplier.Rows[rowIndex].Cells["supplierMobile"].Value = supplier.Mobile;
                dataSupplier.Rows[rowIndex].Cells["supplierEmail"].Value = supplier.Email;
            }
        }
        private void refreshFunction()
        {
            var suppliers = bl.getAllSuppliers();
            PopulateDataGridView(suppliers);
        }


        private void textName_TextChanged(object sender, EventArgs e)
        {
            textName.Text = Regex.Replace(textName.Text, "[^a-zA-Z ]", "");
        }

        private void textMobile_TextChanged(object sender, EventArgs e)
        {
            textMobile.Text = Regex.Replace(textMobile.Text, "[^0-9]", "");
        }


        private void textEmail_TextChanged(object sender, EventArgs e)
        {
            textEmail.Text = Regex.Replace(textEmail.Text, "[^a-zA-Z@._]", "");
        }

        private void textAddress_TextChanged(object sender, EventArgs e)
        {
            textAddress.Text = Regex.Replace(textAddress.Text, "[^a-zA-Z 0-9]", "");
        }

    }
}
