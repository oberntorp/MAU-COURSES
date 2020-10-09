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
    public class DatabaseOperations
    {
        MultiMediaContext dbContext;
        PlaylistDatabaseOperationsHelper playlistOperationsHelper;
        TreeViewNodeDatabaseHelper treeViewNodeDatabaseHelper;
        VideoDatabaseOperationsHelper videoDatabaseOperationsHelper;
        ImagesDatabaseOperationsHelper imagesDatabaseOperationsHelper;

        public DatabaseOperations()
        {
            dbContext = new MultiMediaContext();
            playlistOperationsHelper = new PlaylistDatabaseOperationsHelper(ref dbContext);
            treeViewNodeDatabaseHelper = new TreeViewNodeDatabaseHelper(ref dbContext);
            videoDatabaseOperationsHelper = new VideoDatabaseOperationsHelper(ref dbContext);
            treeViewNodeDatabaseHelper = new TreeViewNodeDatabaseHelper(ref dbContext);
        }

        public void InsertPlaylistToDb(Playlist playlistToAddToDataBase)
        {
            PlaylistModel playlistModel = playlistOperationsHelper.CreatePlaylistModelWithMetaData(playlistToAddToDataBase);

            playlistModel = playlistOperationsHelper.AddRelatingItemsToPlaylistModel(playlistToAddToDataBase, playlistModel);

            dbContext.Playlists.Add(playlistModel);
            dbContext.SaveChanges();
        }

        public void DeleteAllPLaylistFromDb()
        {
            playlistOperationsHelper.DeleteAllPlaylistData();
        }

        public TreeViewStructure GetPlaylistsAndNavigationFromDb()
        {
            return GetTreeViewStructure();
        }

        private TreeViewStructure GetTreeViewStructure()
        {
            var playlists = playlistOperationsHelper.GetPlaylists();
            var nodes = treeViewNodeDatabaseHelper.GetTreeViewNodesFromDatabase();

            return playlistOperationsHelper.ConvertDatabaseObjectToApplicationPlaylistObject(playlists.ToList(), nodes);
            
        }
    }
}
