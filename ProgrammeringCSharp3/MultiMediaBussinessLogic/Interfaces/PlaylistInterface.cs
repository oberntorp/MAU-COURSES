using MultiMediaClassesAndManagers.Implementations;
using MutiMediaClassesAndManagers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMediaBusinessLogic.Interfaces
{
    class PlaylistInterface
    {
        private PlaylistManager manager = null;

        public PlaylistInterface()
        {
            manager = new PlaylistManager();
        }

        public Playlist CreateNewPlayList(string playlistName, string playListDescription)
        {
            Playlist newPlaylist = new Playlist(playlistName, playListDescription);
            return manager.AddPlaylist(newPlaylist);
        }
    }
}
