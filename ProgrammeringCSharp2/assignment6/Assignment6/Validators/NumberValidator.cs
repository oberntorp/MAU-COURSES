using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6.Validators
{
    /// <summary>
    /// This class is used to handle numbers, both validation and to add a separator if missing
    /// </summary>
    class NumberValidator
    {
        /// <summary>
        /// This method adds a separator if needed
        /// </summary>
        /// <param name="inputString">the string to check for a separator, it is a ref parameter, meaning it gets updated and that the result can be used by the caller</param>
        /// <returns>the index where the separator was fond</returns>
        public int AddSeparatorIfSeparatorNotExists(ref string inputString)
        {
            int decimalIndex = 0;
            int decimalIndexComma = inputString.IndexOf(',');
            int decimalIndexDot = inputString.IndexOf('.');
            bool decimalExist = (decimalIndexComma > -1 || decimalIndexDot > -1);

            if (!decimalExist)
            {
                switch (CultureInfo.CurrentCulture.ToString())
                {
                    case "en-GB":
                        inputString += ".0";
                        break;
                    default:
                        inputString += ",0";
                        break;
                }
            }

            return decimalIndex;
        }

        /// <summary>
        /// This method checks if a given number is valid, and updates a ref parameter
        /// </summary>
        /// <param name="inputString">string to check number on</param>
        /// <param name="indexOfComma">index where the comma was found</param>
        /// <param name="resultOfTryParse">the resulting number of the check</param>
        /// <returns>bool, if valid of not, true or false</returns>
        public bool IsNumberValid(string inputString, int indexOfComma, out double resultOfTryParse)
        {
            resultOfTryParse = 0;
            bool result = false;
            double resultEnGb = -1;
            double resultDefault = -1;
            switch (CultureInfo.CurrentCulture.ToString())
            {
                case "en-GB":
                    if (double.TryParse(inputString.Replace(',', '.'), NumberStyles.Float, CultureInfo.GetCultureInfo("en-GB"), out resultEnGb))
                    {
                        resultOfTryParse = resultEnGb;
                        result = true;
                    }
                    break;
                default:
                    if (double.TryParse(inputString.Replace('.', ','), NumberStyles.Float, CultureInfo.GetCultureInfo("se-SV"), out resultDefault))
                    {
                        resultOfTryParse = resultDefault;
                        result = true;
                    }
                    break;
            }

            return result;
        }
    }
}
