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
    /// Interaction logic for ChangePlaylistSettings.xaml
    /// </summary>
    public partial class ChangePlaylistSettings : Window
    {
        public bool ChangesMade { get; set; } = false;
        public string PlaylistDescription { get; set; }
        public int PlaylistMediaDelay { get; set; }
        public ChangePlaylistSettings(string titleOfPlaylistToChange, string descriptionOfPlaylist, int mediaDelayOfPlaylist)
        {
            InitializeComponent();
            Title = $"Change settings of playlist: {titleOfPlaylistToChange}";
            NewDescriptionTextBox.Text = descriptionOfPlaylist;
            NewMediaDelayTextBox.Text = mediaDelayOfPlaylist.ToString();
        }

        private void SaveSettingsCloseButton_Click(object sender, RoutedEventArgs e)
        {
            if(IsInputChanged())
            {
                if (NewDescriptionTextBox.Text != "")
                {
                    if ((NewMediaDelayTextBox.Text == "") || NewMediaDelayTextBox.Text != "" && IsDelayInt())
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

        private bool IsDelayInt()
        {
            return int.TryParse(NewMediaDelayTextBox.Text.Trim(), out int result);
        }

        private bool IsInputChanged()
        {
            return NewDescriptionTextBox.Text.Trim() != PlaylistDescription || NewMediaDelayTextBox.Text.Trim() != PlaylistMediaDelay.ToString();
        }
    }
}
