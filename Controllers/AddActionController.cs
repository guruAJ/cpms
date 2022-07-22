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
    public class AddActionController : Controller
    {
        public readonly RepositoryProject _repositoryProject;

        public AddActionController(RepositoryProject repositoryProject)
        {
            _repositoryProject = repositoryProject;
        }


        //public IActionResult Index(int Id)
        //{
        //    Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");
        //    if (Logins != null)
        //    {
        //        MProject Model = new MProject();
        //        if (Id != 0)
        //        {
        //            Model = _repositoryProject.GetById(Id);
        //            TempData["Id"] = Model.Id;

        //            ViewBag.dnone = false;
        //        }
        //        else
        //        {
        //            TempData["Id"] = 0;
        //            ViewBag.dnone = true;


        //        }
        //        GetDropDown();
        //        return View(Model);
        //    }
        //    else
        //        return Redirect("/Login/Index");
        //}

        //public void GetDropDown()
        //{
        //    var Projects = _repositoryProject.GetAll();

        //    Projects.Insert(0, new MProject { Id = 0, Name = "--Select Project--" });
        //    ViewBag.Projects = Projects;
        //}

        //public JsonResult GetDetails(string name)
        //{
        //    var Projects = _repositoryProject.GetByName(name);

        //    return Json(Projects);
        //}
    }
}
