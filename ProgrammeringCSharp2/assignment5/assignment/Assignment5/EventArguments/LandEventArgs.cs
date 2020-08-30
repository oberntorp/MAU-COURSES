using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{
    /// <summary>
    /// The event args class of the LandEvent
    /// </summary>
    public class LandEventArgs: EventArgs
    {
        /// <summary>
        /// The constructor of this eventargs class
        /// </summary>
        /// <param name="flightCodeIn">What flightCode it is about</param>
        /// <param name="landingTimeIn">The time of landing</param>
        public LandEventArgs(string flightCodeIn, string landingTimeIn)
        {
            FlightCode = flightCodeIn;
            FlightTime = landingTimeIn;
            FlightStatus = "Landed";
        }
        public string FlightCode { get; set; }
        public string FlightStatus { get; set; }
        public string FlightTime { get; set; }
    }
}
