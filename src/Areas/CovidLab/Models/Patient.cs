using LIVESTOCK.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.CovidLab.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Father/Husband Name")]
        public string FatherHusbandName { get; set; }
        [DisplayName("Mobile No 92XXXXXXX")]
        [RegularExpression("^[9][2][3][0-9]{9}$", ErrorMessage = "Mobile No must be in valid format!")]
        public string MobileNo { get; set; }
        [Required]
        [Range(0, 110, ErrorMessage = "Age must be valid")]
        public short Age { get; set; }
        [Required]
        [DisplayName("CNIC XXXXX-XXXXXXX-X")]
        [RegularExpression("^[0-9]{5}-[0-9]{7}-[0-9]$", ErrorMessage = "CNIC No must be valid!")]
        public string CNIC { get; set; }
        [Required]
        public string Profession { get; set; }
        [Display(Name = "District")]
        [Required(ErrorMessage = "{0} is required.")]
        public int DistrictID { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DOS { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short TotalTestConducted { get; set; }
        public short Status { get; set; }
        public string CreatedBy { get; set; }
        public string Gender { get; set; }
        public short LabID { get; set; }
        public virtual District District { get; set; }
        public virtual Lab Lab { get; set; }
    }
}
