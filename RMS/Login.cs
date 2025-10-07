using System;

using System.Data;

using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RMS
{
    public partial class Login : Form
    {
        MySqlCommand command;
        MySqlConnection connection;

        public Login()
        {
            InitializeComponent();
            this.KeyPreview = true; // Set KeyPreview property to true
            textPassword.Focus();
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Programmatically invoke the click event of the login button
                button1.PerformClick();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //cross button
            this.Close();
        }
        // setup DB connection
        private void Login_Load(object sender, EventArgs e)
        { 

            connection = DBConnection.GetConnection();
            /*
             * below code will be used to make atabase and mysql installation automate 
             * also it will not create or execute below code every time if databse and software doesnt exist
             * or executes only once at the time of installation
             */
            //DatabaseInstaller.InstallDatabase();
        }

        //Register Link
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // replace with domain or website link
            System.Diagnostics.Process.Start("www.kodebenchai.com");
        }

        //handle login
        private void button1_Click(object sender, EventArgs e)
        {
            if (textPassword.Text == "")
            {
                MessageBox.Show("Please enter your password to continue", "OmniMart360 version 1.0", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textPassword.Focus();
                return;
            }

            connection.Open();
            try
            {
                // Get user record matching password
                command = new MySqlCommand("SELECT password, days, updatedat FROM users WHERE password = @password", connection);
                command.Parameters.AddWithValue("@password", textPassword.Text);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int daylogin = reader.GetInt32("days");
                        DateTime? updatedAt = reader.IsDBNull(reader.GetOrdinal("updatedat")) ? (DateTime?)null : reader.GetDateTime("updatedat");
                        DateTime today = DateTime.Today;
                        MessageBox.Show("Updated: "+updatedAt+"\nToday: "+today);
                        if (daylogin <= 0)
                        {
                            MessageBox.Show("Your trial period has expired.", "OmniMart360 version 1.0", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        bool shouldUpdate = false;

                        if (updatedAt == null)
                        {
                            daylogin -= 1;
                            shouldUpdate = true;
                        }
                        else
                        {
                            //int daysDiff = (updatedAt.Value-today).Days;
                            // for 24 hourse

                            // for date only
                            int daysDiff = (today.Date - updatedAt.Value.Date).Days;

                            MessageBox.Show(""+daysDiff);
                            if (daysDiff >= 1)
                            {
                                daylogin -= 1;
                                shouldUpdate = true;
                            }
                        }

                        reader.Close(); // Close before executing another command
                        MessageBox.Show(""+shouldUpdate);
                        if (shouldUpdate)
                        {
                            using (MySqlCommand updateCommand = new MySqlCommand("UPDATE users SET days = @daylogin, updatedat = @updatedAt WHERE password = @password", connection))
                            {
                                updateCommand.Parameters.AddWithValue("@daylogin", daylogin);
                                updateCommand.Parameters.AddWithValue("@updatedAt", today);
                                updateCommand.Parameters.AddWithValue("@password", textPassword.Text);
                                updateCommand.ExecuteNonQuery();
                            }
                        }

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("You entered wrong password.", "OmniMart360 version 1.0", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textPassword.Text = "";
                        textPassword.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!\n" + ex.Message, "OmniMart360 version 1.0", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textPassword.Text = "";
                textPassword.Focus();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }



    }
}
