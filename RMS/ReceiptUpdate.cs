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
        
        
        private decimal oldTotalBill=0;
        private decimal oldTotalAmount=0;
        decimal oldPaidAmount = 0;
        decimal oldRemainingAmount = 0;
        
        
 

        // Parameterised Constructor
        public ReceiptUpdate(int invNo)
        {
            this.invNo = invNo;
            InitializeComponent();
        }

        private void ReceiptUpdate_Load(object sender, EventArgs e)
        {
            txtPaidAmt.Text = "0";
            
            c1 = DBConnection.GetConnection();
            
            showDataItems();
        }
        /*
         *  Loads Data 
         */
        private void showDataItems()
        {
            if (btnUpdate.Visible) { 
                btnUpdate.Visible = false;  
            }
            // setting label to  invNo
            invoiceNo.Text=invNo.ToString();
            
            c1.Open();
            try
            {
                //fetching sold items for invoice
                string q = "select itemname,sellingrate,quantity from solditems where invoiceno=" + invNo;
                
                da = new MySqlDataAdapter(q, c1);
                t = new DataTable();
                da.Fill(t);
                //sending to render
                PopulateDataGridView(t);

                //getting customer details
                // issue seperate receivable and direct customers query
                string p= "SELECT r.date, r.cust_name, r.cust_mobile, r.bill, r.discount, r.totalamount, rc.paid, rc.remaining,rc.billamount FROM receipts r left JOIN receivable rc ON r.invoiceno = rc.invoiceno WHERE r.invoiceno = " + invNo+";";

                cmd = new MySqlCommand(p, c1);

                reader = cmd.ExecuteReader();
                if (reader.Read()) {
                    
                    //receipt date
                    lablDate.Text = reader["date"].ToString();
                    
                    //customer name
                    lblName.Text = reader["cust_name"].ToString();
                    
                    //customer mobile
                    lblMobile.Text = reader["cust_mobile"].ToString();
                    
                    // old total bill
                    textBill.Text= reader["bill"].ToString();
                    
                    // discount on old total bill
                    textDiscount.Text = reader["discount"].ToString(); 
                    
                    //old total amount
                    textTotal.Text = reader["totalamount"].ToString();
                    
                    // old paid amount
                    if (reader["paid"] == DBNull.Value)
                    {
                        lblPaidAmt.Text = reader["totalamount"].ToString();
                        oldPaidAmount = 0;
                    }
                    else
                    {
                        oldPaidAmount = Convert.ToDecimal(reader["paid"].ToString());
                        lblPaidAmt.Text = reader["paid"].ToString();
                    }
                    // old remaining amount
                    if (reader["remaining"] == DBNull.Value)
                    {
                        oldRemainingAmount = 0;
                        txtRemAmt.Text ="0";
                        txtRemAmt.ReadOnly = true;
                    }
                    else
                    {
                        oldRemainingAmount = Convert.ToDecimal(reader["remaining"].ToString());
                        txtRemAmt.Text = reader["remaining"].ToString();
                    }

                    if (reader["billamount"] == DBNull.Value)
                    {
                        oldTotalAmount = 0;
                    }
                    else
                    {
                        oldTotalAmount = Convert.ToDecimal(reader["billamount"].ToString());
                        
                    }
                    
                    //get into varibales
                                       
                    oldTotalBill = Convert.ToDecimal(reader["bill"].ToString());
                    if (!(Convert.ToDecimal(txtRemAmt.Text) > 0))
                        txtPaidAmt.ReadOnly = true;
                    
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex,"OmniMart360",MessageBoxButtons.OK,MessageBoxIcon.Error);
                
            }
            finally
            {
                reader.Close();
                if (c1.State == ConnectionState.Open)
                    c1.Close();
            }
        }
        /*
         *  Renders Sold Items 
         */
        private void PopulateDataGridView(DataTable table)
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow row in table.Rows)
            {
                int rowIndex = dataGridView1.Rows.Add();

                dataGridView1.Rows[rowIndex].Cells[0].Value = row["itemname"];
                dataGridView1.Rows[rowIndex].Cells[1].Value = row["sellingrate"];
                // setting purchase rate to calculate productAmount
                decimal sellingrate = Convert.ToDecimal(row["sellingrate"].ToString());
                dataGridView1.Rows[rowIndex].Cells[2].Value = row["quantity"];
                int oldQuantity = Convert.ToInt32(row["quantity"].ToString());

                // calculated productAmount
                dataGridView1.Rows[rowIndex].Cells[4].Value = sellingrate*oldQuantity;
           
            }
        }
        /*
         *  Retun Quantity cell edit change handler 
         *  handles when user return any products and recalculate amount
         */
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // checking user is changing return quantity
            if (e.ColumnIndex == dataGridView1.Columns[3].Index)
            {
                //get return quantity of returned product
                int returnQuantity=0;

                if (dataGridView1[3, e.RowIndex].Value != null && int.TryParse(dataGridView1[3, e.RowIndex].Value.ToString(), out returnQuantity))
                {
                    returnQuantity = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                }
                
                // get its rate from cell
                decimal rate = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                
                // get its sold quantity
                int soldQuantity = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);

                // checking return quantity is valid 
                if (returnQuantity>=0 && returnQuantity <= soldQuantity)
                {
                    // reseting products quantity
                    soldQuantity -= returnQuantity;

                    //getting its oldSoldAmount
                    decimal productAmount = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                    // Calculate the productAmount
                    decimal newSoldAmount = rate * soldQuantity;
                    productAmount = newSoldAmount - productAmount;


                    updateT(productAmount);
                    // Update the Amount cell with the calculated value
                    dataGridView1.Rows[e.RowIndex].Cells[4].Value = newSoldAmount;
                    btnUpdate.Visible = true;
                }
                else
                {
                    MessageBox.Show("Please Enter valid Return Quantity" +
                        "","OmniMart360",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
