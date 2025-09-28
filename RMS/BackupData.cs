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
    public partial class BackupData : Form
    {
        private Timer dateTimeTimer;
        public BackupData()
        {
            InitializeComponent();
        }

        private void BackupData_Load(object sender, EventArgs e)
        {
            dateTimeTimer = new Timer();
            dateTimeTimer.Interval = 60000; // 60,000 ms = 1 minute
            dateTimeTimer.Tick += DateTimeTimer_Tick;
            dateTimeTimer.Start();

            // Initial update
            UpdateDateTimeLabel();
        }

        private void DateTimeTimer_Tick(object sender, EventArgs e)
        {
            UpdateDateTimeLabel();
        }

        private void UpdateDateTimeLabel()
        {
            lblTime.Text = DateTime.Now.ToString("dd MMMM yyyy, hh:mm tt");
        }

        private void cmbOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOption.SelectedIndex == 0)
            {
                txtBackupPath.Enabled = true;
                btnBackFile.Enabled = true;
                btnBackup.Enabled = true;

                txtRestorePath.Enabled = false;
                btnRestFile.Enabled =false;
                btnRestore.Enabled = false;
            }
            else if(cmbOption.SelectedIndex==1)
            {
                txtBackupPath.Enabled = false;
                btnBackFile.Enabled = false;
                btnBackup.Enabled = false;

                txtRestorePath.Enabled = true;
                btnRestFile.Enabled = true;
                btnRestore.Enabled = true;
            }
        }

        private void btnBackFile_Click(object sender, EventArgs e)
        {

        }

        private void btnRestFile_Click(object sender, EventArgs e)
        {

        }

        private void btnBackup_Click(object sender, EventArgs e)
        {

        }

        private void btnRestore_Click(object sender, EventArgs e)
        {

        }

        
    }
}
