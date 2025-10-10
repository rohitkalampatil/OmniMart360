using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
namespace DataAccessLayer
{
    class DBC
    {
        private static readonly string connectionString = "server=localhost;database=storemanagementsystem;uid=root;password=root";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
