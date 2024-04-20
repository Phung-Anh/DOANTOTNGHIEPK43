﻿using DOANTOTNGHIEPK43.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DOANTOTNGHIEPK43.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Edit( string id)
        {
            var item = UserManager.FindById(id);
            var newUser = new EditAccountViewModel();
            if(item != null)
            {
                var rolesForUser = UserManager.GetRoles(id);
                var roles = new List<string>();
                if (rolesForUser != null)
                {

                    foreach (var role in rolesForUser)
                    {
                        roles.Add(role);
                    }
                }
                newUser.FullName = item.Fullname;
                newUser.Email = item.Email;
                newUser.Phone = item.Phone;
                newUser.UserName = item.UserName;
                newUser.Role = roles;


            }
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View(newUser);
        }
        [HttpPost]
        public async Task<ActionResult> DeletedAccount(string user, string id)
        {
            var code = new { Success = false };
        
            var item = UserManager.FindByName(user);
            if(item != null)
            {
                var rolesForUser = UserManager.GetRoles(id);
                if(rolesForUser != null) {

                    foreach (var role in rolesForUser)
                    {
                        await UserManager.RemoveFromRolesAsync(id, role);
                    }
                }
               
                var res = await UserManager.DeleteAsync(item);
                code = new { Success = true };
            }
            return Json(code);
            }
        // GET: Admin/Account
        public ActionResult Index()
        {
            var items = db.Users.ToList();
            return View(items);
        }
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.Role =  new SelectList(db.Roles.ToList(),"Name","Name") ;
            return View();
        }
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Bạn không có quyền");
                    return View(model);
            }
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email , Fullname = model.FullName ,Phone=model.Phone };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if(model.Role != null)
                    {
                        foreach (var r in model.Role)
                        {
                            UserManager.AddToRole(user.Id, r);
                        }
                    }
                    //UserManager.AddToRole(user.Id, model.Role);
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Account");
                }
                AddErrors(result);
            }
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindByName(model.UserName);
                user.Fullname = model.FullName;
                user.Phone = model.Phone;
                user.Email = model.Email;
                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var rolesForUser = UserManager.GetRoles(user.Id);
                    if (model.Role != null)
                    {
                        foreach (var r in model.Role)
                        {
                            var checkRole = rolesForUser.FirstOrDefault(x => x.Equals(r));
                            if(checkRole == null)
                            {
                                UserManager.AddToRole(user.Id, r);
                            }
                            
                        }
                    }
                    //UserManager.AddToRole(user.Id, model.Role);
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Account");
                }
                AddErrors(result);
            }
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
                
            }
            return RedirectToAction("Index", "Home");
        }


    }
}