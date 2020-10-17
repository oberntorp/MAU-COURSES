using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MultiMediaDataAccess.DatabaseModelAndContext.Models
{
    /// <summary>
    /// This class is used when setting up the TreeView (holds information needed)
    /// </summary>
    [Table("TreeViewNodes")]
    public class TreeViewNodeModel
    {
        public int Id { get; set; }
        public TreeNodeTypesModel Type { get; set; }
        public string Name { get; set; }

        public List<TreeViewNodeModel> SubNodes { get; set; }

        /// <summary>
        /// Default constructor, needed for serialization to work
        /// </summary>
        public TreeViewNodeModel()
        {

        }
    }
}
