using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.ViewModels
{
    public class EditTravelTimeModel
    {
        public int ID { get; set; }
        public DateTime ArrivalDate { get; set; }
        public TimeSpan ArrivalTime { get; set; }

        public DateTime DepartureDate { get; set; }
        public TimeSpan DepartureTime { get; set; }

        public DateTime ArrTime
        {
            get
            {
                return ArrivalDate.Add(ArrivalTime);
            }
        }

        public DateTime DepTime
        {
            get
            {
                return DepartureDate.Add(DepartureTime);
            }
        }
    }
}
