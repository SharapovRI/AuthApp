namespace AuthApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("flights_has_traveltime")]
    public partial class flights_has_traveltime
    {
        public int TravelTime_idTravelTime { get; set; }

        public int Flights_idFlights { get; set; }

        [Column(TypeName = "uint")]
        public int Flights_has_TravelTimeID { get; set; }

        [ForeignKey("Flights_idFlights")]
        public virtual flights flights { get; set; }
        [ForeignKey("TravelTime_idTravelTime")]
        public virtual traveltime traveltime { get; set; }
    }
}
