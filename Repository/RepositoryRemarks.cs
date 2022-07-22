using CPMS_AUTO.Data;
using CPMS_AUTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Repository
{
    public class RepositoryRemarks
    {
        public readonly ApplicationDbContext _context;
        public RepositoryRemarks(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Save(MRemarks Db)
        {
            Db.IsActive = 1;
            Db.CreatedOn = DateTime.Now;
            Db.UpdatedOn = DateTime.Now;

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
        
        public List<MRemarks> GetById(int Id)
        {
            var AllData = _context.remarks.Where(i => i.Id == Id && i.IsActive==1).ToList();

            return AllData;
        }
        public List<MRemarks> GetByProjectId(int ProjectId)
        {
            var AllData = _context.remarks.Where(i => i.ProjectId == ProjectId && i.IsActive == 1).OrderByDescending(x=> x.CreatedOn).ToList();

            return AllData;
        }

    }
}
