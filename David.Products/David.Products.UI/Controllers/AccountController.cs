using David.Products.Common.Models;
using David.Products.Domain.Models;
using David.Products.UI.BusinessLayer.Business;
using David.Products.UI.BusinessLayer.Helpers;
using System;
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
            if (ModelState.IsValid)
            {
                Response<User> response = userBLL.Login(model);
                if (response.Message.Count > 0)
                {
                    ViewBag.ScriptUp = response.Message[0].Message;
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
            return View("Index");
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}