using BotDetect.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace David.Products.API.Controllers
{
    public class AccountController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        public IHttpActionResult ValidateCaptcha(string user, string userId)
        {
            if (ModelState.IsValid)
            {
                SimpleCaptcha captchaCode = new SimpleCaptcha();

                bool isHuman = captchaCode.Validate(user, userId);
                if (isHuman)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
