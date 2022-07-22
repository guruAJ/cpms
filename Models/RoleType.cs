using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Models
{
    public class RoleType
    {
        public string Role { get; set; }

        public List<SelectListItem> Roles { get; } = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Admin" },
        new SelectListItem { Value = "2", Text = "User" },
        new SelectListItem { Value = "3", Text = "Other"  },
    };
    }
}
