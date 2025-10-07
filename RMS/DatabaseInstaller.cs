using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace RMS
{
    public class DatabaseInstaller
    {
        public static void InstallDatabase()
        {
            string mysqlUser = "root";
            string mysqlPassword = "root"; // <-- Replace with your actual MySQL password
            string sqlFilePath = Application.StartupPath + "\\storemanagementsystem.sql";

            if (!File.Exists(sqlFilePath))
            {
                MessageBox.Show("SQL file not found: " + sqlFilePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string cmd = "mysql -u " + mysqlUser + " -p" + mysqlPassword + " < \"" + sqlFilePath + "\"";

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c " + cmd);
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                Process process = Process.Start(psi);
                process.WaitForExit();

                MessageBox.Show("Database installed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database installation failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
