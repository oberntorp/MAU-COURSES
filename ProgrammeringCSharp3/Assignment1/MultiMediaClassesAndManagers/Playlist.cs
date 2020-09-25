using MultiMediaClassesAndManagers.Interfaces;
using MultiMediaClassesAndManagers.Managers;
using MultiMediaClassesAndManagers.MediaBaseClass;
using MultiMediaClassesAndManagers.MediaSubClasses;
using MultiMediaClassesAndManagers.TreeNode;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MutiMediaClassesAndManagers
{
    /// <summary>
    /// This class makes up how a playlist looks and what is possible to do
    /// XmlInclude is needed as playlistContent is playlistContent is a list of MediaFile, 
    /// which is base of Image and Video, without XmlInclude, Image and Video will not be recognized by the serializer
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(Image)), XmlInclude(typeof(Video))]
    public class Playlist
    {
        public int Id { get; set; }
        private ListManager<MediaFile> playlistContent = null;
        private int mediaId = 1;

        public string Title { get; set; }
        public string Description { set; get; }
        public int PlaylistPlaybackDelayBetweenMediaSec { set; get; }
        public int PlayListContentCount { get => playlistContent.Count; }
        public TreeViewNode ParentNode { get; set; }

        // Needed for the playlists being serialized, I thought it is unnessecary to be able to serialize the ListManager
        public List<MediaFile> PlaylistContentXML { get; set; }

        /// <summary>
        /// Default constructor, needed for serialization to work
        /// </summary>
        public Playlist()
        {
        }
        /// <summary>
        /// The Playlist constructor, initializes a playlist
        /// </summary>
        /// <param name="nameOfPlayList">thename of the playlist</param>
        /// <param name="parentNode">Node information about parent, used to link loaded playlist and loaded TreeViewItem</param>
        /// <param name="descriptionOfPlaylist">the description of the playlist</param>
        /// <param name="playbackDelayBetweenMediaSec">the time between playing clips</param>
        public Playlist(string nameOfPlayList, TreeViewNode parentNode, string descriptionOfPlaylist, int playbackDelayBetweenMediaSec = 5)
        {
            playlistContent = new ListManager<MediaFile>();
            PlaylistContentXML = new List<MediaFile>();
            Title = nameOfPlayList;
            ParentNode = parentNode;
            Description = descriptionOfPlaylist;
            PlaylistPlaybackDelayBetweenMediaSec = playbackDelayBetweenMediaSec;
        }

        /// <summary>
        /// Adds the media to the playlist, by first giving it an id, then adding it by the base classes method
        /// </summary>
        /// <param name="mediaToAdd">The media being added</param>
        /// <returns></returns>
        public bool AddMediaToPlaylist(MediaFile mediaToAdd)
        {
            AddIdToMediaFile(ref mediaToAdd);
            if (mediaToAdd != null)
            {
                if (playlistContent.Add(mediaToAdd))
                {
                    PlaylistContentXML.Add(mediaToAdd);
                }
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
