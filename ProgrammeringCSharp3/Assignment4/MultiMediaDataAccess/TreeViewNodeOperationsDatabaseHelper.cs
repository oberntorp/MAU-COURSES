using MultiMediaClassesAndManagers.TreeNode;
using MultiMediaClassesAndManagers.TreeViewSave;
using MultiMediaDataAccess.Convert;
using MultiMediaDataAccess.DatabaseModelAndContext;
using MultiMediaDataAccess.DatabaseModelAndContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMediaDataAccess
{
    /// <summary>
    /// The TreeViewNodeOperationsDatabaseHelper is the class responsible for connecting to the database and add/delete TreeViewNodeModel objects stored in the tables
    /// </summary>
    internal class TreeViewNodeOperationsDatabaseHelper
    {
        private MultiMediaContext dbContext;
        private DbModelToApplicationModel dbModelToApplicationModel;

        /// <summary>
        /// Constructor, initiating context helper classes
        /// </summary>
        /// <param name="dbContextIn">dbContext</param>
        public TreeViewNodeOperationsDatabaseHelper(MultiMediaContext dbContextIn)
        {
            dbContext = dbContextIn;
            dbModelToApplicationModel = new DbModelToApplicationModel();
        }

        /// <summary>
        /// Get treeViewNodes from the database
        /// </summary>
        /// <returns></returns>
        public List<TreeViewNode> GetTreeViewNodesFromDatabase()
        {
            return dbModelToApplicationModel.ConvertTreeViewNodeModelToTreeViewNode(dbContext.TreeViewNodes.ToList());
        }

        /// <summary>
        /// Deletes all treeViewNodes from the database
        /// </summary>
        public void DeleteTreeViewNodesFromDatabase()
        {
            foreach(TreeViewNodeModel node in dbContext.TreeViewNodes.ToList())
            {
                dbContext.TreeViewNodes.Remove(node);
            }
        }
    }
}
