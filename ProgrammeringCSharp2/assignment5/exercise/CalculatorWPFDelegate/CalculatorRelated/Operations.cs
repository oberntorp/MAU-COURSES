using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorWPFDelegate.CalculatorRelated
{
    class Operations
    {
        public enum AveilibleOperations
        {
            Add,
            Subtract,
            Multiply,
            Divide,
            Power
        }

        public static string[] operationSymbols =
        {
         "+",
         "-",
         "*",
         "/",
         "X^Y"
        };
            
    }
}
