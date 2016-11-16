namespace AppointMed.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Appointment")]
    public partial class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int patientID { get; set; }

        public int scheduleID { get; set; }

        [StringLength(50)]
        public string prescription { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual Schedule Schedule { get; set; }
    }
}
