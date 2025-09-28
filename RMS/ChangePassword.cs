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
    public partial class ChangePassword : Form
    {
        String cs = "", q = "";
        MySqlCommand cmd;
        MySqlConnection c1;
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            cs = "server=localhost;database=storemanagementsystem;uid=root;password=root";
            c1 = new MySqlConnection(cs);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAll();
        }
        private void clearAll()
        {
            textConfirmpass.Text = "";
            textCurrentpass.Text = "";
            textNewpass.Text = "";
        }
        private void btnChange_Click(object sender, EventArgs e)
        {
            if (textCurrentpass.Text == "")
            {
                lblCurrentpass.Text = "Please enter correct old password.";
                textCurrentpass.Focus();
            }
            else if (textNewpass.Text == "")
            {
                lblNewpass.Text = "Please Enter New password.";
                textNewpass.Focus();
            }
            else if (textConfirmpass.Text == "")
            {
                lblConfirmpass.Text = "Please enter confirm password";
                textConfirmpass.Focus();
            }
            else
            {

                String currentPass = textCurrentpass.Text.ToString();
                String confirmPass = textConfirmpass.Text.ToString();
                String newPass = textNewpass.Text.ToString();
                c1.Open();
                try
                {
                    q = "select password from users";
                    cmd = new MySqlCommand(q, c1);
                    String pwd = cmd.ExecuteScalar().ToString();
                    if (currentPass.Equals(pwd))
                    {

                        if (newPass.Equals(confirmPass))
                        {

                            q = "update users set password='" + newPass + "'";

                            cmd = new MySqlCommand(q, c1);
                            int r = cmd.ExecuteNonQuery();

                            if (r > 0)
                            {
                                clearAll();
                                MessageBox.Show("Password changed successfully.");
                            }
                            else
                            {
                                clearAll();
                                MessageBox.Show("Somehing went erong, Please try  again.");
                            }
                        }
                        else
                        {
                            textNewpass.Text = "";
                            textConfirmpass.Text = "";
                            MessageBox.Show("Confirm password and New password missmatched", "TMS - Tier shop management system");
                        }
                    }
                    else
                    {
                        clearAll();
                        MessageBox.Show("Invalid old password.");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex, "TMS - Tier Management System");
                }
                finally
                {
                    if (c1.State == ConnectionState.Open)
                        c1.Close();
                }

            }
        }
    }
}
