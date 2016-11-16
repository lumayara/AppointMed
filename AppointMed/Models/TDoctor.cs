namespace AppointMed.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TDoctor")]
    public partial class TDoctor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TDoctor()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string firstName { get; set; }

        [Required]
        [StringLength(50)]
        public string lastName { get; set; }

        [Required]
        [StringLength(20)]
        public string speciality { get; set; }

        public int? yearsExperience { get; set; }

        [Column(TypeName = "money")]
        public decimal price { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(15)]
        public string Username { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
