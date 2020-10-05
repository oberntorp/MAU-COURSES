using MultiMediaClassesAndManagers.Interfaces;
using MultiMediaClassesAndManagers.MediaSubClasses;
using MultiMediaClassesAndManagers.TreeNode;
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
    public class DatabaseOperations
    {
        MultiMediaContext dbContext;

        public DatabaseOperations()
        {
            dbContext = new MultiMediaContext();
        }

        public void InsertPLaylistToDb(Playlist playlistToAddToDataBase)
        {
            PlaylistModel playlistModel = new PlaylistModel();
            playlistModel.Id = playlistToAddToDataBase.Id;
            playlistModel.Title = playlistToAddToDataBase.Title;
            playlistModel.Description = playlistToAddToDataBase.Description;
            playlistModel.PlaylistPlaybackDelayBetweenMediaSec = playlistToAddToDataBase.PlaylistPlaybackDelayBetweenMediaSec;

            AddMeddiaToPlaylist(playlistToAddToDataBase, ref playlistModel);

            TreeViewNodeModel newTreeViewNodeModel = new TreeViewNodeModel();
            newTreeViewNodeModel.Name = playlistToAddToDataBase.ParentNode.Name;

            foreach (TreeViewNode node in playlistToAddToDataBase.ParentNode.SubNodes)
            {
                TreeViewNodeModel newNode = new TreeViewNodeModel();
                newNode.Name = node.Name;
                List<TreeViewNodeModel> nodes = AddSubNodes(node);
                newNode.SubNodes = nodes;
            }

            dbContext.Playlists.Add(playlistModel);
            dbContext.SaveChanges();
        }

        private void AddMeddiaToPlaylist(Playlist playlistToAddToDataBase, ref PlaylistModel playlistModel)
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

                    playlistModel.Image = new List<ImageModel>();
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

                    playlistModel.Video = new List<VideoModel>();
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
    }
}
