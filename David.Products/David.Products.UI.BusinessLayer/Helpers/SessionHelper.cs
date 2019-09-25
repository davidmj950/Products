using David.Products.Domain.Models;
using System.Web;
using System.Web.Security;

namespace David.Products.UI.BusinessLayer.Helpers
{
    public class SessionHelper
    {
        public static Role Role
        {
            get => (HttpContext.Current.Session["Role"] == null) ? null : (Role)(HttpContext.Current.Session["Role"]);
            set => HttpContext.Current.Session["Role"] = value;
        }
        public static string Name
        {
            get => (HttpContext.Current.Session["Name"] == null) ? null : (string)(HttpContext.Current.Session["Name"]);
            set => HttpContext.Current.Session["Name"] = value;
        }
        public static string Id
        {
            get => (HttpContext.Current.Session["Id"] == null) ? null : (string)(HttpContext.Current.Session["Id"]);
            set => HttpContext.Current.Session["Id"] = value;
        }

        public static void LogOut()
        {
            Role = null;
            Name = null;
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }
    }
}
