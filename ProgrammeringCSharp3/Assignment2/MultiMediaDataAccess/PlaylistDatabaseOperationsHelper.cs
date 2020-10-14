using MultiMediaClassesAndManagers.Interfaces;
using MultiMediaClassesAndManagers.MediaSubClasses;
using MultiMediaClassesAndManagers.TreeNode;
using MultiMediaClassesAndManagers.TreeViewSave;
using MultiMediaDataAccess.DatabaseModelAndContext;
using MultiMediaDataAccess.DatabaseModelAndContext.Models;
using MutiMediaClassesAndManagers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiMediaDataAccess.Convert;

namespace MultiMediaDataAccess
{
    /// <summary>
    /// The playlistDatabaseOperationsHelper is the class responsible for connecting to the database and add/delete PlaylistModel objects stored in the tables
    /// </summary>
    internal class PlaylistDatabaseOperationsHelper
    {
        private MultiMediaContext dbContext;
        private DbModelToApplicationModel dbModelToApplicationModel;

        /// <summary>
        /// Constructor initializing the context and helper classes
        /// </summary>
        /// <param name="dbContextIn"></param>
        public PlaylistDatabaseOperationsHelper(MultiMediaContext dbContextIn)
        {
            dbContext = dbContextIn;
            dbModelToApplicationModel = new DbModelToApplicationModel();
        }

        /// <summary>
        /// Creates a PlaylistModel to be filled with Media and inserted into the database, this method only fills the object with metadata
        /// </summary>
        /// <param name="playlistToAddToDataBase">Containing the basic information to be used to construct the database model</param>
        /// <returns>PlaylistModel with metaData</returns>
        internal PlaylistModel CreatePlaylistModelWithMetaData(Playlist playlistToAddToDataBase)
        {
            PlaylistModel playlistModel = new PlaylistModel();
            playlistModel.Id = playlistToAddToDataBase.Id;
            playlistModel.Title = playlistToAddToDataBase.Title;
            playlistModel.Description = playlistToAddToDataBase.Description;
            playlistModel.PlaylistPlaybackDelayBetweenMediaSec = playlistToAddToDataBase.PlaylistPlaybackDelayBetweenMediaSec;
            playlistModel.Image = new List<ImageModel>();
            playlistModel.Video = new List<VideoModel>();

            return playlistModel;
        }

        /// <summary>
        /// Adds the Media and ParentNode to the PlaylistModel
        /// </summary>
        /// <param name="playlistToAddToDataBase">Containing the Media information to be used to fill the database model with ParentNode and Media</param>
        /// <param name="playlistModel">PlaylistModel to be filled with Media and ParentNode</param>
        /// <returns>PlaylistModel with Media and ParentNode</returns>
        internal PlaylistModel AddRelatingItemsToPlaylistModel(Playlist playlistToAddToDataBase, PlaylistModel playlistModel)
        {
            AddParentNodePlaylist(playlistToAddToDataBase, playlistModel);
            AddMeddiaToPlaylist(playlistToAddToDataBase, playlistModel);
            return playlistModel;
        }
        /// <summary>
        /// Adds a parent node to the PlaylistModel
        /// </summary>
        /// <param name="playlistToAddToDataBase">Playlist containing the parentNode</param>
        /// <param name="playlistModel">PlaylistModel to get it´s ParentNode</param>
        private void AddParentNodePlaylist(Playlist playlistToAddToDataBase, PlaylistModel playlistModel)
        {
            TreeViewNodeModel newTreeViewNodeModel = new TreeViewNodeModel();
            newTreeViewNodeModel.Name = playlistToAddToDataBase.ParentNode.Name;

            newTreeViewNodeModel.SubNodes = AddSubNodes(playlistToAddToDataBase.ParentNode);
            playlistModel.ParentNode = newTreeViewNodeModel;
            dbContext.TreeViewNodes.Add(newTreeViewNodeModel);
        }

