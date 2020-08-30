using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6.InvoiceInformation
{
    /// <summary>
    /// Model for SellerCompany
    /// </summary>
    class SellingCompany: Company
    {
        public string PhoneNumber { get; set; }
        public string HomePageUrl { get; set; }

        /// <summary>
        /// Used to be able to write information about the selling company to the gui
        /// </summary>
        /// <returns>Nicely formatted SellingCompany as string</returns>
        public override string ToString()
        {
            return $"{base.ToString()} \n{PhoneNumber} \n{HomePageUrl}";
        }
    }
}
