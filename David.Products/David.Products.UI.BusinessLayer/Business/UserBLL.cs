using David.Products.Common.Diagnostics;
using David.Products.Common.Helpers;
using David.Products.Common.Models;
using David.Products.Domain.Models;
using David.Products.UI.BusinessLayer.Helpers;
using System;
using System.Linq;

namespace David.Products.UI.BusinessLayer.Business
{
    public class UserBLL
    {
        public Response<User> Login(UserLoginRequest model)
        {
            Response<User> response = new Response<User> { IsSuccess = false };

            var apiResponse = ApiService.Post<Response<User>>(ManagmentHelper.GetBaseUrl(),
                    ManagmentHelper.GetBaseAPI(),
                    ManagmentHelper.GetKey("Proxy.User.Login"), string.Empty, model);

            //Response<User> userResponse = (Response<User>)response.Result;

            string randomNumber = Utility.GetNewNumber();
            string message = string.Empty;

            //Petición correcta.
            if (apiResponse.IsSuccess)
            {
                var resultApiResponse = (Response<User>)apiResponse.Result;
                if (resultApiResponse.IsSuccess)
                {
                    response.Result = resultApiResponse.Result;
                    if (response.Result != null)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        message = $"ShowError('{string.Format(ManagmentHelper.GetKey("User.Login.Invalid"))}');";
                        response.Message = new System.Collections.Generic.List<MessageResult> { new MessageResult { Message = message } };
                    }
                }
                else
                {
                    message = $"ShowError('{string.Format(ManagmentHelper.GetKey("User.Login.Invalid"), randomNumber)}');";
                    response.Message = new System.Collections.Generic.List<MessageResult> { new MessageResult { Message = message } };

                    ExceptionLogging.LogException(
                         new Exception(
                                 string.Format("Error al validar email de usuario, API Response: {0}",
                                 apiResponse.Message.Select(c => string.Format("{0}|", c.Message)).FirstOrDefault())),
                         SessionHelper.Id,
                         randomNumber);
                }
            }
            else
            {
                message = $"ShowError('{string.Format(ManagmentHelper.GetKey("User.Login.Message.Error"), randomNumber)}');";

                response.Message = new System.Collections.Generic.List<MessageResult> { new MessageResult { Message = message } };

                ExceptionLogging.LogException(
                    new Exception(
                            string.Format("Error al validar Email de usuario, API Response: {0}",
                            apiResponse.Message.Select(c => string.Format("{0}|", c.Message)).FirstOrDefault())),
                    SessionHelper.Id,
                    randomNumber);
            }
            return response;
        }
    }
}
