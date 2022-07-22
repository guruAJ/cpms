using CPMS_AUTO.Helpers;
using CPMS_AUTO.Models;
using CPMS_AUTO.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Controllers
{
    public class ProjectController : Controller
    {
        public readonly RepositoryProject _RepositoryProject;
        public readonly RepositorySponsor _RepositorySponsor;
        public readonly RepositoryStatus _RepositoryStatus;
        public readonly RepositoryVetting _RepositoryVetting;
        public readonly ComponentRepository _componentRepository;
        public readonly RepositoryUser _repositoryUser;
        public readonly RepositoryRemarks _repositoryRemarks;
        public ProjectController(RepositoryProject context, 
            RepositorySponsor contextSponsor,
            RepositoryStatus repositoryStatus, RepositoryVetting repositoryVetting,
            ComponentRepository componentRepository,RepositoryUser repositoryUser, RepositoryRemarks repositoryRemarks)
        {
            _RepositoryProject = context;
            _RepositorySponsor = contextSponsor;
            _RepositoryStatus = repositoryStatus;
            _RepositoryVetting = repositoryVetting;
            _componentRepository = componentRepository;
            _repositoryUser = repositoryUser;
            _repositoryRemarks = repositoryRemarks;
        }

        public IActionResult Index(int Id)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");
            if (Logins != null)
            {
                MProject Model = new MProject();
                if (Id != 0)
                {
                    Model = _RepositoryProject.GetById(Id);
                    TempData["Id"] = Model.Id;

                    ViewBag.dnone = false;
                }
                else
                {
                    TempData["Id"] = 0;
                    ViewBag.dnone = true;


                }
                GetAllDropdown();

                AllprojectData();
                return View(Model);
            }
            else
                return Redirect("/Login/Index");
        }
        public void AllprojectData()
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");
            dynamic AllData;
            if (Logins.RoleType == 1)
            {
                AllData = _RepositoryProject.GetAll();
            }
            else
            {
                AllData = _RepositoryProject.GetAll(Logins.UserID);
            }

            ViewBag.AllData = AllData;
        }

       public void GetAllDropdown()
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");
            var AllSponsor = _RepositorySponsor.GetAll();
            AllSponsor.Insert(0, new MSponsor { Id = 0, Name = "--Select Sponsor Name--" });
            ViewBag.AllSponsor = AllSponsor;

            var AllStatus = _RepositoryStatus.GetAll();
            AllStatus.Insert(0, new MStatus { Id = 0, Name = "--Select Status Code--" });
            ViewBag.AllStatus = AllStatus;

            var AllVetting = _RepositoryVetting.GetAll();
            AllVetting.Insert(0, new MVetting { Id = 0, Description = "--Select Vetting Code--" });
            ViewBag.AllVetting = AllVetting;

            var AllGrants = _RepositoryProject.GetAllGrants();
            AllGrants.Insert(0, new MGrant { Id = 0, GrantName = "--Select Grant Name--" });
            ViewBag.AllGrants = AllGrants;

            var AllVendors = _RepositoryProject.GetAllVendors();
            AllVendors.Insert(0, new MVendor { Id = 0, VendorName = "--Select Vendor Name--" });
            ViewBag.AllVendors = AllVendors;

            //if (Logins.RoleType==1)
            //{
            var AllComponent = _componentRepository.GetAll();

                AllComponent.Insert(0, new MComponent { Id = 0, Name = "--Select Component Code--" });
                ViewBag.AllComponent = AllComponent;
            //}
            //else
            //{
            //    var AllComponent = _componentRepository.GetUser(Logins.ComponentId);

            //    //AllComponent.Insert(0, new MComponent { Id = 0, Name = "--Select Component Code--" });
            //    ViewBag.AllComponent = AllComponent;
            //}

            dynamic AllOfficer;
            if (Logins.RoleType == 1)
            {
                //AllOfficer = _repositoryUser.GetAll();
                //AllOfficer.Insert(0, new Login { Id = 0, UserID = "--Select Officer--" });
                //ViewBag.AllOfficer = AllOfficer;

                AllOfficer = _componentRepository.GetLoginData();
                AllOfficer.Insert(0, new Login { Id = 0, UserID = "--Select Officer--" });
                ViewBag.AllOfficer = AllOfficer;
            }
            else
            {
                //AllOfficer = _repositoryUser.GetByIdAll(Logins.ComponentId);
                AllOfficer = _componentRepository.GetLoginDataById(Logins.UserID);
                AllOfficer.Insert(0, new Login { Id = 0, UserID = "--Select Officer--" });
                ViewBag.AllOfficer = AllOfficer;
            }
        }
        [HttpPost]
        public JsonResult GetICID(int ComponentId)
        {
          var  AllOfficer = _repositoryUser.GetByIdAll(ComponentId);
            return Json(AllOfficer);
        }

        [HttpPost]
        public JsonResult GetICID1(int ComponentId)
        {
            var AllOfficer = _repositoryUser.GetByIdAllForAjax(ComponentId);
            return Json(AllOfficer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ResponseCache(Duration = 5)]
        public async Task<IActionResult> Save(MProject Db)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");

            if (Convert.ToInt32(TempData["Id"]) > 0)
                Db.Id = Convert.ToInt32(TempData["Id"]);

            if (ModelState.IsValid)
            {
                Db.CreatedBy = Logins.UserID;
                
                int ret = await _RepositoryProject.Save(Db);
                if (ret == 1)
                {
                    ViewBag.message = "" + Db.Name + " is saved successfully";

                }
                else if (ret == 2)
                {
                    ViewBag.message = "" + Db.Name + " is Update successfully";

                }
                else if (ret == 3)
                {
                    ViewBag.message = "Already Exits Project Name";
                }
                AllprojectData();

                ViewBag.dnone = true;

                return View("Index", Db);
            }
            else
            {
                GetAllDropdown();
                AllprojectData();

                ViewBag.dnone = false;
                return View("Index", Db);
            }
        }
        public ActionResult isSuccess(Boolean Id)
        {
            ViewBag.dnone = Id;
            AllprojectData();
            GetAllDropdown();
           
            return View("Index");
          
        }

        public IActionResult Remarks(int Id)
        {
            MProject Model = new MProject();
            if (Id != 0)
            {
                Model = _RepositoryProject.GetById(Id);
                TempData["ProjectId"] = Model.Id;

                ViewBag.dnoneRemarks = false;
            }
            else
            {
                ViewBag.dnoneRemarks = true;
            }
            ///Project Data
            GetAllDropdown();
            AllprojectData();

            ///remarks Data
            ViewBag.RemarksAllData = _repositoryRemarks.GetByProjectId(Id);

            Model.Remarks = "";
            return View("Index", Model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveRemarks(MRemarks Db)
        {

            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");

            //foreach (var modelValue in ModelState.Values)
            //{
            //    modelValue.Errors.Clear();
            //}
            //ModelState["Id"].ValidationState = ModelState.IsValid;
            if (Convert.ToInt32(TempData["ProjectId"]) > 0)
                Db.ProjectId = Convert.ToInt32(TempData["ProjectId"]);

            if (Db.Remarks!=null)
            {
                Db.CreatedBy = Logins.UserID;

                int ret = await _repositoryRemarks.Save(Db);
                if (ret == 1)
                {
                    ViewBag.remarksmessage = "" + Db.Remarks + " is saved successfully";

                }
                else if (ret == 2)
                {
                    ViewBag.remarksmessage = "" + Db.Remarks + " is Update successfully";

                }
                else if (ret == 3)
                {
                    ViewBag.remarksmessage = "Already Exits Project Name";
                }
                AllprojectData();
                ///remarks Data
                ViewBag.RemarksAllData = _repositoryRemarks.GetByProjectId(Convert.ToInt32(TempData["ProjectId"]));
                ViewBag.dnone = true;
                ViewBag.dnoneRemarks = true;
                return View("Index");
            }
            else
            {
                //ViewBag.remarksmessage = "Please Enter Remarks";
                ///remarks Data
                ///
                MProject Model = new MProject();
                Model = _RepositoryProject.GetById(Convert.ToInt32(TempData["ProjectId"]));
                TempData["ProjectId"] = Model.Id;
                ViewBag.RemarksAllData = _repositoryRemarks.GetByProjectId(Convert.ToInt32(TempData["ProjectId"]));
                GetAllDropdown();
                AllprojectData();
                ViewBag.dnone = true;
                ViewBag.dnoneRemarks = false;
                return View("Index", Model);
            }
        }

        
    }
}
