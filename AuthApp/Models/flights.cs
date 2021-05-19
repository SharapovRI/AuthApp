namespace AuthApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("flights")]
    public partial class flights
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public flights()
        {
            flights_has_stations = new HashSet<flights_has_stations>();
            flights_has_traveltime = new HashSet<flights_has_traveltime>();
        }

        [Key]
        public int idFlights { get; set; }

        public int TravelTimeId { get; set; }

        public int? FlightCrewId { get; set; }

        public int RouteId { get; set; }

        [ForeignKey("FlightCrewId")]
        public virtual flightcrews flightcrews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<flights_has_stations> flights_has_stations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<flights_has_traveltime> flights_has_traveltime { get; set; }
    }
}
