using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Models
{
    public class Officer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ICID { get; set; }
        [Column(TypeName = "varchar(200)")]

        [Required]
        public string Rank { get; set; }
        [Required]
        public int ComponentId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int IsActive { get; set; }
        public int CreatedBy { get; set; }
    }
}
