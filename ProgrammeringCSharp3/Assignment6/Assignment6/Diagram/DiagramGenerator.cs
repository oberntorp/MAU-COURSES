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
    /// <summary>
    /// The class responsible for Generating the diagram
    /// </summary>
    class DiagramGenerator : Canvas
    {
        public DiagramInformation DiagramDataToDraw { get; set; }

        private DrawingContext globalDc;
        public List<ArrayList> dataYPointsToPlot;
        public List<ArrayList> dataXPointsToPlot;
        public const int axesThickness = 4;
        public const int axesTickThickness = 2;
        public const int diagramDataLineThickness = 2;

        /// <summary>
        /// Defaultconstructor
        /// </summary>
        /// <param name="width">width of the canvas making up the diagramgenerator</param>
        /// <param name="height">hidth of the canvas making up the diagramgenerator</param>
        /// <param name="diagramData">The diagram data being used in the diagramgeneration</param>
        /// <param name="YPointsUsedInDiagramGeneration">Ypoints to use in  the generation</param>
        /// <param name="XPointsUsedInDiagramGeneration">Xpoints to use in  the generation</param>
        public DiagramGenerator(double width, double height, DiagramInformation diagramData, List<ArrayList> YPointsUsedInDiagramGeneration, List<ArrayList> XPointsUsedInDiagramGeneration)
        {
            Width = width;
            Height = height;
            DiagramDataToDraw = diagramData;
            dataYPointsToPlot = YPointsUsedInDiagramGeneration;
            dataXPointsToPlot = XPointsUsedInDiagramGeneration;
        }

        /// <summary>
        /// The method responsible for starting the  generationof the diagram
        /// </summary>
        /// <param name="dc">Drawing context</param>
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            globalDc = dc;
            DrawDiagramHeader(dc);

            DrawYAxis(dc);
            DrawXAxis(dc);
            if (DiagramDataToDraw.DataPoints.Count() > 0)
            {
                WritePointsToDiagram();
            }
        }

        /// <summary>
        /// Draw the diagram header
        /// </summary>
        /// <param name="dc">Drawing context</param>
        private void DrawDiagramHeader(DrawingContext dc)
        {
            FlowDirection dir = FlowDirection.LeftToRight;
            FormattedText textToRender = new FormattedText(DiagramDataToDraw.Title, new System.Globalization.CultureInfo("se-SV"), dir, new Typeface("Arial"), 102, Brushes.Blue, VisualTreeHelper.GetDpi(this).PixelsPerDip);

            dc.DrawText(textToRender, new Point(50, 0));
        }

        /// <summary>
        /// Drawthe Y axis
        /// </summary>
        /// <param name="dc">Drawing context</param>
        private void DrawYAxis(DrawingContext dc)
        {
            Pen yAxisPen = new Pen(Brushes.Blue, axesThickness);
            yAxisPen.StartLineCap = PenLineCap.Triangle;
            dc.DrawLine(yAxisPen, new Point(DiagramDataToDraw.OffsetXAxis, DiagramDataToDraw.OffsetYAxis), new Point(DiagramDataToDraw.OffsetXAxis, DiagramDataToDraw.HeightOfAxes));

            DrawIntervalsYAxis(dc);
        }

        /// <summary>
        /// Draws the intervals of the Y axis
        /// </summary>
        /// <param name="dc">Drawing context</param>
        private void DrawIntervalsYAxis(DrawingContext dc)
        {
            Pen yAxisPen = new Pen(Brushes.Black, axesTickThickness);
            for (int i = 0; i < dataYPointsToPlot.Count; i++)
            {
                if (i > 0)
                {
                    dc.DrawLine(yAxisPen, (Point)dataYPointsToPlot[i][0], (Point)dataYPointsToPlot[i][1]);
                }
                DrawYAxisNumbers(dc, dataYPointsToPlot[i][2].ToString(), (double)dataYPointsToPlot[i][3]);
            }
        }

        /// <summary>
        /// Draw the numbers of the Y axis
        /// </summary>
        /// <param name="dc">Drawing context</param>
        /// <param name="numberToDraw">the number to draw</param>
        /// <param name="positionOfNumber">Where to position the number</param>
        private void DrawYAxisNumbers(DrawingContext dc, string numberToDraw, double positionOfNumber)
        {
            FlowDirection dir = FlowDirection.LeftToRight;
            FormattedText textToRender = new FormattedText(numberToDraw, new System.Globalization.CultureInfo("se-SV"), dir, new Typeface("Arial"), 10, Brushes.Green, VisualTreeHelper.GetDpi(this).PixelsPerDip);

            dc.DrawText(textToRender, new Point(25, positionOfNumber - 4));
        }

        /// <summary>
        /// Draw the X axis
        /// </summary>
        /// <param name="dc">Drawing context</param>
        private void DrawXAxis(DrawingContext dc)
        {
            Pen xAxisTickPen = new Pen(Brushes.Black, axesThickness);
            xAxisTickPen.EndLineCap = PenLineCap.Triangle;

            dc.DrawLine(xAxisTickPen, new Point(DiagramDataToDraw.OffsetXAxis, DiagramDataToDraw.HeightOfAxes), new Point(DiagramDataToDraw.WidthOfAxes, DiagramDataToDraw.HeightOfAxes));

            DrawIntervalsXAxis(dc);
        }

        /// <summary>
        /// Draws the intervals of the X axis
        /// </summary>
        /// <param name="dc">Drawing context</param>
        private void DrawIntervalsXAxis(DrawingContext dc)
        {
            Pen xAxisPen = new Pen(Brushes.Gray, axesTickThickness);
            for (int i = 0; i < dataXPointsToPlot.Count; i++)
            {
                if (i > 0)
                {
                    dc.DrawLine(xAxisPen, (Point)dataXPointsToPlot[i][0], (Point)dataXPointsToPlot[i][1]);
                }
                DrawXAxisNumbers(dc, (string)dataXPointsToPlot[i][2].ToString(), (double)dataXPointsToPlot[i][3]);
            }
        }

        /// <summary>
        /// Draw the numbersa of the Y axis
        /// </summary>
        /// <param name="dc">Drawing context</param>
        /// <param name="numberToDraw">the number to draw</param>
        /// <param name="positionOfNumber">Where to position the number</param>
        private void DrawXAxisNumbers(DrawingContext dc, string numberToDraw, double positionOfNumber)
        {
            FlowDirection dir = FlowDirection.LeftToRight;
            FormattedText textToRender = new FormattedText(numberToDraw, new System.Globalization.CultureInfo("se-SV"), dir, new Typeface("Arial"), 10, Brushes.Green, VisualTreeHelper.GetDpi(this).PixelsPerDip);

            dc.DrawText(textToRender, new Point(positionOfNumber - 4, (DiagramDataToDraw.HeightOfAxes + 15)));
        }

        /// <summary>
        /// Writes the points to the diagram
        /// </summary>
        public void WritePointsToDiagram()
        {
            StreamGeometry streamGeomitry = new StreamGeometry();
            streamGeomitry.FillRule = FillRule.EvenOdd;

            using (StreamGeometryContext context = streamGeomitry.Open())
            {
                context.BeginFigure(DiagramDataToDraw.DataPoints.First().Value, false, false);
                context.PolyLineTo(DiagramDataToDraw.DataPoints.Values.Skip(1).ToArray(), true, false);
            }

            globalDc.DrawGeometry(Brushes.Black, new Pen(Brushes.Black, diagramDataLineThickness), streamGeomitry);
        }
    }
}
