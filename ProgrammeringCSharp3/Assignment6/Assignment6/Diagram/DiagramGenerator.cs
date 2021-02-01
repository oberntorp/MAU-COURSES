using System;
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
        private int sixeOfAxes = 300;

        private int maxLengthYAxis = 105;
        private int maxLengthOfXAxis = 50;
        private string diagramTitle;
        private int intervalX;
        private int intervalY;
        private int divisionsX;
        private int divisionsY;

        public DiagramGenerator(double width, double  height, DiagramInformation diagramData)
        {
            Width = width;
            Height = height;

            diagramTitle = diagramData.Title;
            intervalX = diagramData.IntervalX;
            intervalY = diagramData.IntervalY;
            divisionsX = diagramData.DivisionsX;
            divisionsY = diagramData.DivisionsY;

            maxLengthYAxis += sixeOfAxes;
            maxLengthOfXAxis += sixeOfAxes;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            DrawDiagramHeader(dc);

            DrawYAxis(dc);
            DrawXAxis(dc);
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

            DrawIntervalsYAxis(dc, yAxisPen);
        }

        private void DrawIntervalsYAxis(DrawingContext dc, Pen yAxisPen)
        {
            yAxisPen.Thickness = 1;
            int interval = sixeOfAxes / divisionsY;
            for (int i = (divisionsY/intervalY)+105, intervalFigure = intervalY; i < maxLengthYAxis-intervalY; i += interval, intervalFigure += intervalY)
            {
                dc.DrawLine(yAxisPen, new Point(45, i), new Point(55, i));
            }
        }

        private void DrawXAxis(DrawingContext dc)
        {
            Pen xAxisPen = new Pen(Brushes.Black, 4);
            xAxisPen.EndLineCap = PenLineCap.Triangle;

            dc.DrawLine(xAxisPen, new Point(50, 105 + sixeOfAxes), new Point(maxLengthOfXAxis, maxLengthYAxis));

            DrawIntervalsXAxis(dc, xAxisPen);
        }

        private void DrawIntervalsXAxis(DrawingContext dc, Pen xAxisPen)
        {
            xAxisPen.Thickness = 1;
            int interval = sixeOfAxes / divisionsX;
            for (int offsetXAxis = (divisionsX/intervalX)+50, intervalFigure = intervalX; offsetXAxis < maxLengthOfXAxis-divisionsX; offsetXAxis += interval, intervalFigure += intervalX)
            {
                dc.DrawLine(xAxisPen, new Point(offsetXAxis, (105 + sixeOfAxes) - 5), new Point(offsetXAxis, (105 + sixeOfAxes) + 5));
            }
        }
    }
}
