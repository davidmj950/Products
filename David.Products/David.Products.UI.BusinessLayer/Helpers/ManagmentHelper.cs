using System;
using System.Configuration;

namespace David.Products.UI.BusinessLayer.Helpers
{
    public class ManagmentHelper
    {
        public static string GetKey(string key) => Convert.ToString(ConfigurationManager.AppSettings[key]);
        public static string GetBaseUrl() => Convert.ToString(ConfigurationManager.AppSettings["BaseUrl"]);
        public static string GetBaseAPI() => Convert.ToString(ConfigurationManager.AppSettings["BaseAPI"]);
        public static string GetPageSize() => Convert.ToString(ConfigurationManager.AppSettings["PageSize"]);
    }
}
