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
        private DiagramHandler diagramHandler;
        private DiagramGenerator dGenerator;
        public MainWindow()
        {
            diagramHandler = new DiagramHandler();
            InitializeComponent();
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (AllDataEntered(diagramHandler.DiagramInformation))
            {
                diagramHandler.CreateDiagramInformationObject(DiaTitleTextBox.Text, int.Parse(DiaIntervalXAxisTextBox.Text), int.Parse(DiaIntervalYAxisTextBox.Text), int.Parse(DiaDivisionsXAxisTextBox.Text), int.Parse(DiaDivisionsYAxisTextBox.Text), (int)DiagramGrid.ColumnDefinitions[0].ActualWidth);
                diagramHandler.CreatePointsGenerator();
                dGenerator = new DiagramGenerator(Width, Height, diagramHandler.DiagramInformation, diagramHandler.IntervalPointsGenerator.YPointsUsedInDiagramGeneration, diagramHandler.IntervalPointsGenerator.XPointsUsedInDiagramGeneration);
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
            if (diagramHandler.DiagramInformation.AddPoint(XValidatedValue, YValidatedValue))
            {
                PointsListBox.Items.Add($"({diagramHandler.DiagramInformation.GetOriginalNumberFromPointIdLastAdded()})");
                diagramHandler.UpdatePointsGeneratorUpdatedDiagramPoints();
                SaveDataAndGenerateDiagram();
            }
            else
            {
                MessageBoxes.ShowErrorMessageBox($"Point {XValidatedValue}, {YValidatedValue} already Exists");
            }
        }

        private void SaveDataAndGenerateDiagram()
        {
            dGenerator.DiagramDataToDraw = diagramHandler.DiagramInformation;
            SavePointsInDiagramGenerator();
        }

        private void SavePointsInDiagramGenerator()
        {
            dGenerator.dataYPointsToPlot = diagramHandler.IntervalPointsGenerator.YPointsUsedInDiagramGeneration;
            dGenerator.dataXPointsToPlot = diagramHandler.IntervalPointsGenerator.XPointsUsedInDiagramGeneration;
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
            if ((double.TryParse(xPointValue, out double resultXValue) && double.TryParse(YPointValue, out double resultYVallue)) && (resultXValue >= 0 && resultXValue <= (diagramHandler.DiagramInformation.DivisionsX * diagramHandler.DiagramInformation.IntervalX) && resultYVallue <= (diagramHandler.DiagramInformation.DivisionsY * diagramHandler.DiagramInformation.IntervalY)))
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
            diagramHandler.ResetDiagramData();
            dGenerator = null;
        }

        private void RemoveDiagram()
        {
            UIElement diagramToRemove = DiagramGrid.Children.Cast<UIElement>().Where(x => Grid.GetColumn(x) == 0).First();
            DiagramGrid.Children.Remove(diagramToRemove);
        }

        private void SortXPointsMenuItewm_Click(object sender, RoutedEventArgs e)
        {
            diagramHandler.DiagramInformation.SortPointsAccordingToXAxis();
            WriteNewOrderInListBox();
            RedrawDiagram();
        }

        private void WriteNewOrderInListBox()
        {
            PointsListBox.Items.Clear();
            diagramHandler.DiagramInformation.DataPoints.Keys.ToList().ForEach(x => PointsListBox.Items.Add(GetOriginalNumberFromId(x)));
        }

        private void SortYPointsMenuItewm_Click(object sender, RoutedEventArgs e)
        {
            diagramHandler.DiagramInformation.SortPointsAccordingToYAxis();
            WriteNewOrderInListBox();
            RedrawDiagram();
        }

        private void RedrawDiagram()
        {
            SavePointsInDiagramGenerator();
        }

        private string GetOriginalNumberFromId(string idToGetNumbersFrom)
        {
            return idToGetNumbersFrom.Split('-').Last();
        }
    }
}
