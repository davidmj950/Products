using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace David.Products.Common.CustomExtensions
{
    public static class  StringExtensions
    {
        public static string FormatMessage(this string s)
        {
            return s.Replace("\r\n", " ");
        }

        public static TSelf TrimAllStrings<TSelf>(this TSelf obj)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

            if (obj == null)
            {
                return obj;
            }

            foreach (PropertyInfo p in obj.GetType().GetProperties(flags))
            {
                Type currentNodeType = p.PropertyType;
                if (currentNodeType == typeof(String))
                {
                    string currentValue = (string)p.GetValue(obj, null);
                    if (currentValue != null)
                    {
                        RegexOptions options = RegexOptions.None;
                        Regex regex = new Regex("[ ]{2,}", options);
                        p.SetValue(obj, regex.Replace(currentValue.Trim(), " "), null);
                    }
                }
                else if (!currentNodeType.IsGenericType && currentNodeType != typeof(object) && Type.GetTypeCode(currentNodeType) == TypeCode.Object)
                {
                    p.GetValue(obj, null).TrimAllStrings();
                }
            }

            return obj;
        }
    }
}
