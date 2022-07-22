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
    public class SponsorController : Controller
    {
        public readonly RepositorySponsor _repositorySponsor;
        public SponsorController(RepositorySponsor context)
        {
            _repositorySponsor = context;
        }
       
        public IActionResult Index(int Id)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");
            if (Logins != null)
            {
                MSponsor Model = new MSponsor();
                if (Id != 0)
                {
                    Model = _repositorySponsor.GetById(Id);
                }

                var AllData = _repositorySponsor.GetAll();

                ViewBag.AllData = AllData;


                return View(Model);
            }
            else
                return Redirect("Login/Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(MSponsor Db)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");

            if (ModelState.IsValid)
            {
                Db.CreatedBy = Logins.UserID;
                int ret = await _repositorySponsor.Save(Db);
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
                    ViewBag.message = "Allready Exits Sponsor Name";
                }
                var AllData = _repositorySponsor.GetAll();

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
