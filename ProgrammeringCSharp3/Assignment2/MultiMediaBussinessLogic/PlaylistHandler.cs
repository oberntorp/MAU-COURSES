using MultiMediaClassesAndManagers.Managers;
using MultiMediaClassesAndManagers.Interfaces;
using MultiMediaClassesAndManagers.MediaBaseClass;
using MultiMediaClassesAndManagers.MediaSubClasses;
using MutiMediaClassesAndManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using MultiMediaDataAccess;
using System.Data.Common;

namespace MultiMediaBussinessLogic
{
    /// <summary>
    /// This Class acts as a BussinessLogic class for playlist operations and takes care of the access to the PlaylistManager
    /// </summary>
    public class PlaylistHandler
    {
        private PlaylistManager playlistManager = null;
        private MediaHandler mediaHandler = null;
        DatabaseOperations dataoperations = null;

        public PlaylistManager PlaylistManager { get => playlistManager; }

        /// <summary>
        /// PlaylistHandler constructor, initializes PlaylistManager
        /// </summary>
        public PlaylistHandler()
        {
            playlistManager = new PlaylistManager();
            mediaHandler = new MediaHandler();
            dataoperations = new DatabaseOperations();
        }

        /// <summary>
        /// Calls AddPlaylist on playlistManager, this way the playlist gets an id
        /// </summary>
        /// <param name="playlistToAdd">The playlist being added</param>
        /// <returns>true/false for success</returns>
        public bool AddPlaylist(Playlist playlistToAdd)
        {
            return playlistManager.AddPlaylist(playlistToAdd);
        }

        /// <summary>
        /// Get id of an selected Playlist
        /// </summary>
        /// <param name="nameOfSelectedTreeViewNode">The nameof the selected TreeViewNode</param>
        /// <returns>The id of the selected Playlist</returns>
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
        /// <param name="indexOfPlaylistToReceiveMedia">Index where the playlist being updated resides, first the mediaFile needs to be of Video or Image type</param>
        /// <param name="mediaFileToAdd">The media file to add</param>
        public void AddMediaToSelectedPlaylist(int indexOfPlaylistToReceiveMedia, IMediaFile mediaFileToAdd)
        {
            Playlist updatedPlaylist = playlistManager.GetAt(indexOfPlaylistToReceiveMedia);
            updatedPlaylist.AddMediaToPlaylist(CastMediaToVideoOrImage(mediaFileToAdd));
        }

        /// <summary>
        /// Casts a given media into Video or Image
        /// </summary>
        /// <param name="mediaFileToCast">The mediaFile needing cast operation</param>
        /// <returns>MediaFile going into the playlist media collection</returns>
        private MediaFile CastMediaToVideoOrImage(IMediaFile mediaFileToCast)
        {
            if (mediaHandler.IsMediaVideo((MediaFile)mediaFileToCast))
            {
                return (MultiMediaClassesAndManagers.MediaSubClasses.Video)mediaFileToCast;
            }
            else
            {
                return (MultiMediaClassesAndManagers.MediaSubClasses.Image)mediaFileToCast;
            }
        }

        /// <summary>
        /// Deletes all playlists
        /// </summary>
        public void DeleteAllPlaylists()
        {
            playlistManager.DeleteAll();
        }

        /// <summary>
        /// Get all nedia files in a playlist
        /// </summary>
        /// <param name="indexOfPlaylist">index of playlist where the media is stored</param>
        /// <returns></returns>
        public List<MediaFile> GetMediaFiles(int indexOfPlaylist)
        {
            return playlistManager.GetAt(indexOfPlaylist).GetAllMediaFromPlaylist();
        }

        public void InsertPlaylistsIntoDb()
        {
            foreach (Playlist playlist in PlaylistManager.GetAllItems())
            {
                dataoperations.InsertPlaylistToDb(playlist);
            }
        }

        public void DeleteAllPlaylistsFromDB()
        {
            dataoperations.DeleteAllPLaylistFromDb();
        }
    }
}
