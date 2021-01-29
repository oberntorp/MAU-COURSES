using Assignment6.Diagram;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            string diagramTitle = DiaTitleTextBox.Text;
            string diagramMin = DiaIntervalMinTextBox.Text;
            string diagramMax = DiaIntervalMaxTextBox.Text;
            string diagramStep = DiaIntervallStepTextBox.Text;


            if (AllDataEntered(diagramTitle, diagramMin, diagramMax, diagramStep))
            {
                DiagramGenerator dGenerator = new DiagramGenerator(Width, Height, diagramTitle, int.Parse(diagramMin), int.Parse(diagramMax), int.Parse(diagramStep));
                DiagramGrid.Children.Add(dGenerator);
            }
            else
            {
                MessageBox.Show("You have not entered all data needed", "InvalidData", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool AllDataEntered(string diagramTitle, string diagramMin, string diagramMax, string diagramStep)
        {
            return !(string.IsNullOrEmpty(diagramTitle) || string.IsNullOrEmpty(diagramMin) || string.IsNullOrEmpty(diagramMax) || string.IsNullOrEmpty(diagramStep));
        }
    }
}
