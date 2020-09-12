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
        private string playlistName = string.Empty;
        private string playlistDescription = string.Empty;
        private ListManager<MediaFile> playlistContent = null;
        public int PlayListContentCount { get => playlistContent.Count; }

        public Playlist(string nameOfPlayList, string descriptionOfPlaylist)
        {
            playlistName = nameOfPlayList;
            playlistDescription = descriptionOfPlaylist;
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
