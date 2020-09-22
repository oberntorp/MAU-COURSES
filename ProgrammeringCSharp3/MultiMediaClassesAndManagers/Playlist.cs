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

        public bool AddMediaToPlaylist(MediaFile mediaToAdd)
        {
            AddIdToMediaFile(ref mediaToAdd);
            if(mediaToAdd != null)
            {
                return playlistContent.Add(mediaToAdd);
            }

            return false;
        }

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

        public bool DeleteMediaFromPlaylist(int indexOfMediaToDelete)
        {
            return playlistContent.DeleteAt(indexOfMediaToDelete);
        }

        public void ClearPlaylist()
        {
            playlistContent.DeleteAll();
        }

        public List<MediaFile> GetAllMediaFromPlaylist()
        {
            return playlistContent.GetAllItems();
        }
    }
}
