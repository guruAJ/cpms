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
    public class VettingController : Controller
    {
        public readonly RepositoryVetting _repositoryVetting;
        public VettingController(RepositoryVetting context)
        {
            _repositoryVetting = context;
        }

        public IActionResult Index(int Id)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");
            if (Logins != null)
            {
                MVetting Model = new MVetting();
                if (Id != 0)
                {
                    Model = _repositoryVetting.GetById(Id);
                }

                var AllData = _repositoryVetting.GetAll();

                ViewBag.AllData = AllData;


                return View(Model);
            }
            else
            {
                return Redirect("Login/Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(MVetting Db)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");

            if (ModelState.IsValid)
            {
                Db.CreatedBy = Logins.UserID;
                int ret = await _repositoryVetting.Save(Db);
                if (ret == 1)
                {
                    ViewBag.message = "" + Db.Code + " is saved successfully";

                }
                else if (ret == 2)
                {
                    ViewBag.message = "" + Db.Code + " is Update successfully";

                }
                else if (ret == 3)
                {
                    ViewBag.message = "Allready Exits Status Code";
                }
                var AllData = _repositoryVetting.GetAll();

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
