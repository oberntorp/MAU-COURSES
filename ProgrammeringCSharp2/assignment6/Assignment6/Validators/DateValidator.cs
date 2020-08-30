using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6.Validators
{
    /// <summary>
    /// Class used to validate dates
    /// </summary>
    class DateValidator
    {
        /// <summary>
        /// Checks that date is valid
        /// </summary>
        /// <param name="dateToValidate">the date to validate</param>
        /// <returns>bool, if valid of not, true or false</returns>
        public bool IsDateValid(string dateToValidate)
        {
            return DateTime.TryParseExact(dateToValidate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);
        }
    }
}
