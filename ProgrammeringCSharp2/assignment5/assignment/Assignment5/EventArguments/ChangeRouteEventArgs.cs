using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{
    /// <summary>
    /// The event args of the Change route event
    /// </summary>
    public class ChangeRouteEventArgs: EventArgs
    {
        /// <summary>
        /// The constructor of this eventargs class
        /// </summary>
        /// <param name="flightCodeIn">What flightCode it is about</param>
        /// <param name="timeOfChangeIn">time of change</param>
        /// <param name="newRoute">The new route</param>
        public ChangeRouteEventArgs(string flightCodeIn, string timeOfChangeIn, string newRoute)
        {
            FlightCode = flightCodeIn;
            FlightTime = timeOfChangeIn;
            FlightStatus = $"Route changed: {newRoute}";
        }
        public string FlightCode { get; set; }
        public string FlightStatus { get; set; }
        public string FlightTime { get; set; }
    }
}
