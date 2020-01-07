using David.Products.Common.Models;
using David.Products.Domain.Models;
using David.Products.UI.BusinessLayer.Business;
using David.Products.UI.BusinessLayer.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace David.Products.UI.Controllers
{
    public class AccountController : Controller
    {
        public UserBLL userBLL = new UserBLL();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserLoginRequest model)
        {
            model.Recaptcha = Request["g-recaptcha-response"];
            if (string.IsNullOrEmpty(model.Recaptcha))
            {
                ViewBag.ScriptUp = $"ShowError('{ConfigurationManager.AppSettings.Get("Recaptcha.Empty")}');";
                return View(model);
            }
            
            if (ModelState.IsValid)
            {
                var recaptchaResponse = userBLL.ValidateCaptcha(model.Recaptcha);
                if (!(recaptchaResponse.IsSuccess && recaptchaResponse.Result))
                {
                    ViewBag.ScriptUp = $"ShowError('{ConfigurationManager.AppSettings.Get("Recaptcha.Invalid")}');";
                    return View(model);
                }
                Response<User> response = userBLL.Login(model);
                if (response.Message.Count > 0)
                {
                    ViewBag.ScriptUp = response.Message.FirstOrDefault().Message;
                }
                if (response.IsSuccess && response.Result != null)
                {
                    FormsAuthentication.SignOut();
                    string userName = Convert.ToString(response.Result.Id);
                    SessionHelper.Id = Convert.ToString(response.Result.Id);
                    SessionHelper.Role = response.Result.Role;
                    SessionHelper.Name = response.Result.UserName;

                    //Set the Form Aunthentication Cookie
                    FormsAuthentication.SetAuthCookie(userName, true);
                    return RedirectToAction("Index", "Home");
                }
            }
            //return RedirectToAction("Index", "Account", new { ViewBag.ScriptUp } );
            return View(model);
        }

        public ActionResult Register()
        {
            Common.Models.Response<List<MaritalStatus>> response = userBLL.GetMaritalStatus();
            if (response.IsSuccess)
            {
                ViewBag.MaritalStatusList = response.Result;
            }
            else
            {
                ViewBag.ScriptUp = response.Message.FirstOrDefault();
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
    }
}