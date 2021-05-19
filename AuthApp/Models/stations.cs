namespace AuthApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("stations")]
    public partial class stations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public stations()
        {
            flights_has_stations = new HashSet<flights_has_stations>();
        }

        [Required]
        [StringLength(45)]
        public string Name { get; set; }

        [Key]
        public int IdStations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<flights_has_stations> flights_has_stations { get; set; }
    }
}
