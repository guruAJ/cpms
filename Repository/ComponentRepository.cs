using CPMS_AUTO.Data;
using CPMS_AUTO.Helpers;
using CPMS_AUTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Repository
{
    public class ComponentRepository
    {
        public readonly ApplicationDbContext _context;

        public ComponentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Save(MComponent Db)
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
        public List<MComponent> GetAll()
        {
            var AllData = _context.Components.Where(i => i.IsActive == 1).ToList();


            return AllData;
        }
        public List<MComponent> GetUser(int Id)
        {
            var AllData = _context.Components.Where(i => i.IsActive == 1 && i.Id==Id).ToList();


            return AllData;
        }
        public MComponent GetById(int Id)
        {
            var AllData = _context.Components.Where(i => i.Id == Id).Single();


            return AllData;
        }
        public bool CheckUserExist(string Name)
        {
            //  bool UserExist = _context.logins.Where(i => i.UserID == UserID).FirstOrDefault();


            // Model = _context.logins.Where(i => i.UserID == UserID).FirstOrDefault();

            //another codes to check wheather user exists

            return _context.Components.Any(e => e.Name == Name);

        }

        public List<Login> GetLoginData()
        {
            var result = from MLogin in _context.logins
                         join MRanks in _context.mRank on MLogin.RankId equals MRanks.Id into RDetails
                         from rank in RDetails.DefaultIfEmpty()
                         select new
                         {
                             Id = MLogin.Id,
                             RoleType = MLogin.RoleType,
                             UserID = MLogin.UserID,
                             Name = MLogin.Name,
                             RankName = rank.RankName,
                             RankId = rank.Id,
                             Password = MLogin.Password,
                         };

            List<Login> lst = new List<Login>();

            foreach(var item in result)
            {
                Login login = new Login();

                login.Id = item.Id;
                login.UserID =  item.RankName + " "+item.UserID + "";

                lst.Add(login);
                
            }
            return lst;
        }

        public List<Login> GetLoginDataById(string UserId)
        {
            var result = from MLogin in _context.logins
                         join MRanks in _context.mRank on MLogin.RankId equals MRanks.Id into RDetails
                         from rank in RDetails.DefaultIfEmpty()
                         where MLogin.UserID == UserId
                         select new
                         {
                             Id = MLogin.Id,
                             RoleType = MLogin.RoleType,
                             UserID = MLogin.UserID,
                             Name = MLogin.Name,
                             RankName = rank.RankName,
                             RankId = rank.Id,
                             Password = MLogin.Password,
                         };

            List<Login> lst = new List<Login>();

            foreach (var item in result)
            {
                Login login = new Login();

                login.Id = item.Id;
                login.UserID = item.RankName + " " + item.UserID + "";

                lst.Add(login);

            }
            return lst;
        }
    }
}
