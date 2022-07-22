using CPMS_AUTO.Data;
using CPMS_AUTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Repository
{
    public class RepositoryVetting
    {
        public readonly ApplicationDbContext _context;
        public RepositoryVetting(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Save(MVetting Db)
        {


            Db.IsActive = 1;
            Db.CreatedOn = DateTime.Now;
            Db.UpdatedOn = DateTime.Now;

            if (!CheckUserExist(Db.Code,Db.Id))
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
        public List<MVetting> GetAll()
        {
            var AllData = _context.MVettings.Where(i => i.IsActive == 1).ToList();


            return AllData;
        }
        public MVetting GetById(int Id)
        {
            var AllData = _context.MVettings.Where(i => i.Id == Id).Single();


            return AllData;
        }
        public bool CheckUserExist(string Code,int Id)
        {

            return _context.MVettings.Any(e => e.Code == Code && e.Id!=Id);

        }
    }
}
