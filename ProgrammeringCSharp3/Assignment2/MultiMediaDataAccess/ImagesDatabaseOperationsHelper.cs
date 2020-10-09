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
    internal class ImagesDatabaseOperationsHelper
    {
        private MultiMediaContext dbContext;
        public ImagesDatabaseOperationsHelper(ref MultiMediaContext dbContextIn)
        {
            dbContext = dbContextIn;
        }

        internal DbSet<ImageModel> GetImagesFromDb()
        {
            return dbContext.Images;
        }
    }
}