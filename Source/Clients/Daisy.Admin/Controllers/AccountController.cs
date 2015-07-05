using Daisy.Admin.Models;
using Daisy.Common.Extensions;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Daisy.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [AllowAnonymous]
        public ActionResult LogOn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool isValid = authenticationService.ValidateUser(model.Username, model.Password);
                if (isValid)
                {
                    int cookieExpiration;
                    int.TryParse(ConfigurationManager.AppSettings["CookieExpiration"], out cookieExpiration);
                    if (cookieExpiration <= 0)
                    {
                        cookieExpiration = 30;
                    }

                    var authTicket = new FormsAuthenticationTicket(
                        1,
                        model.Username,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(cookieExpiration),
                        model.RememberMe,
                        "");

                    string encrypedTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypedTicket);
                    if (authTicket.IsPersistent)
                    {
                        faCookie.Expires = DateTime.MaxValue;
                    }
                    Response.Cookies.Add(faCookie);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Incorrect username and/or password");
            }

            return View(model);
        }
    }
}