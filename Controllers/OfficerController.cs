using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Controllers
{
    public class OfficerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
