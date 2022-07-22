using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Models
{
    public class MProject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Name { get; set; }
        [Display(Name = "Component Name")]
        [Required]
        public int ComponentId { get; set; }
        [NotMapped]
        public string ComponentName { get; set; }
        [Required]
        [Display(Name = "Sponsor Name")]
        public int SponsorId { get; set; }
        
        public string SponsorName { get; set; }
        [Required]
        [Display(Name = "Officer")]
        public int ICID { get; set; }

        public string Officer { get; set; }
        [Display(Name = "Status Name")]
        [Required]
        public int StatusCode { get; set; }
        public string StatusName { get; set; }
       
        [Display(Name = "Vetting Code")]
        public int VettingCode { get; set; }
        [Column(TypeName = "varchar(200)")]

        public string VettingName { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        [Display(Name = "Amt.")]
        public Decimal Amount { get; set; }

        [Display(Name = "Vendor")]
        public int VendorId { get; set; }

        [NotMapped]
        public string VendorName { get; set; }

        [Display(Name = "Grant")]
        public int GrantId { get; set; }

        [NotMapped]
        public string GrantName { get; set; }

        public string Remarks { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int IsActive { get; set; }
        public string CreatedBy { get; set; }
        //public string Type { get; set; }
        //public string Search { get; set; }
    }
}
