using David.Products.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.Business.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="usernamme"></param>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        /// <author>Jose David Morales</author>
        User GetUserByLogin(string usernamme, out HttpResponseMessage responseMessage);

        /// <summary>
        /// Autenticate user credecials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        User AuthenticateUser(string userName, string password, out HttpResponseMessage responseMessage);

        /// <summary>
        /// update number of attempts to login 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        bool UpdateUserAttemps(User userLogin, out HttpResponseMessage responseMessage);
        /// <summary>
        /// Save the audit login user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool SaveAuditLoginUser(int id);
    }
}
