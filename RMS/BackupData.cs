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
                openDlg.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
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
            string sqlFile = Path.Combine(backupFolderPath, string.Format("storemanagementsystem_{0}.sql", timestamp));
            string zipFileTemp = Path.Combine(backupFolderPath, string.Format("storemanagementsystem_backup_{0}.zip", timestamp));
            string zipFileFinal = Path.Combine(backupFolderPath, string.Format("storemanagementsystem_backup_{0}.bak", timestamp));

            string dumpCmd = string.Format("mysqldump -u root -proot storemanagementsystem > \"{0}\"", sqlFile);
            string zipCmd = string.Format("powershell Compress-Archive -Path \"{0}\" -DestinationPath \"{1}\"", sqlFile, zipFileTemp);

            try
            {
                // Step 1: Run mysqldump
                ProcessStartInfo dumpPsi = new ProcessStartInfo("cmd.exe", "/c " + dumpCmd);
                dumpPsi.UseShellExecute = false;
                dumpPsi.CreateNoWindow = true;

                using (Process dumpProcess = Process.Start(dumpPsi))
                {
                    dumpProcess.WaitForExit();
                }

                // Step 2: Zip the .sql file
                ProcessStartInfo zipPsi = new ProcessStartInfo("cmd.exe", "/c " + zipCmd);
                zipPsi.UseShellExecute = false;
                zipPsi.CreateNoWindow = true;

                using (Process zipProcess = Process.Start(zipPsi))
                {
                    zipProcess.WaitForExit();
                }

                // Step 3: Rename .zip to .bak
                if (File.Exists(zipFileTemp))
                {
                    File.Move(zipFileTemp, zipFileFinal);
                }
                else
                {
                    MessageBox.Show("Zip file was not created. Please check PowerShell availability.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Step 4: Delete the original .sql file
                if (File.Exists(sqlFile))
                {
                    File.Delete(sqlFile);
                }

                MessageBox.Show("Backup created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            // Create a temporary folder to extract the .sql file
            string tempFolder = Path.Combine(Path.GetTempPath(), "DBRestore_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempFolder);

            try
            {
                // Step 1: Copy .bak to .zip temporarily
                string tempZipFile = Path.Combine(tempFolder, "temp_restore.zip");
                File.Copy(restoreFilePath, tempZipFile, true);

                // Step 2: Unzip the .zip file (originally .bak)
                string unzipCmd = string.Format("powershell Expand-Archive -Path \"{0}\" -DestinationPath \"{1}\"", tempZipFile, tempFolder);
                ProcessStartInfo unzipPsi = new ProcessStartInfo("cmd.exe", "/c " + unzipCmd);
                unzipPsi.UseShellExecute = false;
                unzipPsi.CreateNoWindow = true;

                using (Process unzipProcess = Process.Start(unzipPsi))
                {
                    unzipProcess.WaitForExit();
                }

                // Step 3: Find the .sql file inside the extracted folder
                string[] sqlFiles = Directory.GetFiles(tempFolder, "*.sql");
                if (sqlFiles.Length == 0)
                {
                    MessageBox.Show("No .sql file found in the backup archive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string sqlFile = sqlFiles[0];

                // Step 4: Restore the .sql file using MySQL
                string restoreCmd = string.Format("mysql -u root -proot storemanagementsystem < \"{0}\"", sqlFile);
                ProcessStartInfo restorePsi = new ProcessStartInfo("cmd.exe", "/c " + restoreCmd);
                restorePsi.UseShellExecute = false;
                restorePsi.CreateNoWindow = true;

                using (Process restoreProcess = Process.Start(restorePsi))
                {
                    restoreProcess.WaitForExit();
                }

                MessageBox.Show("Restore completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Restore failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Clean up temporary folder
                if (Directory.Exists(tempFolder))
                {
                    try
                    {
                        Directory.Delete(tempFolder, true);
                    }
                    catch
                    {
                        // Ignore cleanup errors
                    }
                }
            }
        }




        
    }
}
