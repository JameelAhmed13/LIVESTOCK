using LIVESTOCK.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.CovidLab.Models
{
    public class Lab
    {
        [Key]
        public short LabID { get; set; }
        [Required]        
        public string Name { get; set; }                         
        [Required]
        public string Address { get; set; }
        [Required]
        [DisplayName("Phone No")]
        public string ContactNo { get; set; }
        [DisplayName("Mobile No 92XXXXXXX")]
        [RegularExpression("^[9][2][3][0-9]{9}$", ErrorMessage = "Mobile No must be in valid format!")]
        public string MobileNo { get; set; }
        [Display(Name = "District")]
        [Required(ErrorMessage = "{0} is required.")]
        public int DistrictID { get; set; }
              
        public virtual District District { get; set; }
    }
}
