using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Models
{
    public class MStatus
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(200)")]
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "varchar(200)")]
        //[Required]
        public string Code { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int IsActive { get; set; }
        public string CreatedBy { get; set; }
    }
}
