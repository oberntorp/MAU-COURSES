using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BussinessLogic
{
    public class DiagramInformation
    {
        public string Title { get; set; }
        public int IntervalX { get; set; }
        public int IntervalY { get; set; }
        public int DivisionsX { get; set; }
        public int DivisionsY { get; set; }
        public List<Point> Points { get; set; }
        public int sizeOfAxes = 300;
        private int OffsetYAxis = 105;
        private int OffsetXAxis = 50;

        public DiagramInformation(string titleFromGui, int intervalXFromGui, int IntervalYFromGui, int divisionsXFromGui, int divisionsYFromGui)
        {
            Title = titleFromGui;
            IntervalX = intervalXFromGui;
            IntervalY = IntervalYFromGui;
            DivisionsX = divisionsXFromGui;
            DivisionsY = divisionsYFromGui;
            Points = new List<Point>();
        }

        public double ConvertXPointToBeUsed(double XValidatedValue)
        {
            return ((OffsetXAxis + ((sizeOfAxes / DivisionsX) * XValidatedValue)));
        }


        public double ConvertYPointToBeUsed(double YValidatedValue)
        {
            return (((OffsetYAxis + sizeOfAxes) - ((sizeOfAxes / DivisionsY) * (YValidatedValue / IntervalY))));
        }
    }
}
