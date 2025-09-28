using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace RMS
{
    public partial class MainPage : Form
    {

        MySqlConnection c1;

        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            kpiPurchase.Parent = picKpiSales;
            kpisales.Parent = pickpiProfit;
            kpiProfit.Parent = picKpiMargin;
            kpiIReceivable.Parent = picKpiItems;
            c1 = DBConnection.GetConnection();
            try
            {
                c1.Open();

                // Query 1: Total Purchase
                string queryPurchase = "SELECT SUM(total_bill) FROM purchase;";
                MySqlCommand cmdPurchase = new MySqlCommand(queryPurchase, c1);
                object resultPurchase = cmdPurchase.ExecuteScalar();
                decimal totalPurchase = resultPurchase != DBNull.Value ? Convert.ToDecimal(resultPurchase) : 0;
                kpiPurchase.Text = FormatIndianCurrency(totalPurchase);

                // Query 2: Total Sales
                string querySales = "SELECT SUM(totalamount) FROM receipts;";
                MySqlCommand cmdSales = new MySqlCommand(querySales, c1);
                object resultSales = cmdSales.ExecuteScalar();
                decimal totalSales = resultSales != DBNull.Value ? Convert.ToDecimal(resultSales) : 0;
                kpisales.Text = FormatIndianCurrency(totalSales);

                // Query 3: Total Profit
                string queryProfit = @"SELECT 
                                (SELECT SUM(totalamount) FROM receipts) - 
                                (SELECT SUM(quantity * purchaserate) FROM solditems) AS Profit;";
                MySqlCommand cmdProfit = new MySqlCommand(queryProfit, c1);
                object resultProfit = cmdProfit.ExecuteScalar();
                decimal profit = resultProfit != DBNull.Value ? Convert.ToDecimal(resultProfit) : 0;
                kpiProfit.Text = FormatIndianCurrency(profit);

                // Query 4: Total Receivable
                string queryReceivable = "select sum(remaining) from receivable;";
                MySqlCommand cmdReceivable = new MySqlCommand(queryReceivable, c1);
                object resultReceivable = cmdReceivable.ExecuteScalar();
                decimal receivable = resultProfit != DBNull.Value ? Convert.ToDecimal(resultReceivable) : 0;
                kpiIReceivable.Text = FormatIndianCurrency(receivable);
                

                /*
                 * Add Chart Here or create function
                 */
                string queryChart = "select itemname,sum(sellingrate) as sales from solditems group by itemname;"; 
                DataTable dt = new DataTable(); 
                MySqlDataAdapter da = new MySqlDataAdapter(queryChart, c1); 
                da.Fill(dt); 
                chart1.DataSource = dt; 
                chart1.Series["sales"].XValueMember = "itemname"; 
                chart1.Series["sales"].YValueMembers = "sales";

                chart1.Legends[0].Enabled = true;
                chart1.Legends[0].Docking = Docking.Bottom;

                chart1.Titles.Add("Sales by Product");







            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                c1.Close();
            }

        }

        private void supportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public static string FormatIndianCurrency(decimal number)
        {
            if (number >= 10000000)
                return "₹" + (number / 10000000m).ToString("0.#") + " Cr";
            else if (number >= 100000)
                return "₹" + (number / 100000m).ToString("0.#") + " L";
            else if (number >= 1000)
                return "₹" + (number / 1000m).ToString("0.#") + " K";
            else
                return "₹" + number.ToString("0");
        }




    }
}
