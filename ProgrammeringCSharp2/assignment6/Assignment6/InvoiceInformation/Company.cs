using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6.InvoiceInformation
{
    /// <summary>
    /// BaseClass for company
    /// </summary>
    class Company
    {
        public string NameOfCompany { get; set; }
        public Adress CompanyAdress { get; set; }

        /// <summary>
        /// Used to be able to output company information to the gui in a nicer way with appropriate newlines
        /// </summary>
        /// <returns>Nicely formatted Company as string</returns>
        public override string ToString()
        {
            return $"{NameOfCompany} \n{CompanyAdress.ToString()}";
        }
    }
}
