using CPMS_AUTO.Data;
using CPMS_AUTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Repository
{
    public class RepositoryStatus
    {
        public readonly ApplicationDbContext _context;
        public RepositoryStatus(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Save(MStatus Db)
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
        public List<MStatus> GetAll()
        {
            var AllData = _context.Statuss.Where(i => i.IsActive == 1).ToList();


            return AllData;
        }
        public MStatus GetById(int Id)
        {
            var AllData = _context.Statuss.Where(i => i.Id == Id).Single();


            return AllData;
        }
        public bool CheckUserExist(string Name)
        {
          
            return _context.Statuss.Any(e => e.Name == Name);

        }
    }
}
