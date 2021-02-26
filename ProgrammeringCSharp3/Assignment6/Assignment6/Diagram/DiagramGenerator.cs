using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BussinessLogic;

namespace Assignment6.Diagram
{
    class DiagramGenerator: Canvas
    {
        public DiagramInformation DiagramDataToDraw { get; set; }
        private int sixeOfAxes;

        private int maxLengthYAxis = 105;
        private int maxLengthOfXAxis = 50;
        private string diagramTitle;
        private DrawingContext globalDc;
        public List<ArrayList> dataYPointsToPlot;
        public List<ArrayList> dataXPointsToPlot;

        public DiagramGenerator(double width, double  height, DiagramInformation diagramData, List<ArrayList> YPointsUsedInDiagramGeneration, List<ArrayList> XPointsUsedInDiagramGeneration)
        {
            Width = width;
            Height = height;

            diagramTitle = diagramData.Title;
            DiagramDataToDraw = diagramData;
            sixeOfAxes = diagramData.sizeOfAxes;
            maxLengthYAxis += sixeOfAxes;
            maxLengthOfXAxis += sixeOfAxes;
            dataYPointsToPlot = YPointsUsedInDiagramGeneration;
            dataXPointsToPlot = XPointsUsedInDiagramGeneration;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            globalDc = dc;
            DrawDiagramHeader(dc);

            DrawYAxis(dc);
            DrawXAxis(dc);
            if(DiagramDataToDraw.Points.Count() > 0)
            {
                WritePointsToDiagram();
            }
        }

        private void DrawDiagramHeader(DrawingContext dc)
        {
            FlowDirection dir = FlowDirection.LeftToRight;
            FormattedText textToRender = new FormattedText(diagramTitle, new System.Globalization.CultureInfo("se-SV"), dir, new Typeface("Arial"), 102, Brushes.Blue, VisualTreeHelper.GetDpi(this).PixelsPerDip);

            dc.DrawText(textToRender, new Point(50, 0));
        }

        private void DrawYAxis(DrawingContext dc)
        {
            Pen yAxisPen = new Pen(Brushes.Blue, 4);
            yAxisPen.StartLineCap = PenLineCap.Triangle;
            dc.DrawLine(yAxisPen, new Point(50, 105), new Point(50, maxLengthYAxis));

            DrawIntervalsYAxis(dc);
        }

        private void DrawIntervalsYAxis(DrawingContext dc)
        {
            Pen yAxisPen = new Pen(Brushes.Black, 2);
            foreach(ArrayList pointToPlot in dataYPointsToPlot)
            {
                dc.DrawLine(yAxisPen, (Point)pointToPlot[0], (Point)pointToPlot[1]);
                DrawYAxisNumbers(dc, pointToPlot[2].ToString(), (int)pointToPlot[3]);
            }
        }

        private void DrawYAxisNumbers(DrawingContext dc, string intToDraw, int positionOfInt)
        {
            FlowDirection dir = FlowDirection.LeftToRight;
            FormattedText textToRender = new FormattedText(intToDraw, new System.Globalization.CultureInfo("se-SV"), dir, new Typeface("Arial"), 10, Brushes.Green, VisualTreeHelper.GetDpi(this).PixelsPerDip);

            dc.DrawText(textToRender, new Point(25, positionOfInt - 4));
        }

        private void DrawXAxis(DrawingContext dc)
        {
            Pen xAxisTickPen = new Pen(Brushes.Black, 4);
            xAxisTickPen.EndLineCap = PenLineCap.Triangle;

            dc.DrawLine(xAxisTickPen, new Point(50, 105 + sixeOfAxes), new Point(maxLengthOfXAxis, maxLengthYAxis));

            DrawIntervalsXAxis(dc);
        }

        private void DrawIntervalsXAxis(DrawingContext dc)
        {
            Pen xAxisPen = new Pen(Brushes.Gray, 2);
            foreach (ArrayList pointToPlot in dataXPointsToPlot)
            {
                dc.DrawLine(xAxisPen, (Point)pointToPlot[0], (Point)pointToPlot[1]);
                DrawXAxisNumbers(dc, (string)pointToPlot[2].ToString(), (int)pointToPlot[3]);
            }
        }

        private void DrawXAxisNumbers(DrawingContext dc, string intToDraw, int positionOfInt)
        {
            FlowDirection dir = FlowDirection.LeftToRight;
            FormattedText textToRender = new FormattedText(intToDraw, new System.Globalization.CultureInfo("se-SV"), dir, new Typeface("Arial"), 10, Brushes.Green, VisualTreeHelper.GetDpi(this).PixelsPerDip);

            dc.DrawText(textToRender, new Point(positionOfInt - 4, (105 + sixeOfAxes) + 15));
        }

        public void WritePointsToDiagram()
        {
            StreamGeometry streamGeomitry = new StreamGeometry();
            streamGeomitry.FillRule = FillRule.EvenOdd;

            using(StreamGeometryContext context = streamGeomitry.Open())
            {
                context.BeginFigure(DiagramDataToDraw.Points.First().Value, false, false);
                context.PolyLineTo(DiagramDataToDraw.Points.Values.Skip(1).ToArray(), true, false);
            }

            globalDc.DrawGeometry(Brushes.Black, new Pen(Brushes.Black, 5), streamGeomitry);
        }
    }
}
