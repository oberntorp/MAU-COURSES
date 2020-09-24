using MultiMediaClassesAndManagers.TreeNode;
using MutiMediaClassesAndManagers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMediaClassesAndManagers.TreeViewSave
{
    /// <summary>
    /// Class reresenting TreeViewNodesCompleteWithDirectoryNames and its containing Playlists
    /// </summary>
    [Serializable]
    public class TreeViewStructure
    {
        public int Id { get; set; }

        private List<TreeViewNode> treeViewDirectoryNodes = null;
        public List<Playlist> Playlists { get; set; }

        /// <summary>
        /// Initiates a TreeViewSaveStructure object, sets treeViewDirectoryNodes
        /// </summary>
        /// <param name="listOfTreeViewDirectoryNodes">list of treeViewNodes to set</param>
        public TreeViewStructure(List<TreeViewNode> listOfTreeViewDirectoryNodes)
        {
            treeViewDirectoryNodes = listOfTreeViewDirectoryNodes;
        }
    }
}
