using David.Products.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.DataAccess.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// Autenticate user credecials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        User AuthenticateUser(string username, string password);
        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="usernamme"></param>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        /// <author>Jose David Morales</author>
        User GetUserByLogin(string username);
        /// <summary>
        /// Is user active
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool UserIsActive(string username);
        /// <summary>
        /// get a list of marital status
        /// </summary>
        /// <returns></returns>
        List<MaritalStatus> GetmaritalStatusList();
    }
}
