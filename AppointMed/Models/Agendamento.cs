using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointMed.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public System.DateTime startDate { get; set; }
        public string prescription { get; set; }
        public int patientID { get; set; }

    }
}