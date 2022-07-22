using CPMS_AUTO.Data;
using CPMS_AUTO.Helpers;
using CPMS_AUTO.Models;
using CPMS_AUTO.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Controllers
{
    
    public class ProjectSupportController : Controller
    {
        public readonly RepositoryProjectSupport _RepositoryProjectSupport;
        public readonly RepositoryProject _RepositoryProject;
        public readonly RepositorySus _RepositorySus;
        public readonly ApplicationDbContext _applicationDbContext;

        public ProjectSupportController(RepositoryProjectSupport repositoryProjectSupport, RepositoryProject repositoryProject, RepositorySus repositorySus, ApplicationDbContext applicationDbContext)
        {
            _RepositoryProjectSupport = repositoryProjectSupport;
            _RepositoryProject = repositoryProject;
            _RepositorySus = repositorySus;
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Index(int Id)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");
            if (Logins != null)
            {
                MProjectSupport Model = new MProjectSupport();
                if (Id != 0)
                {
                    Model = _RepositoryProjectSupport.GetById(Id);
                    TempData["Id"] = Model.Id;
                    ViewBag.dnone = false;
                }
                else
                {
                    TempData["Id"] = 0;
                    ViewBag.dnone = true;
                }
                GetAllDropdown();
                GetProjectSupport();
                GetProjectData("HRMS");
                return View(Model);
            }
            else
                return Redirect("/Login/Index");
        }

        public void GetAllDropdown()
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");
            var AllProjects = _RepositoryProjectSupport.GetProjectName();
            AllProjects.Insert(0, new MProject { Id = 0, Name = "--Select Project Name--" });
            ViewBag.AllProjects = AllProjects;
        }
       
        public ActionResult isSuccess(Boolean Id)
        {
            ViewBag.dnone = Id;
            GetAllDropdown();

            return View("Index");

        }

        public ActionResult ifSuccess(string Id)
        {
            ViewBag.ddnone = Id;
            GetAllDropdown();
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSUSNo(MSUSNo Db)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");
            if (Convert.ToInt32(TempData["Id"]) > 0)
                Db.Id = Convert.ToInt32(TempData["Id"]);

            if (ModelState.IsValid)
            {
                //Db.CreatedBy = Logins.UserID;
                int ret = await _RepositorySus.Save(Db);
                if (ret == 1)
                {
                    ViewBag.message = "" + Db.SUS + " is saved successfully";

                }
                else if (ret == 2)
                {
                    ViewBag.message = "" + Db.SUS + " is Update successfully";

                }
                else if (ret == 3)
                {
                    ViewBag.message = "Allready Exits Project Name";
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        
        [HttpPost]
        public JsonResult ValidateSus(string susdata)
        {
            var aaa = _RepositorySus.CheckSus(susdata);
            return Json(aaa);
        }

        [HttpPost]
        public IActionResult GetProjectData(string project)
        {
            if(project != null)
            {
                Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");
                var AllProjects = _RepositoryProjectSupport.GetDataBySus(project);
                ViewBag.AllProjects = AllProjects;
                return Json(AllProjects);
            }
            return View("Index");
        }

        public IActionResult GetProjectDataById(int id)
        {
            if(id != 0)
            {
                var AllProjects = _RepositoryProjectSupport.GetDataById(id);
                ViewBag.AllProjects = AllProjects;
                TempData["Id"] = AllProjects.Id;
                return Json(AllProjects);
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ResponseCache(Duration = 5)]
        public async Task<IActionResult> SaveModal(MProjectSupport Db)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");

            if (Convert.ToInt32(TempData["Id"]) > 0)
                Db.Id = Convert.ToInt32(TempData["Id"]);

            if (ModelState.IsValid)
            {
                Db.CreatedBy = Logins.UserID;

                int ret = await _RepositoryProjectSupport.SaveModalData(Db);

                if (ret == 1)
                {
                    ViewBag.message = "" + Db.SUS_No + " is saved successfully";

                }
                else if (ret == 2)
                {
                    ViewBag.message = "" + Db.SUS_No + " is Update successfully";

                }
                else if (ret == 3)
                {
                    ViewBag.message = "Allready Exits Project Name";
                }
                GetAllDropdown();
                //GetProjectSupport();
                return View("Index");
            }
            else
            {
                GetAllDropdown();
                //GetProjectSupport();
                return View("Index", Db);
            }
        }

        public IActionResult Dashboard()
        {
            ViewBag.ClosedCount = _RepositoryProjectSupport.GetClosedCount();
            ViewBag.OpenCount = _RepositoryProjectSupport.GetOpenCount();
            ViewBag.OnWrkCount = _RepositoryProjectSupport.GetOnWrkCount();
            ViewBag.TotalCount = _RepositoryProjectSupport.GetTotalCount();
            ViewBag.TotalOpenCount = _RepositoryProjectSupport.GetStatusOpen();
            ViewBag.HighestPriority = _RepositoryProjectSupport.HighestPriorityProject();
            ViewBag.LowestPriority = _RepositoryProjectSupport.LowestPriorityProject();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitData(MSUSNo _msusno)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");
            if (ModelState.IsValid)
            {
                _msusno.CreatedBy = Logins.UserID;

                int ret = await _RepositoryProjectSupport.SUBMITSUSUNIT(_msusno);
                if (ret == 1)
                {
                    ViewBag.message = "" + _msusno.SUS + " is saved successfully";

                }
                else if (ret == 2)
                {
                    ViewBag.message = "" + _msusno.SUS + " is Update successfully";

                }
                else if (ret == 3)
                {
                    ViewBag.message = "Allready Exits Project Name";
                }
                GetAllDropdown();
                return View("Index");
            }
            else
            {
                return View("Index", _msusno);
            }
        }

        //Status
        [HttpPost]
        [ResponseCache(Duration = 5)]
        public ActionResult SubmitStatus(string status,int id)
        {
            var stat = status;
            var Id = id;

            var statusdata = _RepositoryProjectSupport.SubmitStatus(status, id);
            
            return View("Index");
        }

        public List<MProjectSupport> GetProjectSupport()
        {
            var AllData = _applicationDbContext.support.Where(i => i.IsActive == 1).ToList();
            return AllData;
        }
    }
}
