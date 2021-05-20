namespace AuthApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("flights_has_stations")]
    public partial class flights_has_stations
    {
        public int Stations_IdStations { get; set; }

        public int Flights_idFlights { get; set; }

        [Column(TypeName = "uint")]
        public int Flights_has_StationsID { get; set; }

        [ForeignKey("Flights_idFlights")]
        public virtual flights flights { get; set; }

        [ForeignKey("Stations_IdStations")]
        public virtual stations stations { get; set; }

        public virtual int NumberofStation { get; set; }
    }
}
