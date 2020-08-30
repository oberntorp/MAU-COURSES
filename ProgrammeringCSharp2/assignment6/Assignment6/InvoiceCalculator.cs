using Assignment6.InvoiceInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6
{
    /// <summary>
    /// Used to calculate price details needed in the main window
    /// </summary>
    class InvoiceCalculator
    {
        /// <summary>
        /// Calculates the tax of a product
        /// </summary>
        /// <param name="taxInPercent">tax in percent of product</param>
        /// <param name="productPrice">price of product</param>
        /// <returns>Tax as double</returns>
        public double CalculateTaxOfProduct(int taxInPercent, double productPrice)
        {
            return productPrice * ((double)taxInPercent/100);
        }

        /// <summary>
        /// Calculates the price of a product
        /// </summary>
        /// <param name="totalTax"></param>
        /// <param name="productPrice"></param>
        /// <returns></returns>
        public double CalculateTotalPriceOfProduct(double totalTax, double productPrice)
        {
            return totalTax + productPrice;
        }

        /// <summary>
        /// Calculates the total tax of all products on the invoice
        /// </summary>
        /// <param name="products">Products to compute the total tax of</param>
        /// <returns>total tax as double</returns>
        public double CalculateTaxInvoice(List<Product> products)
        {
            double totalInvoiceTax = 0;
            for (int i = 0; i < products.Count(); i++)
            {
                totalInvoiceTax += CalculateTaxOfProduct(products[i].ProductPrercentTax, products[i].ProductPrice);
            }

            return totalInvoiceTax;
        }

        /// <summary>
        /// Calculates the total price of the invoice
        /// </summary>
        /// <param name="products">Products to compute the total price of</param>
        /// <returns>total price as double</returns>
        public double CalculatePriceInvoice(List<Product> products)
        {
            double totalInvoiceTax = 0;
            double totalInvoicePrice = 0;
            for(int i = 0; i < products.Count(); i++)
            {
                totalInvoiceTax += CalculateTaxOfProduct(products[i].ProductPrercentTax, products[i].ProductPrice);
                totalInvoicePrice += CalculateTotalPriceOfProduct(CalculateTaxOfProduct(products[i].ProductPrercentTax, products[i].ProductPrice), products[i].ProductPrice);
            }

            return totalInvoiceTax + totalInvoicePrice;
        }
    }
}
