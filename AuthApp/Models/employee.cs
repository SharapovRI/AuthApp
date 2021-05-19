namespace AuthApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("employee")]
    public partial class employee
    {
        [Key]
        [Column(TypeName = "uint")]
        public long idEmployee { get; set; }

        [Required]
        [StringLength(45)]
        public string Login { get; set; }

        [Required]
        [StringLength(512)]
        public string Password { get; set; }

        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual posts posts { get; set; }
    }
}
