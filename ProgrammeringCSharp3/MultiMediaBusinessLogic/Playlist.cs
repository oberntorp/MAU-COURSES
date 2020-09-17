using MultiMediaClassesAndManagers.Managers;
using MutiMediaClassesAndManagers.MediaBaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace MutiMediaClassesAndManagers
{
    public class Playlist
    {
        public int Id { get; set; }
        public string playlistName = string.Empty;
        private string playlistDescription = string.Empty;
        private int playlistPlaybackDelayBetweenMediaSec = 0;
        private ListManager<MediaFile> playlistContent = null;

        public string Title { get => playlistName; }
        public string Description { get => playlistDescription; }

        public int PlayListContentCount { get => playlistContent.Count; }

        public Playlist(string nameOfPlayList, string descriptionOfPlaylist, int playbackDelayBetweenMediaSec = 5)
        {
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
