using MultiMediaClassesAndManagers.TreeNode;
using MultiMediaClassesAndManagers.TreeViewSave;
using MultiMediaDataAccess.DatabaseModelAndContext;
using MultiMediaDataAccess.DatabaseModelAndContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMediaDataAccess
{
    internal class TreeViewNodeDatabaseHelper
    {
        private MultiMediaContext dbContext;
        public TreeViewNodeDatabaseHelper(ref MultiMediaContext dbContextIn)
        {
            dbContext = dbContextIn;
        }

        public List<TreeViewNode> GetTreeViewNodesFromDatabase()
        {
            return ConvertDatabaseObjectToApplicationPlaylistObject(dbContext.TreeViewNodes.ToList());
        }

        public void DeleteTreeViewNodesFromDatabase()
        {
            foreach(TreeViewNodeModel node in dbContext.TreeViewNodes.ToList())
            {
                dbContext.TreeViewNodes.Remove(node);
            }
        }

        private List<TreeViewNode> ConvertDatabaseObjectToApplicationPlaylistObject(List<TreeViewNodeModel> treeViewNodesFromDatabase)
        {
            return ConvertTreeViewNodeModelToTreeViewNode(treeViewNodesFromDatabase);
        }

        private List<TreeViewNode> ConvertTreeViewNodeModelToTreeViewNode(List<TreeViewNodeModel> treeViewNodesFromDatabase)
        {
            List<TreeViewNode> result = new List<TreeViewNode>();

            foreach(TreeViewNodeModel model in treeViewNodesFromDatabase)
            {
                TreeViewNode newTreeViewNode = new TreeViewNode();
                newTreeViewNode.Name = model.Name;
                newTreeViewNode.Type = (TreeNodeTypes)model.Type;
                newTreeViewNode.SubNodes = AddSubNodes(model);
                result.Add(newTreeViewNode);
            }

            return result;
        }

        private static List<TreeViewNode> AddSubNodes(TreeViewNodeModel node)
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();

            if (node.SubNodes != null)
            {
                foreach (TreeViewNodeModel subNode in node.SubNodes)
                {
                    TreeViewNode newTreeViewNodeModel = new TreeViewNode();
                    newTreeViewNodeModel.Name = subNode.Name;
                    newTreeViewNodeModel.SubNodes = AddSubNodes(subNode);
                    newTreeViewNodeModel.Type = (TreeNodeTypes)subNode.Type;
                    nodes.Add(newTreeViewNodeModel);
                }
            }

            return nodes;
        }
    }
}
