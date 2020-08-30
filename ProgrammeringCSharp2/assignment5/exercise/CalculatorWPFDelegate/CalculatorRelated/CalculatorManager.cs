using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorWPFDelegate.CalculatorRelated
{
    public delegate double CalculateHandler(double x, double y);
    class CalculatorManager
    {
        public double PerformCalculation(double x, double y, Operations.AveilibleOperations chosenOperator)
        {
            CalculateHandler calcMethod = GetMethod(chosenOperator);
            return calcMethod(x, y);
        }

        public static CalculateHandler GetMethod(Operations.AveilibleOperations chosenOerator)
        {
            CalculateHandler chosenMethod = null;
            switch(chosenOerator)
            {
                case Operations.AveilibleOperations.Add:
                    chosenMethod = Calculator.Add;
                    break;
                case Operations.AveilibleOperations.Subtract:
                    chosenMethod = Calculator.Subtract;
                    break;
                case Operations.AveilibleOperations.Multiply:
                    chosenMethod = Calculator.Multiply;
                    break;
                case Operations.AveilibleOperations.Divide:
                    chosenMethod = Calculator.Divide;
                    break;
                case Operations.AveilibleOperations.Power:
                    chosenMethod = Calculator.Power;
                    break;
            }

            return chosenMethod;
        }

    }
}
