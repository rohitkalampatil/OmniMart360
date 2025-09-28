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

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
            finally
            {
                c1.Close();
            }

        }

        private void supportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

       
    }
}
