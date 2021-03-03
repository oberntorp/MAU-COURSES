using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic
{
    public class DiagramHandler
    {
        public DiagramInformation DiagramInformation { get; set; }
        public DiagramPointsToDrawOfGenerator PointsGenerator { get; set; }

        public DiagramHandler()
        {

        }

        public void CreateDiagramInformationObject(string title, int intervalXAxis, int intervalYAxis, int divisionsXAxis, int divisionsYAxis, int widthOfDiagramGrid)
        {
            DiagramInformation = new DiagramInformation(title, intervalXAxis, intervalYAxis, divisionsXAxis, divisionsYAxis, widthOfDiagramGrid);
        }

        public void CreatePointsGenerator()
        {
            PointsGenerator = new DiagramPointsToDrawOfGenerator(DiagramInformation);
        }

        public void UpdatePointsGeneratorUpdatedDiagramPoints()
        {
            PointsGenerator.DiagramDataToDraw.Points = DiagramInformation.Points;
        }

        public void ResetDiagramData()
        {
            DiagramInformation.ClearDiagramPoints();
            PointsGenerator = null;
        }
    }
}
