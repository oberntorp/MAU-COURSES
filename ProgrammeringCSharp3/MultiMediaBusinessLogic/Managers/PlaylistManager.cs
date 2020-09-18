﻿using MutiMediaClassesAndManagers;
using MultiMediaClassesAndManagers.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMediaClassesAndManagers.Implementations
{
    /// <summary>
    /// This Class Manages the insertion/deletion of Playlists
    /// </summary>
    public class PlaylistManager: ListManager<Playlist>
    {
        private int playlistId = 1;

        /// <summary>
        /// As an Playlist shall have an Id, this method first adds that it, then calls Add from ListManager
        /// </summary>
        /// <param name="animalToAdd"> the animal to add</param>
        /// <returns></returns>
        public Playlist AddPlaylist(Playlist playlistToAdd)
        {
            AddIdToPlaylist(ref playlistToAdd);
            return Add(playlistToAdd) ? playlistToAdd : null;
        }

        /// <summary>
        /// This method adds an id to the Playlist
        /// </summary>
        /// <param name="playlistToAddAnId">The Playlistobject getting an id</param>
        private void AddIdToPlaylist(ref Playlist playlistToAddAnId)
        {
            if (Count == 0)
            {
                playlistToAddAnId.Id = playlistId++;
            }
            else
            {
                playlistToAddAnId.Id = Count + 1;
            }
        }

        /// <summary>
        /// Delete a specific Playlist
        /// </summary>
        /// <param name="idOfPlaylistToRemove">The id of the Playlist to remove</param>
        /// <returns>true/false to keep track of success</returns>
        public bool DeletePlaylist(int idOfPlaylistToRemove)
        {
            return DeleteAt(idOfPlaylistToRemove);
        }

        /// <summary>
        /// Delete all Playlists
        /// </summary>
        public void DeleteAllPlaylists()
        {
            DeleteAll();
        }
    }
}
