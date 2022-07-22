using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Models
{
    public class TokenLog
    {
        [Key]
        public int Id { get; set; }
        public string connectionId { get; set; }

        public string ipaddress { get; set; }
        public string TimeStamp { get; set; }
        public Boolean Active { get; set; } = true;


    }
}
