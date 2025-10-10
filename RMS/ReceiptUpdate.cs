using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;

namespace RMS
{
    public partial class ReceiptUpdate : Form
{
    int invNo = 0;
    MySqlConnection c1;
    MySqlCommand cmd;
    MySqlDataAdapter da;
    DataTable t;
    MySqlDataReader reader;

    private decimal oldTotalBill = 0;
    private decimal oldTotalAmount = 0;
    decimal oldPaidAmount = 0;
    decimal oldRemainingAmount = 0;

    public ReceiptUpdate(int invNo)
    {
        this.invNo = invNo;
        InitializeComponent();
    }

    private void ReceiptUpdate_Load(object sender, EventArgs e)
    {
        txtPaidAmt.Text = "0";
        c1 = DBConnection.GetConnection();
        InitializeDataGridView();
        showDataItems();
    }

    private void InitializeDataGridView()
    {
        dataGridView1.Columns.Clear();
        dataGridView1.Columns.Add("ItemName", "Item Name");
        dataGridView1.Columns.Add("SellingRate", "Selling Rate");
        dataGridView1.Columns.Add("Quantity", "Quantity");
        dataGridView1.Columns.Add("ReturnQuantity", "Return Quantity");
        dataGridView1.Columns.Add("Amount", "Amount");
    }

