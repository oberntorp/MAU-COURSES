using MultiMediaBusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMediaClasses
{
    public class Playlist
    {
        public int Id { get; set; }
        private string playlistName = string.Empty;
        private string playlistDescription = string.Empty;
        private List<MediaFile> playlistContent = null;

        public Playlist(string nameOfPlayList, string descriptionOfPlaylist, List<MediaFile> contentOfPlayList)
        {
            playlistName = nameOfPlayList;
            playlistDescription = descriptionOfPlaylist;
            playlistContent = contentOfPlayList;
        }
    }
}
