using David.Products.Business.Interfaces;
using David.Products.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.Business.BL
{
    public class BlUser : IUser
    {
        public User AuthenticateUser(string userName, string password, out HttpResponseMessage responseMessage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get user object by username
        /// </summary>
        /// <param name="usernamme">user name parameter</param>
        /// <param name="responseMessage"></param>
        /// <returns>user object by username</returns>
        public User GetUserByLogin(string usernamme, out HttpResponseMessage responseMessage)
        {
            throw new NotImplementedException();
        }

        public bool SaveAuditLoginUser(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUserAttemps(User userLogin, out HttpResponseMessage responseMessage)
        {
            throw new NotImplementedException();
        }
    }
}
