using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BussinessLogic
{
    /// <summary>
    /// This class is responsible for Saving the diagram information inputted of the GUI.
    /// It does also hold the data points making up the polyline of the plotted diagram.
    /// It does also sort the Points on request.
    /// </summary>
    public class DiagramInformation
    {
        public string Title { get; set; }
        public int IntervalX { get; set; }
        public int IntervalY { get; set; }
        public int DivisionsX { get; set; }
        public int DivisionsY { get; set; }
        public Dictionary<string, Point> DataPoints { get; set; }
        private double heightOfAxes = 300;
        private double widthOfAxes = 300;
        public double HeightOfAxes
        {
            get => heightOfAxes;
            set { heightOfAxes = value; }
        }
        public double WidthOfAxes
        {
            get => widthOfAxes;
            set
            {
                if (value >= 0)
                {
                    widthOfAxes = value;
                }
            }
        }
        public int OffsetYAxis = 105;
        public int OffsetXAxis = 50;

        private double pixelIntervalYAxis;
        private double pixelIntervalXAxis;

        public double IntervalDistanceYAxis { get => pixelIntervalYAxis; }
        public double IntervalDistanceXAxis { get => pixelIntervalXAxis; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="titleFromGui"></param>
        /// <param name="intervalXFromGui"></param>
        /// <param name="IntervalYFromGui"></param>
        /// <param name="divisionsXFromGui"></param>
        /// <param name="divisionsYFromGui"></param>
        /// <param name="diagramContainerHeight"></param>
        public DiagramInformation(string titleFromGui, int intervalXFromGui, int IntervalYFromGui, int divisionsXFromGui, int divisionsYFromGui, double diagramContainerHeight)
        {
            Title = titleFromGui;
            IntervalX = intervalXFromGui;
            IntervalY = IntervalYFromGui;
            DivisionsX = divisionsXFromGui;
            DivisionsY = divisionsYFromGui;
            DataPoints = new Dictionary<string, Point>();
            HeightOfAxes = diagramContainerHeight - OffsetYAxis;
            WidthOfAxes = HeightOfAxes * 0.8;

            pixelIntervalYAxis = (HeightOfAxes - OffsetYAxis) / DivisionsY;
            pixelIntervalXAxis = (WidthOfAxes - OffsetXAxis) / DivisionsX;
        }

        public double ConvertXPointToBeUsed(double XValidatedValue)
        {
            return OffsetXAxis + (IntervalDistanceXAxis * XValidatedValue);
        }


        public double ConvertYPointToBeUsed(double YValidatedValue)
        {
            return HeightOfAxes - (IntervalDistanceYAxis * (YValidatedValue / IntervalY));
        }

        public void SortPointsAccordingToXAxis()
        {
            DataPoints = DataPoints.OrderBy(p => p.Value.X).ToDictionary(x => x.Key, x => x.Value);
        }

        public void SortPointsAccordingToYAxis()
        {
            DataPoints = DataPoints.OrderByDescending(p => p.Value.Y).ToDictionary(x => x.Key, x => x.Value);
        }

        public bool AddPoint(double XValidatedValue, double YValidatedValue)
        {
            int nbrPointsBeforeAdd = DataPoints.Count();
            Point pointBeingAdded = new Point(ConvertXPointToBeUsed(XValidatedValue), ConvertYPointToBeUsed(YValidatedValue));
            if (!DoesPointExist(pointBeingAdded))
            {
                DataPoints.Add($"{XValidatedValue}, {YValidatedValue}", pointBeingAdded);
            }

            return (DataPoints.Count > nbrPointsBeforeAdd);
        }

        /// <summary>
        /// Remove a datapoint
        /// </summary>
        /// <param name="dataPointToRemove">DataPoint being removed</param>
        /// <returns>true/false</returns>
        public bool RemovePoint(Point dataPointToRemove)
        {
            int nbrPointsBeforeRemove = DataPoints.Count();
            return (nbrPointsBeforeRemove > DataPoints.Count);

            return (DataPoints.Count > nbrPointsBeforeRemove);
        }

        public void ClearDiagramPoints()
        {
            DataPoints.Clear();
        }

        public string GetOriginalNumberFromPointIdLastAdded()
        {
            return DataPoints.Last().Key;
        }

        public bool DoesPointExist(Point checkIfExist)
        {
            return DataPoints.ContainsValue(checkIfExist);
        }
    }
}
