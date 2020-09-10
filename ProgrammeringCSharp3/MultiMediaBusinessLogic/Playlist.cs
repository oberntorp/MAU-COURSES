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
            int playListContentCountBeforeAdd = PlayListContentCount;
            if(mediaToAdd != null)
            {
                playlistContent.Add(mediaToAdd);
            }

            return WasAddSuccessFull(playListContentCountBeforeAdd);
        }

        private bool WasAddSuccessFull(int countBeforeInsert)
        {
            return PlayListContentCount > countBeforeInsert;
        }
    }
}
