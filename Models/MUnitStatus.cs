using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Models
{
    public class MUnitStatus
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Unit Status")]
        public string Status { get; set; }
        public int isActive { get; set; }
    }
}
