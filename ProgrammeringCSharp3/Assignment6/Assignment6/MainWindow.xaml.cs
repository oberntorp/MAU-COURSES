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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            DiagramInformation diagramInformation = new DiagramInformation(DiaTitleTextBox.Text, int.Parse(DiaIntervalXAxisTextBox.Text), int.Parse(DiaIntervalYAxisTextBox.Text), int.Parse(DiaDivisionsXAxisTextBox.Text), int.Parse(DiaDivisionsYAxisTextBox.Text));


            if (AllDataEntered(diagramInformation))
            {
                DiagramGenerator dGenerator = new DiagramGenerator(Width, Height, diagramInformation);
                DiagramGrid.Children.Add(dGenerator);
            }
            else
            {
                MessageBox.Show("You have not entered all data needed", "InvalidData", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool AllDataEntered(DiagramInformation diagramInformation)
        {
            return !(string.IsNullOrEmpty(diagramInformation.Title) || string.IsNullOrEmpty(diagramInformation.IntervalX.ToString()) || string.IsNullOrEmpty(diagramInformation.IntervalY.ToString()) || string.IsNullOrEmpty(diagramInformation.DivisionsX.ToString()) || string.IsNullOrEmpty(diagramInformation.DivisionsY.ToString()));
        }
    }
}
