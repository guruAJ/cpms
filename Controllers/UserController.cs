using CPMS_AUTO.Data;
using CPMS_AUTO.Helpers;
using CPMS_AUTO.Models;
using CPMS_AUTO.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Controllers
{
    public class UserController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly ComponentRepository _componentRepository;
        public readonly RepositoryUser _repositoryUser;

        public UserController(ApplicationDbContext context, ComponentRepository componentRepository, RepositoryUser repositoryUser)
        {
            _context = context;
            _componentRepository = componentRepository;
            _repositoryUser = repositoryUser;
        }

        public IActionResult Index(int Id)
        {
            Login Model = new Login();
            if (Id != 0)
            {
                Model = _context.logins.Where(i => i.Id == Id).Single();
            }

            var AllUser = _context.logins.Where(i => i.IsActive == 1).ToList();
            ViewBag.Name = AllUser;

            var AllData = _repositoryUser.GetAllData();
            ViewBag.MyData = AllData;
            GetDropDown();

            return View(Model);

        }
        public IActionResult List()
        {
            Login model = new Login();
            return View(model);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Login Db)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");

            if (ModelState.IsValid)
            {
                Db.IsActive = 1;
                Db.CreatedOn = DateTime.Now;
                Db.UpdatedOn = DateTime.Now;
                Db.CreatedBy = Db.UserID;
                if (!CheckUserExist(Db.UserID))
                {
                    if (Db.Id == 0)
                    {
                        _context.Add(Db);
                        _context.SaveChanges();
                        ViewBag.message = "The user " + Db.Name + " is saved successfully";
                    }
                    else
                    {
                        _context.Update(Db);
                        await _context.SaveChangesAsync();
                        ViewBag.message = "The user " + Db.Name + " is Update successfully";
                    }

                }
                else
                {
                    @ViewBag.message = "Already Exits UserID";
                }
                var AllUser1 = _context.logins.Where(i => i.IsActive == 1).ToList();
                ViewBag.Name = AllUser1;

                var AllData1 = _repositoryUser.GetAllData();
                ViewBag.MyData = AllData1;

                GetDropDown();
                return View("Index", Db);

            }
            else
            {
                GetDropDown();
                return View("Index", Db);
            }
        }
       
        public async Task<IActionResult> Delete(Login Db)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");

               Db= _context.logins.Where(i => i.Id == Db.Id).Single();

                Db.IsActive = 0;
                Db.UpdatedOn = DateTime.Now;
               
                    _context.Update(Db);
                    await _context.SaveChangesAsync();
                    ViewBag.messageDelete = "The user " + Db.Name + " is Delete successfully";
               
                var AllUser1 = _context.logins.Where(i => i.Id == Db.Id).ToList();

                ViewBag.Name = AllUser1;
            return RedirectToAction("Index");



        }


        public bool CheckUserExist(string UserID)
        {
            return _context.logins.Any(e => e.UserID == UserID);
        }

        public void GetDropDown()
        {
            var Component = _componentRepository.GetAll();
            Component.Insert(0, new MComponent { Id = 0, Name = "--Select Component Code--" });
            ViewBag.AllComponent = Component;

            var Ranks = _repositoryUser.GetAllRanks();
            Ranks.Insert(0, new MRank { Id = 0, RankName = "--Select Rank--" });
            ViewBag.AllRanks = Ranks;
        }
    }
}
