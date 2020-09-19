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

        /// <summary>
        /// PlaylistHandler constructor, initializes PlaylistManager
        /// </summary>
        public PlaylistHandler()
        {
            PlaylistManager = new PlaylistManager();
        }

        /// <summary>
        /// Get id of an selected Playlist
        /// </summary>
        /// <param name="nameOfSelectedTreeViewNode">The nameof the selected TreeViewNode</param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds a media file to a selectedPlaylist
        /// </summary>
        /// <param name="indexOfPlaylistToReceiveMedia">Index where the playlist being updated resides</param>
        /// <param name="mediaFileToAdd">The media file to add</param>
        public void AddMediaToSelectedPlaylist(int indexOfPlaylistToReceiveMedia, IMediaFile mediaFileToAdd)
        {
            Playlist updatedPlaylist = PlaylistManager.GetAt(indexOfPlaylistToReceiveMedia);
            updatedPlaylist.AddMediaToPlayList((MultiMediaClassesAndManagers.MediaSubClasses.Image)mediaFileToAdd);
        }

        /// <summary>
        /// Deletes all playlists
        /// </summary>
        public void DeleteAllPlaylists()
        {
            PlaylistManager.DeleteAll();
        }
    }
}
