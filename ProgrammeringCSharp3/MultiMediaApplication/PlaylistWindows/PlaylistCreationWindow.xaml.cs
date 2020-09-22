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
    /// Interaction logic for PlaylistCreationWindow.xaml
    /// </summary>
    public partial class PlaylistCreationWindow : Window
    {
        List<Playlist> playlists = null;
        private int durationBetweenMedia = 5;
        public string TitlaOfPlaylist { get; set; }
        public string DescriptionOfPlaylist { get; set; }
        public int DurationBetweenMedia { get => durationBetweenMedia; set { durationBetweenMedia = value; } }
        public PlaylistCreationWindow(List<Playlist> currentPlaylists)
        {
            playlists = currentPlaylists;
            InitializeComponent();
        }

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

        private bool CheckIfPlaylistExists(string titleToCheckAgainst)
        {
            return playlists.Any(x => x.Title == titleToCheckAgainst);
        }

        private bool CheckValidityOfInput()
        {
            return (TitleTextBox.Text.Trim() != "" && DescriptionTextBox.Text.Trim() != "") && ((PlaybackDelayBetweenMediaTextBox.Text.Trim() != "" && int.TryParse(PlaybackDelayBetweenMediaTextBox.Text, out int result)) || (PlaybackDelayBetweenMediaTextBox.Text.Trim() == "" && !int.TryParse(PlaybackDelayBetweenMediaTextBox.Text, out int result2)));
        }
    }
}
