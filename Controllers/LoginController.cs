using CPMS_AUTO.Data;
using CPMS_AUTO.Helpers;
using CPMS_AUTO.Models;
using CPMS_AUTO.Repository;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CPMS_AUTO.Controllers
{

    public class LoginController : Controller
    {
        public readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;
        private readonly RepositoryUser _repository;
        public IConfiguration Configuration { get; }
        public LoginController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events, IConfiguration configuration, RepositoryUser repositoryUser)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
            Configuration = configuration;
            _repository = repositoryUser;
        }

        public IActionResult Index()
        {

           
            return View();

        }
        public IActionResult Logout()
        {
            Login db = new Login();
            db = null;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "User", db);

            return View("Index");

        }

        //public async Task getAsync(Login model)
        //{
        //    try
        //    {
        //        LdapDirectoryIdentifier ldapDir = new LdapDirectoryIdentifier(Configuration.GetSection("Appsetings").GetSection("ipaddress").Value, 389);
        //        LdapConnection ldapConn = new LdapConnection(ldapDir);
        //        ldapConn.AuthType = AuthType.Basic;
        //        // System.Net.NetworkCredential myCredentials = new System.Net.NetworkCredential("uid=" + model.Username + ",ou=people, o = army.mil, dc=army,dc =mil", model.Password);
        //        System.Net.NetworkCredential myCredentials = new System.Net.NetworkCredential("CN=" + model.UserID + ", CN = Users, dc=LDAP,dc =COM", model.Password);
        //        ldapConn.Bind(myCredentials);

        //        //var result = await _signInManager.PasswordSignInAsync(model.UserID, model.Password, model.RememberLogin, lockoutOnFailure: true);
        //        //if (result.Succeeded)
        //        //{
        //        //}

        //        return model;
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ex.Message);
        //    }
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Validate1Async(Login Db)
        {
            if (ModelState.IsValid)
            {
                if (Db.UserID == null && Db.Password == null)
                {
                    return NotFound();
                }
                if (Db.UserID == "Admin" && Db.Password == "123456")
                {
                    Db.RoleType = 1;
                    _repository.Save(Db);

                    SessionHelper.SetObjectAsJson(HttpContext.Session, "User", Db);
                    return RedirectToAction("Index", "Home");

                }
                if(Db.UserID == "PS" && Db.Password == "Admin123!")
                {
                    Db.RoleType = 2;
                    _repository.Save(Db);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "User", Db);
                    return RedirectToAction("Dashboard", "ProjectSupport");

                }
                else
                {
                    //var component = await _context.logins.FirstOrDefaultAsync(m => m.UserID == Db.UserID && m.Password == Db.Password);
                    try
                    {
                        LdapDirectoryIdentifier ldapDir = new LdapDirectoryIdentifier(Configuration.GetSection("Appsetings").GetSection("ipaddress").Value, 389);
                        LdapConnection ldapConn = new LdapConnection(ldapDir);
                        ldapConn.AuthType = AuthType.Basic;
                        // System.Net.NetworkCredential myCredentials = new System.Net.NetworkCredential("uid=" + model.Username + ",ou=people, o = army.mil, dc=army,dc =mil", model.Password);
                        System.Net.NetworkCredential myCredentials = new System.Net.NetworkCredential("CN=" + Db.UserID + ", CN = Users, dc=LDAP,dc =COM", Db.Password);
                        ldapConn.Bind(myCredentials);

                        //var result = await _signInManager.PasswordSignInAsync(Db.UserID, Db.Password, Db.RememberLogin, lockoutOnFailure: true);
                        //if (result.Succeeded==true)
                        //{



                        //}

                        // Db.Id = _repository.GetById(Db.UserID);
                        Db.ComponentId = 1;

                        Db.Password = "";
                        _repository.Save(Db);

                        Db = _repository.GetByIdAll(Db.UserID);
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "User", Db);
                        return RedirectToAction("Index", "Home");

                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View("Index", Db);
                    }
                }
            }
    
            else
            {
                return View("Index", Db);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidateAsync(Login Db)
        {
            if (ModelState.IsValid)
            {
                if (Db.UserID == null && Db.Password == null)
                {
                    return NotFound();
                }
                if (Db.UserID == "Admin" && Db.Password == "123456")
                {
                    Db.RoleType = 1;
                    _repository.Save(Db);

                    SessionHelper.SetObjectAsJson(HttpContext.Session, "User", Db);
                    HttpContext.Session.SetInt32("roletype", Db.RoleType);
                    return RedirectToAction("Index", "Home");

                }
                if (Db.UserID == "CO" && Db.Password == "Sigmn@1911")
                {
                    Db.RoleType = 3;
                    _repository.Save(Db);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "User", Db);
                    HttpContext.Session.SetInt32("roletype", Db.RoleType);
                    return RedirectToAction("Dashboard", "Hrms");

                }

                if (Db.UserID == "hrms" && Db.Password == "Admin123!")
                {
                    Db.RoleType = 2;
                    _repository.Save(Db);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "User", Db);

                    HttpContext.Session.SetInt32("roletype", Db.RoleType);

                    //SessionHelper.SetObjectAsJson(HttpContext.Session, "roletype", Db.RoleType);
                    return RedirectToAction("Index", "Hrms");

                }
                if (Db.UserID == "iqmp" && Db.Password == "Admin123!")
                {
                    Db.RoleType = 4;
                    _repository.Save(Db);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "User", Db);

                    HttpContext.Session.SetInt32("roletype", Db.RoleType);

                    //SessionHelper.SetObjectAsJson(HttpContext.Session, "roletype", Db.RoleType);
                    return RedirectToAction("Index", "Hrms");

                }

                else
                {
                    try
                    {
                        var component = await _context.logins.FirstOrDefaultAsync(m => m.UserID == Db.UserID && m.Password == Db.Password);
                        
                        if(component != null && Db.UserID != "co" && Db.UserID != "HRMS" && Db.UserID != "IQMP")
                        {
                            Db.ComponentId = 1;

                            Db.Password = "";
                            //// _repository.Save(Db);

                            Db = _repository.GetByIdAll(Db.UserID);
                            SessionHelper.SetObjectAsJson(HttpContext.Session, "User", Db);
                            return RedirectToAction("Index", "Home");

                        }

                        //LdapDirectoryIdentifier ldapDir = new LdapDirectoryIdentifier(Configuration.GetSection("Appsetings").GetSection("ipaddress").Value, 389);
                        //LdapConnection ldapConn = new LdapConnection(ldapDir);
                        //ldapConn.AuthType = AuthType.Basic;
                        //// System.Net.NetworkCredential myCredentials = new System.Net.NetworkCredential("uid=" + model.Username + ",ou=people, o = army.mil, dc=army,dc =mil", model.Password);
                        //System.Net.NetworkCredential myCredentials = new System.Net.NetworkCredential("CN=" + Db.UserID + ", CN = Users, dc=LDAP,dc =COM", Db.Password);
                        //ldapConn.Bind(myCredentials);

                        //var result = await _signInManager.PasswordSignInAsync(Db.UserID, Db.Password, Db.RememberLogin, lockoutOnFailure: true);
                        //if (result.Succeeded==true)
                        //{
                        ViewBag.ErrorMessage = "Username Or Password is not correct";
                        return View("Index", Db);

                        //}

                        // Db.Id = _repository.GetById(Db.UserID);

                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View("Index", Db);
                    }
                }
            }

            else
            {
                return View("Index", Db);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ValidateAsyncLogin(Login Db)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _repository.Validate(Db);

                    if (user.RoleType == 1)
                    {
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "User", user);
                        HttpContext.Session.SetInt32("roletype", user.RoleType);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (user.RoleType == 5)
                    {
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "User", user);
                        HttpContext.Session.SetInt32("roletype", user.RoleType);
                        return RedirectToAction("Index", "Hrms");
                    }
                    else if(user.RoleType == 3)
                    {
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "User", user);
                        HttpContext.Session.SetInt32("roletype", user.RoleType);
                        return RedirectToAction("Dashboard", "Hrms");
                    }
                    else if (user.RoleType == 4)
                    {
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "User", user);
                        HttpContext.Session.SetInt32("roletype", user.RoleType);
                        return RedirectToAction("Index", "Hrms");
                    }
                    else
                    {
                        try
                        {
                            var component = await _context.logins.FirstOrDefaultAsync(m => m.UserID == Db.UserID && m.Password == Db.Password);

                            if (component != null && Db.UserID != "co" && Db.UserID != "HRMS" && Db.UserID != "IQMP")
                            {
                                Db.ComponentId = 1;

                                Db.Password = "";
                                Db = _repository.GetByIdAll(Db.UserID);
                                SessionHelper.SetObjectAsJson(HttpContext.Session, "User", Db);
                                return RedirectToAction("Index", "Home");

                            }
                           
                            ViewBag.ErrorMessage = "Username Or Password is not correct";
                            return View("Index", Db);
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError(string.Empty, ex.Message);
                            return View("Index", Db);
                        }
                    }
                     
                }
                else
                {
                    return View("Index", Db);
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, "Username Or Password Invalid");
                return View("Index", Db);
            }
            return View();
        }



        //public Login Login(string userName, string password)
        //{
        //    try
        //    {
        //        using (DirectoryEntry entry = new DirectoryEntry(config.Path, config.UserDomainName + "\\" + userName, password))
        //        {
        //            using (DirectorySearcher searcher = new DirectorySearcher(entry))
        //            {
        //                searcher.Filter = String.Format("({0}={1})", SAMAccountNameAttribute, userName);
        //                searcher.PropertiesToLoad.Add(DisplayNameAttribute);
        //                searcher.PropertiesToLoad.Add(SAMAccountNameAttribute);
        //                var result = searcher.FindOne();
        //                if (result != null)
        //                {
        //                    var displayName = result.Properties[DisplayNameAttribute];
        //                    var samAccountName = result.Properties[SAMAccountNameAttribute];

        //                    return new User
        //                    {
        //                        DisplayName = displayName == null || displayName.Count <= 0 ? null : displayName[0].ToString(),
        //                        UserName = samAccountName == null || samAccountName.Count <= 0 ? null : samAccountName[0].ToString()
        //                    };
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // if we get an error, it means we have a login failure.
        //        // Log specific exception
        //    }
        //    return null;
        //}
    }

}
