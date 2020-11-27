using System;
using System.Collections.Generic;
using System.Text;
using MultiMediaClassesAndManagers.Managers;
using MultiMediaClassesAndManagers.TreeNode;
using MultiMediaClassesAndManagers.TreeViewSave;
using MultiMediaDataAccess;
using MutiMediaClassesAndManagers;

namespace MultiMediaBussinessLogic
{
    /// <summary>
    /// This Class acts as a BussinessLogic class for treeViewStructure operations and takes care of the access to the TreeViewStructureManager
    /// </summary>
    public class TreeViewStructureHandler
    {
        private TreeViewStructureManager treeViewStructureManager = null;
        DatabaseOperations dataoperations = null;

        /// <summary>
        /// TreeViewStructureHandler constructor, initializes an object to treeViewStructureManager
        /// </summary>
        public TreeViewStructureHandler()
        {
            treeViewStructureManager = new TreeViewStructureManager();
            dataoperations = new DatabaseOperations();
        }

        /// <summary>
        /// Adds a tree>ViewStructure to the handler
        /// </summary>
        /// <param name="treeViewStructureToSave">The treeViewStructure to add</param>
        /// <returns>true/false</returns>
        public bool AddTreeViewStructure(TreeViewStructure treeViewStructureToSave)
        {
            return treeViewStructureManager.AddTreeViewSaveStructure(treeViewStructureToSave);
        }

        /// <summary>
        /// Adds List of playlists to the treeViewStructure
        /// </summary>
        /// <param name="playlistsToAdd"></param>
        public void AddPlaylistsToTreeViewStructure(List<Playlist> playlistsToAdd)
        {
            treeViewStructureManager.AddPlaylistsToTreeViewStructure(playlistsToAdd);
        }

        /// <summary>
        /// Deletes the structure in the manager
        /// </summary>
        public void DeleteStructure()
        {
            treeViewStructureManager.DeleteAll();
        }

        /// <summary>
        /// Saves the TreeViewStructure as XML
        /// </summary>
        /// <param name="fileName">Where to save the XML</param>
        public void SaveAsXML(string fileName)
        {
            treeViewStructureManager.SerializeToXML(fileName);
        }

        /// <summary>
        /// Load Structure from XML
        /// </summary>
        /// <param name="fileName">Where the file loaded is located</param>
        public void LoadFromXML(string fileName)
        {
            treeViewStructureManager.XMLDeserialize(fileName);
        }

        /// <summary>
        /// Get all treeViewNodes representing directories
        /// </summary>
        /// <returns>List of treeViewNodes</returns>
        public List<TreeViewNode> GetAllTreeViewNodes()
        {
            return treeViewStructureManager.GetAllItems()[0].TreeViewDirectoryNodes;
        }

        /// <summary>
        /// Get all playlists
        /// </summary>
        /// <returns>List of playlists</returns>
        public List<Playlist> GetAllPlaylists()
        {
            return treeViewStructureManager.GetAllItems()[0].Playlists;
        }

        public TreeViewStructure GetTreeViewStructureFromDb()
        {
            return dataoperations.GetPlaylistsAndNavigationFromDb();
        }

        public void DeleteStructureFromDataBase()
        {
            dataoperations.DeleteNavigationStructureFromDb();
        }
    }
}
