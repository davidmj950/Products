using David.Products.Common.Diagnostics;
using David.Products.Common.Helpers;
using David.Products.Common.Models;
using David.Products.Domain.Models;
using David.Products.UI.BusinessLayer.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;

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

        public Response<PaginatorViewModel<User>> PaginatorFilter(PaginatorViewModel<User> paginatorViewModel)
        {
            throw new NotImplementedException();
        }

        public Response<bool> ValidateCaptcha(string recaptcha)
        {
            Response<bool> response = new Response<bool>
            {
                IsSuccess = false,
            };
            //Response<object> apiResponse = ApiService.Post<Response<bool>>(ManagmentHelper.GetBaseUrl(),
            //    ManagmentHelper.GetBaseAPI(),
            //    ManagmentHelper.GetKey("Proxy.ValidateRecaptcha"), null, recaptcha);
            //string randomNumber = Utility.GetNewNumber();
            //string message = string.Empty;
            //if (apiResponse.IsSuccess)
            //{
            //    response = (Response<bool>)apiResponse.Result;
            //    if (!response.IsSuccess)
            //    {
            //        message = $"ShowError('{string.Format(ManagmentHelper.GetKey("MaritalStatus.Get.Message.Error"))}');";
            //        response.Message = new List<MessageResult> { new MessageResult { Message = message } };
            //    }
            //}
            //else
            //{
            //    message = $"ShowError('{string.Format(ManagmentHelper.GetKey("ApiService.Message.Error"), randomNumber)}');";

            //    response.Message = new List<MessageResult> { new MessageResult { Message = message } };

            //    ExceptionLogging.LogException(
            //        new Exception(
            //                string.Format("There was an error to trying load Marital Statuses of the user, API Response: {0}",
            //                apiResponse.Message.Select(c => string.Format("{0}|", c.Message)).FirstOrDefault())),
            //        SessionHelper.Id,
            //        randomNumber);
            //}
            //return response;
            bool isHuman = false;
            try
            {
                string secretKey = ManagmentHelper.GetKey("reCaptchaPrivateKey");
                Uri uri = new Uri("https://www.google.com/recaptcha/api/siteverify" +
                                  $"?secret={secretKey}&response={recaptcha}");
                HttpWebRequest request = WebRequest.CreateHttp(uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = 0;
                HttpWebResponse recaptchaResponse = (HttpWebResponse)request.GetResponse();
                Stream responseStream = recaptchaResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                string result = streamReader.ReadToEnd();
                var serializer = new JavaScriptSerializer();
                //ReCaptchaResponse reCaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(result);
                ReCaptchaResponse reCaptchaResponse = serializer.Deserialize<ReCaptchaResponse>(result);
                isHuman = reCaptchaResponse.Success;
                response.IsSuccess = true;
                response.Result = isHuman;
            }
            catch (Exception ex)
            {
                response.Message.Add(new MessageResult
                {
                    Message = ex.Message
                });
            }
            return response;
        }

        public dynamic GetMaritalStatus()
        {
            Response<List<MaritalStatus>> maritalStatusesR = new Response<List<MaritalStatus>>
            {
                IsSuccess = false
            };
            Response<object> apiResponse = ApiService.Get<Response<List<MaritalStatus>>>(ManagmentHelper.GetBaseUrl(),
                ManagmentHelper.GetBaseAPI(),
                ManagmentHelper.GetKey("Proxy.MaritalStatus.Get"), string.Empty);

            string randomNumber = Utility.GetNewNumber();
            string message = string.Empty;
            if (apiResponse.IsSuccess)
            {
                maritalStatusesR = (Response<List<MaritalStatus>>)apiResponse.Result;
                if (!maritalStatusesR.IsSuccess)
                {
                    message = $"ShowError('{string.Format(ManagmentHelper.GetKey("MaritalStatus.Get.Message.Error"))}');";
                    maritalStatusesR.Message = new List<MessageResult> { new MessageResult { Message = message } };
                }
            }
            else
            {
                message = $"ShowError('{string.Format(ManagmentHelper.GetKey("ApiService.Message.Error"), randomNumber)}');";

                maritalStatusesR.Message = new List<MessageResult> { new MessageResult { Message = message } };

                ExceptionLogging.LogException(
                    new Exception(
                            string.Format("There was an error to trying load Marital Statuses of the user, API Response: {0}",
                            apiResponse.Message.Select(c => string.Format("{0}|", c.Message)).FirstOrDefault())),
                    SessionHelper.Id,
                    randomNumber);
            }
            return maritalStatusesR;
        }
    }
}
