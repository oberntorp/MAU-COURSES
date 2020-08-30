using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6.InvoiceInformation
{
    /// <summary>
    /// Model for Adress
    /// </summary>
    class Adress
    {
        /// <summary>
        /// Adress constructor, setting the properties of the object
        /// </summary>
        public Adress(string streetAdress, int zipCode, string city, string country)
        {
            StreetAdress = streetAdress;
            ZipCode = zipCode;
            City = city;
            Country = country;
        }
        public string StreetAdress { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        /// <summary>
        /// ToString method writing the adress nicer with appropriate newlines
        /// </summary>
        /// <returns>Nicely formatted Adress as string</returns>
        public override string ToString()
        {
            return $"{StreetAdress} {ZipCode} \n{City} {Country}";
        }


    }
}
