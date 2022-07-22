using CPMS_AUTO.Data;
using CPMS_AUTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Repository
{
    public class RepositoryUnitStatus
    {
        private readonly ApplicationDbContext _context;

        public RepositoryUnitStatus(ApplicationDbContext context)
        {
            _context = context;
        }

        public MUnitStatus GetById(int Id)
        {
            var AllData = _context.mUnitStatus.Where(i => i.Id == Id && i.isActive == 1).Single();
            return AllData;
        }

        public List<MUnitStatus> GetAll()
        {
            var AllData = _context.mUnitStatus.Where(i => i.isActive == 1).OrderByDescending(i => i.Id).ToList();

            return AllData;
        }
    }
}
