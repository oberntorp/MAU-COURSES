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

namespace MultiMediaDataAccess
{
    internal class PlaylistDatabaseOperationsHelper
    {
        private MultiMediaContext dbContext;
        public PlaylistDatabaseOperationsHelper(ref MultiMediaContext dbContextIn)
        {
            dbContext = dbContextIn;
        }

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

        internal PlaylistModel AddRelatingItemsToPlaylistModel(Playlist playlistToAddToDataBase, PlaylistModel playlistModel)
        {
            AddParentNodePlaylist(playlistToAddToDataBase, playlistModel);
            AddMeddiaToPlaylist(playlistToAddToDataBase, playlistModel);
            return playlistModel;
        }

        private void AddParentNodePlaylist(Playlist playlistToAddToDataBase, PlaylistModel playlistModel)
        {
            TreeViewNodeModel newTreeViewNodeModel = new TreeViewNodeModel();
            newTreeViewNodeModel.Name = playlistToAddToDataBase.ParentNode.Name;

            newTreeViewNodeModel.SubNodes = GetSubNodesOfParent(playlistToAddToDataBase);
            playlistModel.ParentNode = newTreeViewNodeModel;
            dbContext.TreeViewNodes.Add(newTreeViewNodeModel);
        }

        private List<TreeViewNodeModel> GetSubNodesOfParent(Playlist playlistToAddToDataBase)
        {
            List<TreeViewNodeModel> result = new List<TreeViewNodeModel>();

            foreach (TreeViewNode node in playlistToAddToDataBase.ParentNode.SubNodes)
            {
                TreeViewNodeModel newNode = new TreeViewNodeModel();
                newNode.Name = node.Name;
                List<TreeViewNodeModel> nodes = AddSubNodes(node);
                newNode.SubNodes = nodes;
                result.Add(newNode);
            }

            return result;
        }

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

        private void RemoveParentTreeViewNode(PlaylistModel playlist)
        {
            if (dbContext.TreeViewNodes != null && playlist.ParentNode != null)
            {
                dbContext.TreeViewNodes.Remove(playlist.ParentNode);
            }
        }

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

        internal List<PlaylistModel> GetPlaylists()
        {
            return dbContext.Playlists.Include("Image").Include("Video").Include("ParentNode").ToList();
        }

        public TreeViewStructure ConvertDatabaseObjectToApplicationPlaylistObject(List<PlaylistModel> playlistsFromDatabase, List<TreeViewNode> treeViewNodes)
        {
            List<Playlist> convertedPlaylists = ConvertPlaylistModelToPlaylist(playlistsFromDatabase);

            TreeViewStructure newTreeViewStructure = new TreeViewStructure();
            newTreeViewStructure.AddPlaylistsToTreeViewStructure(convertedPlaylists);
            newTreeViewStructure.AddTreeStructure(treeViewNodes);
            return newTreeViewStructure;
        }

        private List<TreeViewNode> ConvertTreeViewNodeModelToTreeViewNode(List<TreeViewNodeModel> treeViewNodeToTransform)
        {
            List<TreeViewNode> result = new List<TreeViewNode>();
            foreach (TreeViewNodeModel model in treeViewNodeToTransform)
            {
                TreeViewNode newTreeViewNode = new TreeViewNode();
                newTreeViewNode.Name = model.Name;
                newTreeViewNode.Type = (TreeNodeTypes)model.Type;
                newTreeViewNode.SubNodes = AddSubNodes(model);
                result.Add(newTreeViewNode);
            }

            return result;
        }

        private List<Playlist> ConvertPlaylistModelToPlaylist(List<PlaylistModel> playlistsFromDatabase)
        {
            List<Playlist> result = new List<Playlist>();

            foreach (PlaylistModel model in playlistsFromDatabase)
            {
                TreeViewNodeModel newTreeViewNodeModel = new TreeViewNodeModel();
                newTreeViewNodeModel = ConvertTreeViewNodeModelToTreeViewNode(model.ParentNode);
                Playlist playlist = new Playlist(model.Title, ConvertParentTreeViewNodeModelToTreeViewNode(newTreeViewNodeModel), model.Description);
                ConvertMediaModelsToApplicationAwareTypes(playlist, model.Video, model.Image);
                result.Add(playlist);
            }

            return result;
        }

        private void ConvertMediaModelsToApplicationAwareTypes(Playlist playlist, List<VideoModel> videos, List<ImageModel> images)
        {
            if (videos != null)
            {
                playlist.PlaylistContentXML.AddRange(ConvertVideoModelToVieo(videos));
            }
            if(images != null)
            {
                playlist.PlaylistContentXML.AddRange(ConvertImageModelToImage(images));
            }
        }

        private List<Video> ConvertVideoModelToVieo(List<VideoModel> videos)
        {
            List<Video> result = new List<Video>();
            foreach(VideoModel video in videos)
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

        private List<Image> ConvertImageModelToImage(List<ImageModel> images)
        {
            List<Image> result = new List<Image>();
            foreach (ImageModel video in images)
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

        private TreeViewNodeModel ConvertTreeViewNodeModelToTreeViewNode(TreeViewNodeModel treeViewNodeToTransform)
        {
            TreeViewNodeModel result = new TreeViewNodeModel();

            result.Name = treeViewNodeToTransform.Name;
            result.Type = treeViewNodeToTransform.Type;
            result.SubNodes = new List<TreeViewNodeModel>();

            foreach (TreeViewNodeModel model in treeViewNodeToTransform.SubNodes)
            {
                TreeViewNodeModel newTreeViewNode = new TreeViewNodeModel();
                newTreeViewNode.Name = model.Name;
                newTreeViewNode.Type = model.Type;
                newTreeViewNode.SubNodes = AddSubNodesToModel(model);
                result.SubNodes.Add(newTreeViewNode);
            }

            return result;
        }

        private TreeViewNode ConvertParentTreeViewNodeModelToTreeViewNode(TreeViewNodeModel parentNode)
        {
            TreeViewNode newNode = new TreeViewNode();
            newNode.Name = parentNode.Name;
            newNode.SubNodes = GetSubNodesOfParentModel(parentNode);
            newNode.Type = (TreeNodeTypes)parentNode.Type;

            return newNode;
        }

        private List<TreeViewNode> GetSubNodesOfParentModel(TreeViewNodeModel parentNode)
        {
            List<TreeViewNode> result = new List<TreeViewNode>();

            foreach (TreeViewNodeModel node in parentNode.SubNodes)
            {
                TreeViewNode newNode = new TreeViewNode();
                newNode.Name = node.Name;
                List<TreeViewNode> nodes = AddSubNodes(node);
                newNode.SubNodes = nodes;
                newNode.Type = (TreeNodeTypes)node.Type;
                result.Add(newNode);
            }

            return result;
        }

        private static List<TreeViewNode> AddSubNodes(TreeViewNodeModel node)
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();
            foreach (TreeViewNodeModel subNode in node.SubNodes)
            {
                TreeViewNode newTreeViewNodeModel = new TreeViewNode();
                newTreeViewNodeModel.Name = subNode.Name;
                newTreeViewNodeModel.SubNodes = AddSubNodes(subNode);
                newTreeViewNodeModel.Type = (TreeNodeTypes)subNode.Type;
                nodes.Add(newTreeViewNodeModel);
            }

            return nodes;
        }

        private static List<TreeViewNodeModel> AddSubNodesToModel(TreeViewNodeModel node)
        {
            List<TreeViewNodeModel> nodes = new List<TreeViewNodeModel>();
            if (node.SubNodes != null)
            {
                foreach (TreeViewNodeModel subNode in node.SubNodes)
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
    }
}