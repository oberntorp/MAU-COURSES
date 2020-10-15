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
        /// <param name="playlistToAddToDatabase">Containing the basic information to be used to construct the database model</param>
        /// <returns>PlaylistModel with metaData</returns>
        internal PlaylistModel CreatePlaylistModelWithMetaData(Playlist playlistToAddToDatabase)
        {
            PlaylistModel playlistModel = new PlaylistModel();
            playlistModel.Id = playlistToAddToDatabase.Id;
            playlistModel.Title = playlistToAddToDatabase.Title;
            playlistModel.Description = playlistToAddToDatabase.Description;
            playlistModel.PlaylistPlaybackDelayBetweenMediaSec = playlistToAddToDatabase.PlaylistPlaybackDelayBetweenMediaSec;
            playlistModel.Image = new List<ImageModel>();
            playlistModel.Video = new List<VideoModel>();

            return playlistModel;
        }

        /// <summary>
        /// Adds the Media and ParentNode to the PlaylistModel
        /// </summary>
        /// <param name="playlistToAddToDatabase">Containing the Media information to be used to fill the database model with ParentNode and Media</param>
        /// <param name="playlistReceivingItems">PlaylistModel to be filled with Media and ParentNode</param>
        /// <returns>PlaylistModel with Media and ParentNode</returns>
        internal PlaylistModel AddRelatingItemsToPlaylistModel(Playlist playlistToAddToDatabase, PlaylistModel playlistReceivingItems)
        {
            AddParentNodePlaylist(playlistToAddToDatabase, playlistReceivingItems);
            AddMediaToPlaylist(playlistToAddToDatabase, playlistReceivingItems);
            return playlistReceivingItems;
        }
        /// <summary>
        /// Adds a parent node to the PlaylistModel
        /// </summary>
        /// <param name="playlistToAddToDataBase">Playlist containing the parentNode</param>
        /// <param name="playlistReceivingParentNode">PlaylistModel to get it´s ParentNode</param>
        private void AddParentNodePlaylist(Playlist playlistToAddToDataBase, PlaylistModel playlistReceivingParentNode)
        {
            TreeViewNodeModel newTreeViewNodeModel = new TreeViewNodeModel();
            newTreeViewNodeModel.Name = playlistToAddToDataBase.ParentNode.Name;

            newTreeViewNodeModel.SubNodes = AddSubNodes(playlistToAddToDataBase.ParentNode);
            playlistReceivingParentNode.ParentNode = newTreeViewNodeModel;
            dbContext.TreeViewNodes.Add(newTreeViewNodeModel);
        }

        /// <summary>
        /// Adds subNodes to a node
        /// </summary>
        /// <param name="nodeHoldingSubNodes">Nodes to build the subNodes from</param>
        /// <returns>List of treeViewNodeModel</returns>
        private static List<TreeViewNodeModel> AddSubNodes(TreeViewNode nodeHoldingSubNodes)
        {
            List<TreeViewNodeModel> nodes = new List<TreeViewNodeModel>();
            foreach (TreeViewNode subNode in nodeHoldingSubNodes.SubNodes)
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
        /// <param name="playlistToAddToDatabase">Playlist containing the Media</param>
        /// <param name="playlistReceivingMedia">PlaylistModel receiving media</param>
        private void AddMediaToPlaylist(Playlist playlistToAddToDatabase, PlaylistModel playlistReceivingMedia)
        {
            foreach (IMediaFile media in playlistToAddToDatabase.GetAllMediaFromPlaylist())
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

                    playlistReceivingMedia.Image.Add(newImageModel);

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

                    playlistReceivingMedia.Video.Add(newVideoModel);
                    dbContext.Videos.Add(newVideoModel);
                }
            }
        }

        /// <summary>
        /// Delete all PlaylistData in the database
        /// </summary>
        internal void DeleteAllPlaylistDataFromDatabase()
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
        /// <param name="playlistReferencingItemToRemove"></param>
        private void RemoveParentTreeViewNode(PlaylistModel playlistReferencingItemToRemove)
        {
            if (dbContext.TreeViewNodes != null && playlistReferencingItemToRemove.ParentNode != null)
            {
                dbContext.TreeViewNodes.Remove(playlistReferencingItemToRemove.ParentNode);
            }
        }

        /// <summary>
        /// Removes Media pertaining to a givenplaylist from the database
        /// </summary>
        /// <param name="playlistReferencingItemToRemove">The playlist referencing the Media to delete</param>
        private void RemoveRelationsToPlaylist(PlaylistModel playlistReferencingItemToRemove)
        {
            if (playlistReferencingItemToRemove.Video != null)
            {
                dbContext.Videos.RemoveRange(playlistReferencingItemToRemove.Video);
            }
            if (playlistReferencingItemToRemove.Image != null)
            {
                dbContext.Images.RemoveRange(playlistReferencingItemToRemove.Image);
            }
        }

        /// <summary>
        /// Get the playlist stored in the database 
        /// </summary>
        /// <returns>Listof PlaylistModel</returns>
        internal List<PlaylistModel> GetPlaylists()
        {
            return dbContext.Playlists.Include("Image").Include("Video").Include("ParentNode").ToList();
        }

        /// <summary>
        /// Call Methods in class resposible for convertion
        /// </summary>
        /// <param name="playlistsFromDatabase">Playlists from Database taking part in result</param>
        /// <param name="treeViewNodes">treeViewNodes from Database taking part in result</param>
        /// <returns>TreeViewNodeStructure containing Navigation and Playlists</returns>
        public TreeViewStructure ConvertDatabaseObjectToApplicationPlaylistObject(List<PlaylistModel> playlistsFromDatabase, List<TreeViewNode> treeViewNodes)
        {
            return dbModelToApplicationModel.ConvertDatabaseObjectToApplicationPlaylistObject(playlistsFromDatabase, treeViewNodes);
        }
    }
}