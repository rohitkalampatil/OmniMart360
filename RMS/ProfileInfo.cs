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
    public partial class ProfileInfo : Form
    {
        String cs = "", q = "";
        MySqlCommand cmd;
        MySqlConnection c1;
        MySqlDataReader d;
        public ProfileInfo()
        {
            InitializeComponent();
            cs = "server=localhost;database=storemanagementsystem;uid=root;password=root";
            c1 = new MySqlConnection(cs);
        }

        private void ProfileInfo_Load(object sender, EventArgs e)
        {
            
            c1.Open();
            try
            {
                q = "select * from users";
                cmd = new MySqlCommand(q, c1);
                d = cmd.ExecuteReader();

                if (d.Read())
                {
                    textName.Text = d["user_name"].ToString();
                    textContact.Text = d["user_mobile"].ToString();
                    textEmail.Text = d["user_email"].ToString();
                }
                else
                {

                    MessageBox.Show("No User Found!", "TMS - Tier Management System");
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String name = textName.Text.ToString();
            long mobile = Convert.ToInt64(textContact.Text);
            String email = textEmail.Text.ToString();
            c1.Open();
            try
            {
                q = "update users set user_name='" + name + "',user_mobile=" + mobile + ",user_email='" + email + "'";
                cmd = new MySqlCommand(q, c1);
                int r = cmd.ExecuteNonQuery();

                if (r > 0)
                {
                    MessageBox.Show("Updated successfully.");
                }
                else
                {
                    MessageBox.Show("Somehing went erong, Please try  again.");
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

   

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ProfileInfo_Load( sender,  e);
        }



       
   
    }
}
