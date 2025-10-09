using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RMS
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void Help_Load(object sender, EventArgs e)
        {
            string helpFilePath = Path.Combine(Application.StartupPath, @"..\..\help.html");
            helpFilePath = Path.GetFullPath(helpFilePath);

            if (File.Exists(helpFilePath))
            {
                webBrowser1.Navigate(helpFilePath);
            }
            else
            {
                MessageBox.Show("Help file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
