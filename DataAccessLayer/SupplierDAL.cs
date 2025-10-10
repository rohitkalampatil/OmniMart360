using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Models;
namespace DataAccessLayer
{
    public class SupplierDAL
    {
        public int InsertSupplier(SupplierModel supplier)
        {
            try
            {
                using (MySqlConnection conn = DBC.GetConnection() )
                {
                    string query = @"INSERT INTO supplier 
                        (supplier_name, mobile, email, address, bankacc, ifsc, pan) 
                        VALUES (@SupplierName, @Mobile, @Email, @Address, @BankAcc, @IFSC, @PAN)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SupplierName", (object)supplier.SupplierName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Mobile", (object)supplier.Mobile ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", supplier.Email);
                    cmd.Parameters.AddWithValue("@Address", (object)supplier.Address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BankAcc", (object)supplier.BankAcc ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IFSC", (object)supplier.IFSC ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PAN", (object)supplier.PAN ?? DBNull.Value);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                // Log or rethrow as needed
                throw new ApplicationException("Error inserting supplier: " + ex.Message);
            }
        }
    }
}
