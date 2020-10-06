using MultiMediaClassesAndManagers.Interfaces;
using MultiMediaClassesAndManagers.MediaSubClasses;
using MultiMediaClassesAndManagers.TreeNode;
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

        public DatabaseOperations()
        {
            dbContext = new MultiMediaContext();
            playlistOperationsHelper = new PlaylistDatabaseOperationsHelper(ref dbContext);
        }

        public void InsertPLaylistToDb(Playlist playlistToAddToDataBase)
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
    }
}
