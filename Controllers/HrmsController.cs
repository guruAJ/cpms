using CPMS_AUTO.Data;
using CPMS_AUTO.Helpers;
using CPMS_AUTO.Models;
using CPMS_AUTO.Repository;
using Itenso.TimePeriod;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;

namespace CPMS_AUTO.Controllers
{
    //[Route("[controller]")]

    public class HrmsController : Controller
    {
        private readonly RepositoryHrms _repositoryHrms;
        private ApplicationDbContext dbcontext;
        private int roles=0;

        
        public HrmsController(RepositoryHrms repositoryHrms, ApplicationDbContext _dbcontext)
        {
            _repositoryHrms = repositoryHrms;
            dbcontext = _dbcontext;
        }
        public IActionResult Index(int Id,string project)
         {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");
            
           // SessionHelper.GetObjectFromJson(HttpContext.Session, "roletype", Db.RoleType);
           // int role = HttpContextAccessor.HttpContext.Session.GetInt32("roletype");

            roles = (int)HttpContext.Session.GetInt32("roletype");

            if (roles == 5)
            {
                project = "HRMS";

            }
            else if (roles == 4)
            {
                project = "IQMP";
            }
            else
            {
                //project = "ALL";
            }


            if (Logins != null)
            {
                ViewData["SUSNo"] = "";
                HttpContext.Session.SetString("susNO","sanal");
                MHrms Model = new MHrms();
                if (Id != 0)
                {
                    Model = _repositoryHrms.GetById(Id);
                    TempData["Id"] = Model.Id;

                    if (project == "ALL")
                    {
                        ViewBag.AllData = _repositoryHrms.GetAll();
                    }
                    else
                    {
                        ViewBag.radio = project;
                        ViewBag.AllData = _repositoryHrms.GetAllbyProj(project);
                    }
                    
                    TempData.Keep("Id");
                    GetAllDropdown();
                    //GetAllbyProj
                    return View(Model);
                }
                else
                {
                    TempData["Id"] = 0;
                   // ViewBag.radio = project;
                    TempData.Keep("Id");
                    if (project == null)
                    {
                        ViewBag.AllData = _repositoryHrms.GetAll();
                    }
                    else
                    {
                        ViewBag.radio = project;
                        ViewBag.AllData = _repositoryHrms.GetAllbyProj(project);
                    }

                    //ViewBag.AllData = _repositoryHrms.GetAllbyProj(project);
                    
                    
                    GetAllDropdown();
                    return View();
                }
                //var AllData = _repositoryHrms.GetAll();

                //ViewBag.AllData = AllData;
            }
            else
                return Redirect("Login/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(MHrms Db)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");
            if (Convert.ToInt32(TempData["Id"]) > 0)
                Db.Id = Convert.ToInt32(TempData["Id"]);
            Db.QueryDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                Db.CreatedBy = Logins.UserID;
                int ret = await _repositoryHrms.Save(Db);
                if (ret == 1)
                {
                    ViewBag.message = "" + Db.Id + " is saved successfully";

                }
                else if (ret == 2)
                {
                    ViewBag.message = "" + Db.Id + " is Update successfully";

                }
                else if (ret == 3)
                {
                    ViewBag.message = "Already Exits Status Name";
                }


                string project = "";
                var AllData = _repositoryHrms.GetAll();

                roles = (int)HttpContext.Session.GetInt32("roletype");

                if (roles == 5)
                {
                    project = "HRMS";

                }
                else if (roles == 4)

                {
                    project = "IQMP";
                }
                else
                {
                   // project = "ALL";
                }

                if (project == "ALL")
                {
                    AllData = _repositoryHrms.GetAll();
                }
                else
                {
                    ViewBag.radio = project;
                    AllData = _repositoryHrms.GetAllbyProj(project);
                }

               // var AllData = _repositoryHrms.GetAll();

                ViewBag.AllData = AllData;
                //SearchData("");
                //return View("Index", Db);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Index", Db);
            }
        }



        [HttpPost]
        
