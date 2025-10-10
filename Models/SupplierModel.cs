using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class SupplierModel
    {
             
        public string SupplierName { get; set; }

        public long Mobile { get; set; }

        public string Email { get; set; } 

        public string Address { get; set; }

        public string BankAcc { get; set; }

        public string IFSC { get; set; }

        public string PAN { get; set; }
        public string GSTIN { get; set; }
    }
}
