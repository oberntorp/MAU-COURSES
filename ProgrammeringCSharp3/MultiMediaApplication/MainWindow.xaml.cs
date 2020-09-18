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

namespace MultiMediaApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PlaylistManager playlistManager = null;
        public MainWindow()
        {
            playlistManager = new PlaylistManager();
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
                int idOfPlayList = GetPlaylistIdOfSelected(NameOfSelectedTreeViewNode());
                indexOfPlaylist = idOfPlayList - 1;

                if (PlayListTreeView.SelectedItem != null && idOfPlayList > 0)
                {
                    AddMediaToSelectedPlaylist(indexOfPlaylist, CreateImageFile(openFileDialog.FileName));
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

        private string NameOfSelectedTreeViewNode()
        {
            return (((PlayListTreeView.SelectedItem as TreeViewItem).Header as StackPanel).Children[1] as Label).Content.ToString();
        }

        private int GetPlaylistIdOfSelected(string nameOfSelectedTreeViewNode)
        {
            int idToReturn = 0;
            Playlist playList = playlistManager.GetAllItems().Where(pl => pl.Title == nameOfSelectedTreeViewNode).FirstOrDefault();
            if (playList != null)
            {
                idToReturn = playList.Id;
            }

            return idToReturn;
        }

        private IMediaFile CreateImageFile(string fullPath)
        {
            Bitmap image = new Bitmap(fullPath);

            string fileName = FileHandler.GetFileName(fullPath);
            return new MultiMediaClassesAndManagers.MediaSubClasses.Image(fileName, fullPath, FileHandler.GetFileExtension(fileName), image.Width, image.Height);
        }

        private void AddMediaToSelectedPlaylist(int indexOfPlaylistToReceiveMedia, IMediaFile mediaFileToAdd)
        {
            Playlist updatedPlaylist = playlistManager.GetAt(indexOfPlaylistToReceiveMedia);
            updatedPlaylist.AddMediaToPlayList((MultiMediaClassesAndManagers.MediaSubClasses.Image)mediaFileToAdd);
        }

        private void ChoseFolderForNavigationArea_Click(object sender, RoutedEventArgs e)
        {
            if (PlayListTreeView.HasItems)
            {
                MessageBoxes.ShowInformationMessageBox("Your current navigation area will be replaced and your playlists created will be removed.");
            }

            playlistManager.DeleteAll();
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
                return CreateTreeViewNodesFromFolderContent(folderPaths);
            }
            else
            {
                MessageBoxes.ShowInformationMessageBox("You did no selection. The navigation area was not created.");
            }

            return new List<TreeViewNode>();
        }

        private static List<TreeViewNode> CreateTreeViewNodesFromFolderContent(List<string> selectedFolders)
        {
            List<TreeViewNode> treeViewNodes = new List<TreeViewNode>();

            foreach (string folderPath in selectedFolders)
            {
                TreeViewNode treeViewNode = new TreeViewNode(TreeNodeTypes.directory, folderPath.Split('\\').Last());
                treeViewNode.SubNodes = GetSubTreeViewNodes(folderPath);
                treeViewNodes.Add(treeViewNode);
            }

            return treeViewNodes;
        }

        private static List<TreeViewNode> GetSubTreeViewNodes(string folderPath)
        {
            List<TreeViewNode> treeViewNodeList = new List<TreeViewNode>();

            foreach (string nameOfSubDirectory in Directory.GetDirectories(folderPath))
            {
                TreeViewNode subTreeViewNode = new TreeViewNode(TreeNodeTypes.directory, FileHandler.GetFileName(nameOfSubDirectory));
                subTreeViewNode.SubNodes = subTreeViewNode.SubNodes = GetSubTreeViewNodes(nameOfSubDirectory);
                treeViewNodeList.Add(subTreeViewNode);
            }

            return treeViewNodeList;
        }

        private void FillTreeViewWithNodes(List<TreeViewNode> treeNodes)
        {
            TreeViewItem rootNode = GetRootTreeViewItem(treeNodes);
            AddSubNodes(treeNodes[0], ref rootNode);
            PlayListTreeView.Items.Add(rootNode);
        }

        private TreeViewItem GetRootTreeViewItem(List<TreeViewNode> treeNodes)
        {
            TreeViewItem treeViewItem = new TreeViewItem();
            treeViewItem.Header = GetStackOfTreeViewNode(treeNodes.ElementAt(0));
            return treeViewItem;
        }

        private static StackPanel GetStackOfTreeViewNode(TreeViewNode treeNode)
        {
            StackPanel stack = new StackPanel();
            stack.Orientation = System.Windows.Controls.Orientation.Horizontal;
            System.Windows.Controls.Image icon = new System.Windows.Controls.Image();
            string iconPath = (treeNode.type == TreeNodeTypes.directory) ? "folder_icon8.png" : "video_playlist_icons8.png";
            icon.Source = new BitmapImage(new Uri($"/Images/{iconPath}", UriKind.Relative));
            icon.Height = 16;
            icon.Width = 16;
            Label nameOfNode = new Label();
            nameOfNode.Content = treeNode.Name;
            stack.Children.Add(icon);
            stack.Children.Add(nameOfNode);
            return stack;
        }

        private static void AddSubNodes(TreeViewNode currentNode, ref TreeViewItem parentTreeViewItem)
        {
            foreach (TreeViewNode subNode in currentNode.SubNodes)
            {
                TreeViewItem childTreeViewItem = new TreeViewItem();
                childTreeViewItem.Header = GetStackOfTreeViewNode(subNode);
                AddSubNodes(subNode, ref childTreeViewItem);
                parentTreeViewItem.Items.Add(childTreeViewItem);
            }
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
                        newPlaylistTreeViewItem.Header = GetStackOfTreeViewNode(playlistTreeViewNode);
                        (PlayListTreeView.SelectedItem as TreeViewItem).IsExpanded = true;
                        (PlayListTreeView.SelectedItem as TreeViewItem).Items.Add(newPlaylistTreeViewItem);
                        playlistManager.AddPlaylist(newPlaylist);
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
            int idOfPlayList = GetPlaylistIdOfSelected(NameOfSelectedTreeViewNode());
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
