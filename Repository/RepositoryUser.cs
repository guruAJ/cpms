using CPMS_AUTO.Data;
using CPMS_AUTO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Repository
{
    public class RepositoryUser
    {
        public readonly ApplicationDbContext _context;
        public RepositoryUser(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Save(Login Db)
        {
          
           
                Db.IsActive = 1;
                Db.CreatedOn = DateTime.Now;
                Db.UpdatedOn = DateTime.Now;
                Db.CreatedBy = Db.UserID;
                if (!CheckUserExist(Db.UserID))
                {
                   
                        _context.Add(Db);
                        _context.SaveChanges();

                //_context.Update(Db);
                //await _context.SaveChangesAsync();
                //ViewBag.message = "The user " + Db.Name + " is Update successfully";

                return 1;
                }

            else
            {
                //_context.Update(Db);
                //await _context.SaveChangesAsync();
                return 2;
            }



        }
        public bool CheckUserExist(string UserID)
        {
            //  bool UserExist = _context.logins.Where(i => i.UserID == UserID).FirstOrDefault();


            // Model = _context.logins.Where(i => i.UserID == UserID).FirstOrDefault();

            //another codes to check wheather user exists

            return _context.logins.Any(e => e.UserID == UserID);

        }
        public int GetById(string Id)
        {
            var AllData = _context.logins.Where(i => i.UserID == Id).Single();


            return AllData.Id;
        }
        public Login GetByIdAll(string Id)
        {
            var AllData = _context.logins.Where(i => i.UserID == Id).Single();
            return AllData;
        }
        public List<Login> GetAll()
        {
            var AllData = _context.logins.Where(i => i.IsActive == 1 && i.Id !=1).ToList();
            return AllData;
        }
        public List<Login> GetByIdAll(int ComponentId)
        {

            var AllData = _context.logins.Where(i => i.IsActive == 1 && i.ComponentId == ComponentId /*&& i.Id != 1*/).ToList();


            return AllData;
        }

        public List<Login> GetByIdAllForAjax(int ComponentId)
        {
            var result = from MLogin in _context.logins
                         join MRanks in _context.mRank on MLogin.RankId equals MRanks.Id into RDetails
                         from rank in RDetails.DefaultIfEmpty()
                         where MLogin.ComponentId == ComponentId
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

            foreach (var db in result.OrderByDescending(x => x.Id))
            {
                Login login = new Login();
                login.Id = db.Id;
                login.RoleType = db.RoleType;
                login.UserID = db.UserID;
                login.Name = db.Name;
                login.RankName = db.RankName;
                login.RankId = db.RankId;
                login.Password = db.Password;
                lst.Add(login);
            }
            return lst;
            //var AllData = _context.logins.Where(i => i.IsActive == 1 && i.ComponentId == ComponentId /*&& i.Id != 1*/).ToList();


            //return AllData;
        }

        public List<MRank> GetAllRanks()
        {
            var AllData = _context.mRank.Where(i => i.isActive == 1).ToList();

            return AllData;
        }

        public List<Login> GetAllData()
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

            foreach(var db in result.OrderByDescending(x => x.Id))
            {
                Login login = new Login();
                login.Id = db.Id;
                login.RoleType = db.RoleType;
                login.UserID = db.UserID;
                login.Name = db.Name;
                login.RankName = db.RankName;
                login.RankId = db.RankId;
                login.Password = db.Password;
                lst.Add(login);
            }
            return lst;
        }

        public List<Login> GetDataById(string UserId)
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

            foreach (var db in result.OrderByDescending(x => x.Id))
            {
                Login login = new Login();
                login.Id = db.Id;
                login.RoleType = db.RoleType;
                login.UserID = db.UserID;
                login.Name = db.Name;
                login.RankName = db.RankName;
                login.RankId = db.RankId;
                login.Password = db.Password;
                lst.Add(login);
            }
            return lst;
        }

        public Login Validate(Login Db)
        {
            if(!string.IsNullOrEmpty(Db.UserID) && !string.IsNullOrEmpty(Db.Password))
            {
                var AllData = _context.logins.Where(i => i.UserID == Db.UserID && i.Password == Db.Password).Single();
                return AllData;
            }
            else
            {
                return null;
            }
        }
    }
}
