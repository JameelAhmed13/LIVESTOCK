using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.CovidLab.Models
{
    public class PatientResults
    {
        [Key]
        public int PatientResultID { get; set; }
        public int PatientID { get; set; }
        public DateTime DOR { get; set; }
        public bool Result { get; set; }
        public DateTime ReTestDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
