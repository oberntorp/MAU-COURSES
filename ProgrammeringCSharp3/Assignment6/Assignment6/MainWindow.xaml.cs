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
        private DiagramInformation diagramInformation;
        private DiagramGenerator dGenerator;
        private DiagramPointsToDrawOfGenerator pointsGenerator;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (AllDataEntered(diagramInformation))
            {
                diagramInformation = new DiagramInformation(DiaTitleTextBox.Text, int.Parse(DiaIntervalXAxisTextBox.Text), int.Parse(DiaIntervalYAxisTextBox.Text), int.Parse(DiaDivisionsXAxisTextBox.Text), int.Parse(DiaDivisionsYAxisTextBox.Text), (int)DiagramGrid.ColumnDefinitions[0].ActualWidth);
                pointsGenerator = new DiagramPointsToDrawOfGenerator(diagramInformation);
                dGenerator = new DiagramGenerator(Width, Height, diagramInformation, pointsGenerator.YPointsUsedInDiagramGeneration, pointsGenerator.XPointsUsedInDiagramGeneration);
                DiagramGrid.Children.Add(dGenerator);

                DiagramSettingsGroupBox.IsEnabled = false;
                PointsGroupBox.IsEnabled = true;
                ClearDiagramButton.IsEnabled = true;

                ClearDiagramSettings();
            }
            else
            {
                MessageBoxes.ShowErrorMessageBox("You have not entered all data needed");
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
                AddPointToDiagram(XValidatedValue, YValidatedValue);
                SaveDataAndGenerateDiagram();
                ClearPointsInput();
            }
            else
            {
                MessageBoxes.ShowErrorMessageBox("Check that your points is within range");
            }
        }

        private void AddPointToDiagram(double XValidatedValue, double YValidatedValue)
        {
            if (diagramInformation.AddPoint(XValidatedValue, YValidatedValue))
            {
                PointsListBox.Items.Add($"({diagramInformation.GetOriginalNumberFromPointIdLastAdded()})");
                SaveDataAndGenerateDiagram();
            }
            else
            {
                MessageBoxes.ShowErrorMessageBox($"Point {XValidatedValue}, {YValidatedValue} already Exists");
            }
        }

        private void SaveDataAndGenerateDiagram()
        {
            dGenerator.DiagramDataToDraw = diagramInformation;
            GeneratePointsToDrawAndDiagram();
        }

        private void GeneratePointsToDrawAndDiagram()
        {
            pointsGenerator = new DiagramPointsToDrawOfGenerator(diagramInformation);
            dGenerator.dataYPointsToPlot = pointsGenerator.YPointsUsedInDiagramGeneration;
            dGenerator.dataXPointsToPlot = pointsGenerator.XPointsUsedInDiagramGeneration;
            dGenerator.InvalidateVisual();
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
            if ((double.TryParse(xPointValue, out double resultXValue) && double.TryParse(YPointValue, out double resultYVallue)) && (resultXValue >= 0 && resultXValue <= (diagramInformation.DivisionsX * diagramInformation.IntervalX) && resultYVallue <= (diagramInformation.DivisionsY * diagramInformation.IntervalY)))
            {
                XValue = resultXValue;
                YValue = resultYVallue;
                return true;
            }

            return false;
        }

        private void ClearDiagramButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveDiagram();
            DiagramSettingsGroupBox.IsEnabled = true;
            ClearDiagramButton.IsEnabled = false;
            PointsGroupBox.IsEnabled = false;
            PointsListBox.Items.Clear();
            ResetDiagramRelatedVariables();
        }

        private void ResetDiagramRelatedVariables()
        {
            diagramInformation.ClearDiagramPoints();
            pointsGenerator = null;
            dGenerator = null;
        }

        private void RemoveDiagram()
        {
            UIElement diagramToRemove = DiagramGrid.Children.Cast<UIElement>().Where(x => Grid.GetColumn(x) == 0).First();
            DiagramGrid.Children.Remove(diagramToRemove);
        }

        private void SortXPointsMenuItewm_Click(object sender, RoutedEventArgs e)
        {
            diagramInformation.SortPointsAccordingToXAxis();
            WriteNewOrderInListBox();
            RedrawDiagram();
        }

        private void WriteNewOrderInListBox()
        {
            PointsListBox.Items.Clear();
            diagramInformation.Points.Keys.ToList().ForEach(x => PointsListBox.Items.Add(GetOriginalNumberFromId(x)));
        }

        private void SortYPointsMenuItewm_Click(object sender, RoutedEventArgs e)
        {
            diagramInformation.SortPointsAccordingToYAxis();
            WriteNewOrderInListBox();
            RedrawDiagram();
        }

        private void RedrawDiagram()
        {
            GeneratePointsToDrawAndDiagram();
        }

        private string GetOriginalNumberFromId(string idToGetNumbersFrom)
        {
            return idToGetNumbersFrom.Split('-').Last();
        }
    }
}
