using MultiMediaClassesAndManagers.MediaSubClasses;
using MultiMediaClassesAndManagers.TreeNode;
using MultiMediaClassesAndManagers.TreeViewSave;
using MultiMediaDataAccess.DatabaseModelAndContext.Models;
using MutiMediaClassesAndManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMediaDataAccess.Convert
{
    /// <summary>
    /// This class contains Methods to convert from Database Models into classes used by the application
    /// </summary>
    internal class DbModelToApplicationModel
    {
        /// <summary>
        /// Converts database Objects of type PlaylistModel to TreeViewStructure used by the Application
        /// </summary>
        /// <param name="playlistsFromDatabaseToConvert">Playlists fromthe database to convert</param>
        /// <param name="treeViewNodes">TreeViewNodes used in the conversion</param>
        /// <returns>TreeViewStructure containing the Playlists and TreeViewNodes</returns>
        public TreeViewStructure ConvertDatabaseObjectToApplicationPlaylistObject(List<PlaylistModel> playlistsFromDatabaseToConvert, List<TreeViewNode> treeViewNodes)
        {
            List<Playlist> convertedPlaylists = ConvertPlaylistModelToPlaylist(playlistsFromDatabaseToConvert);

            TreeViewStructure newTreeViewStructure = new TreeViewStructure();
            newTreeViewStructure.AddPlaylistsToTreeViewStructure(convertedPlaylists);
            newTreeViewStructure.AddTreeStructure(treeViewNodes);
            return newTreeViewStructure;
        }

        /// <summary>
        /// Converts PlaylistModels to Playlist used by the application
        /// </summary>
        /// <param name="playlistsFromDatabaseToConvert">PlaylistMoels from database containing Playlist information</param>
        /// <returns>List of Playlist</returns>
        private List<Playlist> ConvertPlaylistModelToPlaylist(List<PlaylistModel> playlistsFromDatabaseToConvert)
        {
            List<Playlist> result = new List<Playlist>();

            foreach (PlaylistModel model in playlistsFromDatabaseToConvert)
            {
                TreeViewNodeModel newTreeViewNodeModel = new TreeViewNodeModel();
                newTreeViewNodeModel = ConvertTreeViewNodeModelFromTreeViewNodeModel(model.ParentNode);
                Playlist playlist = new Playlist(model.Title, ConvertParentNodeToTreeViewNode(newTreeViewNodeModel), model.Description);
                ConvertMediaModelsToApplicationAwareTypes(playlist, model.Video, model.Image);
                result.Add(playlist);
            }

            return result;
        }

        /// <summary>
        /// Converts the VideoModels and ImageModels to Video and Image being used by the application, then adds it to the playlist
        /// </summary>
        /// <param name="playlistToReceiveConvertedMedia">The playlist to receive the converted media</param>
        /// <param name="videosToConvert">List of VideoModel to convert to  List of Video</param>
        /// <param name="imagesToConvert">List of ImageModel to convert to List of Image</param>
        private void ConvertMediaModelsToApplicationAwareTypes(Playlist playlistToReceiveConvertedMedia, List<VideoModel> videosToConvert, List<ImageModel> imagesToConvert)
        {
            if (videosToConvert != null)
            {
                playlistToReceiveConvertedMedia.PlaylistContentXML.AddRange(ConvertVideoModelToVieo(videosToConvert));
            }
            if (imagesToConvert != null)
            {
                playlistToReceiveConvertedMedia.PlaylistContentXML.AddRange(ConvertImageModelToImage(imagesToConvert));
            }
        }

        /// <summary>
        /// Converts a List of VideoModel in a PlaylistModel to a list of Video
        /// </summary>
        /// <param name="videosToConvert">VideoModels to convert</param>
        /// <returns>Converted list of Video</returns>
        private List<Video> ConvertVideoModelToVieo(List<VideoModel> videosToConvert)
        {
            List<Video> result = new List<Video>();
            foreach (VideoModel video in videosToConvert)
            {
                Video convertedVideo = new Video();

                convertedVideo.Id = video.Id;
                convertedVideo.Name = video.Name;
                convertedVideo.SourceUrl = video.SourceUrl;
                convertedVideo.PreviewUrl = video.PreviewUrl;
                convertedVideo.FileExtention = video.FileExtention;
                convertedVideo.LengthInSeconds = video.LengthInSeconds;

                result.Add(convertedVideo);
            }

            return result;
        }

        /// <summary>
        /// Converts a List of ImageModel in a PlaylistModel to a list of Video
        /// </summary>
        /// <param name="imagesToConvert">ImageModels to convert</param>
        /// <returns>Converted List of Image</returns>
        private List<Image> ConvertImageModelToImage(List<ImageModel> imagesToConvert)
        {
            List<Image> result = new List<Image>();
            foreach (ImageModel video in imagesToConvert)
            {
                Image convertedVideo = new Image();

                convertedVideo.Id = video.Id;
                convertedVideo.Name = video.Name;
                convertedVideo.SourceUrl = video.SourceUrl;
                convertedVideo.PreviewUrl = video.PreviewUrl;
                convertedVideo.FileExtention = video.FileExtention;
                convertedVideo.Width = video.Width;
                convertedVideo.Height = video.Height;

                result.Add(convertedVideo);
            }

            return result;
        }

        /// <summary>
        /// Extracts the TreeViewNode model of a treeViewNodeMoel used  in the conversion from PlaylistModel to Playlist
        /// </summary>
        /// <param name="treeViewNodeToConvert">TreeViewNodeModel to transform</param>
        /// <returns>TreeViewNode model</returns>
        private TreeViewNodeModel ConvertTreeViewNodeModelFromTreeViewNodeModel(TreeViewNodeModel treeViewNodeToConvert)
        {
            TreeViewNodeModel result = new TreeViewNodeModel();

            result.Name = treeViewNodeToConvert.Name;
            result.Type = treeViewNodeToConvert.Type;

            if (treeViewNodeToConvert.SubNodes != null)
            {
                result.SubNodes = new List<TreeViewNodeModel>();

                foreach (TreeViewNodeModel model in treeViewNodeToConvert.SubNodes)
                {
                    TreeViewNodeModel newTreeViewNode = new TreeViewNodeModel();
                    newTreeViewNode.Name = model.Name;
                    newTreeViewNode.Type = model.Type;
                    newTreeViewNode.SubNodes = AddSubNodesToModel(model);
                    result.SubNodes.Add(newTreeViewNode);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts a TreeViewNodeModel into a TreeViewNode to be used by the application
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        private TreeViewNode ConvertParentNodeToTreeViewNode(TreeViewNodeModel parentNode)
        {
            TreeViewNode newNode = new TreeViewNode();
            newNode.Name = parentNode.Name;
            newNode.SubNodes = (parentNode.SubNodes != null) ? AddSubNodes(parentNode) : null;
            newNode.Type = (TreeNodeTypes)parentNode.Type;

            return newNode;
        }

        /// <summary>
        /// This method gets the subNodes out of a node from the database and converts it into a list of treeViewNodes
        /// </summary>
        /// <param name="nodeHoldingSubNodes">The node fromthe database</param>
        /// <returns>Converted List of TreeViewNodes</returns>
        private List<TreeViewNode> AddSubNodes(TreeViewNodeModel nodeHoldingSubNodes)
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();
            foreach (TreeViewNodeModel subNode in nodeHoldingSubNodes.SubNodes)
            {
                TreeViewNode newTreeViewNodeModel = new TreeViewNode();
                newTreeViewNodeModel.SubNodes = new List<TreeViewNode>();

                newTreeViewNodeModel.Name = subNode.Name;
                newTreeViewNodeModel.SubNodes = (subNode.SubNodes != null) ? AddSubNodes(subNode) : null;
                newTreeViewNodeModel.Type = (TreeNodeTypes)subNode.Type;
                nodes.Add(newTreeViewNodeModel);
            }

            return nodes;
        }

        /// <summary>
        /// This Method is used when Converting PlaylistModel to Playlist to AddSubNodes to the model 
        /// </summary>
        /// <param name="nodeHoldingSubNodes">Node to get subNodes of</param>
        /// <returns></returns>
        private List<TreeViewNodeModel> AddSubNodesToModel(TreeViewNodeModel nodeHoldingSubNodes)
        {
            List<TreeViewNodeModel> nodes = new List<TreeViewNodeModel>();
            if (nodeHoldingSubNodes.SubNodes != null)
            {
                foreach (TreeViewNodeModel subNode in nodeHoldingSubNodes.SubNodes)
                {
                    TreeViewNodeModel newTreeViewNodeModel = new TreeViewNodeModel();
                    newTreeViewNodeModel.Name = subNode.Name;
                    newTreeViewNodeModel.SubNodes = AddSubNodesToModel(subNode);
                    newTreeViewNodeModel.Type = subNode.Type;
                    nodes.Add(newTreeViewNodeModel);
                }
            }

            return nodes;
        }

        /// <summary>
        /// Converts TreeViewNodeModel to TreeViewNode
        /// </summary>
        /// <param name="treeViewNodesFromDatabaseToConvert">Tre treeViewNodes from the database to Convert</param>
        /// <returns>List of treeViewNodes</returns>
        public List<TreeViewNode> ConvertTreeViewNodeModelToTreeViewNode(List<TreeViewNodeModel> treeViewNodesFromDatabaseToConvert)
        {
            List<TreeViewNode> result = new List<TreeViewNode>();

            foreach (TreeViewNodeModel model in treeViewNodesFromDatabaseToConvert)
            {
                TreeViewNode newTreeViewNode = new TreeViewNode();
                newTreeViewNode.Name = model.Name;
                newTreeViewNode.Type = (TreeNodeTypes)model.Type;
                newTreeViewNode.SubNodes = (model.SubNodes != null) ? AddSubNodes(model) : null;
                result.Add(newTreeViewNode);
            }

            return result;
        }
    }
}
