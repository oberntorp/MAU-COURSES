using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic
{
    /// <summary>
    /// This class handles the communication between presentation layer and the DiagramInformation
    /// </summary>
    public class DiagramHandler
    {
        public DiagramInformation DiagramInformation { get; set; }
        public DiagramIntervalPointsGenerator IntervalPointsGenerator { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DiagramHandler()
        {

        }

        /// <summary>
        /// This method creates the diagramInformationObject with data input from the GUI
        /// </summary>
        /// <param name="title">This is the diagram title from the GUI</param>
        /// <param name="intervalXAxis">This is the diagram interval X axis from the GUI</param>
        /// <param name="intervalYAxis">This is the diagram interval Y axis from the GUI</param>
        /// <param name="divisionsXAxis">This is the diagram divisions X axis from the GUI</param>
        /// <param name="divisionsYAxis">This is the diagram divisions Y axis from the GUI</param>
        /// <param name="widthOfDiagramGrid">The width of the diagram containing the grid (used to calculate the size of the diagram)</param>
        public void CreateDiagramInformationObject(string title, int intervalXAxis, int intervalYAxis, int divisionsXAxis, int divisionsYAxis, int widthOfDiagramGrid)
        {
            DiagramInformation = new DiagramInformation(title, intervalXAxis, intervalYAxis, divisionsXAxis, divisionsYAxis, widthOfDiagramGrid);
        }

        /// <summary>
        /// Creates the IntervalPointsGenerator
        /// </summary>
        public void CreatePointsGenerator()
        {
            IntervalPointsGenerator = new DiagramIntervalPointsGenerator(DiagramInformation);
        }

        /// <summary>
        /// Updates the Points collection with the new points that has been added
        /// </summary>
        public void UpdatePointsGeneratorDiagramPoints()
        {
            IntervalPointsGenerator.DiagramDataToDraw.DataPoints = DiagramInformation.DataPoints;
        }

        /// <summary>
        /// Resets the data making up the diagram
        /// </summary>
        public void ResetDiagramData()
        {
            DiagramInformation.ClearDiagramPoints();
            IntervalPointsGenerator = null;
        }
    }
}
