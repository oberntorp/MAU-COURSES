using Microsoft.Win32;
using MutiMediaClassesAndManagers;
using MultiMediaClassesAndManagers.Interfaces;
using MultiMediaClassesAndManagers.TreeNode;
using MultiMediaClassesAndManagers.MediaBaseClass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utilities;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using Label = System.Windows.Controls.Label;
using TreeView = System.Windows.Controls.TreeView;
using MultiMediaBussinessLogic;
using MultiMediaClassesAndManagers.MediaSubClasses;
using MultiMediaApplication.UserControls;
using MultiMediaApplication.PlaylistWindows;
using System.Collections.ObjectModel;
using WMPLib;
using MultiMediaClassesAndManagers.TreeViewSave;

namespace MultiMediaApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PlaylistHandler playlistHandler = null;
        TreeViewNodesHandler treeViewNodesHandler = null;
        MediaHandler mediaHandler = null;
        TreeViewStructureHandler treeViewStructureHandler = null;
        ObservableCollection<MediaFile> mediaToItemsControl = null;
        public ObservableCollection<MediaFile> MediaToItemsControl { get => mediaToItemsControl; }

        /// <summary>
        /// MainWindow constructor, initiates dependencies
        /// </summary>
        public MainWindow()
        {
            playlistHandler = new PlaylistHandler();
            treeViewNodesHandler = new TreeViewNodesHandler();
            mediaHandler = new MediaHandler();
            treeViewStructureHandler = new TreeViewStructureHandler();
            mediaToItemsControl = new ObservableCollection<MediaFile>();

            InitializeComponent();
        }

        /// <summary>
        /// MenuItem click handler for importing video to a selected playlist, if selected, otherwise displays a error
        /// </summary>
        /// <param name="sender">The sending object, in this case a MenuItem</param>
        /// <param name="e">Arguments related to the event</param>
        private void MenuItemVideo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video files | *.mp4; *.wmv;";

            if (PlaylistTreeView.HasItems && playlistHandler.PlaylistManager.Count != 0)
            {
                int idOfPlayList = playlistHandler.GetPlaylistIdOfSelected(treeViewNodesHandler.NameOfSelectedTreeViewNode(PlaylistTreeView));
                int indexOfPlaylist = 0;
                indexOfPlaylist = idOfPlayList - 1;
                if (PlaylistTreeView.SelectedItem != null && idOfPlayList > 0)
                {
                    bool wasFileSelected = (bool)openFileDialog.ShowDialog();

                    if (wasFileSelected && !string.IsNullOrWhiteSpace(openFileDialog.FileName))
                    {
                        playlistHandler.AddMediaToSelectedPlaylist(indexOfPlaylist, CreateVideoFile(openFileDialog.FileName));
                        InitiateViewPlaylist(indexOfPlaylist);
                    }
                    else
                    {
                        MessageBoxes.ShowInformationMessageBox("No File was selected");
                    }
                }
                else
                {
                    MessageBoxes.ShowInformationMessageBox("Please select a playlist.");
                }
            }
            else
            {
                ErrorMessageNonavigationAreaOrPlaylits();
            }

        }

        /// <summary>
        /// Creates a MediaFile object representing the video object
        /// </summary>
        /// <param name="fullPath">Full path to the video needed when creating the video object</param>
        /// <returns>MediaFile object</returns>
        private IMediaFile CreateVideoFile(string fullPath)
        {
            WindowsMediaPlayer wmp = new WindowsMediaPlayer();
            return mediaHandler.CreateVideoObject(fullPath, "../Images/video_icons8.png", wmp.newMedia(fullPath), FileHandler.GetFileName(fullPath));
        }

        /// <summary>
        /// Displays an error regarding if a playlist was not selected or if the navigation area has no items
        /// </summary>
        private void ErrorMessageNonavigationAreaOrPlaylits()
        {
            string message = PlaylistTreeView.HasItems && playlistHandler.PlaylistManager.Count == 0 ? "There are no playlists, please create one." : "Please select a folder to create a navigation area under File.";
            MessageBoxes.ShowErrorMessageBox(message);
        }

        /// <summary>
        /// MenuItem click handler for importing image to a selected playlist, if selected, otherwise displays a error
        /// </summary>
        /// <param name="sender">The sending object, in this case a MenuItem</param>
        /// <param name="e">Arguments related to the event</param>
        private void MenuItemImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files | *.jpg; *.jpeg; *.png";

            if (PlaylistTreeView.HasItems && playlistHandler.PlaylistManager.Count != 0)
            {
                int idOfPlayList = playlistHandler.GetPlaylistIdOfSelected(treeViewNodesHandler.NameOfSelectedTreeViewNode(PlaylistTreeView));
                int indexOfPlaylist = 0;
                indexOfPlaylist = idOfPlayList - 1;
                if (PlaylistTreeView.SelectedItem != null && idOfPlayList > 0)
                {
                    bool wasFileSelected = (bool)openFileDialog.ShowDialog();

                    if (wasFileSelected && !string.IsNullOrWhiteSpace(openFileDialog.FileName))
                    {
                        playlistHandler.AddMediaToSelectedPlaylist(indexOfPlaylist, CreateImageFile(openFileDialog.FileName));
                        InitiateViewPlaylist(indexOfPlaylist);
                    }
                    else
                    {
                        MessageBoxes.ShowInformationMessageBox("No File was selected");
                    }
                }
                else
                {
                    MessageBoxes.ShowInformationMessageBox("Please select a playlist.");
                }
            }
            else
            {
                ErrorMessageNonavigationAreaOrPlaylits();
            }
        }

        /// <summary>
        /// Creates a MediaFile object representing the image object
        /// </summary>
        /// <param name="fullPath">Full path to image needed when creating the image object</param>
        /// <returns>MediaFile object</returns>
        private IMediaFile CreateImageFile(string fullPath)
        {
            Bitmap image = new Bitmap(fullPath);
            return mediaHandler.CreateImageObject(fullPath, fullPath, image, FileHandler.GetFileName(fullPath));
        }

        /// <summary>
        /// This is the click handler for the first step (filling of NavigationArea with nodes)
        /// First, if already initialized it informs you that your navigation as well as playlists are lost, if you continue
        /// </summary>
        /// <param name="sender">The sending object, in this case a MenuItem</param>
        /// <param name="e">Arguments related to the event</param>
        private void ChoseFolderForNavigationArea_Click(object sender, RoutedEventArgs e)
        {
            if (!PlaylistTreeView.HasItems || (PlaylistTreeView.HasItems && WantToContinueWithoutSaving()))
            {
                ResetStructureAnDPlaylistBeforeNewNavigationAreaIsCreated();
                List<TreeViewNode> treeViewNodes = GetNodesOfTreeView();
                if (treeViewNodes.Count != 0)
                {
                    FillTreeViewWithNodes(treeViewNodes);
                    HideInitialExplination();
                    SaveTreeViewStructure(treeViewNodes);
                }
            }
        }

        /// <summary>
        /// Deletes curreent structure and playlists
        /// </summary>
        private void ResetStructureAnDPlaylistBeforeNewNavigationAreaIsCreated()
        {
            playlistHandler.DeleteAllPlaylists();
            PlaylistTreeView.Items.Clear();
            treeViewStructureHandler.DeleteStructure();
        }

        /// <summary>
        /// Saves the treeViewStructure
        /// </summary>
        /// <param name="treeViewStructure">Structure being saved</param>
        private void SaveTreeViewStructure(List<TreeViewNode> treeViewStructure)
        {
            TreeViewStructure newTreeViewStructure = new TreeViewStructure(treeViewStructure);
            treeViewStructureHandler.AddTreeViewStructure(newTreeViewStructure);
        }

        /// <summary>
        /// Asks theuser if they want to continue without saving
        /// </summary>
        /// <returns>true/false</returns>
        private static bool WantToContinueWithoutSaving()
        {
            return (MessageBoxes.ShowSaveWarningMessageBox("Your current navigation area will be replaced and your playlists created will be removed, please save them first. Do you want to continue?") == MessageBoxResult.Yes);
        }

        /// <summary>
        /// Gets the treeNodes needed to fill the navigationAreaTreeView
        /// </summary>
        /// <returns></returns>
        private List<TreeViewNode> GetNodesOfTreeView()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();

            if (!string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                List<string> folderPaths = new List<string>();
                folderPaths.Add(folderBrowserDialog.SelectedPath);
                folderPaths.AddRange(Directory.GetDirectories(folderBrowserDialog.SelectedPath));
                treeViewNodesHandler.CreateTreeViewNodesFromFolderContent(folderPaths);
                return treeViewNodesHandler.TreeViewNodes;
            }
            else
            {
                MessageBoxes.ShowInformationMessageBox("You did no selection. The navigation area was not created.");
            }

            return new List<TreeViewNode>();
        }

        /// <summary>
        /// The treeNode is filled with its node from the first step
        /// </summary>
        /// <param name="treeNodes">The nodes constituting the navigation area</param>
        private void FillTreeViewWithNodes(List<TreeViewNode> treeNodes)
        {
            TreeViewItem rootNode = treeViewNodesHandler.GetRootTreeViewItem(treeNodes);
            treeViewNodesHandler.AddSubNodesToParent(treeNodes[0], ref rootNode);
            PlaylistTreeView.Items.Add(rootNode);
        }

        /// <summary>
        /// The application hints where to start, when that is done, the hint is hidden
        /// </summary>
        private void HideInitialExplination()
        {
            InitialEplinationStackPanel.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// This is the click handler for Create Playlist under File. After checking that a treeViewNode has been selected, it creates the createPlaylistWindow, creates the node for it and expands its parent node
        /// If no navigationItems are set, it urges you to create it, if you have none selected it gives you an error about that, if something went wrong in the creation, you are told that
        /// </summary>
        /// <param name="sender">The sending object, in this case a MenuItem</param>
        /// <param name="e">Arguments related to the event</param>
        private void CreatePlaylist_Click(object sender, RoutedEventArgs e)
        {
            if (PlaylistTreeView.HasItems)
            {
                PlaylistCreationWindow creationPlaylistWindow = new PlaylistCreationWindow(playlistHandler.PlaylistManager.GetAllItems());

                if (PlaylistTreeView.SelectedItem != null)
                {
                    bool result = (bool)creationPlaylistWindow.ShowDialog();
                    if (result)
                    {
                        Playlist newPlaylist = new Playlist(creationPlaylistWindow.TitlaOfPlaylist.Trim(), creationPlaylistWindow.DescriptionOfPlaylist.Trim(), Convert.ToInt32(creationPlaylistWindow.DurationBetweenMedia.ToString().Trim()));
                        if (playlistHandler.AddPlaylist(newPlaylist))
                        {
                            TreeViewNode playlistTreeViewNode = new TreeViewNode(TreeNodeTypes.playlist, newPlaylist.Title);

                            TreeViewItem newPlaylistTreeViewItem = new TreeViewItem();
                            newPlaylistTreeViewItem.Header = treeViewNodesHandler.GetStackOfTreeViewNode(playlistTreeViewNode);
                            (PlaylistTreeView.SelectedItem as TreeViewItem).IsExpanded = true;
                            (PlaylistTreeView.SelectedItem as TreeViewItem).Items.Add(newPlaylistTreeViewItem);
                        }
                        else
                        {
                            MessageBoxes.ShowErrorMessageBox("Something went wrong, the playlist was not added.");
                        }
                    }
                }
                else
                {
                    MessageBoxes.ShowInformationMessageBox("Please select a folder in the navigation area where the playlist is created.");
                }
            }
            else
            {
                MessageBoxes.ShowErrorMessageBox("Please select a folder to create a navigation area under File.");
            }
        }

        /// <summary>
        /// SelectedItemChanged event handler for TreeView. Used to show/hide information about the playlist selected, not selected
        /// </summary>
        /// <param name="sender">The sending object, in this case TreeViewItem</param>
        /// <param name="e">Arguments related to the event</param>
        private void PlaylistTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            int idOfPlayList = playlistHandler.GetPlaylistIdOfSelected(treeViewNodesHandler.NameOfSelectedTreeViewNode(PlaylistTreeView));
            int indexOfPlaylist = idOfPlayList - 1;
            TreeViewItem selectedNode = (TreeViewItem)(sender as TreeView).SelectedItem;

            if (selectedNode != null && indexOfPlaylist != -1)
            {
                ShowInformationAboutPlaylist(indexOfPlaylist);
                InitiateViewPlaylist(indexOfPlaylist);
            }
            else
            {
                HidePlaylistInfo();
            }
        }

        /// <summary>
        /// Gathers and shows information about the currently selected playlist
        /// </summary>
        /// <param name="indexOfPlaylist">index of playlist to gather information about</param>
        private void ShowInformationAboutPlaylist(int indexOfPlaylist)
        {
            PlaylistInfoStackPanel.Children.Clear();
            string playlistTitle = playlistHandler.PlaylistManager.GetAt(indexOfPlaylist).Title;
            string playlistDescription = playlistHandler.PlaylistManager.GetAt(indexOfPlaylist).Description;
            string playlistContentCount = playlistHandler.PlaylistManager.GetAt(indexOfPlaylist).PlayListContentCount.ToString();
            string playlistPlaybackDelayMedia = playlistHandler.PlaylistManager.GetAt(indexOfPlaylist).PlaylistPlaybackDelayBetweenMediaSec.ToString();

            Label playlistTitleLabel = new Label();
            playlistTitleLabel.Content = $"Playlist title: {playlistTitle}";

            Label playlistDescripeionLabel = new Label();
            playlistDescripeionLabel.Content = "Playlist description:";

            TextBlock playlistDescriptionTextBlock = new TextBlock();
            playlistDescriptionTextBlock.TextWrapping = TextWrapping.Wrap;
            playlistDescriptionTextBlock.Text = playlistDescription;
            Thickness marginLeft = playlistDescriptionTextBlock.Margin;
            marginLeft.Left = 5;
            playlistDescriptionTextBlock.Margin = marginLeft;

            Label playlistContentCountLabel = new Label();
            playlistContentCountLabel.Content = $"Media count: {playlistContentCount}";

            Label playlistDelayMediaLabel = new Label();
            playlistDelayMediaLabel.Content = $"Delay between media clips: {playlistPlaybackDelayMedia}";

            PlaylistInfoStackPanel.Children.Add(playlistTitleLabel);
            PlaylistInfoStackPanel.Children.Add(playlistDescripeionLabel);
            PlaylistInfoStackPanel.Children.Add(playlistDescriptionTextBlock);
            PlaylistInfoStackPanel.Children.Add(playlistContentCountLabel);
            PlaylistInfoStackPanel.Children.Add(playlistDelayMediaLabel);

            PlaylistInfoStackPanelBorder.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Hides information about playlist if none is selected
        /// </summary>
        private void HidePlaylistInfo()
        {
            PlaylistInfoStackPanelBorder.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Initiates the view of media in a playlist
        /// </summary>
        /// <param name="indexOfPlaylist">index of playlist to show media from</param>
        private void InitiateViewPlaylist(int indexOfPlaylist)
        {
            List<MediaFile> mediaFileList = playlistHandler.GetMediaFiles(indexOfPlaylist);
            if (mediaFileList.Count > 0)
            {
                CollectMediaToItemsControl(mediaFileList);
            }
            else
            {
                MessageBoxes.ShowInformationMessageBox("No media to show in the Playlist selected");
            }
        }

        /// <summary>
        /// Collects media to be displayed when clicking a playlist
        /// </summary>
        /// <param name="mediaFiles">mediaFiles containing the media</param>
        private void CollectMediaToItemsControl(List<MediaFile> mediaFiles)
        {
            mediaToItemsControl.Clear();
            foreach (MediaFile media in mediaFiles)
            {
                if (mediaHandler.IsMediaVideo(media))
                {
                    Video video = (media as Video);

                    mediaToItemsControl.Add(video);
                }
                else
                {
                    MultiMediaClassesAndManagers.MediaSubClasses.Image image = (media as MultiMediaClassesAndManagers.MediaSubClasses.Image);
                    mediaToItemsControl.Add(image);
                }
            }
            mediaItemsControl.ItemsSource = mediaToItemsControl;
        }

        /// <summary>
        /// This is the click handler for Change playlist settings under the Playlist menu option it simply checks that a playlist was selected and if so, 
        /// prepares and starts the Playlist settings window and if changes were made, they are saved in the PlaylistManager, if no playlist was selected an error is displayed
        /// </summary>
        /// <param name="sender">The sending object, in this case a MenuItem</param>
        /// <param name="e">Arguments related to the event</param>
        private void ChangePlaylistSettings_Click(object sender, RoutedEventArgs e)
        {
            if (PlaylistTreeView.HasItems && playlistHandler.PlaylistManager.Count != 0)
            {
                int idOfPlayList = playlistHandler.GetPlaylistIdOfSelected(treeViewNodesHandler.NameOfSelectedTreeViewNode(PlaylistTreeView));
                int indexOfPlaylist = idOfPlayList - 1;
                if (PlaylistTreeView.SelectedItem != null && idOfPlayList > 0)
                {
                    Playlist playlistInfo = playlistHandler.PlaylistManager.GetAt(indexOfPlaylist);
                    ChangePlaylistSettingsWindow changePlaylistSettingsWindow = new ChangePlaylistSettingsWindow(playlistInfo.Title, playlistInfo.Description, playlistInfo.PlaylistPlaybackDelayBetweenMediaSec);
                    bool result = (bool)changePlaylistSettingsWindow.ShowDialog();
                    if (result && changePlaylistSettingsWindow.ChangesMade)
                    {
                        playlistInfo.Description = changePlaylistSettingsWindow.PlaylistDescription;
                        playlistInfo.PlaylistPlaybackDelayBetweenMediaSec = changePlaylistSettingsWindow.PlaylistMediaDelay;
                        playlistHandler.PlaylistManager.ChangeAt(playlistInfo, indexOfPlaylist);
                        ShowInformationAboutPlaylist(indexOfPlaylist);
                    }

                }
                else
                {
                    MessageBoxes.ShowInformationMessageBox("Please select a playlist.");
                }
            }
            else
            {
                ErrorMessageNonavigationAreaOrPlaylits();
            }
        }

        /// <summary>
        /// This is the click handler for Play playlist under playlist in the menu, it simply checks that a playlist was selected and if so, 
        /// prepares and starts the player window, if no playlist was selected an error is displayed
        /// </summary>
        /// <param name="sender">The sending object, in this case a MenuItem</param>
        /// <param name="e">Arguments related to the event</param>
        private void PlayPlaylist_Click(object sender, RoutedEventArgs e)
        {
            if (PlaylistTreeView.HasItems && playlistHandler.PlaylistManager.Count != 0)
            {
                int idOfPlayList = playlistHandler.GetPlaylistIdOfSelected(treeViewNodesHandler.NameOfSelectedTreeViewNode(PlaylistTreeView));
                int indexOfPlaylist = idOfPlayList - 1;
                if (PlaylistTreeView.SelectedItem != null && idOfPlayList > 0)
                {
                    Playlist playlistInfo = playlistHandler.PlaylistManager.GetAt(indexOfPlaylist);
                    PlaylistPlayWindow playBackWindow = new PlaylistPlayWindow(playlistInfo.Title, playlistHandler.GetMediaFiles(indexOfPlaylist), playlistInfo.PlaylistPlaybackDelayBetweenMediaSec);
                    playBackWindow.Show();
                    playBackWindow.BeginPlayingMedia();
                }
                else
                {
                    MessageBoxes.ShowInformationMessageBox("Please select a playlist.");
                }
            }
            else
            {
                ErrorMessageNonavigationAreaOrPlaylits();
            }
        }

        /// <summary>
        /// Loads a collection of playlists
        /// </summary>
        /// <param name="sender">The sending object, in this case a MenuItem</param>
        /// <param name="e">Arguments related to the event</param>
        private void LoadPlaylistsMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Saves playlists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePlaylistsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML File | *.XML";
            bool result = (bool)saveFileDialog.ShowDialog();

            if (result)
            {
                try
                {
                    treeViewStructureHandler.SaveAsXML(saveFileDialog.FileName);
                }
                catch(Exception ex)
                {
                    MessageBoxes.ShowErrorMessageBox($"{ex.Message} {ex.InnerException}");
                }
            }
        }

        /// <summary>
        /// Click handler for Exit under File, exits the application
        /// </summary>
        /// <param name="sender">The sending object, in this case a MenuItem</param>
        /// <param name="e">Arguments related to the event</param>
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
