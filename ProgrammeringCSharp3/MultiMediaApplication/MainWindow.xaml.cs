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
using System.Collections.ObjectModel;

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
        ObservableCollection<MediaViewSelectionUserControl> mediaToItemsControl = null;
        public ObservableCollection<MediaViewSelectionUserControl> MediaToItemsControl { get => mediaToItemsControl; }
        public MainWindow()
        {
            playlistHandler = new PlaylistHandler();
            treeViewNodesHandler = new TreeViewNodesHandler();
            mediaHandler = new MediaHandler();
            mediaToItemsControl = new ObservableCollection<MediaViewSelectionUserControl>();

            InitializeComponent();
            InitializePlayListTreeView();
        }

        private void InitializePlayListTreeView()
        {

        }

        private void MenuItemVideo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files | *.jpg; *.jpeg; *.png";
            bool wasFileSelected = (bool)openFileDialog.ShowDialog();
            int indexOfPlaylist = 0;

            if (wasFileSelected && !string.IsNullOrWhiteSpace(openFileDialog.FileName))
            {
                int idOfPlayList =  playlistHandler.GetPlaylistIdOfSelected(treeViewNodesHandler.NameOfSelectedTreeViewNode(PlaylistTreeView));
                indexOfPlaylist = idOfPlayList - 1;

                if (PlaylistTreeView.SelectedItem != null && idOfPlayList > 0)
                {
                    playlistHandler.AddMediaToSelectedPlaylist(indexOfPlaylist, CreateImageFile(openFileDialog.FileName));
                }
                else
                {
                    MessageBoxes.ShowInformationMessageBox("Please select a playlist.");
                }
            }
            else
            {
                MessageBoxes.ShowInformationMessageBox("No File was selected");
            }
        }

        private IMediaFile CreateImageFile(string fullPath)
        {
            Bitmap image = new Bitmap(fullPath);
            return mediaHandler.CreateImageObject(fullPath, image, FileHandler.GetFileName(fullPath));
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
            if(treeViewNodes.Count != 0)
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
                MessageBoxes.ShowInformationMessageBox("You did no selection. ThFe navigation area was not created.");
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
                        Playlist newPlaylist = new Playlist(creationPlaylistWindow.TitlaOfPlaylist, creationPlaylistWindow.DescriptionOfPlaylist, creationPlaylistWindow.DurationBetweenMedia);
                        if(playlistHandler.AddPlaylist(newPlaylist))
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

            if ((sender as TreeView).SelectedItem != null && indexOfPlaylist != -1)
            {
                List<MediaFile> mediaFileList = playlistHandler.GetMediaFiles(indexOfPlaylist);
                if (mediaFileList.Count > 0)
                {
                    CreateUiForEveryMediaType(mediaFileList);
                }
                else
                {
                    MessageBoxes.ShowInformationMessageBox("No media to show in the Playlist selected");
                }
            }
        }

        private void CreateUiForEveryMediaType(List<MediaFile> mediaFiles)
        {
            foreach(MediaFile media in mediaFiles)
            {
                if(playlistHandler.IsMediaVideo(media))
                {
                    MediaViewSelectionUserControl userControl = new MediaViewSelectionUserControl();
                    Video video = (media as Video);
                    userControl.MediaId = video.Id;
                    userControl.MediaName = video.Name;
                    userControl.MediaImageSource = video.SourceUrl;
                    mediaToItemsControl.Add(userControl);
                }
                else
                {
                    MediaViewSelectionUserControl userControl = new MediaViewSelectionUserControl();
                    MultiMediaClassesAndManagers.MediaSubClasses.Image image = (media as MultiMediaClassesAndManagers.MediaSubClasses.Image);
                    userControl.MediaId = image.Id;
                    userControl.MediaName = image.Name;
                    userControl.MediaImageSource = image.SourceUrl;
                    mediaToItemsControl.Add(userControl);
                }
            }
            mediaItemsControl.ItemsSource = mediaToItemsControl;
        }
    }
}
