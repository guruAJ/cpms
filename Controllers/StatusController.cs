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
    public class StatusController : Controller
    {
        public readonly RepositoryStatus _repositoryStatus;
        public StatusController(RepositoryStatus context)
        {
            _repositoryStatus = context;
        }

        public IActionResult Index(int Id)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");
            if (Logins != null)
            {
                MStatus Model = new MStatus();
                if (Id != 0)
                {
                    Model = _repositoryStatus.GetById(Id);
                }

                var AllData = _repositoryStatus.GetAll();

                ViewBag.AllData = AllData;


                return View(Model);
            }
            else
                return Redirect("Login/Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(MStatus Db)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");

            if (ModelState.IsValid)
            {
                Db.CreatedBy = Logins.UserID;
                int ret = await _repositoryStatus.Save(Db);
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
                    ViewBag.message = "Already Exits Status Name";
                }
                var AllData = _repositoryStatus.GetAll();

                ViewBag.AllData = AllData;
                return View("Index", Db);
            }
            else
            {
                return View("Index", Db);
            }
        }
        

    }
}
