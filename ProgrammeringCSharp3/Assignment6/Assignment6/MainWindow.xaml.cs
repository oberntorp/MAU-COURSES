using Assignment6.Diagram;
using BussinessLogic;
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

namespace Assignment6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DiagramInformation diagramInformation;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (AllDataEntered(diagramInformation))
            {
                diagramInformation = new DiagramInformation(DiaTitleTextBox.Text, int.Parse(DiaIntervalXAxisTextBox.Text), int.Parse(DiaIntervalYAxisTextBox.Text), int.Parse(DiaDivisionsXAxisTextBox.Text), int.Parse(DiaDivisionsYAxisTextBox.Text));
                DiagramGenerator dGenerator = new DiagramGenerator(Width, Height, diagramInformation);
                DiagramGrid.Children.Add(dGenerator);
                DiagramSettingsGroupBox.IsEnabled = false;
                PointsGroupBox.IsEnabled = true;
                ClearDiagramButton.IsEnabled = true;

                ClearDiagramSettings();
            }
            else
            {
                MessageBox.Show("You have not entered all data needed", "InvalidData", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearDiagramSettings()
        {
            DiaTitleTextBox.Text = "";
            DiaIntervalXAxisTextBox.Text = "";
            DiaIntervalYAxisTextBox.Text = "";
            DiaDivisionsXAxisTextBox.Text = "";
            DiaDivisionsYAxisTextBox.Text = "";
        }

        private bool AllDataEntered(DiagramInformation diagramInformation)
        {
            return !(string.IsNullOrEmpty(DiaTitleTextBox.Text) || string.IsNullOrEmpty(DiaIntervalXAxisTextBox.Text) || string.IsNullOrEmpty(DiaIntervalYAxisTextBox.Text) || string.IsNullOrEmpty(DiaDivisionsXAxisTextBox.Text) || string.IsNullOrEmpty(DiaDivisionsYAxisTextBox.Text));
        }

        private void SavePointButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatePionts(PointXTextBox.Text, PointYTextBox.Text, out double XValidatedValue, out double YValidatedValue))
            {
                diagramInformation.XPoints.Add(XValidatedValue);
                diagramInformation.YPoints.Add(YValidatedValue);
                PointsListBox.Items.Add($"({XValidatedValue} {YValidatedValue})");

                ClearPointsInput();
            }
            else
            {
                MessageBox.Show("Check that your points is within range", "InvalidData", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearPointsInput()
        {
            PointXTextBox.Text = "";
            PointYTextBox.Text = "";
        }

        private bool ValidatePionts(string xPointValue, string YPointValue, out double XValue, out double YValue)
        {
            XValue = -1;
            YValue = -1;
            if ((double.TryParse(xPointValue, out double resultXValue) && double.TryParse(YPointValue, out double resultYVallue)) && (resultXValue >= 0 && resultXValue <= diagramInformation.DivisionsX && resultYVallue <= diagramInformation.DivisionsY))
            {
                XValue = resultXValue;
                YValue = resultYVallue;
                return true;
            }

            return false;
        }

        private void ClearDiagramButton_Click(object sender, RoutedEventArgs e)
        {
            DiagramGrid.Children.RemoveAt(1);
            DiagramSettingsGroupBox.IsEnabled = true;
            ClearDiagramButton.IsEnabled = false;
            PointsGroupBox.IsEnabled = false;
        }
    }
}
