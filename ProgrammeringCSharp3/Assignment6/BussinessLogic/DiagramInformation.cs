using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic
{
    public class DiagramInformation
    {
        public string Title { get; set; }
        public int IntervalX { get; set; }
        public int IntervalY { get; set; }
        public int DivisionsX { get; set; }
        public int DivisionsY { get; set; }

        public DiagramInformation(string titleFromGui, int intervalXFromGui, int IntervalYFromGui, int divisionsXFromGui, int divisionsYFromGui)
        {
            Title = titleFromGui;
            IntervalX = intervalXFromGui;
            IntervalY = IntervalYFromGui;
            DivisionsX = divisionsXFromGui;
            DivisionsY = divisionsYFromGui;
        }
    }
}
