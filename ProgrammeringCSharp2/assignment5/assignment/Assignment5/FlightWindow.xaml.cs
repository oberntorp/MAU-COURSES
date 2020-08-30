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
using System.Windows.Shapes;

namespace Assignment5
{
    /// <summary>
    /// Interaction logic for FlightWindow.xaml (Publisher)
    /// </summary>
    public partial class FlightWindow : Window
    {
        public event EventHandler<TakeOffEventArgs> takeOffEvent;
        public event EventHandler<LandEventArgs> landEvent;
        public event EventHandler<ChangeRouteEventArgs> changeRouteEvent;

        private string flightCode = string.Empty;

        /// <summary>
        /// Constructor for FlightWindow
        /// It inializes the gui
        /// </summary>
        /// <param name="flightCodeIn">The flightCode of the new flight</param>
        public FlightWindow(string flightCodeIn)
        {
            InitializeComponent();
            flightCode = flightCodeIn;
            InitializeGui();
        }

        /// <summary>
        /// Initialize the Gui in different by setting the title, image and disable components not in use yet 
        /// </summary>
        private void InitializeGui()
        {
            Title = $"Flight: {flightCode}";
            SetAppropriateImage();
            DisableGuiComponents();
        }

        /// <summary>
        /// Disable Gui components not in use yet
        /// </summary>
        private void DisableGuiComponents()
        {
            EndFlightBtn.IsEnabled = false;
            RouteComboBox.IsEnabled = false;
        }

        /// <summary>
        /// Sets the Airliner loggo based on the three first characters in the flightcode
        /// </summary>
        private void SetAppropriateImage()
        {
            AirloneLogo.Source = new BitmapImage(new Uri($"{GetImageUrl(GetAirline())}.png", UriKind.Relative));
        }

        /// <summary>
        /// Get the airline from the first three characters in the flightCode
        /// </summary>
        /// <returns>the airline from the first three characters in the flightCode</returns>
        private string GetAirline()
        {
            return Title.Split(' ')[1].ToLower();
        }

        /// <summary>
        /// Get the image uri based on which airline is in the flightCode
        /// </summary>
        /// <param name="airline">the three first characters in the airline flightCode</param>
        /// <returns>the uri of the image to display</returns>
        private string GetImageUrl(string airline)
        {
            return $"/Images/Airlines/{airline}";
        }

        /// <summary>
        /// Reise takeoff event
        /// </summary>
        /// <param name="args">arguments for the event</param>
        public void OnNewTakeOff(TakeOffEventArgs args)
        {
            takeOffEvent?.Invoke(this, args);
        }

        /// <summary>
        /// Reise takeoff event
        /// </summary>
        /// <param name="args">arguments for the event</param>
        public void OnNewLanding(LandEventArgs args)
        {
            landEvent?.Invoke(this, args);
        }

        /// <summary>
        /// Reise Change Route event
        /// </summary>
        /// <param name="args">arguments for the event</param>
        public void OnNewChangeRoute(ChangeRouteEventArgs args)
        {
            changeRouteEvent?.Invoke(this, args);
        }

        /// <summary>
        /// Click event for "Take off" button
        /// </summary>
        /// <param name="sender">the object sending the event</param>
        /// <param name="e">event arguments</param>
        private void StartFlightBtn_Click(object sender, RoutedEventArgs e)
        {
            TakeOffEventArgs takeOffEventArgs = new TakeOffEventArgs(flightCode, FormatDate(DateTime.Now));
            OnNewTakeOff(takeOffEventArgs);
            DisableStartActivateChangeRouteLand();
        }

        /// <summary>
        /// When Starting a flight, we only want some buttons to be active
        /// </summary>
        private void DisableStartActivateChangeRouteLand()
        {
            StartFlightBtn.IsEnabled = false;
            RouteComboBox.IsEnabled = true;
            EndFlightBtn.IsEnabled = true;
        }

        /// <summary>
        /// Click event for "Land" button
        /// </summary>
        /// <param name="sender">the object sending the event</param>
        /// <param name="e">event arguments</param>
        private void EndFlightBtn_Click(object sender, RoutedEventArgs e)
        {
            LandEventArgs landEventArgs = new LandEventArgs(flightCode, FormatDate(DateTime.Now));
            OnNewLanding(landEventArgs);
            CloseWindow();
        }

        /// <summary>
        /// When Ending flight we will close the findow
        /// </summary>
        private void CloseWindow()
        {
            this.Close();
        }

        /// <summary>
        /// Click event for "routeComboBox"
        /// </summary>
        /// <param name="sender">the object sending the event</param>
        /// <param name="e">event arguments</param>
        private void RouteComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RouteComboBox.SelectedIndex > -1)
            {
                ChangeRouteEventArgs changeRouteEventArgs = new ChangeRouteEventArgs(flightCode, FormatDate(DateTime.Now), ((ComboBoxItem)RouteComboBox.SelectedItem).Content.ToString());
                OnNewChangeRoute(changeRouteEventArgs);
            }
        }

        /// <summary>
        /// This method formats the date
        /// </summary>
        /// <param name="dateTimeToFormat"></param>
        /// <returns></returns>
        private string FormatDate(DateTime dateTimeToFormat)
        {
            return dateTimeToFormat.ToString(("yyyy/MM/dd H:mm"));
        }
    }
}
