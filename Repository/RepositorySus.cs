using CPMS_AUTO.Data;
using CPMS_AUTO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Repository
{
    public class RepositorySus
    {
        public readonly ApplicationDbContext _context;
        public RepositorySus(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Save(MSUSNo Db)
        {
            //Db.IsActive = 1;
            //Db.CreatedOn = DateTime.Now;

            if(CheckUserExists(Db.SUS, Db.Id))
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
                _context.Add(Db);
                _context.SaveChanges();
                return 3;
            }
        }

        public bool CheckUserExists(string SusNo, int id)
        {
            return _context.mSUs.Any(e => e.SUS == SusNo && e.Id != id);
        }

        public string CheckSus(string susdata)
        {
            var aaa = _context.mSUs.Any(e => e.SUS == susdata);

            if(aaa == true)
            {
                return "True";
            }
            else
            {
                return "False";
            }
        }
    }
}