    private void showDataItems()
    {
        btnUpdate.Visible = false;
        invoiceNo.Text = invNo.ToString();

        try
        {
            c1.Open();

            // Fetch sold items
            string q = "SELECT itemname, sellingrate, quantity FROM solditems WHERE invoiceno = @invNo";
            cmd = new MySqlCommand(q, c1);
            cmd.Parameters.AddWithValue("@invNo", invNo);
            da = new MySqlDataAdapter(cmd);
            t = new DataTable();
            da.Fill(t);

            if (t.Rows.Count == 0)
            {

                MessageBox.Show("No sold items found for this invoice.", "OmniMart360", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            PopulateDataGridView(t);

            // Fetch receipt and customer details
            string p = @"SELECT r.date, r.cust_name, r.cust_mobile, r.bill, r.discount, r.totalamount,
                         rc.paid, rc.remaining, rc.billamount
                         FROM receipts r
                         LEFT JOIN receivable rc ON r.invoiceno = rc.invoiceno
                         WHERE r.invoiceno = @invNo";

            cmd = new MySqlCommand(p, c1);
            cmd.Parameters.AddWithValue("@invNo", invNo);
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                lablDate.Text = reader["date"].ToString();
                lblName.Text = reader["cust_name"].ToString();
                lblMobile.Text = reader["cust_mobile"].ToString();
                textBill.Text = reader["bill"].ToString();
                textDiscount.Text = reader["discount"].ToString() + "%";
                textTotal.Text = reader["totalamount"].ToString();

                oldPaidAmount = reader["paid"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["paid"]);
                lblPaidAmt.Text = oldPaidAmount.ToString();

                oldRemainingAmount = reader["remaining"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["remaining"]);
                txtRemAmt.Text = oldRemainingAmount.ToString();
                txtRemAmt.ReadOnly = oldRemainingAmount == 0;

                oldTotalAmount = reader["billamount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["billamount"]);
                oldTotalBill = Convert.ToDecimal(reader["bill"]);

                txtPaidAmt.ReadOnly = oldRemainingAmount <= 0;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message, "OmniMart360", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            reader.Close();
            if (c1.State == ConnectionState.Open)
                c1.Close();
        }
    }

    private void PopulateDataGridView(DataTable table)
    {
        dataGridView1.Rows.Clear();

        foreach (DataRow row in table.Rows)
        {
            try
            {
                int rowIndex = dataGridView1.Rows.Add();
                string itemName = row["itemname"].ToString();
                decimal rate = Convert.ToDecimal(row["sellingrate"]);
                int quantity = Convert.ToInt32(row["quantity"]);
                decimal amount = rate * quantity;

                dataGridView1.Rows[rowIndex].Cells[0].Value = itemName;
                dataGridView1.Rows[rowIndex].Cells[1].Value = rate;
                dataGridView1.Rows[rowIndex].Cells[2].Value = quantity;
                dataGridView1.Rows[rowIndex].Cells[3].Value = 0; // default return quantity
                dataGridView1.Rows[rowIndex].Cells[4].Value = amount;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating grid: " + ex.Message, "OmniMart360", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == dataGridView1.Columns["ReturnQuantity"].Index)
        {
            int returnQuantity = 0;
            if (dataGridView1[3, e.RowIndex].Value != null &&
                int.TryParse(dataGridView1[3, e.RowIndex].Value.ToString(), out returnQuantity))
            {
                decimal rate = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                int soldQuantity = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);

                if (returnQuantity >= 0 && returnQuantity <= soldQuantity)
                {
                    int newQuantity = soldQuantity - returnQuantity;
                    decimal newAmount = rate * newQuantity;
                    dataGridView1.Rows[e.RowIndex].Cells[4].Value = newAmount;
                    btnUpdate.Visible = true;
                }
                else
                {
                    MessageBox.Show("Please enter a valid return quantity.", "OmniMart360", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridView1.Rows[e.RowIndex].Cells[3].Value = 0;
                }
            }
        }
    }

        private void updateT(decimal productAmount) 
        {
            oldTotalBill = Convert.ToDecimal(textBill.Text);
            oldTotalBill += productAmount;
            if (oldTotalBill == 0)
                lblPaidAmt.Text = "0";
            textBill.Text = oldTotalBill.ToString();
            //trmp disc for calculation
            int disc = 0;
            disc=Convert.ToInt32(textDiscount.Text);
            decimal newTotalAmount = oldTotalBill - (oldTotalBill * disc / 100);
            textTotal.Text = newTotalAmount.ToString();
            if (newTotalAmount > oldPaidAmount)
            {
                oldRemainingAmount = newTotalAmount - oldPaidAmount;
                txtRemAmt.Text = oldRemainingAmount.ToString();
            }
            else if (newTotalAmount < oldPaidAmount)
            {
                oldRemainingAmount = 0;
                txtRemAmt.Text = oldRemainingAmount.ToString();
                MessageBox.Show("Return: "+(oldPaidAmount-newTotalAmount));
                oldPaidAmount = newTotalAmount;
                lblPaidAmt.Text = oldPaidAmount.ToString();
                txtPaidAmt.ReadOnly = true;

            }

        }
        /*
         *  Handles Update Button 
         */
        private void btnUpdate_Click(object sender, EventArgs e)
        { 

            c1.Open();
            try
            {
                int invNo = Convert.ToInt32(invoiceNo.Text);
                string item;
                int soldQuantity = 0;    //get from table
                int returnQuantity = 0;       // get from table
                decimal finalTotalBill=Convert.ToDecimal(textBill.Text);
                decimal finalTotalAmount = Convert.ToDecimal(textTotal.Text);
                decimal newPaidAmount = Convert.ToDecimal(txtPaidAmt.Text)+oldPaidAmount;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    if (row.IsNewRow)
                    {
                        continue;
                    }
                    item = row.Cells[0].Value.ToString();
                    soldQuantity = Convert.ToInt32(row.Cells[2].Value);
                    if (row.Cells[3].Value == null)
                        returnQuantity = 0;
                    else
                        returnQuantity = Convert.ToInt32(row.Cells[3].Value);
                    int newQuantity =soldQuantity - returnQuantity;
                    // here if new quantity is 0 then try to delete from sold items
                    string q = "update solditems set quantity="+ newQuantity +" where itemname='"+item+"' and invoiceno="+invNo+";";
                    cmd = new MySqlCommand(q, c1);
                    int r = cmd.ExecuteNonQuery();
                    if (r > 0)
                    {
                        q = "select qty from inventory where name='"+item+"';";
                        cmd = new MySqlCommand(q, c1 );
                        //initial database quatity
                        int dbqty = 0;
                        reader = cmd.ExecuteReader();
                        if(reader.Read())
                        {
                            dbqty= Convert.ToInt32(reader["qty"].ToString());    
                        }
                        reader.Close();
                        dbqty += returnQuantity;
                        string p = "update inventory set qty=" + dbqty + " where name ='" + item + "';";
                        cmd = new MySqlCommand(p, c1);
                        cmd.ExecuteNonQuery();
                    }
                    
                }
                
                string query = "update receipts set bill="+finalTotalBill+", totalamount="+finalTotalAmount+" where invoiceno="+invNo+";";
                cmd= new MySqlCommand(query, c1);
                int res = cmd.ExecuteNonQuery();

                if(res>0)
                {
                    decimal newRemAmt = Convert.ToDecimal(txtRemAmt.Text);
                    

                    query = "update receivable set billamount =" + finalTotalBill + ",paid=" + newPaidAmount + ", remaining=" + newRemAmt + " where invoiceno=" + invNo + ";";
                    cmd = new MySqlCommand(query, c1);
                    cmd.ExecuteNonQuery();
                }
                
                MessageBox.Show("Receipt Updated Successfully...", "OmniMart360", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                txtPaidAmt.Text = "0";
                c1.Close();
                showDataItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex, "OmniMart360", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                reader.Close();
                c1.Close();
            }   
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /*
         *  Handles Print Button
         */
        private void btnPrint_Click(object sender, EventArgs e)
        {
            printReceipt();
        }

        private void printReceipt()
        {
            // Gather data from the form
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("Items", typeof(string));
            dt.Columns.Add("Rate", typeof(decimal));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("Amount", typeof(decimal));

            foreach (DataGridViewRow dgv in dataGridView1.Rows)
            {
                if (dgv.IsNewRow) continue;
                string item = dgv.Cells[0].Value != null ? dgv.Cells[0].Value.ToString() : "";
                decimal rate = dgv.Cells[1].Value != null ? Convert.ToDecimal(dgv.Cells[1].Value) : 0;
                int qty = dgv.Cells[2].Value != null ? Convert.ToInt32(dgv.Cells[2].Value) : 0;
                decimal amount = dgv.Cells[4].Value != null ? Convert.ToDecimal(dgv.Cells[4].Value) : 0;
                dt.Rows.Add(item, rate, qty, amount);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("salesItems.xml");

            // Get values from controls
            string customerName = lblName.Text;
            long mobile = 0;
            long.TryParse(lblMobile.Text, out mobile);
            decimal bill = 0;
            decimal.TryParse(textBill.Text, out bill);
            int disc = 0;
            int.TryParse(textDiscount.Text, out disc);
            decimal total = 0;
            decimal.TryParse(textTotal.Text, out total);

            SaleReceiptView sl = new SaleReceiptView(invNo, customerName, mobile, bill, disc, total, ds);
            sl.ShowDialog();
        }

        

        /*
         *  Handle and Calculate remaining amount 
         */
        private void txtPaidAmt_TextChanged(object sender, EventArgs e)
        {
            btnUpdate.Visible = true;
            decimal enteredPaidAmt;
            try
            {
                // If input is empty, reset to original remaining amount
                if (string.IsNullOrWhiteSpace(txtPaidAmt.Text))
                {
                    txtRemAmt.Text = oldRemainingAmount.ToString("0.00");
                    return;
                }

                // Try to parse the entered amount
                if (decimal.TryParse(txtPaidAmt.Text, out enteredPaidAmt))
                {
                    // Check if entered amount exceeds remaining
                    if (enteredPaidAmt > oldRemainingAmount)
                    {
                        MessageBox.Show("Entered amount exceeds remaining balance.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPaidAmt.Text = "";
                        txtRemAmt.Text = oldRemainingAmount.ToString("0.00");
                        return;
                    }

                    // Update remaining amount dynamically
                    decimal newRemaining = oldRemainingAmount - enteredPaidAmt;
                    txtRemAmt.Text = newRemaining.ToString("0.00");
                }
                else
                {
                    // Invalid input: reset to original remaining amount
                    txtRemAmt.Text = oldRemainingAmount.ToString("0.00");
                }
            }
            catch
            {
                txtRemAmt.Text = oldRemainingAmount.ToString("0.00");
            }

        }
    }
}
