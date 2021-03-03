using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BussinessLogic
{
    public class DiagramPointsToDrawOfGenerator
    {
        public DiagramInformation DiagramDataToDraw { get; set; }
        private int sixeOfAxes;
        private int maxLengthYAxis = 105;
        private int maxLengthOfXAxis = 50;
        public List<ArrayList> YPointsUsedInDiagramGeneration { get; set; }
        public List<ArrayList> XPointsUsedInDiagramGeneration { get; set; }

        public DiagramPointsToDrawOfGenerator(DiagramInformation diagramData)
        {
            DiagramDataToDraw = diagramData;
            sixeOfAxes = diagramData.sizeOfAxes;
            maxLengthYAxis += sixeOfAxes;
            maxLengthOfXAxis += sixeOfAxes;
            YPointsUsedInDiagramGeneration = new List<ArrayList>();
            XPointsUsedInDiagramGeneration = new List<ArrayList>();
            StartPointsGenerator();
        }

        private void StartPointsGenerator()
        {
            DrawIntervalsYAxis();
            DrawIntervalsXAxis();
        }

        private void DrawIntervalsYAxis()
        {
            int interval = sixeOfAxes / DiagramDataToDraw.DivisionsY;
            for (int offsetYAxis = maxLengthYAxis - interval, intervalFigure = DiagramDataToDraw.IntervalY; offsetYAxis >= 105; offsetYAxis -= interval, intervalFigure += DiagramDataToDraw.IntervalY)
            {
                ArrayList pointArray = new ArrayList();
                pointArray.Add(new Point(45, offsetYAxis));
                pointArray.Add(new Point(55, offsetYAxis));
                pointArray.Add(intervalFigure);
                pointArray.Add(offsetYAxis);

                YPointsUsedInDiagramGeneration.Add(pointArray);
            }
        }

        private void DrawIntervalsXAxis()
        {
            int interval = sixeOfAxes / DiagramDataToDraw.DivisionsX;
            for (int offsetXAxis = interval + 50, intervalFigure = DiagramDataToDraw.IntervalX; offsetXAxis <= maxLengthOfXAxis; offsetXAxis += interval, intervalFigure += DiagramDataToDraw.IntervalX)
            {
                ArrayList pointArray = new ArrayList();
                pointArray.Add(new Point(offsetXAxis, (105 + sixeOfAxes) - 5));
                pointArray.Add(new Point(offsetXAxis, (105 + sixeOfAxes) + 5));
                pointArray.Add(intervalFigure);
                pointArray.Add(offsetXAxis);

                XPointsUsedInDiagramGeneration.Add(pointArray);
            }
        }
    }
}
