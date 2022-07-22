using CPMS_AUTO.Data;
using CPMS_AUTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Repository
{
    public class RepositoryProject
    {
        public readonly ApplicationDbContext _context;
        public RepositoryProject(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Save(MProject Db)
        {


            Db.IsActive = 1;
            Db.CreatedOn = DateTime.Now;
            Db.UpdatedOn = DateTime.Now;

            if (!CheckUserExist(Db.Name,Db.Id))
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
        public List<MProject> GetAll()
        {
            //  var AllData = _context.Projects.Where(i => i.IsActive == 1).ToList();


            var result = from MProject in _context.Projects
                         join MComponent in _context.Components on MProject.ComponentId equals MComponent.Id into CDetails
                         from Component in CDetails.DefaultIfEmpty()
                         join MSponsor in _context.Sponsors on MProject.SponsorId equals MSponsor.Id into SDetails
                         from Sponsor in SDetails.DefaultIfEmpty()
                         join MStatus in _context.Statuss on MProject.StatusCode equals MStatus.Id into Stdetails
                         from Status in Stdetails.DefaultIfEmpty()
                         join MVetting in _context.MVettings on MProject.VettingCode equals MVetting.Id into Mvtdetails
                         from Vetting in Mvtdetails.DefaultIfEmpty()
                         join Login in _context.logins on MProject.ICID equals Login.Id into logindetails
                         from logins in logindetails.DefaultIfEmpty()

                         join MGrant in _context.mGrants on MProject.GrantId equals MGrant.Id into GDetails
                         from Grant in GDetails.DefaultIfEmpty()

                         join MVendor in _context.mVendors on MProject.VendorId equals MVendor.Id into VDetails
                         from Vendor in VDetails.DefaultIfEmpty()

                         select new
                         {
                             Id = MProject.Id,
                             Name = MProject.Name,
                             ComponentName = Component.Name,
                             ComponentId = Component.Id,
                             SponsorId = MProject.SponsorId,
                             SponsorName = Sponsor.Name,
                             ICID = MProject.ICID,
                             Officer = logins.UserID,
                             StatusCode = MProject.StatusCode,
                             StatusName = Status.Name,
                             VettingCode = MProject.VettingCode,
                             VettingName = Vetting.Code,
                             Remarks = MProject.Remarks,
                             CreatedOn = MProject.CreatedOn,
                             UpdatedOn = MProject.UpdatedOn,
                             IsActive = MProject.IsActive,
                             CreatedBy = MProject.CreatedBy,
                             GrantId = Grant.Id,
                             GrantName = Grant.GrantName,
                             VendorId = Vendor.Id,
                             VendorName = Vendor.VendorName,
                             Amount = MProject.Amount
                         };
            
            //select m;


            List < MProject > lst = new List<MProject>();
          
            foreach (var db in result)
            {
                MProject mProject = new MProject();

                mProject.Id = db.Id;
                mProject.Name = db.Name;
                mProject.ComponentName = db.ComponentName;
                mProject.ComponentId = db.ComponentId;
                mProject.SponsorId = db.SponsorId;
                mProject.SponsorName = db.SponsorName;
                mProject.ICID = db.ICID;
                mProject.Officer = db.Officer;
                mProject.StatusName = db.StatusName;
                mProject.StatusCode = db.StatusCode;
                mProject.VettingName = db.VettingName;
                mProject.VettingCode = db.VettingCode;
                mProject.Remarks = db.Remarks;
                mProject.CreatedOn = db.CreatedOn;
                mProject.UpdatedOn = db.UpdatedOn;
                mProject.IsActive = db.IsActive;
                mProject.CreatedBy = db.CreatedBy;
                mProject.GrantName = db.GrantName;
                mProject.GrantId = db.GrantId;
                mProject.VendorId = db.VendorId;
                mProject.VendorName = db.VendorName;
                mProject.Amount = db.Amount;
                lst.Add(mProject);
            }

            return lst;
        }
        public List<MProject> GetAll(string UserId)
        {
            //  var AllData = _context.Projects.Where(i => i.IsActive == 1).ToList();


            var result = from MProject in _context.Projects
                         join MComponent in _context.Components on MProject.ComponentId equals MComponent.Id into CDetails
                         from Component in CDetails.DefaultIfEmpty()
                         join MSponsor in _context.Sponsors on MProject.SponsorId equals MSponsor.Id into SDetails
                         from Sponsor in SDetails.DefaultIfEmpty()
                         join MStatus in _context.Statuss on MProject.StatusCode equals MStatus.Id into Stdetails
                         from Status in Stdetails.DefaultIfEmpty()
                         join MVetting in _context.MVettings on MProject.VettingCode equals MVetting.Id into Mvtdetails
                         from Vetting in Mvtdetails.DefaultIfEmpty()
                         join Login in _context.logins on MProject.ICID equals Login.Id into logindetails
                         from logins in logindetails.DefaultIfEmpty()

                         join MGrant in _context.mGrants on MProject.GrantId equals MGrant.Id into GDetails
                         from Grant in GDetails.DefaultIfEmpty()

                         join MVendor in _context.mVendors on MProject.VendorId equals MVendor.Id into VDetails
                         from Vendor in VDetails.DefaultIfEmpty()

                         where logins.UserID==UserId
                        
                         select new
                         {
                             Id = MProject.Id,
                             Name = MProject.Name,
                             ComponentName = Component.Name,
                             ComponentId = Component.Id,
                             SponsorId = MProject.SponsorId,
                             SponsorName = Sponsor.Name,
                             ICID = MProject.ICID,
                             Officer = logins.UserID,
                             StatusCode = MProject.StatusCode,
                             StatusName = Status.Name,
                             VettingCode = MProject.VettingCode,
                             VettingName = Vetting.Description,
                             Remarks = MProject.Remarks,
                             CreatedOn = MProject.CreatedOn,
                             UpdatedOn = MProject.UpdatedOn,
                             IsActive = MProject.IsActive,
                             CreatedBy = MProject.CreatedBy,
                             GrantId = Grant.Id,
                             GrantName = Grant.GrantName,
                             VendorId = Vendor.Id,
                             VendorName = Vendor.VendorName,
                             Amount = MProject.Amount
                         };

            //select m;


            List<MProject> lst = new List<MProject>();

            foreach (var db in result.OrderByDescending(x => x.CreatedOn))
            {
                MProject mProject = new MProject();


                mProject.Id = db.Id;
                mProject.Name = db.Name;
                mProject.ComponentName = db.ComponentName;
                mProject.ComponentId = db.ComponentId;
                mProject.SponsorId = db.SponsorId;
                mProject.SponsorName = db.SponsorName;
                mProject.ICID = db.ICID;
                mProject.Officer = db.Officer;
                mProject.StatusName = db.StatusName;
                mProject.StatusCode = db.StatusCode;
                mProject.VettingName = db.VettingName;
                mProject.VettingCode = db.VettingCode;
                mProject.Remarks = db.Remarks;
                mProject.CreatedOn = db.CreatedOn;
                mProject.UpdatedOn = db.UpdatedOn;
                mProject.IsActive = db.IsActive;
                mProject.CreatedBy = db.CreatedBy;
                mProject.GrantName = db.GrantName;
                mProject.GrantId = db.GrantId;
                mProject.VendorId = db.VendorId;
                mProject.VendorName = db.VendorName;
                mProject.Amount = db.Amount;
                lst.Add(mProject);
            }

            return lst;
        }
        public MProject GetById(int Id)
        {
            var AllData = _context.Projects.Where(i => i.Id == Id).Single();


            return AllData;
        }
        
        public bool CheckUserExist(string Name,int Id)
        {

            return _context.Projects.Any(e => e.Name == Name && e.Id!=Id);

        }
        
        public MProject GetByName(string name)
        {
            var AllData = _context.Projects.Where(i => i.Name == name).Single();
            return AllData;
        }

        public List<MGrant> GetAllGrants()
        {
            var AllData = _context.mGrants.Where(i => i.isActive == 1).ToList();
            return AllData;
        }

        public List<MVendor> GetAllVendors()
        {
            var AllData = _context.mVendors.Where(i => i.isActive == 1).ToList();
            return AllData;
        }
    }
}
