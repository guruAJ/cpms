using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Models
{
    public class MRank
    {
        [Key]
        public int Id { get; set; }
        public string RankName { get; set; }
        public int isActive { get; set; }
    }
}
