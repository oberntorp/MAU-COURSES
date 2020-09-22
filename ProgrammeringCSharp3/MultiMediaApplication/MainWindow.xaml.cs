using Microsoft.Win32;
using MultiMediaClassesAndManagers.Implementations;
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
        ObservableCollection<MediaFile> mediaToItemsControl = null;
        public ObservableCollection<MediaFile> MediaToItemsControl { get => mediaToItemsControl; }
        public MainWindow()
        {
            playlistHandler = new PlaylistHandler();
            treeViewNodesHandler = new TreeViewNodesHandler();
            mediaHandler = new MediaHandler();
            mediaToItemsControl = new ObservableCollection<MediaFile>();

            InitializeComponent();
            InitializePlayListTreeView();
        }

        private void InitializePlayListTreeView()
        {

        }

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

        private void ErrorMessageNonavigationAreaOrPlaylits()
        {
            string message = PlaylistTreeView.HasItems && playlistHandler.PlaylistManager.Count == 0 ? "There are no playlists, please create one." : "Please select a folder to create a navigation area under File.";
            MessageBoxes.ShowErrorMessageBox(message);
        }

        private IMediaFile CreateVideoFile(string fullPath)
        {
            WindowsMediaPlayer wmp = new WindowsMediaPlayer();
            return mediaHandler.CreateVideoObject(fullPath, "../Images/video_icons8.png", wmp.newMedia(fullPath), FileHandler.GetFileName(fullPath));
        }

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

        private IMediaFile CreateImageFile(string fullPath)
        {
            Bitmap image = new Bitmap(fullPath);
            return mediaHandler.CreateImageObject(fullPath, fullPath, image, FileHandler.GetFileName(fullPath));
        }

        private void ChoseFolderForNavigationArea_Click(object sender, RoutedEventArgs e)
        {
            if (PlaylistTreeView.HasItems)
            {
                MessageBoxes.ShowInformationMessageBox("Your current navigation area will be replaced and your playlists created will be removed.");
            }

            playlistHandler.DeleteAllPlaylists();
            PlaylistTreeView.Items.Clear();
            List<TreeViewNode> treeViewNodes = GetNodesOfTreeView();
            if (treeViewNodes.Count != 0)
            {
                FillTreeViewWithNodes(treeViewNodes);
                HideInitialExplination();
            }
        }

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

        private void FillTreeViewWithNodes(List<TreeViewNode> treeNodes)
        {
            TreeViewItem rootNode = treeViewNodesHandler.GetRootTreeViewItem(treeNodes);
            treeViewNodesHandler.AddSubNodesToParent(treeNodes[0], ref rootNode);
            PlaylistTreeView.Items.Add(rootNode);
        }

        private void HideInitialExplination()
        {
            InitialEplinationStackPanel.Visibility = Visibility.Hidden;
        }

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

        private void HidePlaylistInfo()
        {
            PlaylistInfoStackPanelBorder.Visibility = Visibility.Hidden;
        }

        private void InitiateViewPlaylist(int indexOfPlaylist)
        {
            List<MediaFile> mediaFileList = playlistHandler.GetMediaFiles(indexOfPlaylist);
            if (mediaFileList.Count > 0)
            {
                CreateUiForEveryMediaType(mediaFileList, indexOfPlaylist);
            }
            else
            {
                MessageBoxes.ShowInformationMessageBox("No media to show in the Playlist selected");
            }
        }

        private void CreateUiForEveryMediaType(List<MediaFile> mediaFiles, int indexOfPlaylist)
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

        private void ChangePlaylistSettings_Click(object sender, RoutedEventArgs e)
        {
            if (PlaylistTreeView.HasItems && playlistHandler.PlaylistManager.Count != 0)
            {
                int idOfPlayList = playlistHandler.GetPlaylistIdOfSelected(treeViewNodesHandler.NameOfSelectedTreeViewNode(PlaylistTreeView));
                int indexOfPlaylist = idOfPlayList - 1;
                if (PlaylistTreeView.SelectedItem != null && idOfPlayList > 0)
                {
                    Playlist playlistInfo = playlistHandler.PlaylistManager.GetAt(indexOfPlaylist);
                    ChangePlaylistSettings changePlaylistSettingsWindow = new ChangePlaylistSettings(playlistInfo.Title, playlistInfo.Description, playlistInfo.PlaylistPlaybackDelayBetweenMediaSec);
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
    }
}
