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
    /// Interaction logic for ChangePlaylistSettingsWindow.xaml, used for changing the settings of a playlist
    /// </summary>
    public partial class ChangePlaylistSettingsWindow : Window
    {
        public bool ChangesMade { get; set; } = false;
        public string PlaylistDescription { get; set; }
        public int PlaylistMediaDelay { get; set; }

        /// <summary>
        /// The constructor of ChangePlaylistSettingsWindow, sets the windows Title, as well as the former values of description and media delay, all comming from the MainWindow
        /// </summary>
        /// <param name="titleOfPlaylistToChange">Title of the playlist being changed</param>
        /// <param name="descriptionOfPlaylist">Description of the playlist being changed</param>
        /// <param name="mediaDelayOfPlaylist">Nedia Delay of the playlist being changed</param>
        public ChangePlaylistSettingsWindow(string titleOfPlaylistToChange, string descriptionOfPlaylist, int mediaDelayOfPlaylist)
        {
            InitializeComponent();
            Title = $"Change settings of playlist: {titleOfPlaylistToChange}";
            NewDescriptionTextBox.Text = descriptionOfPlaylist;
            NewMediaDelayTextBox.Text = mediaDelayOfPlaylist.ToString();
        }

        /// <summary>
        /// Save and close buttons click handler, checks that the input is currect, if so closes the window, otherwise displays an error and 
        /// </summary>
        /// <param name="sender">The object firing the evebt (button)</param>
        /// <param name="e">arguments of the event</param>
        private void SaveSettingsCloseButton_Click(object sender, RoutedEventArgs e)
        {
            if(IsInputChanged())
            {
                if (NewDescriptionTextBox.Text != "")
                {
                    if (NewMediaDelayTextBox.Text == "" || (NewMediaDelayTextBox.Text != "" && IsDelayInt()))
                    {
                        PlaylistDescription = NewDescriptionTextBox.Text.Trim();
                        PlaylistMediaDelay = (NewMediaDelayTextBox.Text.Trim() != "") ? int.Parse(NewMediaDelayTextBox.Text.Trim()) : 5;
                        ChangesMade = true;
                        this.DialogResult = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBoxes.ShowErrorMessageBox("The Media delay was not a number");
                    }
                }
                else
                {
                    MessageBoxes.ShowErrorMessageBox("Description can not be empty.");
                }
            }
            else
            {
                if (MessageBoxes.ShowSaveWarningMessageBox("No changes made, do you want to save?") == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// The delay can be blank as there is a default value, if it is not, this method checks that it is a valid number
        /// </summary>
        /// <returns>true/false</returns>
        private bool IsDelayInt()
        {
            return int.TryParse(NewMediaDelayTextBox.Text.Trim(), out int result);
        }

        /// <summary>
        /// To know if there is a need of updating the values in the MainWindow as well as if validation is needed, this method checks for changes 
        /// </summary>
        /// <returns>true/false</returns>
        private bool IsInputChanged()
        {
            return NewDescriptionTextBox.Text.Trim() != PlaylistDescription || NewMediaDelayTextBox.Text.Trim() != PlaylistMediaDelay.ToString();
        }
    }
}
