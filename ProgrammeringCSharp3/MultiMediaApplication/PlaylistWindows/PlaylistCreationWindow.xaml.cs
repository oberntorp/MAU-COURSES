using MultiMediaClassesAndManagers.TreeNode;
using MutiMediaClassesAndManagers;
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

namespace MultiMediaApplication.PlaylistWindows
{
    /// <summary>
    /// Interaction logic for PlaylistCreationWindow.xaml, creates a playlist
    /// </summary>
    public partial class PlaylistCreationWindow : Window
    {
        List<Playlist> playlists = null;
        private int durationBetweenMedia = 5;
        public string TitlaOfPlaylist { get; set; }
        public string DescriptionOfPlaylist { get; set; }
        public int DurationBetweenMedia { get => durationBetweenMedia; set { durationBetweenMedia = value; } }

        /// <summary>
        /// PlaylistCreationWindow constructor, sets the title of the Window and sets the list of current playlists used for checking if the name of the playlist is not occupied
        /// </summary>
        /// <param name="currentPlaylists">the list of current playlists used for checking if the name of the playlist is not occupied</param>
        public PlaylistCreationWindow(List<Playlist> currentPlaylists)
        {
            playlists = currentPlaylists;
            Title = "Create new Playlist";
            InitializeComponent();
        }

        /// <summary>
        /// Click handler CreateCloseButton, checks the input to see that it is currect and if so saves it so that MainWindow has access to it, if not it displays appropriate error messages.
        /// </summary>
        /// <param name="sender">The sending object, in this case a Button</param>
        /// <param name="e">Arguments related to the event</param>
        private void CreateCloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValidityOfInput())
            {
                if (!CheckIfPlaylistExists(TitleTextBox.Text))
                {
                    TitlaOfPlaylist = TitleTextBox.Text;
                    DescriptionOfPlaylist = DescriptionTextBox.Text;
                    DurationBetweenMedia = (PlaybackDelayBetweenMediaTextBox.Text != "") ? int.Parse(PlaybackDelayBetweenMediaTextBox.Text) : DurationBetweenMedia;
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBoxes.ShowErrorMessageBox("A Playlist with the same name already exists, please select another name");
                }
            }
            else
            {
                MessageBoxes.ShowErrorMessageBox("Some information was not currectly entered");
            }
        }

        /// <summary>
        /// Checks if a playlist alreade exists with the title supplied
        /// </summary>
        /// <param name="titleToCheckAgainst">Title of playlist that is checked against existing playlists</param>
        /// <returns>true/false</returns>
        private bool CheckIfPlaylistExists(string titleToCheckAgainst)
        {
            return playlists.Any(x => x.Title == titleToCheckAgainst);
        }

        /// <summary>
        /// Checks that the input is valid and supplied as it should
        /// </summary>
        /// <returns>true/false</returns>
        private bool CheckValidityOfInput()
        {
            return (TitleTextBox.Text.Trim() != "" && DescriptionTextBox.Text.Trim() != "") && ((PlaybackDelayBetweenMediaTextBox.Text.Trim() != "" && int.TryParse(PlaybackDelayBetweenMediaTextBox.Text, out int result)) || (PlaybackDelayBetweenMediaTextBox.Text.Trim() == "" && !int.TryParse(PlaybackDelayBetweenMediaTextBox.Text, out int result2)));
        }
    }
}
