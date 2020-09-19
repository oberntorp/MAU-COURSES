using MultiMediaClassesAndManagers.TreeNode;
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

    public class TreeViewNodesHandler
    {
        public static List<TreeViewNode> CreateTreeViewNodesFromFolderContent(List<string> selectedFolders)
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

        public TreeViewItem GetRootTreeViewItem(List<TreeViewNode> treeNodes)
        {
            TreeViewItem treeViewItem = new TreeViewItem();
            treeViewItem.Header = GetStackOfTreeViewNode(treeNodes.ElementAt(0));
            return treeViewItem;
        }

        public StackPanel GetStackOfTreeViewNode(TreeViewNode treeNode)
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

        public void AddSubNodes(TreeViewNode currentNode, ref TreeViewItem parentTreeViewItem)
        {
            foreach (TreeViewNode subNode in currentNode.SubNodes)
            {
                TreeViewItem childTreeViewItem = new TreeViewItem();
                childTreeViewItem.Header = GetStackOfTreeViewNode(subNode);
                AddSubNodes(subNode, ref childTreeViewItem);
                parentTreeViewItem.Items.Add(childTreeViewItem);
            }
        }

        public string NameOfSelectedTreeViewNode(TreeView playlistTreeView)
        {
            return (((playlistTreeView.SelectedItem as TreeViewItem).Header as StackPanel).Children[1] as Label).Content.ToString();
        }
    }
}
