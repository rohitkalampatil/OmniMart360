using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

using System.Diagnostics;
using System.IO;

namespace RMS
{
    public partial class BackupData : Form
    {
        private Timer dateTimeTimer;
        private string backupFolderPath = "";
        private string restoreFilePath = "";

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
            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                folderDlg.Description = "Select folder to save backup file";
                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    backupFolderPath = folderDlg.SelectedPath;
                    txtBackupPath.Text = backupFolderPath;
                }
            }
        }

        private void btnRestFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.Filter = "SQL Files (*.sql)|*.sql|All Files (*.*)|*.*";
                openDlg.Title = "Select backup file to restore";
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    restoreFilePath = openDlg.FileName;
                    txtRestorePath.Text = restoreFilePath;
                }
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(backupFolderPath))
            {
                MessageBox.Show("Please select a folder to save the backup.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string backupFile = Path.Combine(backupFolderPath, "storemanagementsystem_backup_" + timestamp + ".sql");

            string cmd = "mysqldump -u root -proot storemanagementsystem > \"" + backupFile + "\"";


            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c " + cmd);
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                Process process = Process.Start(psi);
                process.WaitForExit();

                MessageBox.Show("Backup completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Backup failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(restoreFilePath))
            {
                MessageBox.Show("Please select a backup file to restore.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string cmd = "mysql -u root -p root storemanagementsystem < \"" + restoreFilePath + "\"";


            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c " + cmd);
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                Process process = Process.Start(psi);
                process.WaitForExit();

                MessageBox.Show("Restore completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Restore failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
