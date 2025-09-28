using System;

using System.Drawing;

using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace RMS
{
    public partial class HomePage : Form
    {
        private Form activeForm;

        public HomePage()
        {
            InitializeComponent();
            //this.Opacity = 0;
           
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
        }
        private void openChildForm(Form childForm, String title)
        {
            try
            {
                if (activeForm != null)
                {
                    activeForm.Close();
                }
                activeForm = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                this.panelDesktop.Controls.Add(childForm);
                this.panelDesktop.Tag = childForm;
                childForm.BringToFront();
                childForm.Show();
                labelHeading.Text = title;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please wait..","OmniMart360");
            }
        }
     
        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HomePage_Load(object sender, EventArgs e)
        {         
            openChildForm(new SalePage(), "SALES");
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            openChildForm(new PurchasePage(),"PURCHASE");
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            openChildForm(new SalePage(), "SALES");
        }


        private void btnInventory_Click(object sender, EventArgs e)
        {
            openChildForm(new InventoryPage(), "INVENTORY");
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            openChildForm(new SupplierPage(), "SUPPLIERS");
        }

        private void btnReceipts_Click(object sender, EventArgs e)
        {
            openChildForm(new ReceiiptsPage(), "RECEIPTS");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openChildForm(new Profile(), "PROFILE");
        }

        private void btnQuit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button6_Click_1(object sender, EventArgs e)
        {
            openChildForm(new MainPage(), "Dashboard");
        }

        private void btnReceivable_Click_1(object sender, EventArgs e)
        {
            openChildForm(new Receivable(), "Receivables");
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupData obj = new BackupData();

            obj.ShowDialog();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help obj = new Help();
            obj.ShowDialog();
        }


    }
}
