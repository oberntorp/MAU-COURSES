using MultiMediaClassesAndManagers.Implementations;
using MultiMediaClassesAndManagers.Interfaces;
using MultiMediaClassesAndManagers.MediaBaseClass;
using MultiMediaClassesAndManagers.MediaSubClasses;
using MutiMediaClassesAndManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace MultiMediaBussinessLogic
{
    /// <summary>
    /// This Class acts as a BussinessLogic class for playlist operations and takes care of the access to the PlaylistManager
    /// </summary>
    public class PlaylistHandler
    {
        private PlaylistManager playlistManager = null;
        private MediaHandler mediaHandler = null;
        public PlaylistManager PlaylistManager { get => playlistManager; }
        /// <summary>
        /// PlaylistHandler constructor, initializes PlaylistManager
        /// </summary>
        public PlaylistHandler()
        {
            playlistManager = new PlaylistManager();
            mediaHandler = new MediaHandler();
        }

        public bool AddPlaylist(Playlist playlistToAdd)
        {
            return playlistManager.AddPlaylist(playlistToAdd);
        }

        /// <summary>
        /// Get id of an selected Playlist
        /// </summary>
        /// <param name="nameOfSelectedTreeViewNode">The nameof the selected TreeViewNode</param>
        /// <returns></returns>
        public int GetPlaylistIdOfSelected(string nameOfSelectedTreeViewNode)
        {
            int idToReturn = 0;
            Playlist playList = playlistManager.GetAllItems().Where(pl => pl.Title == nameOfSelectedTreeViewNode).FirstOrDefault();
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
            Playlist updatedPlaylist = playlistManager.GetAt(indexOfPlaylistToReceiveMedia);
            updatedPlaylist.AddMediaToPlaylist(CastMediaToVideoOrImage(mediaFileToAdd));
        }

        private MediaFile CastMediaToVideoOrImage(IMediaFile mediaFileToAdd)
        {
            if (mediaHandler.IsMediaVideo((MediaFile)mediaFileToAdd))
            {
                return (MultiMediaClassesAndManagers.MediaSubClasses.Video)mediaFileToAdd;
            }
            else
            {
                return (MultiMediaClassesAndManagers.MediaSubClasses.Image)mediaFileToAdd;
            }
        }

        /// <summary>
        /// Deletes all playlists
        /// </summary>
        public void DeleteAllPlaylists()
        {
            playlistManager.DeleteAll();
        }


        public List<MediaFile> GetMediaFiles(int indexOfPlaylist)
        {
            return playlistManager.GetAt(indexOfPlaylist).GetAllMediaFromPlaylist();
        }
    }
}
