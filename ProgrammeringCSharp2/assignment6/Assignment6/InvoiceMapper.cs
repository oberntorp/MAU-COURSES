using Assignment6.InvoiceInformation;
using Assignment6.Validators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6
{
    /// <summary>
    /// Class to mapp the invoice data with corresponding classes
    /// </summary>
    class InvoiceMapper
    {
        private bool numberOfProducsValid = true;
        private int indexNumberProducts = 10;
        private int indexFirstSellerInfoFromEnd = 7;
        private DateValidator dateValidator;
        private NumberValidator numberValidator;
        private string[] linesOfInvoiceToProcess;

        public List<string> InvoiceErrors { get; set; }

        /// <summary>
        /// Constructor, initializes different objects needed
        /// </summary>
        /// <param name="linesOfInvoice">lines of invoice being mapped</param>
        public InvoiceMapper(string[] linesOfInvoice)
        {
            linesOfInvoiceToProcess = linesOfInvoice;
            dateValidator = new DateValidator();
            numberValidator = new NumberValidator();
            InvoiceErrors = new List<string>();
        }

        /// <summary>
        /// Adds any errors to the error list
        /// </summary>
        public void AddErrorsIfAnyInFile()
        {
            AddErorIfDateOrDueDateInValid();
            AddErrorIfNumberOfProductsInValid();
            if (numberOfProducsValid)
            {
                AddErrorsIfProductPricesInValid(int.Parse(linesOfInvoiceToProcess[indexNumberProducts]));
            }
        }

        /// <summary>
        /// Adds errors about dates in gui if any
        /// </summary>
        private void AddErorIfDateOrDueDateInValid()
        {
            if (!dateValidator.IsDateValid(linesOfInvoiceToProcess[1]))
            {
                InvoiceErrors.Add(ErrorMessages.invoiceDateError);
            }
            if (!dateValidator.IsDateValid(linesOfInvoiceToProcess[2]))
            {
                InvoiceErrors.Add(ErrorMessages.invoiceDueDateError);
            }
        }

        /// <summary>
        /// Adds errors about number of product if any
        /// </summary>
        private void AddErrorIfNumberOfProductsInValid()
        {
            if (!int.TryParse(linesOfInvoiceToProcess[indexNumberProducts], out int result))
            {
                numberOfProducsValid = false;
                InvoiceErrors.Add(ErrorMessages.numberOfProductsError);
            }
        }

        /// <summary>
        /// Adds errors if product prices are malformed
        /// </summary>
        /// <param name="numberOfProducts">number of poducts, used to loop througth all product prices</param>
        private void AddErrorsIfProductPricesInValid(int numberOfProducts)
        {
            int indexUnitPrice = indexNumberProducts + 3;
            for (int i = 0; i < numberOfProducts; i++)
            {
                int indexOfComma = numberValidator.AddSeparatorIfSeparatorNotExists(ref linesOfInvoiceToProcess[indexUnitPrice]);
                if (!numberValidator.IsNumberValid(linesOfInvoiceToProcess[indexUnitPrice], indexOfComma, out double result))
                {
                    InvoiceErrors.Add($"{ErrorMessages.productPriceError}, the error occured on product number {i + 1}");
                }
                // Increment indexes
                indexUnitPrice += 4;
            }
        }

        /// <summary>
        /// Mapps invoice data
        /// </summary>
        /// <returns></returns>
        public Invoice StartToMappInvoice()
        {
            Invoice invoiceToMapTo = MapGeneralInvoiceInformation();
            invoiceToMapTo.CustomerCompanyInfo = MapCustomerInformation();
            invoiceToMapTo.Products = MapProducts(int.Parse(linesOfInvoiceToProcess[indexNumberProducts]));
            invoiceToMapTo.SellerCompanyInfo = MapSellerInformation();

            return invoiceToMapTo;

        }

        /// <summary>
        /// Mapps general invoice information
        /// </summary>
        /// <returns>the invoice itself after all mapping has been done</returns>
        private Invoice MapGeneralInvoiceInformation()
        {
            return new Invoice()
            {
                InvoiceNumber = linesOfInvoiceToProcess[0],
                InvoiceDate = DateTime.Parse(linesOfInvoiceToProcess[1]),
                InvoiceDueDate = DateTime.Parse(linesOfInvoiceToProcess[2])
            };
        }

        /// <summary>
        /// Mapp customerInformation
        /// </summary>
        /// <returns></returns>
        private Company MapCustomerInformation()
        {
            return new CustomerCompany()
            {
                NameOfCompany = linesOfInvoiceToProcess[4],
                CustomerRef = linesOfInvoiceToProcess[5],
                CompanyAdress = new Adress(streetAdress: linesOfInvoiceToProcess[6], zipCode: Convert.ToInt32(linesOfInvoiceToProcess[7]), city: linesOfInvoiceToProcess[8], country: linesOfInvoiceToProcess[9])
            };
        }

        /// <summary>
        /// Mapp products
        /// </summary>
        /// <param name="numberOfProducts"></param>
        /// <returns>List of products after being mapped</returns>
        private List<Product> MapProducts(int numberOfProducts)
        {
            List<Product> products = new List<Product>();
            int indexDescription = indexNumberProducts + 1;
            int indexQuantity = indexNumberProducts + 2;
            int indexUnitPrice = indexNumberProducts + 3;
            int indexTax = indexNumberProducts + 4;
            for (int i = 0; i < numberOfProducts; i++)
            {
                Product productToAdd = new Product()
                {
                    ProductDescription = linesOfInvoiceToProcess[indexDescription],
                    ProductQuantity = Convert.ToInt32(linesOfInvoiceToProcess[indexQuantity]),
                    ProductPrice = double.Parse(linesOfInvoiceToProcess[indexUnitPrice], CultureInfo.InvariantCulture),
                    ProductPrercentTax = Convert.ToInt32(linesOfInvoiceToProcess[indexTax])
                };

                products.Add(productToAdd);

                // Increment indexes
                indexDescription += 4;
                indexQuantity += 4;
                indexUnitPrice += 4;
                indexTax += 4;
            }

            return products;
        }

        /// <summary>
        /// Maping SellingCompany information
        /// </summary>
        /// <returns></returns>
        private SellingCompany MapSellerInformation()
        {
            int indexFromEnd = linesOfInvoiceToProcess.Count() - indexFirstSellerInfoFromEnd;

            string streetAdress = linesOfInvoiceToProcess[indexFromEnd + 1];

            return new SellingCompany()
            {
                NameOfCompany = linesOfInvoiceToProcess[indexFromEnd],
                CompanyAdress = new Adress(streetAdress: linesOfInvoiceToProcess[indexFromEnd + 1], zipCode: Convert.ToInt32(linesOfInvoiceToProcess[indexFromEnd + 2]), city: linesOfInvoiceToProcess[indexFromEnd + 3], country: linesOfInvoiceToProcess[indexFromEnd + 4]),
                PhoneNumber = linesOfInvoiceToProcess[indexFromEnd + 5],
                HomePageUrl = linesOfInvoiceToProcess[indexFromEnd + 6]
            };
        }
    }
}
