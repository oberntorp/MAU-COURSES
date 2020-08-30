using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{
    public class TakeOffEventArgs: EventArgs
    {
        /// <summary>
        /// The constructor of this eventargs class
        /// </summary>
        /// <param name="flightCodeIn">What flightCode it is about</param>
        /// <param name="startTimeIn">the time of takeOff</param>
        public TakeOffEventArgs(string flightCodeIn, string startTimeIn)
        {
            FlightCode = flightCodeIn;
            FlightTime = startTimeIn;
            FlightStatus = "Sent to runway";
        }
        public string FlightCode { get; set; }
        public string FlightStatus { get; set; }
        public string FlightTime { get; set; }


    }
}
