using MultiMediaDataAccess.DatabaseModelAndContext.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMediaDataAccess.DatabaseModelAndContext
{
    public class MultiMediaContext: DbContext
    {
        /// <summary>
        /// DbContext constructor, creates database
        /// </summary>
        public MultiMediaContext(): base("name=Database")
        {
            if (!Database.Exists())
            {
                Database.Create();
            }
        }

        public DbSet<PlaylistModel> Playlists { get; set; }

        public DbSet<VideoModel> Videos { get; set; }

        public DbSet<ImageModel> Images { get; set; }

        public DbSet<TreeViewNodeModel> TreeViewNodes { get; set; }

    }
}
