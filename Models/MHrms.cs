using Itenso.TimePeriod;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPMS_AUTO.Models
{
    public class MHrms
    {
        [Key]
        [Display(Name = "Case No")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "SUS No")]
        public string Sus_No { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "Unit Name")]
        public string Unit { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Project { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "Complaint")]
        public string Query { get; set; }

        //[Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Complaint")]
       // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime QueryDate { get; set; } /*= System.DateTime.Today;*/

        [NotMapped]
        [DataType(DataType.Date)]
        public Date MyDate { get; set; }

        [NotMapped]
        [DataType(DataType.Date)]
        [Display(Name = "To Date")]
        public DateTime ToDate { get; set; }

        [NotMapped]
        [DataType(DataType.Date)]
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }

        [Required]
        [Display(Name = "Complaint By")]
        public string QueryRaisedBy { get; set; }

        [Required]
        [Display(Name = "Mode Of Query")]
        public string ModeOfQuery { get; set; }
        
        //[Required]
        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        //[Required]
        //[Display(Name = "Remarks")]
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

        [Display(Name = "Unit Status")]
        public string Unit_Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int IsActive { get; set; }
        public string CreatedBy { get; set; }
        
    }
}
