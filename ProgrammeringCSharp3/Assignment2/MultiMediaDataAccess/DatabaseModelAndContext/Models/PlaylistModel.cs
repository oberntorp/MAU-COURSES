using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMediaDataAccess.DatabaseModelAndContext.Models
{
    public class PlaylistModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PlaylistPlaybackDelayBetweenMediaSec { set; get; }
        public VideoModel Video { get; set; }
        public ImageModel Image { get; set; }
        public TreeViewNodeModel ParentNode { get; set; }

    }
}
