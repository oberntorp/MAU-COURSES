using ApusAnimalMotel.Interfaces;
using ApusAnimalMotel.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApusAnimalMotel
{
    /// <summary>
    /// StaffClass containing information about a StaffMember, namely Name and it´s Qualifications 
    /// </summary>
    public class Staff
    {
        public string Name { get; set; }
        public IListManager<string> Qualifications { get; set; }

        /// <summary>
        /// Staff constructor, instansiates Qualifications
        /// </summary>
        public Staff()
        {
            Qualifications = new ListManager<string>();
        }

        /// <summary>
        /// ToString containing the Name of the recipe and its ingredients
        /// </summary>
        /// <returns>Returns a ToString containing the Name of the recipe and its ingredients</returns>
        public override string ToString()
        {
            string qualifications = ConvertQualificationsToString();
            return $"{Name} {qualifications}";
        }

        private string ConvertQualificationsToString()
        {
            StringBuilder qualifications = new StringBuilder();
            foreach (string qualification in Qualifications.ToStringList())
            {
                qualifications.Append(qualification);
                qualifications.Append(", ");
            }

            return qualifications.ToString();
        }
    }
}
