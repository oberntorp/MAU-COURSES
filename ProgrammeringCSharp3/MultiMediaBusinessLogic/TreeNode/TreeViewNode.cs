using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMediaClassesAndManagers.TreeNode
{
    public class TreeViewNode
    {
        public TreeNodeTypes type { get; set; }
        public string Name { get; set; }

        public List<TreeViewNode> SubNodes { get; set; }

        public TreeViewNode(TreeNodeTypes treeNodeType, string treeNodeName)
        {
            type = treeNodeType;
            Name = treeNodeName;
        }
    }
}
