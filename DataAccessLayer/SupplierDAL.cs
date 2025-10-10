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
                        (supplier_name, mobile, email, address, bankacc, ifsc, pan,gstin) 
                        VALUES (@SupplierName, @Mobile, @Email, @Address, @BankAcc, @IFSC, @PAN,@gstin)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SupplierName", (object)supplier.SupplierName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Mobile", (object)supplier.Mobile ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", supplier.Email);
                    cmd.Parameters.AddWithValue("@Address", (object)supplier.Address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BankAcc", (object)supplier.BankAcc ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IFSC", (object)supplier.IFSC ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PAN", (object)supplier.PAN ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@gstin",(object)supplier.GSTIN??DBNull.Value);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                /*
                * Handling the Error or Exception while inserting valid data 
                * return nothing on exception
                */
                return 0;
            }
        }

        public int UpdateSupplier(SupplierModel supplier)
        {
            try
            {
                using (MySqlConnection conn = DBC.GetConnection())
                {
                    string query = @"UPDATE supplier SET 
                supplier_name = @SupplierName,
                mobile = @Mobile,
                address = @Address,
                bankacc = @BankAcc,
                ifsc = @IFSC,
                pan = @PAN,
                gstin = @GSTIN
                WHERE email = @Email";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SupplierName", (object)supplier.SupplierName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Mobile", (object)supplier.Mobile ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", (object)supplier.Address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BankAcc", (object)supplier.BankAcc ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IFSC", (object)supplier.IFSC ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PAN", (object)supplier.PAN ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GSTIN", (object)supplier.GSTIN ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", supplier.Email);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public SupplierModel GetSupplierByEmail(string email)
        {
            try
            {
                using (MySqlConnection conn = DBC.GetConnection())
                {
                    string query = "SELECT * FROM supplier WHERE email = @Email";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", email);

                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SupplierModel
                            {
                                SupplierName = reader["supplier_name"].ToString(),
                                Mobile = Convert.ToInt64(reader["mobile"].ToString()),
                                Email = reader["email"].ToString(),
                                Address = reader["address"].ToString(),
                                BankAcc = reader["bankacc"].ToString(),
                                IFSC = reader["ifsc"].ToString(),
                                PAN = reader["pan"].ToString(),
                                GSTIN = reader["gstin"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }
        public List<SupplierModel> GetAllSuppliers()
        {
            List<SupplierModel> suppliers = new List<SupplierModel>();

            try
            {
                using (MySqlConnection conn = DBC.GetConnection())
                {
                    string query = "SELECT * FROM supplier";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(new SupplierModel
                            {
                                
                                SupplierName = reader["supplier_name"].ToString(),
                                Mobile = Convert.ToInt64(reader["mobile"].ToString()),
                                Email = reader["email"].ToString(),
                                Address = reader["address"].ToString(),
                                BankAcc = reader["bankacc"].ToString(),
                                IFSC = reader["ifsc"].ToString(),
                                PAN = reader["pan"].ToString(),
                                GSTIN = reader["gstin"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Optionally log error
            }

            return suppliers;
        }


    }
}
