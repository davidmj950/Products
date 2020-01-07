using David.Products.Common.Models;
using David.Products.Domain.Models;
using David.Products.UI.BusinessLayer.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace David.Products.UI.Controllers
{
    public class UserController : Controller
    {
        private UserBLL userBLL;

        public UserController()
        {
            userBLL = new UserBLL();
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult _Users()
        {
            var response = userBLL.PaginatorFilter(new PaginatorViewModel<User>());
            return PartialView("~/Views/User/Partials/_Users.cshtml", response);
        }
    }
}