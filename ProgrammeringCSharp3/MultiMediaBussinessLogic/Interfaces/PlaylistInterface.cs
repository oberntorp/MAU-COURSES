using MultiMediaClassesAndManagers.Implementations;
using MultiMediaClassesAndManagers.Managers;
using MutiMediaClassesAndManagers;
using MultiMediaClassesAndManagers.MediaBaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMediaBusinessLogic.Interfaces
{
    class PlaylistInterface
    {
        private PlaylistManager manager = null;

        public PlaylistInterface()
        {
            manager = new PlaylistManager();
        }

        public Playlist CreateNewPlayList(string playlistName, string playListDescription)
        {
            Playlist newPlaylist = new Playlist(playlistName, playListDescription);
            return manager.AddPlaylist(newPlaylist);
        }

        public bool AddMediaToPlaylist(int indexOfPlaylist, MediaFile mediaFileToAdd)
        {
            Playlist playlistToGetMediaFile = manager.GetAt(indexOfPlaylist);
            playlistToGetMediaFile.AddMediaToPlayList(mediaFileToAdd);
            return manager.ChangeAt(playlistToGetMediaFile, indexOfPlaylist);
        }

        public bool DeleteMediaFromPlaylist(int indexOfPlaylist, int indexOfMediaToDelete)
        {
            Playlist playlistToGetMediaFile = manager.GetAt(indexOfPlaylist);
            playlistToGetMediaFile.DeleteMediaFromPlayList(indexOfMediaToDelete);
            return manager.ChangeAt(playlistToGetMediaFile, indexOfPlaylist);
        }

        public bool DeleteAllMediaFromPlaylist(int indexOfPlaylist, int indexOfMediaToDelete)
        {
            Playlist playlistToClear = manager.GetAt(indexOfPlaylist);
            playlistToClear.ClearPlayList();
            return manager.ChangeAt(playlistToClear, indexOfPlaylist);
        }

        public List<Playlist> GetAllPlaylists()
        {
            return manager.GetAllItems();
        }

        public void DeleteAllPlayLists()
        {
            manager.DeleteAll();
        }
    }
}
