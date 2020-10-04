using MultiMediaClassesAndManagers.Interfaces;
using MultiMediaClassesAndManagers.MediaSubClasses;
using MultiMediaDataAccess.DatabaseModelAndContext;
using MultiMediaDataAccess.DatabaseModelAndContext.Models;
using MutiMediaClassesAndManagers;
using System;
using System.Collections.Generic;
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
            
            foreach(IMediaFile media in playlistToAddToDataBase.GetAllMediaFromPlaylist())
            {
                if(media is Image)
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

                    playlistModel.Image = newImageModel;
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
                }
            }

            TreeViewNodeModel newTreeViewNodeModel = new TreeViewNodeModel();
            newTreeViewNodeModel.Name = playlistToAddToDataBase.ParentNode.Name;

            foreach(TreeViewNodeModel node in playlistModel.ParentNode.SubNodes)
            {
                dbContext.TreeViewNodes.Add(node);
            }

            dbContext.Playlists.Add(playlistModel);
            dbContext.SaveChanges();
        }
    }
}
