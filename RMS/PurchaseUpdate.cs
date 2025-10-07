using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace RMS
{
    public partial class PurchaseUpdate : Form
    {
        
        MySqlCommand cmd = null;
        MySqlConnection conn = null;    
        MySqlDataAdapter da = null;
        DataTable dt = null;
        MySqlDataReader dr = null;
        String query = "";

        // Form  data members
        string supName = "";
        int invNo = 0;
        decimal paidAmount = 0;
        decimal totalAmount = 0;
        decimal remAmount = 0;
        decimal bill = 0;



        public PurchaseUpdate(int invNo)
        {
            InitializeComponent();
            this.invNo = invNo;

        }

 

        /* 
         * Database connectivity
         */
        private void PurchaseUpdate_Load(object sender, EventArgs e)
        {
            // DB connectionn string
            string connectionString = "server=localhost;database=storemanagementsystem;uid=root;password=root";
            conn = new MySqlConnection(connectionString);   
            // Database connection successfull
            label1.Text=invNo.ToString();
            
            loadInvoiceData(invNo);
            
        }
        /*
         * Loading invoice Data 
         */
        private void loadInvoiceData(int invoiceNumber)
        {
            conn.Open();
            try
            {
                // getting purchased items list
                query = "select * from purchasesitems  where invoiceno=" + invoiceNumber;
                da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                PopulateDataGridView(dt);

                //getting supplier information
                query = "select * from purchase where invoiceno="+ invoiceNumber;
                cmd = new MySqlCommand(query, conn);
                dr = cmd.ExecuteReader();
                if (dr.Read()) 
                {
                    supplierName.Text = dr["supplier_name"].ToString().ToUpper();
                   // supplierEmail.Text = dr[""].ToString();
                    supplierMobile.Text = dr["supplier_contact"].ToString();
                    //supplierAddress.Text = dr[""].ToString();
                    txtPayMethod.Text= dr["pay_method"].ToString().ToUpper();
                    txtAmount.Text = dr["total_bill"].ToString();
                    txtPaidAmount.Text = dr["paid_amount"].ToString();
                    txtRemainingAmount.Text = dr["remaining_amount"].ToString();
                    txtTotalAmount.Text = dr["total_bill"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"\n\nPlease Restart Application", "RMS - Retail Management System 1.0", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dr.Close();
                conn.Close();
            }
        }
        /*
         * Populating PurchasedItems List 
         */
        private void PopulateDataGridView(DataTable table)
        {
            dataGridView1.Rows.Clear();
         
            foreach (DataRow row in table.Rows)
            {
                int rowIndex = dataGridView1.Rows.Add();

                dataGridView1.Rows[rowIndex].Cells[0].Value = row["itemname"];
                dataGridView1.Rows[rowIndex].Cells[1].Value = row["purchase_rate"];
                decimal purchase = Convert.ToDecimal(row["purchase_rate"].ToString());
                dataGridView1.Rows[rowIndex].Cells[2].Value = row["quantity"];
                int quantity = Convert.ToInt32(row["quantity"].ToString());
                dataGridView1.Rows[rowIndex].Cells[3].Value = row["selling_rate"];
   
                dataGridView1.Rows[rowIndex].Cells[4].Value = purchase * quantity;

            }
        }
       

        /*
         * calculate and update Amount cell value
         */
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
       
            if (e.ColumnIndex >=1 && e.ColumnIndex <=3)
            {
                decimal rate, quantity, selling;
                if (dataGridView1[1, e.RowIndex].Value != null &&
    dataGridView1[2, e.RowIndex].Value != null &&
    dataGridView1[3, e.RowIndex].Value != null &&
    decimal.TryParse(dataGridView1[1, e.RowIndex].Value.ToString(), out rate) &&
    decimal.TryParse(dataGridView1[2, e.RowIndex].Value.ToString(), out quantity) &&
    decimal.TryParse(dataGridView1[3, e.RowIndex].Value.ToString(), out selling))

                {
                    rate = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    quantity = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    decimal oldamount = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[4].Value);

                    decimal newamount = rate * quantity;
                    decimal amt = newamount - oldamount;

                    updateT(amt);
                    dataGridView1.Rows[e.RowIndex].Cells[4].Value = newamount;
                }

            }

        }
        /* 
        * Updates Amount, Paid amount, remaining Amount and Total Amount value
        */
        private void updateT(decimal a)
        {
            totalAmount = Convert.ToDecimal(txtTotalAmount.Text.ToString());
            totalAmount += a;
            paidAmount = Convert.ToDecimal(txtPaidAmount.Text.ToString());
            remAmount=totalAmount - paidAmount;

            txtRemainingAmount.Text = remAmount.ToString();
            txtAmount.Text = totalAmount.ToString();    
            txtTotalAmount.Text = totalAmount.ToString();
        }
        /* 
         * Data validation
         */
        private void supplierName_TextChanged(object sender, EventArgs e)
        {
            supplierName.Text= Regex.Replace(supplierName.Text, "[^A-Za-z ]", "");
        }

        private void supplierMobile_TextChanged(object sender, EventArgs e)
        {
            supplierMobile.Text = Regex.Replace(supplierMobile.Text,"[^0-9]", "");
        }





        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            txtPaidAmount.Text = Regex.Replace(txtPaidAmount.Text, "[^0-9.]", "");
        }
     

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /*
         * Button update 
         */
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            conn.Open();
            try 
            {
                int invNo = Convert.ToInt32(label1.Text);
                /*
                 * if item present in p items update it in both place
                 * else insert new in both place
                 */
                string item;
                decimal prate=0;
                decimal srate=0;
                int N = 0;
                int I = 0,O=0;
                bool NqErr = false;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow)
                    {
                        // newRow += "\n" + row.Cells[0].Value;
                        continue;
                    }
                    item = row.Cells[0].Value.ToString();
                    if (row.Cells[2].Value == null)
                    {
                        N = 0;
                        MessageBox.Show("Enter quantity");
                        break;
                    }
                    else
                    {
                        N = Convert.ToInt32(row.Cells[2].Value.ToString());
                    }

                    // inventory quantity
                    query = "select qty from inventory where name='" + item + "';";
                    cmd = new MySqlCommand(query, conn);
                    
                    //I = Convert.ToInt32(cmd.ExecuteScalar());
                    dr= cmd.ExecuteReader();
                    if (dr.Read()) 
                    {
                        I = Convert.ToInt32(dr["qty"].ToString());
                        dr.Close();
                        query = "select quantity  from  purchasesitems where itemname='" + item + "' and invoiceno=" + invNo;
                        cmd = new MySqlCommand(query, conn);
                        O = Convert.ToInt32(cmd.ExecuteScalar());

                        if (N >= O - I && I >= O - N)
                        {
                            int qty = O - N;
                            prate = Convert.ToDecimal(row.Cells[1].Value.ToString());
                            srate = Convert.ToDecimal(row.Cells[3].Value.ToString());

                            query = "update purchasesitems set  quantity=" + N + ", purchase_rate=" + prate + ", selling_rate=" + srate + " where itemname='" + item + "' and invoiceno=" + invNo;
                            cmd = new MySqlCommand(query, conn);
                            int r = cmd.ExecuteNonQuery();
                            if (r > 0)
                            {
                                I -= qty;
                                // here update item based on old price 
                                // like if same name item is present then update whoes prate or s rate is diff
                                query = "update inventory set qty=" + I + ", purchase_rate=" + prate + ",selling_rate=" + srate + " where name='" + item + "';";
                                cmd = new MySqlCommand(query, conn);
                                cmd.ExecuteNonQuery();

                            }
                        }
                        else
                        {
                            NqErr = true;
                            break;

                        }
                    }
                    else
                    {
                        dr.Close();
                        prate = Convert.ToDecimal(row.Cells[1].Value.ToString());
                        srate = Convert.ToDecimal(row.Cells[3].Value.ToString());

                        //insert new row into purchasesitems  table and inventory table
                        query = "insert into purchasesitems(invoiceno,itemname,quantity,purchase_rate,selling_rate) values(@invoiceNumber,@item,@quantity,@purchaserate,@sellingrate)";
                        cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@invoiceNumber", invNo);
                        cmd.Parameters.AddWithValue("@item", item);
                        cmd.Parameters.AddWithValue("@quantity", N);
                        cmd.Parameters.AddWithValue("@purchaserate", prate);
                        cmd.Parameters.AddWithValue("@sellingrate", srate);
                        cmd.ExecuteNonQuery();

                        query = "insert into inventory(name,qty,purchase_rate,selling_rate) values(@itemname,@qty,@purrate,@salerate);";
                        cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@itemname", item);
                        cmd.Parameters.AddWithValue("@qty", N);
                        cmd.Parameters.AddWithValue("@purrate", prate);

                        cmd.Parameters.AddWithValue("@salerate", srate);

                        cmd.ExecuteNonQuery();

                    }
                }

                 if (NqErr)
                 {
                     MessageBox.Show("New quantity should be greater  than :"+(O-I));
                 }
                 else
                 {
                     decimal totalAmount = Convert.ToDecimal(txtAmount.Text.ToString());
                     decimal paidAmount = Convert.ToDecimal(txtPaidAmount.Text.ToString());
                     decimal remAmount = totalAmount - paidAmount;
                     query = "update purchase set total_bill=" + totalAmount + ", paid_amount=" + paidAmount + ", remaining_amount=" + remAmount + " where invoiceno=" + invNo;
                     cmd = new MySqlCommand(query, conn);
                     cmd.ExecuteNonQuery();
                     conn.Close();
                     
                     MessageBox.Show("Updated Successfully....", "RMS - Retail Management System 1.0", MessageBoxButtons.OK);
                     loadInvoiceData(invNo);
                 }
                dr.Close();
               
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Please try Later... "+ex.StackTrace);
            }
            finally 
            {
                conn.Close(); 
            }
        }

       
    }
}
