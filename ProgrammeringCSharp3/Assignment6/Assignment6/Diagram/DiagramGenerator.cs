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

        private static void DrawYAxis(DrawingContext dc)
        {
            Pen yAxis = new Pen(Brushes.Blue, 4);
            yAxis.StartLineCap = PenLineCap.Triangle;
            dc.DrawLine(yAxis, new Point(50, 105), new Point(50, 280));
        }

        private static void DrawXAxis(DrawingContext dc)
        {
            Pen xAxis = new Pen(Brushes.Black, 4);
            xAxis.EndLineCap = PenLineCap.Triangle;

            dc.DrawLine(xAxis, new Point(50, 280), new Point(280, 280));
        }
    }
}