        public async void UpdatStatus(int Id, string Statu)
        {
            MHrms Db = new MHrms();
            Db = _repositoryHrms.GetDataById(Id);

            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");
            //if (Convert.ToInt32(TempData["Id"]) > 0)
            //Db.Id = Convert.ToInt32(Id);// Convert.ToInt32(TempData["Id"]);
            Db.UpdatedOn = DateTime.Now;
            Db.Status = Statu;

            if (ModelState.IsValid)
            {
                Db.CreatedBy = Logins.UserID;
                int ret = await _repositoryHrms.UpdateStatus(Db);
                if (ret == 1)
                {
                    ViewBag.message = "" + Db.Id + " is saved successfully";

                }
                else if (ret == 2)
                {
                    ViewBag.message = "" + Db.Id + " Status Updated successfully";

                }
                else if (ret == 3)
                {
                    ViewBag.message = "Status Updated.....";
                }
                var AllData = _repositoryHrms.GetAll();

                //return Json(AllData);

              
            }
            else
            {
               // return View("Index", Db);
            }
        }


        [HttpPost]
        public IActionResult GetProjectDataById(int Id)
        {
            if (Id != 0)
            {
                var AllProjects = _repositoryHrms.GetDataById(Id);
                
                ViewBag.AllProjects = AllProjects;
                TempData["Id"] = AllProjects.Id;
                return Json(AllProjects);
            }
            return View("Index");
        }


