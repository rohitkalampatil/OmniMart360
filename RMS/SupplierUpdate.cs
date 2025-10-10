using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayer;
using Models;

namespace RMS
{
    public partial class SupplierUpdate : Form
    {
        SupplierBLL bl = new SupplierBLL();
        private string supplierEmail;
        public SupplierUpdate(string supplierEmail)
        {
            InitializeComponent();
            this.supplierEmail = supplierEmail;
        }

        private void SupplierUpdate_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            SupplierModel supplier = bl.getSupplierByEmail(supplierEmail);
            if (supplier != null)
            {
                textName.Text = supplier.SupplierName;
                textEmail.Text = supplier.Email;
                textMobile.Text = supplier.Mobile.ToString();
                textAddress.Text = supplier.Address;
                textBank.Text = supplier.BankAcc;
                textIfsc.Text = supplier.IFSC;
                textPan.Text = supplier.PAN;
                textGstin.Text = supplier.GSTIN;
            }
            else
            {
                MessageBox.Show("पुरवठादाराची माहिती सापडली नाही.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /*
         * Code Save Button
         */
        private void button2_Click(object sender, EventArgs e)
        {
            SupplierModel supplier = new SupplierModel
            {
                SupplierName = textName.Text,
                Mobile = Convert.ToInt64(textMobile.Text),
                Email = textEmail.Text,
                Address = textAddress.Text,
                BankAcc = textBank.Text,
                IFSC = textIfsc.Text,
                PAN = textPan.Text,
                GSTIN = textGstin.Text

            };
            string msg = bl.UpdateSupplier(supplier);
            MessageBox.Show(msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
        }
        /*
         * Code Clear Button
         */
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       

    }
}
