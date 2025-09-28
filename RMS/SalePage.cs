using System;

using System.Data;

using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace RMS
{
    public partial class SalePage : Form
    {
        private decimal tamount = 0;
        MySqlConnection connection;
        MySqlCommand cmd;
        string customerName;
        long mobile;
        decimal bill,paidAmt,remAmt;
        decimal total;
        int disc;
        int invNo;
        string date;
        public SalePage()
        {
            InitializeComponent();
        }
       
        private void SalePage_Load(object sender, EventArgs e)
        {

            connection = DBConnection.GetConnection();
            labalCurrentDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            textTotalAmount.Text = tamount.ToString();
            txtRate.Text = "0.00";
            txtPaidAmount.Text = "0.00";
            txtRemainingAmount.Text = "0.00";
            txtAmt.Text = "0.00";
            textDiscount.Text = "0";
            textAmount.Text = "0.00";
            lblItems.Text = "0";
            cmbItems.Focus();
            LoadNewInvoice();
            populateItem();
        }
        //function to get Items from db and populate to combobox
        private void populateItem()
        {  
            connection.Open();
            try
            {
                string query = "SELECT name FROM inventory where qty >0";
                cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string itemName = reader.GetString("name");
                    // Add items to the ComboBox column
                    cmbItems.Items.Add(itemName);
                
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message,"Omnimart360 ");
                labelInvoiceNo.Text = "1";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            
        }

        //function to load invoice number
        private void LoadNewInvoice()
        {
            connection.Open();
            try
            {
                string query = "select max(invoiceno) from receipts";
                cmd = new MySqlCommand(query, connection);
                int invoiceno = Convert.ToInt32(cmd.ExecuteScalar());

                labelInvoiceNo.Text = (invoiceno + 1).ToString();         
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Something Went Wrong Try Again...", "Omnimart360 ",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                labelInvoiceNo.Text = "1";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        
        
 
        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            RecalculateTotalAmount();
        }

        private void updateT(decimal a)
        {
            tamount += a;
            textTotalAmount.Text = tamount.ToString("0.00");
            textAmount.Text = tamount.ToString("0.00");    
        }

        private void RecalculateTotalAmount()
        {
            // Recalculate total amount when a row is deleted or updated
            tamount = 0;
            int itemCount = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                itemCount++;
                if (!row.IsNewRow)
                {
                    // Calculate the amount for each row and add to the total
                    decimal amount = Convert.ToDecimal(row.Cells["Amount"].Value);
                    tamount += amount;
                }
            }
            lblItems.Text = itemCount.ToString();
            textTotalAmount.Text = tamount.ToString();
            textAmount.Text = tamount.ToString();
            textDiscount.Text = "0";
        }        
        
        
        // Allow user t input numbers only
        private void textDiscount_TextChanged(object sender, EventArgs e)
        {
            textDiscount.Text = Regex.Replace(textDiscount.Text,"[^0-9]","");
            int disc = 0;
            decimal bill = 0;
            if (textDiscount.Text == "")
                textAmount.Text = tamount.ToString();
            else
            {
                disc = Convert.ToInt32(textDiscount.Text);
                bill = tamount - (tamount * disc / 100);
                textAmount.Text = bill.ToString();
            }
        }
        // Allow user t input numbers only
        private void textCutomerMobile_TextChanged(object sender, EventArgs e)
        {
            textCutomerMobile.Text = Regex.Replace(textCutomerMobile.Text, "[^0-9]", "");
        }
        // Allow user t input numbers only
        private void textCustomerName_TextChanged(object sender, EventArgs e)
        {
            textCustomerName.Text = Regex.Replace(textCustomerName.Text, "[^A-Za-z ]", "");
        }
        // Save Logic
        //Save button
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (textCustomerName.Text == "")
            {
                MessageBox.Show("Fill Empty Fields...", "Omnimart360 ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textCustomerName.Focus();
            }
            else if (textCutomerMobile.Text == "")
            {
                MessageBox.Show("Fill Empty Fields...", "Omnimart360 ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textCutomerMobile.Focus();
            }
            else
            {
                saveReceipt();
            }        
        }

        /// <summary>
        /// Save Receipt
        /// </summary>
        private void saveReceipt()
        {
            getData();
            connection.Open();
            try
            {
                string q = "INSERT INTO receipts VALUES (@InvoiceNo, @date, @name, @mobile, @bill, @discount, @totalamount)";
                cmd = new MySqlCommand(q, connection);
                // Add parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@InvoiceNo", invNo);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@name", customerName);
                cmd.Parameters.AddWithValue("@mobile", mobile);
                cmd.Parameters.AddWithValue("@bill", bill);
                cmd.Parameters.AddWithValue("@discount", disc);
                cmd.Parameters.AddWithValue("@totalamount", total);

                int r = cmd.ExecuteNonQuery();  
                if (r > 0)
                {
                    //add solditems to table
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // Skip the last row if it's new row
                        if (row.IsNewRow) continue;

                        // Get values from DataGridView row
                        string item = row.Cells[0].Value.ToString();
                        decimal sellingrate=Convert.ToDecimal(row.Cells[1].Value); 
                        int quantity = Convert.ToInt32(row.Cells[2].Value);

                        //get quantity from db
                        q = "select qty,purchase_rate from inventory where name=@item";
                        cmd = new MySqlCommand(q, connection);
                        cmd.Parameters.AddWithValue("@item", item);
                        int qtyInDB=0;
                        decimal purchaserate=0;
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            qtyInDB = reader.GetInt32(0);         // qty
                            purchaserate = reader.GetDecimal(1);     // selling_rate
                            // Use qtyInDB and prate as needed
                        }
                        reader.Close();
                        //update quantity in DB
                        q = "update inventory set qty=@newqty where name=@item";
                        cmd = new MySqlCommand(q, connection);
                        cmd.Parameters.AddWithValue("@newqty", qtyInDB - quantity);
                        cmd.Parameters.AddWithValue("@item", item);

                        //Add sold items to table
                        int res = cmd.ExecuteNonQuery();
                        if (res > 0)
                        {
                            // SQL query
                            q = "INSERT INTO solditems(invoiceno,itemname,purchaserate,sellingrate,quantity) VALUES (@invno, @item,@purchaserate,@sellingrate, @qty)";

                            cmd = new MySqlCommand(q, connection);
                            cmd.Parameters.AddWithValue("@invno", invNo);
                            cmd.Parameters.AddWithValue("@item", item);

                            cmd.Parameters.AddWithValue("@purchaserate", purchaserate);
                            cmd.Parameters.AddWithValue("@sellingrate", sellingrate);
                            cmd.Parameters.AddWithValue("@qty", quantity);

                            // Execute command
                            int rowsAffected = cmd.ExecuteNonQuery();
                        }
                    }

                    //here ask do you want to print Receipt while showing message receipt saved successfully
                    //MessageBox.Show("Receipt saved successfully..", "RMS-Store Management System. 1.0", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Ask if user wants to print the receipt
                    DialogResult result = MessageBox.Show("Receipt Saved\nDo you want to print the receipt?", "Print Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        printReceipt(); // Call your print function
                    }

                    ClearAll();
                    connection.Close();
                   
                   LoadNewInvoice();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Went Wrong While Inserting..."+ex, "Omnimart360 ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            finally { connection.Close(); } 
        }

        //Print Receipt
        private void printReceipt()
        {
            // in receipt update form create print button and apply below code on it
            getData();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("Items", typeof(string));
            dt.Columns.Add("Rate", typeof(decimal));
            dt.Columns.Add("Quantity", typeof(int));
            
            dt.Columns.Add("Amount", typeof(decimal), "Quantity*Rate");

            foreach (DataGridViewRow dgv in dataGridView1.Rows)
            {
                dt.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value, dgv.Cells[2].Value, dgv.Cells[3].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("salesItems.xml");
            SaleReceiptView sl = new SaleReceiptView(invNo, customerName, mobile, bill, disc, total, ds);
            
            sl.ShowDialog();
        }

        //Customers Basic Details
        public void getData()
        {
            customerName = textCustomerName.Text;
            mobile = Convert.ToInt64(textCutomerMobile.Text);
            bill = Convert.ToDecimal(textTotalAmount.Text);
            total = Convert.ToDecimal(textAmount.Text);
            disc = Convert.ToInt32(textDiscount.Text);
            invNo = Convert.ToInt32(labelInvoiceNo.Text);
            date = labalCurrentDate.Text;
            //paidAmt = Convert.ToDecimal(txtPaidAmount.Text);
            //remAmt = Convert.ToDecimal(txtRemainingAmount.Text);
  
        }


        private void btnAddCart_Click(object sender, EventArgs e)
        {
            // ADD items to datagride and clear form and focus to select anather item
            if (cmbItems.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Items..", "OmniMart360", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbItems.Focus();
            }
            else
            {

                string itemName = cmbItems.SelectedItem != null ? cmbItems.SelectedItem.ToString() : "";

                decimal rate, amount;
                int quantity = (int)numericUpDown1.Value;

                // Check quantity in database
                if (!IsQuantityAvailable(itemName, quantity))
                {
                    MessageBox.Show("Product Quantity Low In Stock", "Omnimart360 ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(itemName) ||
                    !decimal.TryParse(txtRate.Text, out rate) ||
                    !decimal.TryParse(txtAmt.Text, out amount))
                {
                    MessageBox.Show("Please enter valid item, rate, and amount.", "Omnimart360 ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Add row to DataGridView
                dataGridView1.Rows.Add(itemName, rate.ToString("0.00"), quantity, amount.ToString("0.00"));
                cmbItems.Focus();
                resetCart();
                // Recalculate totals
                RecalculateTotalAmount();
            }
        }
        //check quantity available in db
        private bool IsQuantityAvailable(string itemName, int requestedQty)
        {
            bool isAvailable = false;
            string query = "SELECT qty FROM inventory WHERE name = @name";
            int availableQty;
            using (MySqlConnection conn = DBConnection.GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", itemName);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out availableQty))
                    {
                        isAvailable = requestedQty <= availableQty;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking inventory: " + ex.Message,"OmniMart360",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }

            return isAvailable;
        }



        /*
         *  Below is the logic for get data and load into combobox and set and calculate
         *  rate and amount of items dynamicaly
         */
        //here get value when user select any item
        private void cmbItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = cmbItems.SelectedItem != null ? cmbItems.SelectedItem.ToString() :"";
            if (!string.IsNullOrEmpty(selectedItem))
            {
                FetchRateFromDatabase(selectedItem);
            }
        }
        //here is the method to get selling rate from db
        private void FetchRateFromDatabase(string itemName)
        {
            connection = DBConnection.GetConnection();
            string query = "SELECT selling_rate FROM inventory WHERE name = @name";

            
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@name", itemName);

                try
                {
                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        txtRate.Text = Convert.ToDecimal(result).ToString("0.00");
                        //is user chandes later his selection
                        txtAmt.Text = "0.00";
                        numericUpDown1.Value = 0;
                    }
                    else
                    {
                        txtRate.Text = "0.00"; // Default if item not found
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Fetching Rate: " + ex.Message, "OmniMart360", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        // calculate amount and set
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            decimal itemRate;
            if (decimal.TryParse(txtRate.Text, out itemRate))
            {
                int quantity = (int)numericUpDown1.Value;
                /*
                 * here check qty is available in db after add button
                 */
                // Calculate the amount
                decimal amount = itemRate * quantity;
                // Update the amount cell with the calculated value
                txtAmt.Text = amount.ToString("0.00");

            }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            resetCart();
        }
        private void resetCart()
        {
            cmbItems.SelectedIndex = -1;
            txtAmt.Text = "0.00";
            txtRate.Text = "0.00";
            numericUpDown1.Value = 0;
        }
        //Clear Button ---- may not need
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void ClearAll()
        {
            cmbItems.SelectedIndex = -1;
            txtAmt.Text = "0.00";
            txtRate.Text = "0.00";
            numericUpDown1.Value = 0;
            textCustomerName.Text = "";
            textCutomerMobile.Text = "";
            dataGridView1.Rows.Clear();
            textDiscount.Text = "0.00";
            textAmount.Text = "0.00";
            textTotalAmount.Text = "0.00";
            txtPaidAmount.Text = "0.00";
            txtRemainingAmount.Text = "0.00";
        }

        private void btnReceivable_Click(object sender, EventArgs e)
        {
            int invNo = Convert.ToInt32(labelInvoiceNo.Text);
            // if customer is new display add receivable form 
            if (textCustomerName.Text == "")
            {
                MessageBox.Show("Please Enter Customer Name", "OmniMart360");
                textCustomerName.Focus();
            }
            else if (textCutomerMobile.Text == "")
            {
                MessageBox.Show("Please Enter Customer Mobile", "OmniMart360");
                textCutomerMobile.Focus();
            }
            else
            {
                try
                {

                    connection.Open();
                    string q = "select count(cmobile) from customers where cmobile =" + textCutomerMobile.Text;

                    MySqlCommand cmd = new MySqlCommand(q, connection);
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result > 0) // Customer exists
                    {
                        // Check if this invoice already exists
                        string checkInvoiceQuery = "SELECT COUNT(*) FROM receivable WHERE invoiceno = @inv";
                        MySqlCommand checkCmd = new MySqlCommand(checkInvoiceQuery, connection);
                        checkCmd.Parameters.AddWithValue("@inv", invNo);

                        int invoiceExists = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (invoiceExists > 0)
                        {
                            // Invoice exists — update the record
                            string updateQuery = "UPDATE receivable SET cmobile = @mob, billamount = @bill, paid = @paid, remaining = @rem WHERE invoiceno = @inv";
                            MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection);
                            updateCmd.Parameters.AddWithValue("@mob", textCutomerMobile.Text);
                            updateCmd.Parameters.AddWithValue("@inv", invNo);
                            updateCmd.Parameters.AddWithValue("@bill", textAmount.Text);
                            updateCmd.Parameters.AddWithValue("@paid", txtPaidAmount.Text);
                            updateCmd.Parameters.AddWithValue("@rem", txtRemainingAmount.Text);

                            int rowsAffected = updateCmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                connection.Close();
                                saveReceipt();
                                MessageBox.Show("Receivable updated successfully.", "OmniMart360");

                            }
                        }
                        else
                        {
                            // Invoice does not exist — insert new record
                            string insertQuery = "INSERT INTO receivable (cmobile, invoiceno, billamount, paid, remaining) VALUES (@mob, @inv, @bill, @paid, @rem)";
                            MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection);
                            insertCmd.Parameters.AddWithValue("@mob", textCutomerMobile.Text);
                            insertCmd.Parameters.AddWithValue("@inv", invNo);
                            insertCmd.Parameters.AddWithValue("@bill", textAmount.Text);
                            insertCmd.Parameters.AddWithValue("@paid", txtPaidAmount.Text);
                            insertCmd.Parameters.AddWithValue("@rem", txtRemainingAmount.Text);

                            int rowsAffected = insertCmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                connection.Close();
                                saveReceipt();
                                MessageBox.Show("Successfully added receivable.", "OmniMart360");
                            }
                        }
                    }
                    else
                    {
                        // User not exist — ask for address
                        string address = Microsoft.VisualBasic.Interaction.InputBox("Enter customer address:", "Customer Address", "");

                        if (!string.IsNullOrWhiteSpace(address))
                        {
                            // Insert new customer
                            string insertCustomerQuery = "INSERT INTO customers (cname, cmobile, caddress) VALUES (@name, @mobile, @address)";
                            MySqlCommand insertCustomerCmd = new MySqlCommand(insertCustomerQuery, connection);
                            insertCustomerCmd.Parameters.AddWithValue("@name", textCustomerName.Text);
                            insertCustomerCmd.Parameters.AddWithValue("@mobile", textCutomerMobile.Text);
                            insertCustomerCmd.Parameters.AddWithValue("@address", address);

                            int customerInserted = insertCustomerCmd.ExecuteNonQuery();

                            if (customerInserted > 0)
                            {
                                // Now insert into receivable
                                string insertReceivableQuery = "INSERT INTO receivable (cmobile, invoiceno, billamount, paid, remaining) VALUES (@mob, @inv, @bill, @paid, @rem)";
                                MySqlCommand insertReceivableCmd = new MySqlCommand(insertReceivableQuery, connection);
                                insertReceivableCmd.Parameters.AddWithValue("@mob", textCutomerMobile.Text);
                                insertReceivableCmd.Parameters.AddWithValue("@inv", invNo);
                                insertReceivableCmd.Parameters.AddWithValue("@bill", textAmount.Text);
                                insertReceivableCmd.Parameters.AddWithValue("@paid", txtPaidAmount.Text);
                                insertReceivableCmd.Parameters.AddWithValue("@rem", txtRemainingAmount.Text);

                                int receivableInserted = insertReceivableCmd.ExecuteNonQuery();

                                if (receivableInserted > 0)
                                {
                                    DialogResult res = MessageBox.Show(
                                        "Receivable created successfully.\nDo you want to save the receipt?",
                                        "OmniMart360",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question
                                    );

                                    if (res == DialogResult.Yes)
                                    {
                                        connection.Close();
                                        saveReceipt(); // Call your receipt-saving method
                                    }
                                    else
                                    {
                                        btnReceivable.Visible = false; // Hide the button if user says No
                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show("Failed to add customer.", "OmniMart360");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Address is required to create customer.", "OmniMart360");
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(""+ex, "OmniMart360");
                }
                finally
                {
                    connection.Close();
                }

            }

        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            decimal remAmt = 0;
            try
            {
                decimal paidAmt = 0;
                if (string.IsNullOrWhiteSpace(txtPaidAmount.Text) || txtPaidAmount.Text == "0.00")
                    paidAmt = 0;
                else
                    paidAmt = Convert.ToDecimal(txtPaidAmount.Text);

                decimal amt = Convert.ToDecimal(textAmount.Text);

                if (paidAmt > amt)
                {
                    paidAmt = 0;
                    txtPaidAmount.Text = "";
                    txtRemainingAmount.Text = textAmount.Text;
                }

                remAmt = amt - paidAmt;
                txtRemainingAmount.Text = remAmt.ToString("0.00");

                // Show/hide buttons based on remaining amount
                if (remAmt > 0)
                {
                    btnReceivable.Visible = true;
                    btnSave.Visible = false;
                }
                else
                {
                    btnReceivable.Visible = false;
                    btnSave.Visible = true;
                }
            }
            catch (Exception ex)
            {
                txtRemainingAmount.Text = textAmount.Text;
                btnReceivable.Visible = false;
                btnSave.Visible = true;
            }

        }




    }
}
