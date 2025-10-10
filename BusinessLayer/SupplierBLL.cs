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

        public SupplierModel getSupplierByEmail(string email)
        {
            return dl.GetSupplierByEmail(email);
        }
        public List<SupplierModel> getAllSuppliers()
        {
            return dl.GetAllSuppliers();
        }
        public string AddSupplier(SupplierModel supplier)
        {
            try
            {
                string error = ValidateSupplier(supplier);
                if (!string.IsNullOrEmpty(error))
                    return error;

                int result = dl.InsertSupplier(supplier);
                
                if(result > 0)
                    return "Supplier Added Successfully!\nपुरवठादार यशस्वीरित्या जोडला गेला.";
                else
                    return "Failed to Add Supplier.\nपुरवठादार जोडण्यात अयशस्वी.";
            }
            catch (Exception ex)
            {
                // Handle or log exception
                return "Validation or insertion failed\nमाहितीची पडताळणी किंवा नोंदणी अयशस्वी: " + ex.Message;
                
            }
            
        }
        public string UpdateSupplier(SupplierModel supplier)
        {
            try
            {
                string error = ValidateSupplier(supplier);
                if (!string.IsNullOrEmpty(error))
                    return error;

                int result = dl.UpdateSupplier(supplier);

                if (result > 0)
                    return "Supplier updated successfully.\nपुरवठादाराची माहिती यशस्वीरित्या अद्ययावत झाली.";
                else
                    return "failed to Update Supplier Information.\nमाहिती अद्ययावत करताना त्रुटी आली.";
            }
            catch (Exception ex)
            {
                // Handle or log exception
                return "failed to Update Supplier Information.\nमाहिती अद्ययावत करताना त्रुटी आली.";

            }
          
        }
        /*
         * Method to Validate input fields
         * 
         */
        private string ValidateSupplier(SupplierModel supplier)
        {
            if (string.IsNullOrEmpty(supplier.Email) || !IsValidEmail(supplier.Email))
                return "Invalid or missing email.\nअवैध किंवा अनुपस्थित ईमेल.";

            if (supplier.Mobile.ToString().Length != 10)
                return "Mobile number must be 10 digits.\nमोबाइल क्रमांक १० आकड्यांचा असावा.";

            if (!string.IsNullOrEmpty(supplier.PAN) && supplier.PAN.Length != 10)
                return "PAN must be 10 characters.\nपॅन क्रमांक १० अक्षरांचा असावा.";

            if (!string.IsNullOrEmpty(supplier.IFSC) && supplier.IFSC.Length != 11)
                return "IFSC must be 11 characters.\nआयएफएससी कोड ११ अक्षरांचा असावा.";

            if (!string.IsNullOrEmpty(supplier.GSTIN) && supplier.GSTIN.Length != 15)
                return "GSTIN must be 15 characters.\nजीएसटी ओळख क्रमांक १५ अक्षरांचा असावा.";


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