        /// <summary>
        /// Adds subNodes to a node
        /// </summary>
        /// <param name="node">Nodes to build the subNodes from</param>
        /// <returns>List of treeViewNodeModel</returns>
        private static List<TreeViewNodeModel> AddSubNodes(TreeViewNode node)
        {
            List<TreeViewNodeModel> nodes = new List<TreeViewNodeModel>();
            foreach (TreeViewNode subNode in node.SubNodes)
            {
                TreeViewNodeModel newTreeViewNodeModel = new TreeViewNodeModel();
                newTreeViewNodeModel.Name = subNode.Name;
                newTreeViewNodeModel.SubNodes = AddSubNodes(subNode);
                nodes.Add(newTreeViewNodeModel);
            }

            return nodes;
        }

        /// <summary>
        /// Adds media to a given PlaylistModel
        /// </summary>
        /// <param name="playlistToAddToDataBase">Playlist containing the Media</param>
        /// <param name="playlistModel">PlaylistModel receiving media</param>
        private void AddMeddiaToPlaylist(Playlist playlistToAddToDataBase, PlaylistModel playlistModel)
        {
            foreach (IMediaFile media in playlistToAddToDataBase.GetAllMediaFromPlaylist())
            {
                if (media is Image)
                {
                    ImageModel newImageModel = new ImageModel();
                    Image imageData = media as Image;
                    newImageModel.Id = imageData.Id;
                    newImageModel.Name = imageData.Name;
                    newImageModel.PreviewUrl = imageData.PreviewUrl;
                    newImageModel.SourceUrl = imageData.SourceUrl;
                    newImageModel.FileExtention = imageData.FileExtention;
                    newImageModel.Width = imageData.Width;
                    newImageModel.Height = imageData.Height;

                    playlistModel.Image.Add(newImageModel);

                    dbContext.Images.Add(newImageModel);
                }
                else
                {
                    VideoModel newVideoModel = new VideoModel();
                    Video videoData = media as Video;
                    newVideoModel.Id = videoData.Id;
                    newVideoModel.Name = videoData.Name;
                    newVideoModel.PreviewUrl = videoData.PreviewUrl;
                    newVideoModel.SourceUrl = videoData.SourceUrl;
                    newVideoModel.FileExtention = videoData.FileExtention;
                    newVideoModel.LengthInSeconds = videoData.LengthInSeconds;

                    playlistModel.Video.Add(newVideoModel);
                    dbContext.Videos.Add(newVideoModel);
                }
            }
        }

        /// <summary>
        /// Delete all PlaylistData in the database
        /// </summary>
        internal void DeleteAllPlaylistData()
        {
            List<PlaylistModel> playlists = GetPlaylists();

            foreach (PlaylistModel playlist in playlists)
            {
                RemoveRelationsToPlaylist(playlist);
                dbContext.Playlists.Remove(playlist);

                RemoveParentTreeViewNode(playlist);
            }
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Remove ParentNode of playlist from the database
        /// </summary>
        /// <param name="playlist"></param>
        private void RemoveParentTreeViewNode(PlaylistModel playlist)
        {
            if (dbContext.TreeViewNodes != null && playlist.ParentNode != null)
            {
                dbContext.TreeViewNodes.Remove(playlist.ParentNode);
            }
        }

        /// <summary>
        /// Removes Media pertaining to a givenplaylist from the database
        /// </summary>
        /// <param name="playlist">The playlist referencing the Media to delete</param>
        private void RemoveRelationsToPlaylist(PlaylistModel playlist)
        {
            if (playlist.Video != null)
            {
                dbContext.Videos.RemoveRange(playlist.Video);
            }
            if (playlist.Image != null)
            {
                dbContext.Images.RemoveRange(playlist.Image);
            }
        }

        /// <summary>
        /// Get the playlist stored in the database 
        /// </summary>
        /// <returns></returns>
        internal List<PlaylistModel> GetPlaylists()
        {
            return dbContext.Playlists.Include("Image").Include("Video").Include("ParentNode").ToList();
        }

        public TreeViewStructure ConvertDatabaseObjectToApplicationPlaylistObject(List<PlaylistModel> playlistsFromDatabase, List<TreeViewNode> treeViewNodes)
        {
            return dbModelToApplicationModel.ConvertDatabaseObjectToApplicationPlaylistObject(playlistsFromDatabase, treeViewNodes);
        }
    }
}