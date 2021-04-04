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
        private DiagramGenerator diagramGenerator;

        /// <summary>
        /// Default constructor, initializes the component and initializes DiagramHandler
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            diagramHandler = new DiagramHandler();
        }

        /// <summary>
        /// Event Handler for clicking "Save Settings"
        /// </summary>
        /// <param name="sender">The Button initiating the click</param>
        /// <param name="e">Event arguments</param>
        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (AllDataEntered(diagramHandler.DiagramInformation))
            {
                diagramHandler.CreateDiagramInformationObject(DiaTitleTextBox.Text, int.Parse(DiaIntervalXAxisTextBox.Text), int.Parse(DiaIntervalYAxisTextBox.Text), int.Parse(DiaDivisionsXAxisTextBox.Text), int.Parse(DiaDivisionsYAxisTextBox.Text), DiagramGrid.RowDefinitions[0].ActualHeight, DiagramGrid.ColumnDefinitions[1].ActualWidth);
                diagramHandler.CreatePointsGenerator();
                diagramGenerator = new DiagramGenerator(DiagramGrid.ColumnDefinitions[0].ActualWidth, DiagramGrid.RowDefinitions[0].ActualHeight, diagramHandler.DiagramInformation, diagramHandler.IntervalPointsGenerator.YPointsUsedInDiagramGeneration, diagramHandler.IntervalPointsGenerator.XPointsUsedInDiagramGeneration);
                DiagramGrid.Children.Add(diagramGenerator);

                DiagramSettingsGroupBox.IsEnabled = false;
                PointsGroupBox.IsEnabled = true;
                ClearDiagramButton.IsEnabled = true;

                ClearDiagramSettingsInput();
            }
            else
            {
                MessageBoxes.ShowErrorMessageBox("You have not entered all data needed");
            }
        }
        /// <summary>
        /// Checks that the data required was entered
        /// </summary>
        private bool AllDataEntered(DiagramInformation diagramInformation)
        {
            return !(string.IsNullOrEmpty(DiaTitleTextBox.Text) || string.IsNullOrEmpty(DiaIntervalXAxisTextBox.Text) || string.IsNullOrEmpty(DiaIntervalYAxisTextBox.Text) || string.IsNullOrEmpty(DiaDivisionsXAxisTextBox.Text) || string.IsNullOrEmpty(DiaDivisionsYAxisTextBox.Text));
        }

        /// <summary>
        /// Clears the DiagramSettingsInput
        /// </summary>
        private void ClearDiagramSettingsInput()
        {
            DiaTitleTextBox.Text = "";
            DiaIntervalXAxisTextBox.Text = "";
            DiaIntervalYAxisTextBox.Text = "";
            DiaDivisionsXAxisTextBox.Text = "";
            DiaDivisionsYAxisTextBox.Text = "";
        }

        /// <summary>
        /// Event Handler for clicking "Save Point"
        /// </summary>
        /// <param name="sender">The Button initiating the click</param>
        /// <param name="e">Event arguments</param>
        private void SavePointButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatePionts(PointXTextBox.Text, PointYTextBox.Text, out double XValidatedValue, out double YValidatedValue))
            {
                AddPointToDiagram(XValidatedValue, YValidatedValue);
                UpdateDiagramGeneratorAndGenerateDiagram();
                ClearPointsInput();
            }
            else
            {
                MessageBoxes.ShowErrorMessageBox("Check that your points is within range");
            }
        }

        /// <summary>
        /// Uses TryParse to convert to double,  in at the same time validates the value
        /// </summary>
        /// <param name="xPointValue">The X value being validated</param>
        /// <param name="YPointValue">The Y value being validated</param>
        /// <param name="XValue">The X value after validation</param>
        /// <param name="YValue">The Y value after validation</param>
        /// <returns>true/false indicating successfull conversion to double</returns>
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

        /// <summary>
        /// Adds the point to the diagram
        /// </summary>
        /// <param name="XValidatedValue">The X value of the point</param>
        /// <param name="YValidatedValue">The Y value of the point</param>
        private void AddPointToDiagram(double XValidatedValue, double YValidatedValue)
        {
            if (diagramHandler.DiagramInformation.AddPoint(XValidatedValue, YValidatedValue))
            {
                PointsListBox.Items.Add($"({diagramHandler.DiagramInformation.GetOriginalNumberFromPointIdLastAdded()})");
                diagramHandler.UpdatePointsGeneratorDiagramPoints();
                UpdateDiagramGeneratorAndGenerateDiagram();
            }
            else
            {
                MessageBoxes.ShowErrorMessageBox($"Point {XValidatedValue}, {YValidatedValue} already Exists");
            }
        }

        /// <summary>
        /// Saves the newly set diagramInformation in the diagramGenerator, including the Points
        /// </summary>
        private void UpdateDiagramGeneratorAndGenerateDiagram()
        {
            diagramGenerator.DiagramDataToDraw = diagramHandler.DiagramInformation;
            SavePointsInDiagramGenerator();
        }

        /// <summary>
        /// Save the DataPoints in the diagramGenerator
        /// </summary>
        private void SavePointsInDiagramGenerator()
        {
            diagramGenerator.dataYPointsToPlot = diagramHandler.IntervalPointsGenerator.YPointsUsedInDiagramGeneration;
            diagramGenerator.dataXPointsToPlot = diagramHandler.IntervalPointsGenerator.XPointsUsedInDiagramGeneration;
            diagramGenerator.InvalidateVisual();
        }

        /// <summary>
        /// Clears the Points input
        /// </summary>
        private void ClearPointsInput()
        {
            PointXTextBox.Text = "";
            PointYTextBox.Text = "";
        }

        /// <summary>
        /// Event Handler for clicking "Clear Diagram"
        /// </summary>
        /// <param name="sender">The Button initiating the click</param>
        /// <param name="e">Event arguments</param>
        private void ClearDiagramButton_Click(object sender, RoutedEventArgs e)
        {
            ResetGui();
            ResetDiagramRelatedVariables();
        }

        /// <summary>
        /// Resets The GUI
        /// </summary>
        private void ResetGui()
        {
            RemoveDiagram();
            DiagramSettingsGroupBox.IsEnabled = true;
            ClearDiagramButton.IsEnabled = false;
            PointsGroupBox.IsEnabled = false;
            PointsListBox.Items.Clear();
        }

        /// <summary>
        /// Removes the Diagram
        /// </summary>
        private void RemoveDiagram()
        {
            UIElement diagramToRemove = DiagramGrid.Children.Cast<UIElement>().Where(x => Grid.GetColumn(x) == 0).First();
            DiagramGrid.Children.Remove(diagramToRemove);
        }

        /// <summary>
        /// Resets the diagraminformation
        /// </summary>
        private void ResetDiagramRelatedVariables()
        {
            diagramHandler.ResetDiagramData();
            diagramGenerator = null;
        }

        /// <summary>
        /// Event Handler for clicking MenuItem "Sort X axis points"
        /// </summary>
        /// <param name="sender">The MenuItem initiating the click</param>
        /// <param name="e">Event arguments</param>
        private void SortXPointsMenuItewm_Click(object sender, RoutedEventArgs e)
        {
            diagramHandler.DiagramInformation.SortPointsAccordingToXAxis();
            UpdateListBoxAfterPointsSort();
            RedrawDiagram();
        }

        /// <summary>
        /// Updated the PointsListBox with new order of Points
        /// </summary>
        private void UpdateListBoxAfterPointsSort()
        {
            PointsListBox.Items.Clear();
            diagramHandler.DiagramInformation.DataPoints.Keys.ToList().ForEach(x => PointsListBox.Items.Add(GetOriginalNumberFromId(x)));
        }

        /// <summary>
        /// Get the Point Values from Id
        /// </summary>
        /// <param name="idToGetNumbersFrom"></param>
        /// <returns></returns>
        private string GetOriginalNumberFromId(string idToGetNumbersFrom)
        {
            return idToGetNumbersFrom.Split('-').Last();
        }

        /// <summary>
        /// Event Handler for clicking MenuItem "Sort Y axis points"
        /// </summary>
        /// <param name="sender">The MenuItem initiating the click</param>
        /// <param name="e">Event arguments</param>
        private void SortYPointsMenuItewm_Click(object sender, RoutedEventArgs e)
        {
            diagramHandler.DiagramInformation.SortPointsAccordingToYAxis();
            UpdateListBoxAfterPointsSort();
            RedrawDiagram();
        }

        /// <summary>
        /// Redraws the diagram
        /// </summary>
        private void RedrawDiagram()
        {
            SavePointsInDiagramGenerator();
        }

        /// <summary>
        /// Event Handler for clicking "Remove Point"
        /// </summary>
        /// <param name="sender">The Button initiating the click</param>
        /// <param name="e">Event arguments</param>
        private void RemovePointButton_Click(object sender, RoutedEventArgs e)
        {
            int indexOfPointRemove = PointsListBox.SelectedIndex;
            diagramHandler.DiagramInformation.RemovePoint(indexOfPointRemove);
            PointsListBox.Items.Remove(PointsListBox.SelectedItem);
            diagramHandler.UpdatePointsGeneratorDiagramPoints();
            UpdateDiagramGeneratorAndGenerateDiagram();
        }
    }
}
