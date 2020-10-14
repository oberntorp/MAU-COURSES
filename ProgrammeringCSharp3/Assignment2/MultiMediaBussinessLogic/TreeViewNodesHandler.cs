using MultiMediaClassesAndManagers.TreeNode;
using MutiMediaClassesAndManagers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Utilities;

namespace MultiMediaBussinessLogic
{
    /// <summary>
    /// Class that is responsible for creating TreeViewNodes
    /// </summary>
    public class TreeViewNodesHandler
    {
        private List<TreeViewNode> treeViewNodes = null;

        public List<TreeViewNode> TreeViewNodes { get => treeViewNodes; }

        /// <summary>
        /// TreeViewNodesHandlers constructor, listOfTreeNodes gets initialized
        /// </summary>
        public TreeViewNodesHandler()
        {
            treeViewNodes = new List<TreeViewNode>();
        }

        /// <summary>
        /// Method that creates a treeViewNodes from a set of folders that has been selected
        /// </summary>
        /// <param name="selectedFolders">The folders from which the treeViewNodes are created</param>
        /// <returns></returns>
        public void CreateTreeViewNodesFromFolderContent(List<string> selectedFolders)
        {
            List<TreeViewNode> result = new List<TreeViewNode>();

            foreach (string folderPath in selectedFolders)
            {
                TreeViewNode treeViewNode = new TreeViewNode(TreeNodeTypes.directory, folderPath.Split('\\').Last());
                treeViewNode.SubNodes = GetSubTreeViewNodes(folderPath);
                result.Add(treeViewNode);
            }

            treeViewNodes = result;
        }

        /// <summary>
        /// Get subNodes of specific folder
        /// </summary>
        /// <param name="folderPath">folder path to check for subNodes</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get the root treeViewNode of all treeViewNodes
        /// </summary>
        /// <param name="treeNodes">A collection of all treeViewNodes</param>
        /// <returns>The root treeViewNode</returns>
        public TreeViewItem GetRootTreeViewItem(List<TreeViewNode> treeNodes)
        {
            TreeViewItem treeViewItem = new TreeViewItem();
            treeViewItem.Header = GetStackOfTreeViewNode(treeNodes.ElementAt(0));
            return treeViewItem;
        }

        /// <summary>
        /// Get TreeViewItem for a given playlist
        /// </summary>
        /// <param name="newPlaylist">Playlist getting treeViewItem</param>
        /// <returns>TreeViewItem for a given playlist</returns>
        public TreeViewItem GetNewPlaylistTreeViewItem(Playlist newPlaylist)
        {
            TreeViewNode playlistTreeViewNode = new TreeViewNode(TreeNodeTypes.playlist, newPlaylist.Title);

            TreeViewItem newPlaylistTreeViewItem = new TreeViewItem();
            newPlaylistTreeViewItem.Header = GetStackOfTreeViewNode(playlistTreeViewNode);
            return newPlaylistTreeViewItem;
        }

        /// <summary>
        /// Creates the stackPanel with content that a TreeViewItem.Header contains
        /// </summary>
        /// <param name="treeNode">The treeNode containing the name and type of the treeViewItem being created</param>
        /// <returns>StackPanel used in TreeViewItem.Header</returns>
        public StackPanel GetStackOfTreeViewNode(TreeViewNode treeNode)
        {
            StackPanel stack = new StackPanel();
            stack.Orientation = System.Windows.Controls.Orientation.Horizontal;
            System.Windows.Controls.Image icon = new System.Windows.Controls.Image();
            string iconPath = (treeNode.Type == TreeNodeTypes.directory) ? "folder_icon8.png" : "video_playlist_icons8.png";
            icon.Source = new BitmapImage(new Uri($"/Images/{iconPath}", UriKind.Relative));
            icon.Height = 16;
            icon.Width = 16;
            Label nameOfNode = new Label();
            nameOfNode.Content = treeNode.Name;
            stack.Children.Add(icon);
            stack.Children.Add(nameOfNode);
            return stack;
        }

        /// <summary>
        /// Add subNodes to a treeViewItemParent
        /// </summary>
        /// <param name="currentNode">Current node to check for subnodes of</param>
        /// <param name="parentTreeViewItem">Parent TreeViewItem that will contain the subNodes</param>
        public void AddSubNodesToParent(TreeViewNode currentNode, TreeViewItem parentTreeViewItem)
        {
            foreach (TreeViewNode subNode in currentNode.SubNodes)
            {
                TreeViewItem childTreeViewItem = new TreeViewItem();
                childTreeViewItem.Header = GetStackOfTreeViewNode(subNode);
                if(subNode.SubNodes != null)
                {
                    AddSubNodesToParent(subNode, childTreeViewItem);
                }
                parentTreeViewItem.Items.Add(childTreeViewItem);
            }
        }

        /// <summary>
        /// Get the name of a currently selected treeViewItem
        /// </summary>
        /// <param name="playlistTreeView">TreeView to look for currently selected name</param>
        /// <returns>The name of the currently selected node</returns>
        public string NameOfSelectedTreeViewItem(TreeView playlistTreeView)
        {
            return (((playlistTreeView.SelectedItem as TreeViewItem).Header as StackPanel).Children[1] as Label).Content.ToString();
        }

        public TreeViewNode GetTreeViewNodeByName(string nameOfSelectedTreeNode, TreeViewNode currentTreeViewNode)
        {
            if (currentTreeViewNode.Name == nameOfSelectedTreeNode)
            {
                return currentTreeViewNode;
            }
            else
            {
                foreach (TreeViewNode treeViewNode in currentTreeViewNode.SubNodes)
                {
                    if (treeViewNode.Name == nameOfSelectedTreeNode)
                    {
                        return treeViewNode;
                    }
                    else
                    {
                        return GetTreeViewNodeByName(nameOfSelectedTreeNode, treeViewNode);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameToLookFor"></param>
        /// <param name="treeViewItem"></param>
        /// <returns></returns>
        public TreeViewItem GetTreeViewItemFromName(string nameToLookFor, TreeViewItem treeViewItem)
        {
            if (NameOfTreeViewItem(treeViewItem) == nameToLookFor)
            {
                return treeViewItem;
            }
            else
            {
                foreach(TreeViewItem item in treeViewItem.Items)
                {
                    return GetTreeViewItemFromName(nameToLookFor, item);
                }
            }

            return null;
        }

        /// <summary>
        /// Get the name of a given treeViewItem
        /// </summary>
        /// <param name="treeViewItem">TreeView to look for currently selected name</param>
        /// <returns>The name of the currently selected node</returns>
        public string NameOfTreeViewItem(TreeViewItem treeViewItem)
        {
            return ((treeViewItem.Header as StackPanel).Children[1] as Label).Content.ToString();
        }

        /// <summary>
        /// Adds a treeViewNode to the handler, is used when data have been loaded from XML
        /// </summary>
        /// <param name="treeViewNodeToAdd">TreeViewNode being added</param>
        public void AddTreeViewNode(TreeViewNode treeViewNodeToAdd)
        {
            treeViewNodes.Add(treeViewNodeToAdd);
        }
    }
}
