using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RMS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new SalePage());
            Login obj = new Login();
            if (obj.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new HomePage());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
