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
        public List<TreeViewNode> TreeViewDirectoryNodes { get; set; }

        public List<Playlist> Playlists { get; set; }

        public TreeViewStructure()
        {
        }

        /// <summary>
        /// Add TreeStructure
        /// </summary>
        /// <param name="listOfTreeViewDirectoryNodes">TreeStructure to add</param>
        public void AddTreeStructure(List<TreeViewNode> listOfTreeViewDirectoryNodes)
        {
            TreeViewDirectoryNodes = listOfTreeViewDirectoryNodes;
        }

        /// <summary>
        /// Adds a list of playlists to the structure
        /// </summary>
        /// <param name="playlistsToAdd">List of playlists being added</param>
        public void AddPlaylistsToTreeViewStructure(List<Playlist> playlistsToAdd)
        {
            Playlists = playlistsToAdd;
        }
    }
}
