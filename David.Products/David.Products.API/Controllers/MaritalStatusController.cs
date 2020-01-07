using David.Products.API.Models;
using David.Products.Business.BL;
using David.Products.Business.Interfaces;
using David.Products.Common.Models;
using David.Products.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace David.Products.API.Controllers
{
    public class MaritalStatusController : ApiController
    {
        private readonly IUser userBL;

        public MaritalStatusController()
        {
            userBL = new BlUser();
        }

        /// <summary>
        /// Return Marital status list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MaritalStatus")]
        [ResponseType(typeof(Response<List<MaritalStatus>>))]
        public IHttpActionResult GetMaritalStatus()
        {
            Response <List <MaritalStatus>> response;
            response = userBL.GetmaritalStatusList();
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
