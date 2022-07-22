using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Models
{
    public class MVendor
    {
        [Key]
        public int Id { get; set; }

        public string VendorName { get; set; }
        public int isActive { get; set; }
    }
}
