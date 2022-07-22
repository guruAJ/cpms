using CPMS_AUTO.Data;
using CPMS_AUTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Repository
{
    public class RepositorySponsor
    {
        public readonly ApplicationDbContext _context;
        public RepositorySponsor(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Save(MSponsor Db)
        {


            Db.IsActive = 1;
            Db.CreatedOn = DateTime.Now;
            Db.UpdatedOn = DateTime.Now;

            if (!CheckUserExist(Db.Name))
            {
                if (Db.Id == 0)
                {
                    _context.Add(Db);
                    _context.SaveChanges();
                    return 1;
                    // ViewBag.message = "" + Db.Name + " is saved successfully";
                }
                else
                {
                    _context.Update(Db);
                    await _context.SaveChangesAsync();
                    //   ViewBag.message = "" + Db.Name + " is Update successfully";
                    return 2;
                }

            }
            else
            {
                return 3;
                //@ViewBag.message = "Allready Exits Component Name";
            }
            // var AllData = _context.Components.Where(i => i.IsActive == 1).ToList();

            //ViewBag.AllData = AllData;
            // return View("Index", Db);


        }
        public List<MSponsor> GetAll()
        {
            var AllData = _context.Sponsors.Where(i => i.IsActive == 1).ToList();


            return AllData;
        }
        public MSponsor GetById(int Id)
        {
            var AllData = _context.Sponsors.Where(i => i.Id == Id).Single();


            return AllData;
        }
        public bool CheckUserExist(string Name)
        {
            //  bool UserExist = _context.logins.Where(i => i.UserID == UserID).FirstOrDefault();


            // Model = _context.logins.Where(i => i.UserID == UserID).FirstOrDefault();

            //another codes to check wheather user exists

            return _context.Sponsors.Any(e => e.Name == Name);

        }
    }
}
