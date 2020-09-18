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

        public string Title { get => playlistName; }
        public string Description { get => playlistDescription; }

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

        public bool AddMediaToPlayList(MediaFile mediaToAdd)
        {
            if(mediaToAdd != null)
            {
                return playlistContent.Add(mediaToAdd);
            }

            return false;
        }

        public bool DeleteMediaFromPlayList(int indexOfMediaToDelete)
        {
            return playlistContent.DeleteAt(indexOfMediaToDelete);
        }

        public void ClearPlayList()
        {
            playlistContent.DeleteAll();
        }
    }
}
