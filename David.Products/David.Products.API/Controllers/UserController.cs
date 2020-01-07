using David.Products.API.Models;
using David.Products.Common.Diagnostics;
using David.Products.Common.Helpers;
using David.Products.Common.Models;
using David.Products.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace David.Products.API.Controllers
{
    //[Route("api/[Controller]")]
    //[Authorize]
    public class UserController : ApiController
    {
        private DataContextLocal context = new DataContextLocal();

        [Route("api/User/Login")]
        [HttpPost]
        [ResponseType(typeof(Response<User>))]
        public IHttpActionResult Login(UserLoginRequest model)
        {
            Response<User> response = new Response<User>
            {
                IsSuccess = false,
            };
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        IQueryable<User> queryable = context.Set<User>().Where(c => c.Email == model.Email && c.Password == model.Password && c.Active == true).AsNoTracking();
                        queryable = queryable.Include<User, object>(c => c.Role);
                        User myUser = queryable.FirstOrDefault();

                        if (myUser != null)
                        {
                            myUser.Role.ClaimActions = context.Set<ClaimAction>().Where(c => c.RoleId == myUser.RoleId).ToList();
                            response.Result = myUser;
                            response.IsSuccess = true;
                        }
                        else
                        {
                            response.Message.Add(new MessageResult { Message = "Usuario y/o contraseña no válido" });
                        }
                    }
                    catch (Exception ex)
                    {
                        response =  ResponseHelper<User>.ExceptionDatabase(ex, "Select", User.GetType().FullName, null);
                    }
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                response.Message.Add(new MessageResult { Message = ex.Message });
                return Ok(response);
            }
        }

        [Route("api/Users/Recaptcha")]
        [HttpPost]
        [ResponseType(typeof(Response<bool>))]
        public IHttpActionResult ValidateRecaptcha([FromBody]string token)
        {
            Response<bool> response = new Response<bool>
            {
                IsSuccess = false
            };
            bool isHuman = false;
            try
            {
                string secretKey = ConfigurationManager.AppSettings["reCaptchaPrivateKey"];
                Uri uri = new Uri("https://www.google.com/recaptcha/api/siteverify" +
                                  $"?secret={secretKey}&response={token}");
                HttpWebRequest request = WebRequest.CreateHttp(uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = 0;
                HttpWebResponse recaptchaResponse = (HttpWebResponse)request.GetResponse();
                Stream responseStream = recaptchaResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                string result = streamReader.ReadToEnd();
                ReCaptchaResponse reCaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(result);
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
            return Ok(response);
        }
    }
}
