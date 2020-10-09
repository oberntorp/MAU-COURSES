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
    internal class VideoDatabaseOperationsHelper
    {
        private MultiMediaContext dbContext;
        public VideoDatabaseOperationsHelper(ref MultiMediaContext dbContextIn)
        {
            dbContext = dbContextIn;
        }

        public DbSet<VideoModel> GetAllVideo()
        {
            return dbContext.Videos;
        }
    }
}