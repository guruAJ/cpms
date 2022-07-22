using System;
using System.ComponentModel.DataAnnotations;

namespace CPMS_AUTO.Models
{
    public class MSUSNo
    {
        [Key]
        [Display(Name = "S/No")]
        public int Id { get; set; }

        [Display(Name = "SUS No")]
        public string SUS { get; set; }
        public string Unit { get; set; }
        public int Hrms { get; set; }
        public int Iqmp { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int IsActive { get; set; }
        public string CreatedBy { get; set; }

    }
}
