using CalculatorWPFDelegate.CalculatorRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorWPFDelegate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double resultFromCalc = 0.0;

        CalculatorManager calcManager;
        public MainWindow()
        {
            InitializeComponent();
            FillOperationsComboBoxWithSymbols();
            calcManager = new CalculatorManager();
        }

        /// <summary>
        /// Fills the operationsComboBox with operations
        /// </summary>
        private void FillOperationsComboBoxWithSymbols()
        {
            for (int i = 0; i < Operations.operationSymbols.Length; i++)
            {
                operationsComboBox.Items.Add(Operations.operationSymbols[i]);
            }


            operationsComboBox.SelectedIndex = 0;
        }

        private void CalculateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ReadOperator(out double op1, out double op2))
            {
                resultFromCalc = calcManager.PerformCalculation(op1, op2, (Operations.AveilibleOperations)operationsComboBox.SelectedIndex);
                ResultTextBox.Text = resultFromCalc.ToString();
            }
            else
            {
                MessageBox.Show("Wrong input!");
            }
        }

        private bool ReadOperator(out double op1, out double op2)
        {
            op1 = 0.0;
            op2 = 0.0;
            bool op1Ok = double.TryParse(Op1TextBox.Text, out op1);
            bool op2Ok = false;
            if (op1Ok)
            {
                return double.TryParse(Op2TextBox.Text, out op2);
            }

            return op1Ok && op2Ok;
        }

        private void IncrDecimal_Click(object sender, RoutedEventArgs e)
        {
            int numberDecimalsCurrent = GetNumberOfDecimals();
            numberDecimalsCurrent++;
            FormatWithDecimals(numberDecimalsCurrent);
        }

        private int GetNumberOfDecimals()
        {
            int indexOfComma = ResultTextBox.Text.ToString().IndexOf(',');
            if(indexOfComma >= 0)
            {
                return ResultTextBox.Text.Length - indexOfComma - 1;
            }
            else
            {
                return 0;
            }
        }

        private void DecrDecimal_Click(object sender, RoutedEventArgs e)
        {
            if (resultFromCalc != 0.0)
            {
                int numberDecimalsCurrent = GetNumberOfDecimals();
                numberDecimalsCurrent--;
                FormatWithDecimals(numberDecimalsCurrent);
            }
        }

        private void FormatWithDecimals(int numberDecimalsCurrent)
        {
            if (resultFromCalc != 0.0 && numberDecimalsCurrent >= 0)
            {
                double resultFromCalcWithDecimals = Math.Round(resultFromCalc, numberDecimalsCurrent);
                string resultWithNewDecimalPlaces = string.Format("{0,0:f" + numberDecimalsCurrent.ToString() + "}", resultFromCalcWithDecimals);
                ResultTextBox.Text = resultWithNewDecimalPlaces;
            }
        }
    }
}
