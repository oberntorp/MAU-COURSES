using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMediaClasses.TreeNode
{
    public class TreeNode
    {
        public TreeNodeTypes type { get; set; }
        public string Name { get; set; }

        public TreeNode(TreeNodeTypes treeNodeType, string treeNodeName)
        {
            type = treeNodeType;
            Name = treeNodeName;
        }
    }
}
