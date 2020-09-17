using Microsoft.Win32;
using MultiMediaClassesAndManagers.Implementations;
using MutiMediaClassesAndManagers;
using MutiMediaClassesAndManagers.TreeNode;
using System;
using System.Collections.Generic;
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

        }

        private void ChoseFolderForNavigationArea_Click(object sender, RoutedEventArgs e)
        {
            if (PlayListTreeView.HasItems)
            {
                MessageBoxes.ShowInformationMessageBox("Your current navigation area will be replaced and your playlists created will be removed.");
            }

            playlistManager.DeleteAll();
            PlayListTreeView.Items.Clear();
            FillTreeViewWithNodes(GetNodesOfTreeView());
        }

        private static List<TreeViewNode> GetNodesOfTreeView()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();

            if (dialogResult == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                List<string> folderPaths = new List<string>();
                folderPaths.Add(folderBrowserDialog.SelectedPath);
                folderPaths.AddRange(Directory.GetDirectories(folderBrowserDialog.SelectedPath));
                return CreateTreeViewNodesFromFolderContent(folderPaths);
            }

            return null;
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
                TreeViewNode subTreeViewNode = new TreeViewNode(TreeNodeTypes.directory, nameOfSubDirectory.Split('\\').Last());
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
            Image icon = new Image();
            string iconPath = (treeNode.type == TreeNodeTypes.directory) ? "folder_icon8.png" : "video_playlist_icons8.png";
            icon.Source = new BitmapImage(new Uri($"/Images/{iconPath}", UriKind.Relative));
            icon.Height = 16;
            icon.Width = 16;
            System.Windows.Controls.Label nameOfNode = new System.Windows.Controls.Label();
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
                        playlistManager.Add(newPlaylist);
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
    }
}
