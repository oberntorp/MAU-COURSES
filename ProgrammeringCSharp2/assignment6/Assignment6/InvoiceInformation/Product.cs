using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6.InvoiceInformation
{
    /// <summary>
    /// Model for product
    /// </summary>
    class Product
    {
        public string ProductDescription { get; set; }
        public int ProductQuantity { get; set; }
        public double ProductPrice { get; set; }
        public int ProductPrercentTax { get; set; }
    }
}
