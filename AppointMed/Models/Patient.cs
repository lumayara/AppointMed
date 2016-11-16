namespace AppointMed.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Patient")]
    public partial class Patient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(13)]
        public string Cellphone { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(15)]
        public string Username { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Compare("Password", ErrorMessage = "Password does not match.")]
        //[DataType(DataType.Password)]
        //public string confPassword { get; set; }

        public bool authorized { get; set; }

        public int? insuranceNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
