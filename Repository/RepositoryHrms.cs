using CPMS_AUTO.Data;
using CPMS_AUTO.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPMS_AUTO.Repository
{
    public class RepositoryHrms
    {
        private readonly ApplicationDbContext _context;

        public RepositoryHrms(ApplicationDbContext context)
        {
            _context = context;
        }

        public MHrms GetById(int Id)
        {
            var AllData = _context.mHrms.Where(i => i.Id == Id && i.IsActive == 1).Single();
            return AllData;
        }

        public List<MHrms> GetAll()
        {
            var AllData = _context.mHrms.Where(i => i.IsActive == 1).OrderByDescending(i => i.Id).ToList();

            return AllData;
        }

        public List<MHrms> GetAllbyProj(string Project)
        {
            var AllData = _context.mHrms.Where(i => i.IsActive == 1 && i.Project == Project).OrderByDescending(i => i.Id).ToList();
            //var AllData = _context.mHrms.Where(i => i.Project == Project).OrderByDescending(i => i.Id).ToList();

            return AllData;
        }

        public async Task<int> Save(MHrms Db)
        {
            Db.IsActive = 1;
            Db.CreatedOn = DateTime.Now;
            Db.UpdatedOn = DateTime.Now;

            if (!CheckUserExist(Db.Id))
            {
                if (Db.Id == 0)
                {
                    Db.Status = "Open";
                    _context.Add(Db);
                    _context.SaveChanges();
                    return 1;
                }
                else
                {
                    Db.Status = "Open";
                    _context.Update(Db);
                    await _context.SaveChangesAsync();
                    return 2;
                }
            }
            else
            {
                Db.Status = "Open";
                _context.Update(Db);
                _context.SaveChanges();
                return 2;
            }
        }




        public async Task<int> UpdateStatus(MHrms Db)
        {
            Db.IsActive = 1;


            Db.UpdatedOn = DateTime.Now;

            if (!CheckUserExist(Db.Id))
            {
                if (Db.Id == 0)
                {
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
                _context.SaveChanges();
                return 2;
            }
        }



        public bool CheckUserExist(int Id)
        {
            return _context.mHrms.Any(e => e.Id == Id);
        }

        public MHrms GetDataById(int Id)
        {
            var details = _context.mHrms.Where(i => i.Id == Id && i.IsActive == 1).Single();
            return details;
        }

        public MSUSNo VerifysusUnit(string sus, string units)
        {
            dynamic details = "";
            if (sus != null && sus != "")
                details = _context.mSUs.Where(i => i.SUS == sus).FirstOrDefault();
            else if (units != null && units != "")
                details = _context.mSUs.Where(i => i.Unit == units).FirstOrDefault();

            if (details != null)
            {
                return details;
            }
            else
            {
                return null;
            }

        }

        public List<MHrms> GetData(string sus, string unit, string project)
        {
            dynamic details = "";
            if (project == "1")
            {
                if (sus != null && sus != "")
                {
                    details = _context.mHrms.Where(i => i.Sus_No == sus && i.Project == "HRMS").OrderByDescending(i => i.Id).ToList();
                }
                else if (unit != null && unit != "")
                {
                    details = _context.mHrms.Where(i => i.Unit == unit && i.Project == "HRMS").OrderByDescending(i => i.Id).ToList();
                }
            }
            else if (project == "2")
            {
                if (sus != null && sus != "")
                {
                    details = _context.mHrms.Where(i => i.Sus_No == sus && i.Project == "IQMP").OrderByDescending(i => i.Id).ToList();
                }
                else if (unit != null && unit != "")
                {
                    details = _context.mHrms.Where(i => i.Unit == unit && i.Project == "IQMP").OrderByDescending(i => i.Id).ToList();
                }
            }


            if (details != null)
            {
                return details;
            }
            else
            {
                return null;
            }
        }

        public List<MHrms> GetDataByDate(string project, DateTime dateTo, DateTime dateFrom, string unit)
        {
            dynamic details = "";
            //dateTo = dateTo.AddDays(1);
            dateFrom = dateFrom.AddDays(1);
            if (project == "1")
            {
                if (unit == null)
                {
                    details = _context.mHrms.Where(x => x.QueryDate >= dateTo && x.QueryDate <= dateFrom && x.Project == "HRMS" /*&& x.Unit == unit*/).ToList();
                }
                else
                {
                    details = _context.mHrms.Where(x => x.QueryDate >= dateTo && x.QueryDate <= dateFrom && x.Project == "HRMS" && x.Unit == unit).ToList();
                }
                //details = _context.mHrms.Where(x => x.QueryDate >= dateTo && x.QueryDate <= dateFrom && x.Project == "HRMS" && x.Unit == unit).ToList();

            }
            else if (project == "2")
            {
                if (unit == null)
                {
                    details = _context.mHrms.Where(x => x.QueryDate >= dateTo && x.QueryDate <= dateFrom && x.Project == "IQMP" /*&& x.Unit == unit*/).ToList();
                }
                else
                {
                    details = _context.mHrms.Where(x => x.QueryDate >= dateTo && x.QueryDate <= dateFrom && x.Project == "IQMP" && x.Unit == unit).ToList();
                }
                //details = _context.mHrms.Where(x => x.QueryDate >= dateTo && x.QueryDate <= dateFrom && x.Project == "IQMP" && x.Unit == unit).ToList();

            }
            if (details != null)
            {
                return details;
            }
            else
            {
                return null;
            }
        }




        ///Start
        ///Auth :- Ajit Pandey
        ///Purpose :- Get counts of complaints according to unit
        ///Dated :- 07/07/2022
        ///Remarks :- Completely Done
        ///Reviewed by :- 
        ///Reviewed Date :-
        ///Tested By :- 
        ///Tested Date :-
        public List<UnitCountAndStatus> GetUnitCountHrms(string project, DateTime ToDate, DateTime DateFrom)
        {
            dynamic result = "";
            DateFrom = DateFrom.AddDays(1);

            var result4 = (from msusobj in _context.mSUs
                           join mhrmsobj in _context.mHrms on msusobj.Unit equals mhrmsobj.Unit
                           where mhrmsobj.QueryDate >= ToDate && mhrmsobj.QueryDate <= DateFrom && mhrmsobj.Project == project

                           group new { msusobj, mhrmsobj } by new
                           {
                               msusobj.Unit,
                               msusobj.Hrms,
                               //msusobj.Iqmp

                           } into grp

                           select new
                           {
                               Unit = grp.Key.Unit,
                               Hrms = grp.Key.Hrms,
                               //Iqmp = grp.Key.Iqmp,
                               Count = grp.Count()
                           }).ToList();


            List<UnitCountAndStatus> msuslst1 = new List<UnitCountAndStatus>();
            foreach (var db in result4)
            {
                UnitCountAndStatus Mdata = new UnitCountAndStatus();

                //  Mdata.Value = db.Id;
                Mdata.Unit = db.Unit;
                Mdata.Hrms = db.Hrms;
                //Mdata.Iqmp = db.Iqmp;
                Mdata.Count = db.Count;
                msuslst1.Add(Mdata);
            }

            return msuslst1;
        }
        ///End

        public List<UnitCountAndStatus1> GetUnitCountIqmp(string project, DateTime ToDate, DateTime DateFrom)
        {
            dynamic result = "";
            DateFrom = DateFrom.AddDays(1);

            var result4 = (from msusobj in _context.mSUs
                           join mhrmsobj in _context.mHrms on msusobj.Unit equals mhrmsobj.Unit
                           where mhrmsobj.QueryDate >= ToDate && mhrmsobj.QueryDate <= DateFrom && mhrmsobj.Project == project

                           group new { msusobj, mhrmsobj } by new
                           {
                               msusobj.Unit,
                               //msusobj.Hrms,
                               msusobj.Iqmp

                           } into grp

                           select new
                           {
                               Unit = grp.Key.Unit,
                               //Hrms = grp.Key.Hrms,
                               Iqmp = grp.Key.Iqmp,
                               Count = grp.Count()
                           }).ToList();


            List<UnitCountAndStatus1> msuslst1 = new List<UnitCountAndStatus1>();
            foreach (var db in result4)
            {
                UnitCountAndStatus1 Mdata = new UnitCountAndStatus1();

                //  Mdata.Value = db.Id;
                Mdata.Unit = db.Unit;
                //Mdata.Hrms = db.Hrms;
                Mdata.Iqmp = db.Iqmp;
                Mdata.Count = db.Count;
                msuslst1.Add(Mdata);
            }

            return msuslst1;
        }


        public class UnitCountAndStatus
        {
            public string Unit { get; set; }
            public int Hrms { get; set; }
            //public int Iqmp { get; set; }
            public int Count { get; set; }
        }

        public class UnitCountAndStatus1
        {
            public string Unit { get; set; }
            //public int Hrms { get; set; }
            public int Iqmp { get; set; }
            public int Count { get; set; }
        }
        public List<MHrms> GetUnitCount(string unit, string project)
        {
            var result = from m in _context.mHrms
                         select m;

            //if (!String.IsNullOrEmpty(unit))
            //{
            //    result = result.Where(s => s.Unit.Contains(unit));
            //}

            if (!String.IsNullOrEmpty(unit))
            {
                result = _context.mHrms.Where(s => s.Unit == unit && s.Project == project);
            }
            return result.ToList();
        }



        //Validate SUS And Unit Code That is Used For First Time
        public MSUSNo VerifyMYsusUnit(string sus, string units)
        {
            var details = _context.mSUs.Where(i => i.SUS == sus || i.Unit == units).FirstOrDefault();
            if (details != null)
            {
                return details;
            }
            else
            {
                return null;
            }

        }

        public string CheckSus(string susdata)
        {
            var aaa = _context.mSUs.Any(e => e.SUS == susdata);

            if (aaa == true)
            {
                return "True";
            }
            else
            {
                return "False";
            }
        }

        public List<MHrms> GetSearchData(string project)
        {
            var details = _context.mHrms.Where(x => x.Project == project).OrderByDescending(i => i.Id).ToList();
            return details;
        }

        //Status
        public string SubmitStatus(string status, int id)
        {
            MHrms model = new MHrms();
            var details = _context.mHrms.FirstOrDefault(x => x.Id == id);
            if (details != null)
            {
                //model.Status = details.Status;
                details.Status = status;
                _context.SaveChangesAsync();
            }
            return details.Status;
        }

        public List<MSUSNo> GetAllSus()
        {
            var AllData = _context.mSUs.Where(e => e.IsActive == 1).ToList();
            return AllData;
        }

        public List<MSUSNo> GetAllUnit()
        {
            var AllData = _context.mSUs.Where(e => e.IsActive == 1).ToList();
            return AllData;
        }

        //public List<MSUSNo> GETSUS(string sus)
        //{
        //    return _context.mSUs
        //                  .Where(p => p.SUS.Contains(sus))
        //                  .ToList();
        //}

        public class AutoVal
        {
            public string label { get; set; }

            // public int Value { get; set; }
        }
        public List<AutoVal> GetSUSAUTO(string prefix)
        {
            var data = (from susobject in _context.mSUs
                        where susobject.SUS.StartsWith(prefix)
                        select new
                        {
                            SUS = susobject.SUS,
                            Id = susobject.Id
                        }).ToList();

            List<AutoVal> msuslst = new List<AutoVal>();
            foreach (var db in data)
            {
                AutoVal Mdata = new AutoVal();

                //  Mdata.Value = db.Id;
                Mdata.label = db.SUS;
                msuslst.Add(Mdata);
            }

            return msuslst;
        }



        public List<AutoVal> GetUnitAUTO(string prefix)
        {
            var data = (from susobject in _context.mSUs
                        where susobject.Unit.StartsWith(prefix)
                        select new
                        {
                            SUS = susobject.Unit,
                            Id = susobject.Id
                        }).ToList();

            List<AutoVal> msuslst = new List<AutoVal>();
            foreach (var db in data)
            {
                AutoVal Mdata = new AutoVal();

                //  Mdata.Value = db.Id;
                Mdata.label = db.SUS;
                msuslst.Add(Mdata);
            }

            return msuslst;
        }

        public int GetTotalCountForHrms()
        {
            var totalcount = (from hrmsobj in _context.mSUs
                              where hrmsobj.IsActive == 1
                              select hrmsobj).Count();
            return totalcount;
        }

        public int GetTotalCountForIqmp()
        {
            var totalcount = (from hrmsobj in _context.mSUs
                              where hrmsobj.IsActive == 1
                              select hrmsobj).Count();
            return totalcount;
        }

        public int GetTotalCountSummary()
        {
            var totalcount = (from hrmsobj in _context.mHrms
                              where hrmsobj.IsActive == 1
                              select hrmsobj).Count();
            return totalcount;
        }

        public int GetTotalCountSummaryHrms()
        {
            var totalcount = (from hrmsobj in _context.mHrms
                              where hrmsobj.Project == "HRMS" && hrmsobj.IsActive == 1
                              select hrmsobj).Count();
            return totalcount;
        }

        public int GetTotalCountSummaryIqmp()
        {
            var totalcount = (from hrmsobj in _context.mHrms
                              where hrmsobj.Project == "IQMP" && hrmsobj.IsActive == 1
                              select hrmsobj).Count();
            return totalcount;
        }

        public int GetClosedCount()
        {
            var closedcount = (from hrmsobj in _context.mHrms
                               where hrmsobj.Status == "Closed"
                               select hrmsobj).Count();
            return closedcount;
        }

        public int GetClosedCountHrms()
        {
            var closedcount = (from hrmsobj in _context.mHrms
                               where hrmsobj.Status == "Closed" && hrmsobj.Project == "HRMS"
                               select hrmsobj).Count();
            return closedcount;
        }

        public int GetClosedCountIqmp()
        {
            var closedcount = (from hrmsobj in _context.mHrms
                               where hrmsobj.Status == "Closed" && hrmsobj.Project == "IQMP"
                               select hrmsobj).Count();
            return closedcount;
        }

        public int GetOpenCount()
        {
            var opencount = (from hrmsobj in _context.mHrms
                             where hrmsobj.Status == "Open"
                             select hrmsobj).Count();
            return opencount;
        }
        public int GetOpenCountHrms()
        {
            var opencount = (from hrmsobj in _context.mHrms
                             where hrmsobj.Status == "Open" && hrmsobj.Project == "HRMS"
                             select hrmsobj).Count();
            return opencount;
        }

        public int GetOpenCountIqmp()
        {
            var opencount = (from hrmsobj in _context.mHrms
                             where hrmsobj.Status == "Open" && hrmsobj.Project == "IQMP"
                             select hrmsobj).Count();
            return opencount;
        }

        public int GetOpenCountForHrms()
        {
            var opencount = (from hrmsobj in _context.mHrms
                             where hrmsobj.Status == "Open" && hrmsobj.Project == "hrms"
                             select hrmsobj).Count();
            return opencount;
        }

        public int GetOpenCountForIqmp()
        {
            var opencount = (from hrmsobj in _context.mHrms
                             where hrmsobj.Status == "Open" && hrmsobj.Project == "iqmp"
                             select hrmsobj).Count();
            return opencount;
        }

        public int GetWeekOpenCount()
        {
            DateTime todaysss = DateTime.Now;

            DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek), // prev sunday 00:00
            endDate = startDate.AddDays(-7); // next sunday 00:00


            var weekcount = (from hrmsobj in _context.mHrms
                             where hrmsobj.Status == "Open" && (hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate)
                             select hrmsobj).ToList().Count();

            return weekcount;
        }

        public int GetWeekOpenCountHrms()
        {
            DateTime todaysss = DateTime.Now;

            DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek), // prev sunday 00:00
            endDate = startDate.AddDays(-7); // next sunday 00:00


            var weekcount = (from hrmsobj in _context.mHrms
                             where hrmsobj.Status == "Open" && hrmsobj.Project == "HRMS" && (hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate)
                             select hrmsobj).ToList().Count();

            return weekcount;
        }

        public int GetWeekOpenCountIqmp()
        {
            DateTime todaysss = DateTime.Now;

            DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek), // prev sunday 00:00
            endDate = startDate.AddDays(-7); // next sunday 00:00


            var weekcount = (from hrmsobj in _context.mHrms
                             where hrmsobj.Status == "Open" && hrmsobj.Project == "IQMP" && (hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate)
                             select hrmsobj).ToList().Count();

            return weekcount;
        }

        public int GetWeekCloseCount()
        {
            DateTime todaysss = DateTime.Now;

            DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek), // prev sunday 00:00
            endDate = startDate.AddDays(-7); // next sunday 00:00


            var weekcount = (from hrmsobj in _context.mHrms
                             where hrmsobj.Status == "Closed" && (hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate)
                             select hrmsobj).ToList().Count();

            return weekcount;
        }

        public int GetWeekCloseCountHrms()
        {
            DateTime todaysss = DateTime.Now;

            DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek), // prev sunday 00:00
            endDate = startDate.AddDays(-7); // next sunday 00:00


            var weekcount = (from hrmsobj in _context.mHrms
                             where hrmsobj.Status == "Closed" && hrmsobj.Project == "HRMS" && (hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate)
                             select hrmsobj).ToList().Count();

            return weekcount;
        }

        public int GetWeekCloseCountIqmp()
        {
            DateTime todaysss = DateTime.Now;

            DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek), // prev sunday 00:00
            endDate = startDate.AddDays(-7); // next sunday 00:00


            var weekcount = (from hrmsobj in _context.mHrms
                             where hrmsobj.Status == "Closed" && hrmsobj.Project == "IQMP" && (hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate)
                             select hrmsobj).ToList().Count();

            return weekcount;
        }

        public int GetTotalWeeklyCases()
        {
            DateTime todaysss = DateTime.Now;

            DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek), // prev sunday 00:00
            endDate = startDate.AddDays(-7); // next sunday 00:00


            var weekcount = (from hrmsobj in _context.mHrms
                             where (hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate)
                             select hrmsobj).ToList().Count();

            return weekcount;
        }

        public int GetTotalWeeklyCasesHrms()
        {
            DateTime todaysss = DateTime.Now;

            DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek), // prev sunday 00:00
            endDate = startDate.AddDays(-7); // next sunday 00:00


            var weekcount = (from hrmsobj in _context.mHrms
                             where (hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate && hrmsobj.Project == "HRMS")
                             select hrmsobj).ToList().Count();

            return weekcount;
        }

        public int GetTotalWeeklyCasesIqmp()
        {
            DateTime todaysss = DateTime.Now;

            DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek), // prev sunday 00:00
            endDate = startDate.AddDays(-7); // next sunday 00:00


            var weekcount = (from hrmsobj in _context.mHrms
                             where (hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate && hrmsobj.Project == "IQMP")
                             select hrmsobj).ToList().Count();

            return weekcount;
        }

        public int GetMonthlyClosedCount()
        {
            DateTime todaysss = DateTime.Now;
            //DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.Month),
            //endDate = startDate.AddDays(-30);

            DateTime startDate = DateTime.Today.AddDays(-30),
                endDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.Month);

            var monthcount = (from hrmsobj in _context.mHrms
                              where hrmsobj.Status == "Closed" && hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate
                              select hrmsobj).ToList().Count();
            return monthcount;
        }

        public int GetMonthlyClosedCountHrms()
        {
            DateTime todaysss = DateTime.Now;
            //DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.Month),
            //endDate = startDate.AddDays(-30);

            DateTime startDate = DateTime.Today.AddDays(-30),
                endDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.Month);

            var monthcount = (from hrmsobj in _context.mHrms
                              where hrmsobj.Status == "Closed" && hrmsobj.Project == "HRMS" && hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate
                              select hrmsobj).ToList().Count();
            return monthcount;
        }
        public int GetMonthlyClosedCountIqmp()
        {
            DateTime todaysss = DateTime.Now;
            //DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.Month),
            //endDate = startDate.AddDays(-30);

            DateTime startDate = DateTime.Today.AddDays(-30),
                endDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.Month);

            var monthcount = (from hrmsobj in _context.mHrms
                              where hrmsobj.Status == "Closed" && hrmsobj.Project == "IQMP" && hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate
                              select hrmsobj).ToList().Count();
            return monthcount;
        }

        public int GetMonthlyOpenCount()
        {
            DateTime todaysss = DateTime.Now;
            //DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.Month),
            //endDate = startDate.AddDays(-30);

            DateTime startDate = DateTime.Today.AddDays(-30),
                endDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.Month);

            var monthcount = (from hrmsobj in _context.mHrms
                              where hrmsobj.Status == "Open" && hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate
                              select hrmsobj).ToList().Count();
            return monthcount;
        }
        public int GetMonthlyOpenCountHrms()
        {
            DateTime todaysss = DateTime.Now;
            //DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.Month),
            //endDate = startDate.AddDays(-30);

            DateTime startDate = DateTime.Today.AddDays(-30),
                endDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.Month);

            var monthcount = (from hrmsobj in _context.mHrms
                              where hrmsobj.Status == "Open" && hrmsobj.Project == "HRMS" && hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate
                              select hrmsobj).ToList().Count();
            return monthcount;
        }
        public int GetMonthlyOpenCountIqmp()
        {
            DateTime todaysss = DateTime.Now;
            //DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.Month),
            //endDate = startDate.AddDays(-30);

            DateTime startDate = DateTime.Today.AddDays(-30),
                endDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.Month);

            var monthcount = (from hrmsobj in _context.mHrms
                              where hrmsobj.Status == "Open" && hrmsobj.Project == "IQMP" && hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate
                              select hrmsobj).ToList().Count();
            return monthcount;
        }


        public List<MHrms> GetOpenCasesListHrms1()
        {
            var AllData = _context.mHrms.Where(i => i.Status == "Open" && i.Project == "HRMS").ToList();
            return AllData;
        }

        public List<MHrms> GetOpenCasesListIqmp1()
        {
            var AllData = _context.mHrms.Where(i => i.Status == "Open" && i.Project == "IQMP").ToList();
            return AllData;
        }

        public List<MHrms> GetOpenCasesListHrms()
        {
            var AllData = _context.mHrms.Where(i => i.Status == "Open" && i.Project == "hrms").ToList();
            return AllData;
        }

        public List<MHrms> GetOpenCasesListIqmp()
        {
            var AllData = _context.mHrms.Where(i => i.Status == "Open" && i.Project == "iqmp").ToList();
            return AllData;
        }

        public List<MHrms> GetOpenCasesWeeklyListHrms()
        {
            DateTime todaysss = DateTime.Now;

            DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek), // prev sunday 00:00
            endDate = startDate.AddDays(-7); // next sunday 00:00


            var weekcount = (from hrmsobj in _context.mHrms
                             where hrmsobj.Status == "Open" && hrmsobj.Project == "HRMS" && (hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate)
                             select hrmsobj).ToList();

            return weekcount;
        }

        public List<MHrms> GetOpenCasesWeeklyListIqmp()
        {
            DateTime todaysss = DateTime.Now;

            DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek), // prev sunday 00:00
            endDate = startDate.AddDays(-7); // next sunday 00:00


            var weekcount = (from hrmsobj in _context.mHrms
                             where hrmsobj.Status == "Open" && hrmsobj.Project == "IQMP" && (hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate)
                             select hrmsobj).ToList();

            return weekcount;
        }

        public int GetNotInfoCountForHRMS()
        {
            var exceptionList = _context.mSUs
                                 .Select(e => e.SUS);


            var query = (from c in _context.mSUs
                         where !(from o in _context.mHrms
                                 where o.Project == "HRMS"
                                 select o.Sus_No)
                         .Contains(c.SUS)
                         select c).Count();

            return query;
        }

        public int GetNotInfoCountForIQMP()
        {
            var exceptionList = _context.mSUs
                                 .Select(e => e.SUS);


            var query = (from c in _context.mSUs
                         where !(from o in _context.mHrms
                                 where o.Project == "IQMP"
                                 select o.Sus_No)
                         .Contains(c.SUS)
                         select c).Count();

            return query;
        }
        //Counts For Unit CO Dashboard Starts...
        public int FunctionalCountHrms()
        {
            var totalcount = (from hrmsobj in _context.mSUs
                              where hrmsobj.Hrms == 4
                              select hrmsobj).Count();
            return totalcount;
        }

        public int FunctionalCountIqmp()
        {
            var totalcount = (from hrmsobj in _context.mSUs
                              where hrmsobj.Iqmp == 4
                              select hrmsobj).Count();
            return totalcount;
        }

        public int ComplaintPendingHrms()
        {
            var totalcount = (from hrmsobj in _context.mSUs
                              where hrmsobj.Hrms != 4 && hrmsobj.Hrms != 1
                              select hrmsobj).Count();
            return totalcount;
        }

        public int ComplaintPendingIqmp()
        {
            var totalcount = (from hrmsobj in _context.mSUs
                              where hrmsobj.Iqmp != 4 && hrmsobj.Iqmp != 1
                              select hrmsobj).Count();
            return totalcount;
        }

        public int NoInformationHrms()
        {
            var totalunitcount = (from hrmsobj in _context.mSUs
                                  where hrmsobj.IsActive == 1
                                  select hrmsobj).Count();

            var totalfunctionalunit = (from hrmsobj in _context.mSUs
                                       where hrmsobj.Hrms == 4
                                       select hrmsobj).Count();

            var totalcomplaintpending = (from hrmsobj in _context.mSUs
                                         where hrmsobj.Hrms != 4 && hrmsobj.Hrms != 1
                                         select hrmsobj).Count();

            var noinfocount = totalunitcount - (totalfunctionalunit + totalcomplaintpending);

            return noinfocount;
        }

        public int NoInformationIqmp()
        {
            var totalunitcount = (from hrmsobj in _context.mSUs
                                  where hrmsobj.IsActive == 1
                                  select hrmsobj).Count();

            var totalfunctionalunit = (from hrmsobj in _context.mSUs
                                       where hrmsobj.Iqmp == 4
                                       select hrmsobj).Count();

            var totalcomplaintpending = (from hrmsobj in _context.mSUs
                                         where hrmsobj.Iqmp != 4 && hrmsobj.Iqmp != 1
                                         select hrmsobj).Count();

            var noinfocount = totalunitcount - (totalfunctionalunit + totalcomplaintpending);

            return noinfocount;
        }

        //Counts For Unit CO Dashboard Ends...

        public int GetFunctionalCountForHrms()
        {
            var totalcount = (from hrmsobj in _context.mSUs
                              where hrmsobj.IsActive == 1
                              select hrmsobj).Count();

            var exceptionList = _context.mSUs
                                 .Select(e => e.SUS);


            var NoTInfo = (from c in _context.mSUs
                           where !(from o in _context.mHrms
                                   where o.Project == "hrms"
                                   select o.Sus_No)
                           .Contains(c.SUS)
                           select c).Count();

            var opencount = (from hrmsobj in _context.mHrms
                             where hrmsobj.Status == "Open" && hrmsobj.Project == "hrms"
                             select hrmsobj).Count();

            //var functional = totalcount - (opencount + NoTInfo);

            var functional = (opencount + NoTInfo) - totalcount;
            return functional;
        }

        public int GetFunctionalCountForIqmp()
        {
            var totalcount = (from hrmsobj in _context.mSUs
                              where hrmsobj.IsActive == 1
                              select hrmsobj).Count();

            var exceptionList = _context.mSUs
                                 .Select(e => e.SUS);


            var NoTInfo = (from c in _context.mSUs
                           where !(from o in _context.mHrms
                                   where o.Project == "iqmp"
                                   select o.Sus_No)
                           .Contains(c.SUS)
                           select c).Count();

            var opencount = (from hrmsobj in _context.mHrms
                             where hrmsobj.Status == "Open" && hrmsobj.Project == "iqmp"
                             select hrmsobj).Count();

            //var functional = totalcount - (opencount + NoTInfo);
            var functional = (opencount + NoTInfo) - totalcount;
            return functional;
        }

        public int GetUnitStatusHrms(string project, string unit, string sus)
        {
            //dynamic aaa = "";
            var aaa = (from status in _context.mSUs
                       where status.Unit == unit || status.SUS == sus
                       select status.Hrms).FirstOrDefault();
            return aaa;


            //dynamic aaa = "";
            //if(project == "1")
            //{
            //    aaa = _context.mSUs.Where(i => (i.Unit == unit || i.SUS == sus) && i.Project == project).FirstOrDefault();
            //    if (aaa != null)
            //    {
            //        return aaa;
            //    }
            //    return null;
            //}
            //else if(project == "2")
            //{
            //    aaa = _context.mSUs.Where(i => (i.Unit == unit || i.SUS == sus) && i.Project == project).FirstOrDefault();

            //    if(aaa != null)
            //    {
            //        return aaa;
            //    }
            //}
            //return null;
        }

        public int GetUnitStatusIqmp(string project, string unit, string sus)
        {
            var aaa = (from status in _context.mSUs
                       where status.Unit == unit || status.SUS == sus
                       select status.Iqmp).FirstOrDefault();
            return aaa;
        }

        public string UpdateUnitStatus(string project, string unit, int unitstat, string sus)
        {
            MSUSNo ms = new MSUSNo();
            if (project == "1")
            {
                var aa = _context.mSUs.Where(i => i.Unit == unit || i.SUS == sus).FirstOrDefault();
                aa.Hrms = unitstat;
                _context.SaveChangesAsync();
            }
            else if (project == "2")
            {
                var aa = _context.mSUs.Where(i => i.Unit == unit || i.SUS == sus).FirstOrDefault();
                aa.Iqmp = unitstat;
                _context.SaveChangesAsync();
            }
            return "AAA";
        }

        public List<MSUSNo> GetUnitList(int project, string unit)
        {
            //var aaaa = _context.mSUs.Where(i => i.IsActive == 1).FirstOrDefault();
            //if(aaaa.Hrms == "1")
            dynamic listdata = "";

            if (project == 1)
            {
                listdata = _context.mSUs.Where(i => i.Hrms != 0 && i.Hrms != 4 && i.Hrms != 1).ToList();
            }
            else if (project == 2)
            {
                listdata = _context.mSUs.Where(i => i.Iqmp != 0 && i.Iqmp != 4 && i.Iqmp != 1).ToList();
            }

            if (listdata != null)
            {
                return listdata;
            }
            else
            {
                return null;
            }
        }

        public List<MSUSNo> GetByStatus(int project, int status)
        {
            var aaaa = _context.mSUs.Where(i => i.IsActive == 1).ToList();

            dynamic listdata = "";

            if (project == 1)
            {
                var data = 0;
                foreach (var item in aaaa)
                {
                    data = item.Hrms;
                    {
                        if (data == status && data == 1)
                        {
                            listdata = _context.mSUs.Where(i => i.Hrms == 1).ToList();
                        }
                        else if (data == status && data == 2)
                        {
                            listdata = _context.mSUs.Where(i => i.Hrms == 2).ToList();
                        }
                        else if (data == status && data == 3)
                        {
                            listdata = _context.mSUs.Where(i => i.Hrms == 3).ToList();
                        }
                        else if (data == status && data == 4)
                        {
                            listdata = _context.mSUs.Where(i => i.Hrms == 4).ToList();
                        }
                    }

                }
            }
            else if (project == 2)
            {
                var data = 0;
                foreach (var item in aaaa)
                {
                    data = item.Iqmp;
                    {
                        if (data == status && data == 1)
                        {
                            listdata = _context.mSUs.Where(i => i.Iqmp == 1).ToList();
                        }
                        else if (data == status && data == 2)
                        {
                            listdata = _context.mSUs.Where(i => i.Iqmp == 2).ToList();
                        }
                        else if (data == status && data == 3)
                        {
                            listdata = _context.mSUs.Where(i => i.Iqmp == 3).ToList();
                        }
                        else if (data == status && data == 4)
                        {
                            listdata = _context.mSUs.Where(i => i.Iqmp == 4).ToList();
                        }
                    }

                }

            }

            if (listdata != null)
            {
                return listdata;
            }
            else
            {
                return null;
            }
        }


        public int GetCountNotInstalledHrms()
        {
            var data = (from obj in _context.mSUs
                        where obj.Hrms == 1
                        select obj).Count();
            return data;
        }
        public int GetCountNotFunctionalHrms()
        {
            var data = (from obj in _context.mSUs
                        where obj.Hrms == 2
                        select obj).Count();
            return data;
        }
        public int GetCountPartiallyFunctionalHrms()
        {
            var data = (from obj in _context.mSUs
                        where obj.Hrms == 3
                        select obj).Count();
            return data;
        }
        public int GetCountFullyFunctionalHrms()
        {
            var data = (from obj in _context.mSUs
                        where obj.Hrms == 4
                        select obj).Count();
            return data;
        }

        public int GetCountNotInstalledIqmp()
        {
            var data = (from obj in _context.mSUs
                        where obj.Iqmp == 1
                        select obj).Count();
            return data;
        }
        public int GetCountNotFunctionalIqmp()
        {
            var data = (from obj in _context.mSUs
                        where obj.Iqmp == 2
                        select obj).Count();
            return data;
        }
        public int GetCountPartiallyFunctionalIqmp()
        {
            var data = (from obj in _context.mSUs
                        where obj.Iqmp == 3
                        select obj).Count();
            return data;
        }
        public int GetCountFullyFunctionalIqmp()
        {
            var data = (from obj in _context.mSUs
                        where obj.Iqmp == 4
                        select obj).Count();
            return data;
        }

        public List<MSUSNo> GetListOfUnits(string project, string status)
        {
            if (project == "HRMS" && status == "Functional_Units")
            {
                var data = (from obj in _context.mSUs
                            where obj.Hrms == 4
                            select obj).ToList();
                return data;
            }
            else if (project == "HRMS" && status == "Complaint_Pending")
            {
                var data = (from obj in _context.mSUs
                            where obj.Hrms != 4 && obj.Hrms != 1
                            select obj).ToList();
                return data;
            }
            else if (project == "HRMS" && status == "No_Information")
            {
                var totalunit = (from hrmsobj in _context.mSUs
                                 where hrmsobj.IsActive == 1
                                 select hrmsobj).ToList();

                var totalfunctionalunit = (from hrmsobj in _context.mSUs
                                           where hrmsobj.Hrms == 4
                                           select hrmsobj).ToList();

                var totalcomplaintpending = (from hrmsobj in _context.mSUs
                                             where hrmsobj.Hrms != 4 && hrmsobj.Hrms != 1
                                             select hrmsobj).ToList();

                var functional_and_pending = totalfunctionalunit.Concat(totalcomplaintpending).ToList();

                var noinfocount = totalunit.Except(functional_and_pending).ToList();

                return noinfocount;
            }
            else if (project == "IQMP" && status == "Functional_Units")
            {
                var data = (from obj in _context.mSUs
                            where obj.Iqmp == 4
                            select obj).ToList();
                return data;
            }
            else if (project == "IQMP" && status == "Complaint_Pending")
            {
                var data = (from obj in _context.mSUs
                            where obj.Iqmp != 4 && obj.Iqmp != 1
                            select obj).ToList();
                return data;
            }
            else if (project == "IQMP" && status == "No_Information")
            {
                var totalunit = (from hrmsobj in _context.mSUs
                                 where hrmsobj.IsActive == 1
                                 select hrmsobj).ToList();

                var totalfunctionalunit = (from hrmsobj in _context.mSUs
                                           where hrmsobj.Iqmp == 4
                                           select hrmsobj).ToList();

                var totalcomplaintpending = (from hrmsobj in _context.mSUs
                                             where hrmsobj.Iqmp != 4 && hrmsobj.Iqmp != 1
                                             select hrmsobj).ToList();

                var functional_and_pending = totalfunctionalunit.Concat(totalcomplaintpending).ToList();

                var noinfocount = totalunit.Except(functional_and_pending).ToList();
                return noinfocount;
            }
            return null;
        }

        public List<MHrms> GetOpenCountTable()
        {
            var opencount = (from hrmsobj in _context.mHrms
                             where hrmsobj.Status == "Open"
                             select hrmsobj).ToList();
            return opencount;
        }

        public List<MHrms> GetWeekOpenCountTable()
        {
            DateTime todaysss = DateTime.Now;

            DateTime startDate = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek), // prev sunday 00:00
            endDate = startDate.AddDays(-7); // next sunday 00:00


            var weekcount = (from hrmsobj in _context.mHrms
                             where hrmsobj.Status == "Open" && (hrmsobj.CreatedOn <= todaysss && hrmsobj.CreatedOn >= endDate)
                             select hrmsobj).ToList();

            return weekcount;
        }

        //Calling Stored Procedure...
        public List<MHrms> GetDataByUnit(string unit,string project)
        {
            var list = _context.mHrms.FromSqlRaw($"SelectProject {unit},{project}").ToList();
            return list;
        }
       
    }
}
