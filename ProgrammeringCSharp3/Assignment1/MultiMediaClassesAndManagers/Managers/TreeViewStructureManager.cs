using MultiMediaClassesAndManagers.TreeViewSave;
using MutiMediaClassesAndManagers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMediaClassesAndManagers.Managers
{
    public class TreeViewStructureManager: ListManager<TreeViewStructure>
    {
        private int treeViewStructureId = 1;

        /// <summary>
        /// As an treeViewStructure shall have an Id, this method first adds the id, then calls Add from ListManager
        /// </summary>
        /// <param name="TreeViewStructure">The treeViewStructure to add</param>
        /// <returns>true/false</returns>
        public bool AddTreeViewSaveStructure(TreeViewStructure treeViewStructureToAdd)
        {
            AddIdToTreeViewStructure(ref treeViewStructureToAdd);
            return Add(treeViewStructureToAdd);
        }

        /// <summary>
        /// This method adds an id to the Playlist
        /// </summary>
        /// <param name="treeViewStructuretToAddAnId">The Playlistobject getting an id</param>
        private void AddIdToTreeViewStructure(ref TreeViewStructure treeViewStructuretToAddAnId)
        {
            if (Count == 0)
            {
                treeViewStructuretToAddAnId.Id = treeViewStructureId++;
            }
            else
            {
                treeViewStructuretToAddAnId.Id = Count + 1;
            }
        }

        /// <summary>
        /// Adds a list of playlists to the treeViewStructure
        /// </summary>
        /// <param name="playlistsToAdd">List of playlists to add</param>
        public void AddPlaylistsToTreeViewStructure(List<Playlist> playlistsToAdd)
        {
            TreeViewStructure treeViewStructureToChange = GetAt(0);
            treeViewStructureToChange.AddPlaylistsToTreeViewStructure(playlistsToAdd);
            ChangeAt(treeViewStructureToChange, 0);
        }

        /// <summary>
        /// Serialize to XML
        /// </summary>
        /// <param name="filePath">file path where to save the xmlFile</param>
        public void SerializeToXML(string filePath)
        {
            XMLSerialize(filePath);
        }

        /// <summary>
        /// Deserialize from XML
        /// </summary>
        /// <param name="filePath">file path where file xhould be loaded from</param>
        public void DeerializeFromXML(string filePath)
        {
            XMLDeserialize(filePath);
        }
    }
}
