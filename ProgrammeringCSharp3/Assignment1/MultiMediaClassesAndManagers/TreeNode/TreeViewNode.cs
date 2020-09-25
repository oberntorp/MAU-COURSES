using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMediaClassesAndManagers.TreeNode
{
    /// <summary>
    /// This class is used when setting up the TreeView (holds information needed)
    /// </summary>
    [Serializable]
    public class TreeViewNode
    {
        public TreeNodeTypes Type { get; set; }
        public string Name { get; set; }

        public List<TreeViewNode> SubNodes { get; set; }

        /// <summary>
        /// Default constructor, needed for serialization to work
        /// </summary>
        public TreeViewNode()
        {

        }
        /// <summary>
        /// THe TreeViewNode constructor, initializes a treeViewNode
        /// </summary>
        /// <param name="treeNodeType">The type of the tree node (directory/playlist)</param>
        /// <param name="treeNodeName">the name of the treeNode</param>
        public TreeViewNode(TreeNodeTypes treeNodeType, string treeNodeName)
        {
            Type = treeNodeType;
            Name = treeNodeName;
        }
    }
}
