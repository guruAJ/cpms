using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Models
{
    public class MComponent
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(200)")]
        [Required]
        [Display(Name = "Component")]
        public string Name { get; set; }

        [Display(Name ="Name")]
        public string ICID { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int IsActive { get; set; }
        public int CreatedBy { get; set; }
    }
}
