using CPMS_AUTO.Helpers;
using CPMS_AUTO.Models;
using CPMS_AUTO.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Controllers
{
 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly RepositoryProject _RepositoryProject;
        public readonly RepositorySponsor _RepositorySponsor;
        public readonly RepositoryStatus _RepositoryStatus;
        public readonly RepositoryVetting _RepositoryVetting;
        public readonly RepositoryProjectSupport _RepositoryProjectSupport;
        public HomeController(ILogger<HomeController> logger,RepositoryProject repositoryProject,
            RepositorySponsor contextSponsor,
            RepositoryStatus repositoryStatus, RepositoryVetting repositoryVetting, RepositoryProjectSupport repositoryProjectSupport)
        {
            _logger = logger;
            _RepositorySponsor = contextSponsor;
            _RepositoryStatus = repositoryStatus;
            _RepositoryVetting = repositoryVetting;
            _RepositoryProject = repositoryProject;
            _RepositoryProjectSupport = repositoryProjectSupport;
        }

        public IActionResult Index()
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");

            if (Logins != null)

            {
                if (Logins.RoleType == 1)
                {
                    ViewBag.ProjectCount = _RepositoryProject.GetAll();
                }
                else
                {
                    ViewBag.ProjectCount = _RepositoryProject.GetAll(Logins.UserID);
                }
               // ViewBag.ProjectCount = _RepositoryProject.GetAll(Logins.UserID);

                ViewBag.Sponsor = _RepositorySponsor.GetAll();
                ViewBag.Status = _RepositoryStatus.GetAll();
                ViewBag.Vetting = _RepositoryVetting.GetAll();
                ViewBag.ClosedCount = _RepositoryProjectSupport.GetClosedCount();
                ViewBag.OpenCount = _RepositoryProjectSupport.GetOpenCount();
                ViewBag.OnWrkCount = _RepositoryProjectSupport.GetOnWrkCount();
                return View();
            }
            else
                return Redirect("/Login/Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
