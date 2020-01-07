using David.Products.Business.Interfaces;
using David.Products.Common.Models;
using David.Products.DataAccess.DAO;
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
        private DataAccess.Interfaces.IUser daoUser;

        public BlUser()
        {
            daoUser = new DaoUser();
        }
        public User AuthenticateUser(string userName, string password, out HttpResponseMessage responseMessage)
        {
            var response = this.GetUserByLogin(userName, out responseMessage);
            responseMessage = new HttpResponseMessage();
            if (response != null && response.Id > 0)
            {
                response = daoUser.AuthenticateUser(userName, password);
                if (!(response != null && response.Id > 0))
                {
                    responseMessage.ReasonPhrase = Resources.Message_es.IncorrectPassword;
                    return response;
                }
                else if (!this.UserIsActive(userName))
                {
                    responseMessage.ReasonPhrase = Resources.Message_es.InactiveUser;
                    return response;
                }
                else
                {
                    return response;
                }
            }
            else
            {
                responseMessage.ReasonPhrase = Resources.Message_es.UserNotFound;
                return null;
            }
        }

        public Response<List<MaritalStatus>> GetmaritalStatusList()
        {
            Response<List<MaritalStatus>> response = new Response<List<MaritalStatus>>
            {
                IsSuccess = false
            };
            try
            {
                List<MaritalStatus> maritals = daoUser.GetmaritalStatusList();
                if (maritals == null)
                {
                    response.Message.Add(new MessageResult
                    {
                        Message = "No se pudo cargar la lista"
                    });
                }
                else
                {
                    response.IsSuccess = true;
                    response.Result = maritals;
                }
                
            }
            catch (Exception ex)
            {
                response.Message.Add(new MessageResult
                {
                    Message = $"Ha ocurrido un error en el proceso, Descripción: {ex.Message}"
                });
            }
            return response;
        }

        /// <summary>
        /// Get user object by username
        /// </summary>
        /// <param name="username">user name parameter</param>
        /// <param name="responseMessage"></param>
        /// <returns>user object by username</returns>
        public User GetUserByLogin(string username, out HttpResponseMessage responseMessage)
        {
            responseMessage = new HttpResponseMessage();
            var response = daoUser.GetUserByLogin(username);
            if (!(response != null && response.Id > 0))
            {
                responseMessage.ReasonPhrase = Resources.Message_es.UserNotFound;
            }
            return response;
        }

        public bool SaveAuditLoginUser(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUserAttemps(User userLogin, out HttpResponseMessage responseMessage)
        {
            throw new NotImplementedException();
        }

        public bool UserIsActive(string username)
        {
            return daoUser.UserIsActive(username);
        }
    }
}
