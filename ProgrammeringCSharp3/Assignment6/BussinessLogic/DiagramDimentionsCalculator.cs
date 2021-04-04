using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6.BussinessLogic
{
    /// <summary>
    /// Class for calculating the height of the diagram
    /// </summary>
    public class DiagramDimentionsCalculator
    {
        private double width;
        private double height;
        private double containerWidth;
        private double containerHeight;
        private double offsetXAxisToUseInMargin;
        private double offsetYAxisToUseInMargin;
        public double CalculatedHeight { get; set; }
        public double CalculatedWidth { get; set; }

        /// <summary>
        /// Constructor, initializes the class
        /// </summary>
        /// <param name="heightOfDiagram">The original height of the diagram</param>
        /// <param name="widthOfDiagram">The original width of the diagram</param>
        /// <param name="widthOfDiagramContainer">The width of the containing element holding the diagram</param>
        /// <param name="heightOfDiagramContainer">The height of the containing element holding the diagram</param>
        /// <param name="offsetXAxis">The offset in X axis</param>
        /// <param name="offsetYAxis">The offset in Y axis</param>
        public DiagramDimentionsCalculator(double heightOfDiagram, double widthOfDiagram, double widthOfDiagramContainer, double heightOfDiagramContainer, double offsetXAxis, double offsetYAxis)
        {
            height = heightOfDiagram;
            width = widthOfDiagram;
            containerHeight = heightOfDiagramContainer;
            containerWidth = widthOfDiagramContainer;
            offsetXAxisToUseInMargin = offsetXAxis;
            offsetYAxisToUseInMargin = offsetYAxis;

            CalculateDiagramDimentions();
        }

        /// <summary>
        /// Calculates the dimaentions of the diagram
        /// </summary>
        private void CalculateDiagramDimentions()
        {
            double margin = Math.Min(containerHeight * 0.1, containerWidth * 0.2);

            double scaleXAxis = (containerHeight - (offsetYAxisToUseInMargin/margin))/margin;
            double scaleYAxis = containerWidth - (offsetXAxisToUseInMargin/margin)/margin;

            double scale = Math.Abs(Math.Min(scaleXAxis, scaleYAxis));

            CalculatedHeight = containerHeight - (offsetYAxisToUseInMargin + margin);
            CalculatedWidth = CalculatedHeight * 0.8;

        }
    }
}
