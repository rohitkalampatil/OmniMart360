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
                MessageBox.Show("Please enter your password to continue", "OmniMart360 version 1.0", MessageBoxButtons.OK,MessageBoxIcon.Error);
                textPassword.Focus();
            }
            else
            {
                connection.Open();
                try
                {
                    command = new MySqlCommand("select password from users", connection);

                    if (textPassword.Text == command.ExecuteScalar().ToString())
                    {
                        this.DialogResult= DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("You entered wrong password.", "OmniMart360 version 1.0", MessageBoxButtons.OK,MessageBoxIcon.Error);
                        textPassword.Text = "";
                        textPassword.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error!\n"+ex.Message, "OmniMart360 version 1.0", MessageBoxButtons.OK,MessageBoxIcon.Error);
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
}