        [HttpPost]
        public IActionResult GetProjDetl(string projct)
        {
            if (projct != null)
            {
                var AllProjects = _repositoryHrms.GetAllbyProj(projct);

                ViewBag.AllData = AllProjects;
                //ViewBag.AllData = AllData;
                //SearchData("");
                //return View("Index", Db);
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult ValidateSusUnit(string sus, string units)
        {
            string ss="";
            var json = "";
            try
            {
                if (sus != null  || units != null)
                {
                    var Uunitdetl = _repositoryHrms.VerifysusUnit(sus, units);

                    if (Uunitdetl != null)
                    {
                       // ViewData["SUSNo"] = Uunitdetl.SUS;
                        ViewData["Unit"] = Uunitdetl.Unit;
                        //return Json(Uunitdetl);
                       
                        HttpContext.Session.SetString("susNO", Uunitdetl.SUS.ToString());
                        ss = Uunitdetl.SUS; //HttpContext.Session.GetString("susNO");
                       // json = JsonSerializer.Serialize(Uunitdetl);

                    }
                    return Json(Uunitdetl);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
           
            return Json(0);
        }

        [HttpPost]
        //[EnableCors("AllowOrigin")]
        public JsonResult Validate(string sus, string units)
        {
            string ss = "";
            var json = "";
            try
            {
                //if (sus != "" || units != "")
                //{
                    var Uunitdetl = _repositoryHrms.VerifysusUnit(sus, units);

                    if (Uunitdetl != null)
                    {
                        // ViewData["SUSNo"] = Uunitdetl.SUS;
                        ViewData["Unit"] = Uunitdetl.Unit;
                        //return Json(Uunitdetl);

                        HttpContext.Session.SetString("susNO", Uunitdetl.SUS.ToString());
                        ss = Uunitdetl.SUS; //HttpContext.Session.GetString("susNO");
                                            // json = JsonSerializer.Serialize(Uunitdetl);
                    return Json(Uunitdetl);
                }

                //    return Json(Uunitdetl);
                //}
                else
                {
                    return Json(0); ;
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Search(string sus, string unit,string project)
        {
            var searchdata = _repositoryHrms.GetData(sus, unit,project);

            if(searchdata != null)
            {
                ViewBag.GetSearchData = searchdata;
                ViewBag.dnone = false;
                return Json(searchdata);
            }
            else
            {
                ViewBag.dnone = true;
            }

            return View("Index");
        }

        [HttpPost]
        public JsonResult SearchByDateToDateFrom(string project,DateTime DateTo, DateTime DateFrom,string unit)
        {
            var searchdata = _repositoryHrms.GetDataByDate(project, DateTo, DateFrom,unit);

            if (searchdata != null)
            {
                ViewBag.GetSearchData = searchdata;
                ViewBag.dnone = false;
                return Json(searchdata);
            }
            else
            {
                ViewBag.dnone = true;
            }

            return null;
        }

        //public IActionResult GetCountByUnit(string unit,string Project)
        //{
        //    var data = _repositoryHrms.GetUnitCount(unit, Project);
        //    ViewBag.UnitData = data;
        //    return View();
        //}
        [HttpGet]
        public IActionResult GetCountByUnit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetCountByUnit(string Project, DateTime ToDate, DateTime FromDate)
        {
            if(Project == "HRMS")
            {
                var data = _repositoryHrms.GetUnitCountHrms(Project, ToDate, FromDate);
                ViewBag.UnitDataHrms = data;
            }
            else if(Project == "IQMP")
            {
                var data = _repositoryHrms.GetUnitCountIqmp(Project, ToDate, FromDate);
                ViewBag.UnitDataIqmp = data;
            }
            return View();
        }

        [HttpPost]
        public JsonResult ValidateSus(string susdata)
        {
            var aaa = _repositoryHrms.CheckSus(susdata);
            return Json(aaa);
        }

        [HttpPost]
        //[ResponseCache(Duration = 5)]
        public ActionResult SubmitStatus(string status, int id)
        {
            var statusdata = _repositoryHrms.SubmitStatus(status, id);
            return View("Index");
        }

        public void GetAllDropdown()
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");

            var AllSus = _repositoryHrms.GetAllSus();
            AllSus.Insert(0, new MSUSNo { Id = 0, SUS = "--Select SUS NO--" });
            ViewBag.AllSus = AllSus;

            var AllUnit = _repositoryHrms.GetAllUnit();
            AllUnit.Insert(0, new MSUSNo { Id = 0, Unit = "--Select Uni--" });
            ViewBag.AllUnit = AllUnit;

        }
       
        //public JsonResult GetSus(string sus)
        //{
        //    var data = _repositoryHrms.GETSUS(sus);
        //    return Json(data);
        //}

        [HttpPost]
        public JsonResult AutoComplete(string term)
        {


            var data = _repositoryHrms.GetSUSAUTO(term);
            return Json(data);
        }


        [HttpPost]
        public JsonResult AutoCompleteUnit(string term)
        {


            var data = _repositoryHrms.GetUnitAUTO(term);
            return Json(data);
        }

        public IActionResult Dashboard()
        {
            ViewBag.TotalCountHrms = _repositoryHrms.GetTotalCountForHrms();
            ViewBag.TotalCountIqmp = _repositoryHrms.GetTotalCountForIqmp();

            ViewBag.ComplainPendingHrms = _repositoryHrms.ComplaintPendingHrms();
            ViewBag.ComplainPendingIqmp = _repositoryHrms.ComplaintPendingIqmp();

            ViewBag.NoInformationHrms = _repositoryHrms.NoInformationHrms();
            ViewBag.NoInformationIqmp = _repositoryHrms.NoInformationIqmp();

            ViewBag.FullyFunctionalHrms = _repositoryHrms.FunctionalCountHrms();
            ViewBag.FullyFunctionalIqmp = _repositoryHrms.FunctionalCountIqmp();

            ////ViewBag.ClosedCount = _repositoryHrms.GetClosedCount();
            //ViewBag.OpenCountHrms = _repositoryHrms.GetOpenCountForHrms();
            //ViewBag.OpenCountIqmp = _repositoryHrms.GetOpenCountForIqmp();
            ////ViewBag.WeeklyOpenCount = _repositoryHrms.GetWeekOpenCount();
            ////ViewBag.WeeklyCloseCount = _repositoryHrms.GetWeekCloseCount();
            //ViewBag.MonthlyClosedCount = _repositoryHrms.GetMonthlyCount();
            //ViewBag.MonthlyOpenCount = _repositoryHrms.GetMonthlyOpenCount();
            ////ViewBag.TotalWeeklyCount = _repositoryHrms.GetTotalWeeklyCases();
            //ViewBag.FunctionalCountForHrms = _repositoryHrms.GetFunctionalCountForHrms();
            //ViewBag.FunctionalCountForIqmp = _repositoryHrms.GetFunctionalCountForIqmp();
            //ViewBag.NotInfoCountHRMS = _repositoryHrms.GetNotInfoCountForHRMS();
            //ViewBag.NotInfoCountIQMP = _repositoryHrms.GetNotInfoCountForIQMP();
            return View();
        }

        public IActionResult SummaryDashboard()
        {
            //For Total Count 
            ViewBag.TotalCountSummary = _repositoryHrms.GetTotalCountSummary();
            ViewBag.TotalCountSummaryHrms = _repositoryHrms.GetTotalCountSummaryHrms();
            ViewBag.TotalCountSummaryIqmp = _repositoryHrms.GetTotalCountSummaryIqmp();

            //For Total Open Count
            ViewBag.OpenCount = _repositoryHrms.GetOpenCount();
            ViewBag.OpenCountHrms = _repositoryHrms.GetOpenCountHrms();
            ViewBag.OpenCountHrms = _repositoryHrms.GetOpenCountIqmp();

            //For Total Close Count
            ViewBag.ClosedCount = _repositoryHrms.GetClosedCount();
            ViewBag.ClosedCountHrms = _repositoryHrms.GetClosedCountHrms();
            ViewBag.ClosedCountIqmp = _repositoryHrms.GetClosedCountIqmp();

            //Weekly Total Count
            ViewBag.TotalWeeklyCount = _repositoryHrms.GetTotalWeeklyCases();
            ViewBag.TotalWeeklyCountHrms = _repositoryHrms.GetTotalWeeklyCasesHrms();
            ViewBag.TotalWeeklyCountIqmp = _repositoryHrms.GetTotalWeeklyCasesIqmp();

            //Weekly Close Count
            ViewBag.WeeklyCloseCount = _repositoryHrms.GetWeekCloseCount();
            ViewBag.WeeklyCloseCountHrms = _repositoryHrms.GetWeekCloseCountHrms();
            ViewBag.WeeklyCloseCountIqmp = _repositoryHrms.GetWeekCloseCountIqmp();

            
            ViewBag.OpenCountHrms = _repositoryHrms.GetOpenCountForHrms();
            ViewBag.OpenCountIqmp = _repositoryHrms.GetOpenCountForIqmp();

            //Weekly Open Count 
            ViewBag.WeeklyOpenCount = _repositoryHrms.GetWeekOpenCount();
            ViewBag.WeeklyOpenCountHrms = _repositoryHrms.GetWeekOpenCountHrms();
            ViewBag.WeeklyOpenCountIqmp = _repositoryHrms.GetWeekOpenCountIqmp();



            //Monthly Open Count
            ViewBag.MonthlyOpenCount = _repositoryHrms.GetMonthlyOpenCount();
            ViewBag.MonthlyOpenCountHrms = _repositoryHrms.GetMonthlyOpenCountHrms();
            ViewBag.MonthlyOpenCountIqmp = _repositoryHrms.GetMonthlyOpenCountIqmp();

            //Monthly Closed Count
            ViewBag.MonthlyClosedCount = _repositoryHrms.GetMonthlyClosedCount();
            ViewBag.MonthlyClosedCountHrms = _repositoryHrms.GetMonthlyClosedCountHrms();
            ViewBag.MonthlyClosedCountIqmp = _repositoryHrms.GetMonthlyClosedCountIqmp();


            ViewBag.NotInfoCountHRMS = _repositoryHrms.GetNotInfoCountForHRMS();
            ViewBag.NotInfoCountIQMP = _repositoryHrms.GetNotInfoCountForIQMP();
            ViewBag.FunctionalCountForHrms = _repositoryHrms.GetFunctionalCountForHrms();
            ViewBag.FunctionalCountForIqmp = _repositoryHrms.GetFunctionalCountForIqmp();
            return View();
        }

        public IActionResult OpenCaseHrms1()
        {
            ViewBag.OpenCaseData = _repositoryHrms.GetOpenCasesListHrms1();
            return View();
        }

        public IActionResult OpenCaseIqmp1()
        {
            ViewBag.OpenCaseData = _repositoryHrms.GetOpenCasesListIqmp1();
            return View();
        }

        public IActionResult WeeklyOpenCaseHrms()
        {
            ViewBag.WeeklyOpenCaseData = _repositoryHrms.GetOpenCasesWeeklyListHrms();
            return View();
        }

        public IActionResult WeeklyOpenCaseIqmp()
        {
            ViewBag.WeeklyOpenCaseData = _repositoryHrms.GetOpenCasesWeeklyListIqmp();
            return View();
        }

        public IActionResult OpenCaseHrms()
        {
            ViewBag.OpenCaseDataHrms = _repositoryHrms.GetOpenCasesListHrms();
            return View();
        }

        public IActionResult OpenCaseIqmp()
        {
            ViewBag.OpenCaseDataIqmp = _repositoryHrms.GetOpenCasesListIqmp();
            return View();
        }

        [HttpPost]
        public JsonResult GetUnitStatus(string unit,string sus,string project)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");
            roles = (int)HttpContext.Session.GetInt32("roletype");
            if (roles == 5)
            {
                project = "HRMS";
                var a = _repositoryHrms.GetUnitStatusHrms(project, unit, sus);
                return Json(a);

            }
            else if (roles == 4)
            {
                project = "IQMP";
                var a = _repositoryHrms.GetUnitStatusIqmp(project, unit, sus);
                return Json(a);
            }
            else if (roles == 3)
            {
                if (project == "1")
                {
                    project = "HRMS";
                    var a = _repositoryHrms.GetUnitStatusHrms(project, unit, sus);
                    return Json(a);
                }
                else if (project == "2")
                {
                    project = "IQMP";
                    var a = _repositoryHrms.GetUnitStatusIqmp(project, unit, sus);
                    return Json(a);
                }
            }
            return null;
        }

        [HttpPost]
        public JsonResult GetRecordStat(string unit, string sus, string project)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");
            roles = (int)HttpContext.Session.GetInt32("roletype");
            if (roles == 5)
            {
                project = "HRMS";
                var a = _repositoryHrms.GetUnitStatusHrms(project, unit, sus);
                return Json(a);

            }
            else if (roles == 4)
            {
                project = "IQMP";
                var a = _repositoryHrms.GetUnitStatusIqmp(project, unit, sus);
                return Json(a);
            }
            else if(roles == 3)
            {
                if(project == "1")
                {
                    project = "HRMS";
                    var a = _repositoryHrms.GetUnitStatusHrms(project, unit, sus);
                    return Json(a);
                }
                else if(project == "2")
                {
                    project = "IQMP";
                    var a = _repositoryHrms.GetUnitStatusIqmp(project, unit, sus);
                    return Json(a);
                }
            }
            return null;
        }

        [HttpPost]
        public JsonResult SaveUnitStatus(string unit,int unitstat,string project,string sus)
        {
            var data = _repositoryHrms.UpdateUnitStatus(project, unit, unitstat,sus);
            return Json(data);
        }

        //[HttpPost]
        public IActionResult GetUnitTable()
        {
            var data1 = _repositoryHrms.GetCountNotInstalledHrms();
            ViewBag.NotInstalledHrms = data1;

            var data2 = _repositoryHrms.GetCountNotFunctionalHrms();
            ViewBag.NotFunctionalHrms = data2;

            var data3 = _repositoryHrms.GetCountPartiallyFunctionalHrms();
            ViewBag.PartiallyFunctionalHrms = data3;

            var data4 = _repositoryHrms.GetCountFullyFunctionalHrms();
            ViewBag.FullyFunctionalHrms = data4;

            var data11 = _repositoryHrms.GetCountNotInstalledIqmp();
            ViewBag.NotInstalledIqmp = data11;

            var data22 = _repositoryHrms.GetCountNotFunctionalIqmp();
            ViewBag.NotFunctionalIqmp = data22;

            var data33 = _repositoryHrms.GetCountPartiallyFunctionalIqmp();
            ViewBag.PartiallyFunctionalIqmp = data33;

            var data44 = _repositoryHrms.GetCountFullyFunctionalIqmp();
            ViewBag.FullyFunctionalIqmp = data44;

            return View();
        }

        [HttpPost]
        public JsonResult UnitSearch(int project,string unit)
        {
            var data = _repositoryHrms.GetUnitList(project,unit);
            return Json(data);
        }

        [HttpPost]
        public JsonResult UnitSearchForStatus(int project, int status)
        {
            var data = _repositoryHrms.GetByStatus(project, status);
            
            return Json(data);
        }

        public IActionResult Record(int Id, string project)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");

            roles = (int)HttpContext.Session.GetInt32("roletype");

            if (roles == 5)
            {
                project = "HRMS";

            }
            else if (roles == 4)
            {
                project = "IQMP";
            }
            else
            {
                //project = "ALL";
            }


            if (Logins != null)
            {
                ViewData["SUSNo"] = "";
                HttpContext.Session.SetString("susNO", "sanal");
                MHrms Model = new MHrms();
                if (Id != 0)
                {
                    Model = _repositoryHrms.GetById(Id);
                    TempData["Id"] = Model.Id;

                    if (project == "ALL")
                    {
                        ViewBag.AllData = _repositoryHrms.GetAll();
                    }
                    else
                    {
                        ViewBag.radio = project;
                        ViewBag.AllData = _repositoryHrms.GetAllbyProj(project);
                    }

                    TempData.Keep("Id");
                    GetAllDropdown();
                    return View(Model);
                }
                else
                {
                    TempData["Id"] = 0;
                    TempData.Keep("Id");
                    if (project == null)
                    {
                        ViewBag.AllData = _repositoryHrms.GetAll();
                    }
                    else
                    {
                        ViewBag.radio = project;
                        ViewBag.AllData = _repositoryHrms.GetAllbyProj(project);
                    }
                    GetAllDropdown();
                    return View();
                }
            }
            else
                return Redirect("Login/Index");
        }

        public IActionResult SearchByDate(string project, string DateTo, string DateFrom, int Id)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");

            roles = (int)HttpContext.Session.GetInt32("roletype");

            if (roles == 5)
            {
                project = "HRMS";

            }
            else if (roles == 4)
            {
                project = "IQMP";
            }
            else if(roles == 3)
            {
                //project = "ALL";
            }


            if (Logins != null)
            {
                ViewData["SUSNo"] = "";
                HttpContext.Session.SetString("susNO", "sanal");
                MHrms Model = new MHrms();
                if (Id != 0)
                {
                    Model = _repositoryHrms.GetById(Id);
                    TempData["Id"] = Model.Id;

                    if (project == "ALL")
                    {
                        ViewBag.AllData = _repositoryHrms.GetAll();
                    }
                    else
                    {
                        ViewBag.radio = project;
                        ViewBag.AllData = _repositoryHrms.GetAllbyProj(project);
                    }

                    TempData.Keep("Id");
                    GetAllDropdown();
                    return View(Model);
                }
                else
                {
                    TempData["Id"] = 0;
                    TempData.Keep("Id");
                    if (project == null)
                    {
                        ViewBag.AllData = _repositoryHrms.GetAll();
                    }
                    else
                    {
                        ViewBag.radio = project;
                        ViewBag.AllData = _repositoryHrms.GetAllbyProj(project);
                    }
                    GetAllDropdown();
                    return View();
                }
            }
            else
                return Redirect("Login/Index");
        }

        public IActionResult COSupport(int Id, string project)
        {
            Login Logins = SessionHelper.GetObjectFromJson<Login>(HttpContext.Session, "User");

            roles = (int)HttpContext.Session.GetInt32("roletype");

            if (roles == 5)
            {
                project = "HRMS";

            }
            else if (roles == 4)
            {
                project = "IQMP";
            }
            else
            {
                //project = "ALL";
            }
            if (Logins != null)
            {
                ViewData["SUSNo"] = "";
                HttpContext.Session.SetString("susNO", "sanal");
                MHrms Model = new MHrms();
                if (Id != 0)
                {
                    Model = _repositoryHrms.GetById(Id);
                    TempData["Id"] = Model.Id;

                    if (project == "ALL")
                    {
                        ViewBag.AllData = _repositoryHrms.GetAll();
                    }
                    else
                    {
                        ViewBag.radio = project;
                        ViewBag.AllData = _repositoryHrms.GetAllbyProj(project);
                    }

                    TempData.Keep("Id");
                    GetAllDropdown();
                    return View(Model);
                }
                else
                {
                    TempData["Id"] = 0;
                    TempData.Keep("Id");
                    if (project == null)
                    {
                        ViewBag.AllData = _repositoryHrms.GetAll();
                    }
                    else
                    {
                        ViewBag.radio = project;
                        ViewBag.AllData = _repositoryHrms.GetAllbyProj(project);
                    }

                    GetAllDropdown();
                    return View();
                }
            }
            else
                return Redirect("Login/Index");
        }

        public IActionResult SearchTest(string Project,string Status)
        {
            var data4 = _repositoryHrms.GetListOfUnits(Project,Status);
            return Json(data4);
        }
            
        public IActionResult GetOpenCountTable()
        {
            var data = _repositoryHrms.GetOpenCountTable();
            ViewBag.OpenCountTable = data;
            return View();
        }

        public IActionResult GetOpenCountTableWeekly()
        {
            var data = _repositoryHrms.GetWeekOpenCountTable();
            ViewBag.OpenCountTableWeekly = data;
            return View();
        }

        public IActionResult CheckSP(string unit,string project)
        {
            var data = _repositoryHrms.GetDataByUnit(unit,project);

            return View();
        }
    }
}
