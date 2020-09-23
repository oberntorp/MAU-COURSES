using MultiMediaClassesAndManagers.Interfaces;
using MultiMediaClassesAndManagers.Managers;
using MultiMediaClassesAndManagers.MediaBaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace MutiMediaClassesAndManagers
{
    /// <summary>
    /// This class makes up how a playlist looks and what is possible to do
    /// </summary>
    public class Playlist
    {
        public int Id { get; set; }
        private string playlistName = string.Empty;
        private string playlistDescription = string.Empty;
        private int playlistPlaybackDelayBetweenMediaSec = 0;
        private ListManager<MediaFile> playlistContent = null;
        private int mediaId = 1;

        public string Title { get => playlistName; }
        public string Description { set => playlistDescription = value;  get => playlistDescription; }
        public int PlaylistPlaybackDelayBetweenMediaSec { set => playlistPlaybackDelayBetweenMediaSec = value; get => playlistPlaybackDelayBetweenMediaSec; }

        public int PlayListContentCount { get => playlistContent.Count; }

        /// <summary>
        /// The Playlist constructor, initializes a playlist
        /// </summary>
        /// <param name="nameOfPlayList">thename of the playlist</param>
        /// <param name="descriptionOfPlaylist">the description of the playlist</param>
        /// <param name="playbackDelayBetweenMediaSec">the time between playing clips</param>
        public Playlist(string nameOfPlayList, string descriptionOfPlaylist, int playbackDelayBetweenMediaSec = 5)
        {
            playlistContent = new ListManager<MediaFile>();
            playlistName = nameOfPlayList;
            playlistDescription = descriptionOfPlaylist;
            playlistPlaybackDelayBetweenMediaSec = playbackDelayBetweenMediaSec;
        }

        /// <summary>
        /// Adds the media to the playlist, by first giving it an id, then adding it by the base classes method
        /// </summary>
        /// <param name="mediaToAdd">The media being added</param>
        /// <returns></returns>
        public bool AddMediaToPlaylist(MediaFile mediaToAdd)
        {
            AddIdToMediaFile(ref mediaToAdd);
            if(mediaToAdd != null)
            {
                return playlistContent.Add(mediaToAdd);
            }

            return false;
        }

        /// <summary>
        /// Adds an id to the media being added
        /// </summary>
        /// <param name="mediaFileToAddAnId"></param>
        private void AddIdToMediaFile(ref MediaFile mediaFileToAddAnId)
        {
            if (PlayListContentCount == 0)
            {
                mediaFileToAddAnId.Id = mediaId++;
            }
            else
            {
                mediaFileToAddAnId.Id = PlayListContentCount + 1;
            }
        }

        /// <summary>
        /// Deletes a particular media
        /// </summary>
        /// <param name="indexOfMediaToDelete"></param>
        /// <returns>true/false</returns>
        public bool DeleteMediaFromPlaylist(int indexOfMediaToDelete)
        {
            return playlistContent.DeleteAt(indexOfMediaToDelete);
        }

        /// <summary>
        /// Removes all media from a playlist
        /// </summary>
        public void ClearPlaylist()
        {
            playlistContent.DeleteAll();
        }

        /// <summary>
        /// Gets all media from a playlist
        /// </summary>
        /// <returns>List of media in playlist</returns>
        public List<MediaFile> GetAllMediaFromPlaylist()
        {
            return playlistContent.GetAllItems();
        }
    }
}
