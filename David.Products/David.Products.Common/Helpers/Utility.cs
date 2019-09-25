using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.Common.Helpers
{
    public class Utility
    {
        public static string GetNewNumber()
        {
            object _Lock = new object();
            lock (_Lock)
            {
                System.Threading.Thread.Sleep(50);
                DateTime startDate = new DateTime(1970, 1, 1);
                long tmpTicks = DateTime.Now.Ticks - startDate.Ticks;
                TimeSpan span = new TimeSpan(tmpTicks);
                long tmpId = (long)span.TotalMilliseconds;
                return tmpId.ToString().Substring(1);
            }
        }
    }
}
