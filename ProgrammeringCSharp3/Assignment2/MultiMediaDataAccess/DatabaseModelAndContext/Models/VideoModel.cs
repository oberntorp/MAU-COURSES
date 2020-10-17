﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMediaDataAccess.DatabaseModelAndContext.Models
{
    [Table("Videos")]
    public class VideoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SourceUrl { get; set; }
        public string PreviewUrl { get; set; }
        public string FileExtention { get; set; }
        public double LengthInSeconds { get; set; }
        public int SortInPlaylist { get; set; }

    }
}
