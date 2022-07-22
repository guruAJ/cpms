using CPMS_AUTO.Data;
using CPMS_AUTO.Helpers;
using CPMS_AUTO.Models;
using CPMS_AUTO.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Controllers
{
    public class ComponentController : Controller
    {
        public readonly ComponentRepository _componentRepository;

        public ComponentController(ComponentRepository context)
        {
            _componentRepository = context;
        }
        public IActionResult Index(int Id)
        {
            MComponent Model = new MComponent();
            if (Id != 0)
            {
                Model = _componentRepository.GetById(Id);
            }

            var AllData = _componentRepository.GetAll();

            ViewBag.AllData = AllData;

            GetDropDown();
            
            return View(Model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(MComponent Db)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");

            if (ModelState.IsValid)
            {
                Db.CreatedBy = Logins.Id;
                int ret = await _componentRepository.Save(Db);
                if(ret==1)
                {
                    ViewBag.message = "" + Db.Name + " is saved successfully";
                }
                else if (ret == 2)
                {
                    ViewBag.message = "" + Db.Name + " is Update successfully";
                }
                else if (ret == 3)
                {
                    ViewBag.message = "Already Exits Component Name";
                }
                var AllData = _componentRepository.GetAll();

                ViewBag.AllData = AllData;
                GetDropDown();
                return View("Index", Db);
            }
            else
            {
                GetDropDown();
                return View("Index", Db);
            }
        }

        public void GetDropDown()
        {
            var ComponentData = _componentRepository.GetLoginData();
            ComponentData.Insert(0, new Login { Id = 0, UserID = "--Select ICID--" });
            ViewBag.ComponentData = ComponentData;
        }
    }
}
