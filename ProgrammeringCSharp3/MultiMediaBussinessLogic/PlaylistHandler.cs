using MultiMediaClassesAndManagers.Implementations;
using MultiMediaClassesAndManagers.Interfaces;
using MutiMediaClassesAndManagers;
using System;
using System.Linq;

namespace MultiMediaBussinessLogic
{
    /// <summary>
    /// This Class acts as a BussinessLogic class for playlist operations and takes care of the access to the PlaylistManager
    /// </summary>
    public class PlaylistHandler
    {
        public PlaylistManager PlaylistManager { get; set; }

        public PlaylistHandler()
        {
            PlaylistManager = new PlaylistManager();
        }

        public int GetPlaylistIdOfSelected(string nameOfSelectedTreeViewNode)
        {
            int idToReturn = 0;
            Playlist playList = PlaylistManager.GetAllItems().Where(pl => pl.Title == nameOfSelectedTreeViewNode).FirstOrDefault();
            if (playList != null)
            {
                idToReturn = playList.Id;
            }

            return idToReturn;
        }

        public void AddMediaToSelectedPlaylist(int indexOfPlaylistToReceiveMedia, IMediaFile mediaFileToAdd)
        {
            Playlist updatedPlaylist = PlaylistManager.GetAt(indexOfPlaylistToReceiveMedia);
            updatedPlaylist.AddMediaToPlayList((MultiMediaClassesAndManagers.MediaSubClasses.Image)mediaFileToAdd);
        }

        public void DeleteAllPlaylists()
        {
            PlaylistManager.DeleteAll();
        }
    }
}
