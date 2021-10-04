using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.ViewModels
{
    public class EditTravelTimeModel : ValidationAttribute
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

        [EditTravelTimeModel(ErrorMessage = "Arrival date can't be bigger than departure time")]
        public DateTime DepTime
        {
            get
            {
                return DepartureDate.Add(DepartureTime);
            }
        }

        public override bool IsValid(object value)
        {
            if (ArrTime > DepTime)
            {
                return false;
            }
            return true;
        }
    }
}
