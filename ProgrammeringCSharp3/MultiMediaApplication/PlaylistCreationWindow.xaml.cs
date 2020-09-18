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

namespace MultiMediaApplication
{
    /// <summary>
    /// Interaction logic for PlaylistCreationWindow.xaml
    /// </summary>
    public partial class PlaylistCreationWindow : Window
    {
        private int durationBetweenMedia = 5;
        public string TitleOfPlaylist { get; set; }
        public string DescriptionOfPlaylist { get; set; }
        public int DurationBetweenMedia { get => durationBetweenMedia; set { durationBetweenMedia = value; } }
        public PlaylistCreationWindow()
        {
            InitializeComponent();
        }

        private void CreateCloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValidityOfInput())
            {
                TitleOfPlaylist = TitleTextBox.Text;
                DescriptionOfPlaylist = DescriptionTextBox.Text;
                DurationBetweenMedia = (PlaybackDelayBetweenMediaTextBox.Text != "") ? int.Parse(PlaybackDelayBetweenMediaTextBox.Text) : DurationBetweenMedia;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBoxes.ShowErrorMessageBox("Some information was not currectly entered");
            }
        }

        private bool CheckValidityOfInput()
        {
            return (TitleTextBox.Text != null && DescriptionTextBox.Text != null) && ((PlaybackDelayBetweenMediaTextBox.Text != "" && int.TryParse(PlaybackDelayBetweenMediaTextBox.Text, out int result)) || (PlaybackDelayBetweenMediaTextBox.Text == "" && !int.TryParse(PlaybackDelayBetweenMediaTextBox.Text, out int result2)));
        }
    }
}
