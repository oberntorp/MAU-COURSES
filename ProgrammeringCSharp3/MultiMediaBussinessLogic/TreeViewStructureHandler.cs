using System;
using System.Collections.Generic;
using System.Text;
using MultiMediaClassesAndManagers.Managers;
using MultiMediaClassesAndManagers.TreeViewSave;

namespace MultiMediaBussinessLogic
{
    /// <summary>
    /// This Class acts as a BussinessLogic class for treeViewStructure operations and takes care of the access to the TreeViewStructureManager
    /// </summary>
    public class TreeViewStructureHandler
    {
        private TreeViewStructureManager treeViewStructureManager = null;

        /// <summary>
        /// TreeViewStructureHandler constructor, initializes an object to treeViewStructureManager
        /// </summary>
        public TreeViewStructureHandler()
        {
            treeViewStructureManager = new TreeViewStructureManager();
        }

        /// <summary>
        /// Adds a tree>ViewStructure to the handler
        /// </summary>
        /// <param name="treeViewStructureToSave">The treeViewStructure to add</param>
        /// <returns>true/false</returns>
        public bool AddTreeViewStructure(TreeViewStructure treeViewStructureToSave)
        {
            return treeViewStructureManager.AddTreeViewSaveStructure(treeViewStructureToSave);
        }

        /// <summary>
        /// Deletes the structure in the manager
        /// </summary>
        public void DeleteStructure()
        {
            treeViewStructureManager.DeleteAll();
        }

        /// <summary>
        /// Saves the TreeViewStructure as XML
        /// </summary>
        /// <param name="fileName">Where to save the XML</param>
        public void SaveAsXML(string fileName)
        {
            treeViewStructureManager.SerializeToXML(fileName);
        }

        /// <summary>
        /// Load Structure from XML
        /// </summary>
        /// <param name="fileName">Where the file loaded is located</param>
        public void LoadFromXML(string fileName)
        {
            treeViewStructureManager.XMLDeserialize(fileName);
        }
    }
}
