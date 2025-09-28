using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RMS
{
    public partial class SaleReceiptView : Form
    {
        string customerName;
        long mobile;
        decimal bill;
        decimal total;
        int disc;
        int invNo;
        DataSet ds;

        public SaleReceiptView(int invNo,string customerName,long mobile,decimal bill,int disc,decimal total,DataSet ds)
        {
            this.customerName = customerName;
            this.mobile = mobile;
            this.invNo = invNo;
            this.bill = bill;
            this.disc = disc;
            this.total = total;
            this.ds = ds;
            InitializeComponent();
        }

        private void SaleReceiptView_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.Refresh();

            SaleReceipt cr = new SaleReceipt();
            cr.SetDataSource(ds);
            cr.SetParameterValue("name", customerName);
            cr.SetParameterValue("mobile", mobile);
            cr.SetParameterValue("discount", disc);
            cr.SetParameterValue("total", bill);
            cr.SetParameterValue("totalbill", total);

            cr.SetParameterValue("invoiceNo", invNo);
            crystalReportViewer1.ReportSource = cr;
           
        }
    }
}
