using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Assignment6.Diagram
{
    class DiagramGenerator: Canvas
    {
        private string diagramTitle;
        private int diagramMin;
        private int diagramMax;
        private int diagramStep;

        public DiagramGenerator(double width, double  height, string diagramTitleFromGui, int diagramIntervalMinFromGui, int diagramIntervalMaxFromGui, int diagramStepFromGui)
        {
            Width = width;
            Height = height;
            diagramTitle = diagramTitleFromGui;
            diagramMin = diagramIntervalMinFromGui;
            diagramMax = diagramIntervalMaxFromGui;
            diagramStep = diagramStepFromGui;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            FlowDirection dir = FlowDirection.LeftToRight;
            FormattedText textToRender = new FormattedText(diagramTitle, new System.Globalization.CultureInfo("se-SV"), dir, new Typeface("Arial"), 102, Brushes.Blue, VisualTreeHelper.GetDpi(this).PixelsPerDip);

            dc.DrawText(textToRender, new Point(50, 0));

            Pen yAxis = new Pen(Brushes.Blue, 4);
            yAxis.StartLineCap = PenLineCap.Triangle;

            Pen xAxis = new Pen(Brushes.Black, 4);
            xAxis.EndLineCap = PenLineCap.Triangle;

            dc.DrawLine(yAxis, new Point(50, 105), new Point(50, 280));
            dc.DrawLine(xAxis, new Point(50, 280), new Point(280, 280));
        }
    }
}
