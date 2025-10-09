using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace RMS
{
    public partial class PurchasePage : Form
    {

        private decimal tamount = 0;
        
        MySqlConnection c1;
        MySqlCommand cmd;

        public PurchasePage()
        {
            InitializeComponent();
        }

        private void PurchasePage_Load(object sender, EventArgs e)
        {
            textInvoiceNo.Focus();
            c1 = DBConnection.GetConnection(); ;
            comboBox1.SelectedIndex = 0; 
            textTotalBill.Text = string.Format(new System.Globalization.CultureInfo("en-IN"), "{0:N}", tamount);
        }

        private void dataItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 1 && e.ColumnIndex <= 3)
            {
                string productName = " ";
                object cellValue = dataItems.Rows[e.RowIndex].Cells["Items"].Value;

                if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
                {
                    productName = cellValue.ToString();
                }

                if (!string.IsNullOrEmpty(productName))
                {
                    if (IsProductNameExists(productName))
                    {
                        dataItems.Rows[e.RowIndex].Cells["Items"].Value = productName + "-1";
                        MessageBox.Show("This product name already exists in inventory. It has been renamed to avoid conflicts.", "Duplicate Product Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                decimal rate, quantity, selling;
                if (dataItems[1, e.RowIndex].Value != null &&
                    dataItems[3, e.RowIndex].Value != null &&
                    dataItems[2, e.RowIndex].Value != null &&
                    decimal.TryParse(dataItems[1, e.RowIndex].Value.ToString(), out rate) &&
                    decimal.TryParse(dataItems[3, e.RowIndex].Value.ToString(), out quantity) &&
                    decimal.TryParse(dataItems[2, e.RowIndex].Value.ToString(), out selling))
                {
                    rate = Convert.ToDecimal(dataItems.Rows[e.RowIndex].Cells["Rate"].Value);
                    quantity = Convert.ToDecimal(dataItems.Rows[e.RowIndex].Cells["Quantity"].Value);

                    decimal amount = rate * quantity;
                    dataItems.Rows[e.RowIndex].Cells["Amount"].Value = amount;

                    // ✅ Recalculate total bill after every edit
                    RecalculateTotalAmount();
                }
            }
        }


        private bool IsProductNameExists(string productName)
        {
            bool exists = false;
            
            using (MySqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM inventory WHERE name = @name";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", productName);
                    exists = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            return exists;
        }

        private void dataItems_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            // Recalculate total amount when a row is removed
            RecalculateTotalAmount();
        }

       /*
        * this methos is no longer in use
        * 
        * private void updateT(decimal a)
        {
            tamount += a;
            textTotalBill.Text = string.Format(new System.Globalization.CultureInfo("en-IN"), "{0:N}", tamount);
        }
        */
        private void RecalculateTotalAmount()
        {
            tamount = 0;

            foreach (DataGridViewRow row in dataItems.Rows)
            {
                if (!row.IsNewRow)
                {
                    object cellVal = row.Cells["Amount"].Value;
                    decimal amount;

                    if (cellVal != null && decimal.TryParse(cellVal.ToString(), out amount))
                    {
                        tamount += amount;
                    }
                }
            }

            CultureInfo culture = new CultureInfo("en-IN");
            textTotalBill.Text = string.Format(culture, "{0:N}", tamount);
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (textInvoiceNo.Text == "") 
            {
                MessageBox.Show("Please enter invoice number.", "RMS-Store Management System. 1.0", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textInvoiceNo.Focus();
            }
            else if(textSupplierName.Text=="")
            {
                MessageBox.Show("Enter supplier name.", "RMS-Store Management System. 1.0", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textSupplierName.Focus();
            }
            else if(textSupplierContact.Text=="")
            {
                MessageBox.Show("Enter supplier contact.", "RMS-Store Management System. 1.0", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textSupplierContact.Focus();
            }
            else if(textPaidAmount.Text=="")
            { 
                MessageBox.Show("Enter paid amount", "RMS-Store Management System. 1.0", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textPaidAmount.Focus();
            }
            else if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Select payment method.", "RMS-Store Management System. 1.0", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox1.Focus();
            }
            else
            {
                SaveItemsToDatabase();
            }
        }
    
        private void SaveItemsToDatabase()
        {
            c1.Open();            
            try
            {
                // getting values
                int invoiceno = Convert.ToInt32(textInvoiceNo.Text);
                String supplierName = textSupplierName.Text;
                String supplierContact = textSupplierContact.Text;
                DateTime date = dateTime.Value.Date;
                
                decimal totalBill = Convert.ToDecimal(textTotalBill.Text);
                decimal paidBill = Convert.ToDecimal(textPaidAmount.Text);
                if(paidBill > totalBill)
                {
                   MessageBox.Show("Please enter valid Amount " , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   textPaidAmount.Focus();
                }
                decimal remainingAmt = totalBill - paidBill;
                String paymentMethod = comboBox1.SelectedItem.ToString();
                String query = "";
               
                // add purchase details
                query = "INSERT INTO purchase (invoiceno, supplier_name,supplier_contact, total_bill, paid_amount, remaining_amount, pay_method,date) " +
                   "VALUES (@InvoiceNo, @SupplierName,@SupplierContact,@totalBill, @PaidAmount,@remainingAmount, @PayMethod,@date)";

                cmd = new MySqlCommand(query, c1);
                // Add parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@InvoiceNo", invoiceno);
                cmd.Parameters.AddWithValue("@SupplierName", supplierName);
                cmd.Parameters.AddWithValue("@SupplierContact", supplierContact);
                cmd.Parameters.AddWithValue("@totalBill", totalBill);
                cmd.Parameters.AddWithValue("@PaidAmount", paidBill);
                cmd.Parameters.AddWithValue("@remainingAmount", remainingAmt);
                cmd.Parameters.AddWithValue("@PayMethod", paymentMethod);
                cmd.Parameters.AddWithValue("@date", date);
     
                int r = cmd.ExecuteNonQuery();
                if (r > 0) 
                {
                    // addeditems to list
                    foreach (DataGridViewRow row in dataItems.Rows)
                    {
                        // Skip the last row if it's new row
                        if (row.IsNewRow) continue;
                        // Get values from DataGridView row
                        string name = row.Cells["items"].Value.ToString();
                        decimal purchaseRate = Convert.ToDecimal(row.Cells["rate"].Value);
                        decimal sellingRate = Convert.ToDecimal(row.Cells["price"].Value);
                        int quantity = Convert.ToInt32(row.Cells["quantity"].Value);

                        query = "insert into purchasesitems(invoiceno,itemname,quantity,purchase_rate,selling_rate) values(@invoiceNumber,@item,@quantity,@purchaserate,@sellingrate)";
                        cmd = new MySqlCommand(query, c1);
                        cmd.Parameters.AddWithValue("@invoiceNumber", invoiceno);
                        cmd.Parameters.AddWithValue("@item", name);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.Parameters.AddWithValue("@purchaserate", purchaseRate);
                        cmd.Parameters.AddWithValue("@sellingrate", sellingRate);
                        cmd.ExecuteNonQuery();
                        /* update | insert into inventory items
                                * first check if itemis already present if yes update its quantity
                                * else simply insert new item to inventory
                                */

                        query = "select count(*) from inventory where name =@item and purchase_rate=@prate";
                        cmd = new MySqlCommand(query, c1);
                        cmd.Parameters.AddWithValue("@item",name);
                        cmd.Parameters.AddWithValue("@prate", purchaseRate);
                        int res = Convert.ToInt32(cmd.ExecuteScalar());


                        if(!(res>0))
                        {
                            query = "insert into inventory(name,qty,purchase_rate,selling_rate) values(@itemname,@qty,@purrate,@salerate);";
                            cmd = new MySqlCommand(query, c1);
                            cmd.Parameters.AddWithValue("@itemname",name);
                            cmd.Parameters.AddWithValue("@qty", quantity);
                            cmd.Parameters.AddWithValue("@purrate", purchaseRate);
                            
                            cmd.Parameters.AddWithValue("@salerate", sellingRate);

                            res = cmd.ExecuteNonQuery();

                        }
                        else
                        {
                            //updating quantity
                            query = "select qty from inventory where name = @itemname and purchase_rate=@prate";
                            cmd = new MySqlCommand(query, c1);
                            cmd.Parameters.AddWithValue("@itemname",name);
                            cmd.Parameters.AddWithValue("@prate", purchaseRate);
                            int qty = Convert.ToInt32(cmd.ExecuteScalar())+quantity;

                            query = "update inventory set qty=@qty where name = @itemname and purchase_rate=@prate";
                            cmd = new MySqlCommand(query, c1);
                            cmd.Parameters.AddWithValue("@qty", qty);
                            cmd.Parameters.AddWithValue("@itemname", name);
                            cmd.Parameters.AddWithValue("@prate", purchaseRate);
                            cmd.ExecuteNonQuery();

                            
                        }
                        
                    }
                    
                    MessageBox.Show("Bill saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearAll();
                }
        
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (c1.State == ConnectionState.Open)
                    c1.Close();
            }
        }

        private void textSupplierName_TextChanged(object sender, EventArgs e)
        {
            textSupplierName.Text = Regex.Replace(textSupplierName.Text, "[^A-Za-z ]", "");
        }

        private void textInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            textInvoiceNo.Text = Regex.Replace(textInvoiceNo.Text, "[^0-9]", "");
        }

        private void textSupplierContact_TextChanged(object sender, EventArgs e)
        {
            textSupplierContact.Text = Regex.Replace(textSupplierContact.Text, "[^0-9]", "");
        }

        private void textSupplierEmail_TextChanged(object sender, EventArgs e)
        {
            textSupplierEmail.Text = Regex.Replace(textSupplierEmail.Text, "[^A-Za-z@.$#]", "");

        }

        private void textSupplierAddress_TextChanged(object sender, EventArgs e)
        {
            textSupplierAddress.Text = Regex.Replace(textSupplierAddress.Text, "[^A-Za-z ]", "");
        }

        private void textPaidAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Remove non-digit characters
                string raw = Regex.Replace(textPaidAmount.Text, "[^0-9]", "");

                if (string.IsNullOrEmpty(raw))
                {
                    textPaidAmount.Text = "";
                    return;
                }

                // Parse paid amount
                double paidAmount;
                if (!double.TryParse(raw, out paidAmount))
                {
                    textPaidAmount.Text = "";
                    return;
                }

                // Parse total bill from textTotalBill
                double totalBill;
                if (!double.TryParse(textTotalBill.Text.Replace(",", ""), out totalBill))
                {
                    totalBill = 0;
                }

                // Restrict paid amount to not exceed total bill
                if (paidAmount > totalBill)
                {
                    MessageBox.Show("Paid amount cannot exceed total bill.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    paidAmount = totalBill;
                }

                // Format and update textbox
                textPaidAmount.Text = string.Format(new System.Globalization.CultureInfo("en-IN"), "{0:N0}", paidAmount);
                textPaidAmount.SelectionStart = textPaidAmount.Text.Length;
            }
            catch
            {
                textPaidAmount.Text = "";
            }
        }


        private void purchaseReport_Click(object sender, EventArgs e)
        {   
            PurchaseReport obj = new PurchaseReport();
            obj.ShowDialog();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            textInvoiceNo.Text = "";
            textSupplierAddress.Text = "";
            textSupplierContact.Text = "";
            textSupplierEmail.Text = "";
            textSupplierName.Text = "";
            textPaidAmount.Text = "";
            dataItems.Rows.Clear();
            tamount = 0;
            textTotalBill.Text = string.Format(new System.Globalization.CultureInfo("en-IN"), "{0:N}", tamount);
        }

    }
}
