using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorWPFDelegate.CalculatorRelated
{
    class Calculator
    {
        public static double Add(double x, double y)
        {
            return x + y;
        }

        public static double Subtract(double x, double y)
        {
            return x - y;
        }

        public static double Multiply(double x, double y)
        {
            return x - y;
        }

        public static double Divide(double x, double y)
        {
            return (y != 0) ? x / y : 0.0;
        }

        public static double Power(double x, double y)
        {
            return Math.Pow(x, y);
        }
    }
}
