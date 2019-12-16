using David.Products.Common.CustomExtensions;
using David.Products.Common.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace David.Products.API.CustomAttributes
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string exceptionMessage = string.Empty;
            if (actionExecutedContext.Exception.InnerException == null)
            {
                exceptionMessage = actionExecutedContext.Exception.Message.FormatMessage();
            }
            else
            {
                exceptionMessage = actionExecutedContext.Exception.InnerException.Message.FormatMessage();
            }

            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Forbidden,
                Content = new StringContent(exceptionMessage),
                ReasonPhrase = exceptionMessage
            };

            if (!string.IsNullOrEmpty(exceptionMessage))
            {
                ExceptionLogging.LogException(actionExecutedContext.Exception);
            }

            actionExecutedContext.Response = response;
        }
    }
}