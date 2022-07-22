using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPMS_AUTO.Models
{
    public class MProjectSupport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Project { get; set; }

        [Required]
        [Display(Name = "SUS No.")]
        public string SUS_No { get; set; } 

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Query { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Query Date")]
        public DateTime QueryDate { get; set; } = System.DateTime.Today;

        [Required]
        [Display(Name = "Query Raised By")]
        public string QueryRaisedBy { get; set; }

        [Required]
        [Display(Name = "Mode Of Query")]
        public string ModeOfQuery { get; set; }

        //[Required]
        public string Reply { get; set; }

        public string Status { get; set; }

        //[Required]
        [DataType(DataType.Date)]
        [Display(Name = "Replied Date")]
        public DateTime RepliedDate { get; set; } 

        //[Required]
        [Display(Name = "Replied By")]
        public string RepliedBy { get; set; }

        [Display(Name = "Complain Category")]
        public string Cpl_Category { get; set; }
        //public string Unit { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int IsActive { get; set; }
        public string CreatedBy { get; set; }
    }
}
