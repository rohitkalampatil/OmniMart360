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
    public partial class Profile : Form
    {
        private Form activeForm;
        
        public Profile()
        {
            InitializeComponent();
        }

        private void Profile_Load(object sender, EventArgs e)
        {
           
            openChildForm(new ProfileInfo());
            
        }

        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopProfile.Controls.Add(childForm);
            this.panelDesktopProfile.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        

        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            openChildForm(new ProfileInfo());
            btnProfile.ForeColor = Color.FromArgb(51, 51, 36);
            button2.ForeColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new ChangePassword());
            btnProfile.ForeColor = Color.White;
            button2.ForeColor = Color.FromArgb(51,51,36);

        }

            

     
    }
}
