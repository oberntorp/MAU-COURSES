using MultiMediaBussinessLogic;
using MultiMediaClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMediaBusinessLogic.Managers
{
    class PlaylistManager: ListManager<Playlist>
    {
        private int playlistId = 1;
        /// <summary>
        /// As an Playlist shall have an Id, this method first adds that it, then calls Add from ListManager
        /// </summary>
        /// <param name="animalToAdd"> the animal to add</param>
        /// <returns></returns>
        public bool AddPlaylist(Playlist playlistToAdd)
        {
            AddIdToPlaylist(ref playlistToAdd);
            return Add(playlistToAdd);
        }

        /// <summary>
        /// This method adds an id to the animal
        /// </summary>
        /// <param name="animalToAddAnId">animal to assign id, that is passed by reference</param>
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
    }
}
