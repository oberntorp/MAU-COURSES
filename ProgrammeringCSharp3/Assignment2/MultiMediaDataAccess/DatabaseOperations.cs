using MultiMediaClassesAndManagers.Interfaces;
using MultiMediaClassesAndManagers.MediaSubClasses;
using MultiMediaClassesAndManagers.TreeNode;
using MultiMediaClassesAndManagers.TreeViewSave;
using MultiMediaDataAccess.DatabaseModelAndContext;
using MultiMediaDataAccess.DatabaseModelAndContext.Models;
using MutiMediaClassesAndManagers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMediaDataAccess
{
    /// <summary>
    /// This class actsas aconteiner for the databaseoperations namely Add/Delete/Get playlists from the database (localDB)
    /// </summary>
    public class DatabaseOperations
    {
        MultiMediaContext dbContext;
        PlaylistDatabaseOperationsHelper playlistOperationsHelper;
        TreeViewNodeOperationsDatabaseHelper treeViewNodeDatabaseHelper;

        /// <summary>
        /// The class of the constreuctor, it initializes helper classes
        /// </summary>
        public DatabaseOperations()
        {
            dbContext = new MultiMediaContext();
            playlistOperationsHelper = new PlaylistDatabaseOperationsHelper(dbContext);
            treeViewNodeDatabaseHelper = new TreeViewNodeOperationsDatabaseHelper(dbContext);
        }

        /// <summary>
        /// Adds a given playlist to the db
        /// </summary>
        /// <param name="playlistToAddToDataBase">The playlist to add to the database</param>
        public void InsertPlaylistToDb(Playlist playlistToAddToDataBase)
        {
            PlaylistModel playlistModel = playlistOperationsHelper.CreatePlaylistModelWithMetaData(playlistToAddToDataBase);

            playlistModel = playlistOperationsHelper.AddRelatingItemsToPlaylistModel(playlistToAddToDataBase, playlistModel);

            dbContext.Playlists.Add(playlistModel);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Daletes all playlists from the database
        /// </summary>
        public void DeleteAllPLaylistFromDb()
        {
            playlistOperationsHelper.DeleteAllPlaylistDataFromDatabase();
        }

        /// <summary>
        /// Get the TreeViewStructure made up  by playlists and navigation
        /// </summary>
        /// <returns>TreeViewStructure with playlists and navigation</returns>
        public TreeViewStructure GetPlaylistsAndNavigationFromDb()
        {
            return GetTreeViewStructure();
        }

        /// <summary>
        /// Get the playlists and the navigation from the database as a TreeViewStructure from the database
        /// </summary>
        /// <returns>TreeViewStructure with playlists and navigation</returns>
        private TreeViewStructure GetTreeViewStructure()
        {
            var playlists = playlistOperationsHelper.GetPlaylists();
            var nodes = treeViewNodeDatabaseHelper.GetTreeViewNodesFromDatabase();

            return playlistOperationsHelper.ConvertDatabaseObjectToApplicationPlaylistObject(playlists.ToList(), nodes);
            
        }

        /// <summary>
        /// Delete NavigationStructure from the database
        /// </summary>
        public void DeleteNavigationStructureFromDb()
        {
            treeViewNodeDatabaseHelper.DeleteTreeViewNodesFromDatabase();
        }
    }
}
