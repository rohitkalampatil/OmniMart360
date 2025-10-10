using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using Models;
using System.Text.RegularExpressions;

namespace BusinessLayer
{
    public class SupplierBLL
    {
        SupplierDAL dl = new SupplierDAL();

        public string AddSupplier(SupplierModel supplier)
        {
            try
            {
                string error = ValidateSupplier(supplier);
                if (!string.IsNullOrEmpty(error))
                    return error;
                int result = dl.InsertSupplier(supplier);
                
                
                if(result > 0)
                    return "Supplier Added Successfully!";
                else
                    return "Failed to Add Supplier";
            }
            catch (Exception ex)
            {
                // Handle or log exception
                return "Validation or insertion failed: " + ex.Message;
                
            }
            
        }

        /*
         * Method to Validate input fields
         * 
         */
        private string ValidateSupplier(SupplierModel supplier)
        {
            if (string.IsNullOrEmpty(supplier.Email) || !IsValidEmail(supplier.Email))
                return "Invalid or missing email.";

            if (supplier.Mobile.ToString().Length != 10)
                return "Mobile number must be 10 digits.";

            if (!string.IsNullOrEmpty(supplier.PAN) && supplier.PAN.Length != 10)
                return "PAN must be 10 characters.";

            if (!string.IsNullOrEmpty(supplier.IFSC) && supplier.IFSC.Length != 11)
                return "IFSC must be 11 characters.";

            return string.Empty;

        }
        /*
         * Method to check email format
         */
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
