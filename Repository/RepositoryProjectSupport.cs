using CPMS_AUTO.Data;
using CPMS_AUTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Repository
{
    public class RepositoryProjectSupport
    {
        public readonly ApplicationDbContext _context;
        public RepositoryProjectSupport(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Save(MProjectSupport Db)
        {
            Db.IsActive = 1;
            Db.CreatedOn = DateTime.Now;
            Db.UpdatedOn = DateTime.Now;

            if (!CheckUserExist(Db.Project, Db.Id))
            {
                if(Db.Project == null)
                {
                    _context.Add(Db);
                    _context.SaveChanges();
                    return 1;
                }
                else
                {
                    _context.Update(Db);
                    await _context.SaveChangesAsync();
                    return 2;
                }
            }
            else
            {
                _context.Update(Db);
                await _context.SaveChangesAsync();
                return 3;
            }
        }
        public bool CheckUserExist(string Project, int Id)
        {
            return _context.support.Any(e => e.Project == Project && e.Id != Id);
        }
        public bool CheckUserExist1(string SUS_No, int Id)
        {
            return _context.support.Any(e => e.SUS_No == SUS_No && e.Id != Id);
        }

        public MProjectSupport GetById(int Id)
        {
            var AllData = _context.support.Where(i => i.Id == Id).Single();
            return AllData;
        }

        public List<MProject> GetProjectName()
        {
            var AllData = _context.Projects.Where(i => i.IsActive == 1).ToList();
            return AllData;
        }

        //public List<MProjectSupport> GetDataBySus(string sus,string project)
        //{
        //    var details = _context.support.Where(x => x.SUS_No == sus && x.Project == project).ToList();
        //    return details;
        //}
        public List<MProjectSupport> GetDataBySus(string project)
        {
            var details = _context.support.Where(x => x.Project == project).OrderByDescending(i=> i.Id).ToList();
            return details;
        }

        public MProjectSupport GetDataById(int Id)
        {
            var details = _context.support.Where(x => x.Id == Id).Single();
            return details;
        }

        public async Task<int> SaveModalData(MProjectSupport Db)
        {
            Db.IsActive = 1;
            Db.CreatedOn = DateTime.Now;
            Db.UpdatedOn = DateTime.Now;

            if (CheckUserExist1(Db.SUS_No, Db.Id))
            {
                if (Db.Id == 0)
                {
                     Db.Status = "Open";
                    _context.Add(Db);
                    _context.SaveChanges();
                    return 1;
                    GetProjectSupport();
                }
                else
                {
                    _context.Update(Db);
                    await _context.SaveChangesAsync();
                    return 2;
                }
            }
            else
            {
                _context.Update(Db);
                await _context.SaveChangesAsync();
                return 2;
            }

        }

        public int GetClosedCount()
        {
            var closeddata = (from MProjectSupport in _context.support
                       where MProjectSupport.Status == "Closed"
                       select MProjectSupport).Count();
            return closeddata;
        }

        public int GetOpenCount()
        {
            var opendata = (from MProjectSupport in _context.support
                        where MProjectSupport.Status == "Open"
                        select MProjectSupport).Count();
            return opendata;
        }

        public int GetOnWrkCount()
        {
            var onwrkdata = (from MProjectSupport in _context.support
                        where MProjectSupport.Status == "Assisted & user on wkg"
                        select MProjectSupport).Count();
            return onwrkdata;
        }

        public int GetTotalCount()
        {
            var totaldata = (from MProjectSupport in _context.support
                        select MProjectSupport.SUS_No).Distinct().Count();
            return totaldata;
        }

        public int GetStatusOpen()
        {
            var ProjectOpenStatus = _context.support.Any(s => (s.Project == "1") && (s.Status == "Closed"));
            return 1;
        }

        public int HighestPriorityProject()
        {
            var hpp = (from MProjectSupport in _context.support
                       where MProjectSupport.Cpl_Category == "Not Functioning"
                       select MProjectSupport).Count();
            return hpp;
        }

        public int LowestPriorityProject()
        {
            var lpp = (from MProjectSupport in _context.support
                       where MProjectSupport.Cpl_Category == "Full Functional"
                       select MProjectSupport).Count();
            return lpp;
        }

        public async Task<int> SUBMITSUSUNIT(MSUSNo Db)
        {
            Db.IsActive = 1;
            Db.CreatedOn = DateTime.Now;
            Db.UpdatedOn = DateTime.Now;

            if (!CheckUserExist2(Db.SUS, Db.Id))
            {
                if (Db.Id == 0)
                {
                    _context.Add(Db);
                    _context.SaveChanges();
                    return 1;

                }
                else
                {
                    _context.Update(Db);
                    await _context.SaveChangesAsync();
                    return 2;
                }
            }
            else
            {
                return 3;
            }
        }

        public bool CheckUserExist2(string SUS, int Id)
        {
            return _context.mSUs.Any(e => e.SUS == SUS && e.Id != Id);
        }

        //Status
        public string SubmitStatus(string status,int id)
        {
            MProjectSupport model = new MProjectSupport();
            var details = _context.support.FirstOrDefault(x => x.Id == id);
            if (details != null)
            {
                //model.Status = details.Status;
                details.Status = status;   
                _context.SaveChangesAsync();
            }
            return details.Status;
        }
        public List<MProjectSupport> GetProjectSupport()
        {
            var AllData = _context.support.Where(i => i.IsActive == 1).ToList();
            return AllData;
        }

    }
}
