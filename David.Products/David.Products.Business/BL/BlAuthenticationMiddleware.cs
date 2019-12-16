using David.Products.Common.Constants;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.Business.BL
{
    public class BlAuthenticationMiddleware : OwinMiddleware
    {
        public BlAuthenticationMiddleware(OwinMiddleware next) : base(next)
        {
        }

        /// <summary>
        /// Override requests owin
        /// </summary>
        /// <param name="context">Owin Context</param>
        /// <returns>Context response header</returns>
        public override async Task Invoke(IOwinContext context)
        {
            await Next.Invoke(context);

            if (context.Response.StatusCode == 400 && context.Response.Headers.ContainsKey(Constants.OwinChallengeFlag))
            {
                var headerValues = context.Response.Headers.GetValues(Constants.OwinChallengeFlag);
                context.Response.StatusCode = Convert.ToInt16(headerValues.FirstOrDefault());
                context.Response.Headers.Remove(Constants.OwinChallengeFlag);
            }
        }
    }
}
