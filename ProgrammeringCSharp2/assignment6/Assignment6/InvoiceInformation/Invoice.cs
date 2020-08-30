using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6.InvoiceInformation
{
    /// <summary>
    /// Model for invoice
    /// </summary>
    class Invoice
    {
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime InvoiceDueDate { get; set; }
        public Company CustomerCompanyInfo { get; set; }
        public List<Product> Products { get; set; }
        public SellingCompany SellerCompanyInfo { get; set; }



    }
}
