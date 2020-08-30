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

namespace Assignment5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml (Subscriber)
    /// </summary>
    public partial class ControlTowerWindow : Window
    {
        /// <summary>
        /// Constructor for ControlTowerWindow
        /// </summary>
        public ControlTowerWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Click event for "Start new flight" button
        /// </summary>
        /// <param name="sender">the object sending the event</param>
        /// <param name="e">event arguments</param>
        private void StartNewFlightBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CheckFormatOfFlightCode(FlightCodeTextBox.Text)))
            {
                FlightWindow flight = new FlightWindow(FlightCodeTextBox.Text);
                flight.Show();
                SubscribeToTheEventsOfFlightWindow(flight);
            }
            else if (!string.IsNullOrEmpty(FlightCodeTextBox.Text) && string.IsNullOrEmpty(CheckFormatOfFlightCode(FlightCodeTextBox.Text)))
            {
                MessageBox.Show("Please supply a flight code in the form ABC AB123 (in the case of SAS: SAS, American Airlines: AAL, Lufthansa: DLH))", "Take note");
            }
            else
            {
                MessageBox.Show("Please supply a flight code", "Take note");
            }

        }

        /// <summary>
        /// Checks that the flightCode is of the right format (the three first characters)
        /// </summary>
        /// <param name="text"></param>
        /// <returns>if currect, the three first characters of the flightCode that was checked, else an empty string</returns>
        private string CheckFormatOfFlightCode(string text)
        {
            return (text.Split(' ')[0].All(c => !char.IsNumber(c)) && text.Split(' ')[0].Length == 3) ? text : string.Empty;
        }

        /// <summary>
        /// Subscribes the the events of the FlightWindow
        /// </summary>
        /// <param name="flightWindow">The flightWindow object containing the events</param>
        private void SubscribeToTheEventsOfFlightWindow(FlightWindow flightWindow)
        {
            flightWindow.takeOffEvent += TakeOffHandler;
            flightWindow.landEvent += NewLandingHandler;
            flightWindow.changeRouteEvent += ChangeRouteHandler;
        }

        /// <summary>
        /// This method is responsible for logging that a new Flight has taken off
        /// </summary>
        /// <param name="sender">the object sending the event</param>
        /// <param name="flightEventArgs">event arguments</param>
        private void TakeOffHandler(object sender, TakeOffEventArgs flightEventArgs)
        {
            FlightsListView.Items.Add(flightEventArgs);
        }

        /// <summary>
        /// This method is responsible for logging that a new Flight has landed
        /// </summary>
        /// <param name="sender">the object sending the event</param>
        /// <param name="landingEventArgs">event arguments</param>
        private void NewLandingHandler(object sender, LandEventArgs landingEventArgs)
        {
            FlightsListView.Items.Add(landingEventArgs);
        }

        /// <summary>
        /// This method is responsible for logging that a new Flight has changed it´s route
        /// </summary>
        /// <param name="sender">the object sending the event</param>
        /// <param name="landingEventArgs">event arguments</param>
        private void ChangeRouteHandler(object sender, ChangeRouteEventArgs changeEventArgs)
        {
            FlightsListView.Items.Add(changeEventArgs);
        }
    }
}
