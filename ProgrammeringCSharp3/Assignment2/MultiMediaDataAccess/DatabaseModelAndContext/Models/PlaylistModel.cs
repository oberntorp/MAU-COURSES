using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMediaDataAccess.DatabaseModelAndContext.Models
{
    [Table("Playlists")]
    public class PlaylistModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PlaylistPlaybackDelayBetweenMediaSec { set; get; }
        public List<VideoModel> Video { get; set; }
        public List<ImageModel> Image { get; set; }
        public TreeViewNodeModel ParentNode { get; set; }

    }
}
