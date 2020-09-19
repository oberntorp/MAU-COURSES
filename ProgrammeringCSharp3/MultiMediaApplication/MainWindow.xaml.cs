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
        public MainWindow()
        {
            playlistHandler = new PlaylistHandler();
            treeViewNodesHandler = new TreeViewNodesHandler();
            mediaHandler = new MediaHandler();

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
                int idOfPlayList =  playlistHandler.GetPlaylistIdOfSelected(treeViewNodesHandler.NameOfSelectedTreeViewNode(PlayListTreeView));
                indexOfPlaylist = idOfPlayList - 1;

                if (PlayListTreeView.SelectedItem != null && idOfPlayList > 0)
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

            string fileName = FileHandler.GetFileName(fullPath);
            return mediaHandler.GetImageObject(fullPath, image, fileName);
        }

        private void ChoseFolderForNavigationArea_Click(object sender, RoutedEventArgs e)
        {
            if (PlayListTreeView.HasItems)
            {
                MessageBoxes.ShowInformationMessageBox("Your current navigation area will be replaced and your playlists created will be removed.");
            }

            playlistHandler.DeleteAllPlaylists();
            PlayListTreeView.Items.Clear();
            List<TreeViewNode> treeViewNodes = GetNodesOfTreeView();
            if(treeViewNodes.Count != 0)
            {
                FillTreeViewWithNodes(treeViewNodes);
            }
        }

        private static List<TreeViewNode> GetNodesOfTreeView()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();

            if (!string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                List<string> folderPaths = new List<string>();
                folderPaths.Add(folderBrowserDialog.SelectedPath);
                folderPaths.AddRange(Directory.GetDirectories(folderBrowserDialog.SelectedPath));
                return TreeViewNodesHandler.CreateTreeViewNodesFromFolderContent(folderPaths);
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
            treeViewNodesHandler.AddSubNodes(treeNodes[0], ref rootNode);
            PlayListTreeView.Items.Add(rootNode);
        }

        private void CreatePlaylist_Click(object sender, RoutedEventArgs e)
        {
            if (PlayListTreeView.HasItems)
            {
                PlaylistCreationWindow creationPlaylistWindow = new PlaylistCreationWindow();

                if (PlayListTreeView.SelectedItem != null)
                {
                    bool result = (bool)creationPlaylistWindow.ShowDialog();
                    if (result)
                    {
                        Playlist newPlaylist = new Playlist(creationPlaylistWindow.TitlaOfPlaylist, creationPlaylistWindow.DescriptionOfPlaylist, creationPlaylistWindow.DurationBetweenMedia);
                        TreeViewNode playlistTreeViewNode = new TreeViewNode(TreeNodeTypes.playlist, newPlaylist.Title);

                        TreeViewItem newPlaylistTreeViewItem = new TreeViewItem();
                        newPlaylistTreeViewItem.Header = treeViewNodesHandler.GetStackOfTreeViewNode(playlistTreeViewNode);
                        (PlayListTreeView.SelectedItem as TreeViewItem).IsExpanded = true;
                        (PlayListTreeView.SelectedItem as TreeViewItem).Items.Add(newPlaylistTreeViewItem);
                        playlistHandler.PlaylistManager.AddPlaylist(newPlaylist);
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

        private void PlayListTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            int idOfPlayList = playlistHandler.GetPlaylistIdOfSelected(treeViewNodesHandler.NameOfSelectedTreeViewNode(PlayListTreeView));
            int indexOfPlaylist = idOfPlayList - 1;

            if ((sender as TreeView).SelectedItem != null && indexOfPlaylist > 0)
            {
                ShowMediaFiles();
            }
        }

        private void ShowMediaFiles()
        {
            throw new NotImplementedException();
        }
    }
}
