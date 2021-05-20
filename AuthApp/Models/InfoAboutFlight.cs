using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.Models
{
    public class InfoAboutFlight
    {
        public int ID { get; private set; }

        public int Route { get; private set; }

        public string From { get; private set; }

        public string To { get; private set; }

        public string ArrDate { get; private set; }

        public string DepDate { get; private set; }

        public string ArrivalTime { get; private set; }

        public string DepartureTime { get; private set; }

        public flights Flight { get; private set; }

        public InfoAboutFlight(int id, string from, string to, flights flight, int route)
        {
            ID = id;
            From = from;
            To = to;
            Flight = flight;
            Route = route;
        }
    }
}
