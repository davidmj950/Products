using David.Products.Business.Interfaces;
using David.Products.Common.Constants;
using David.Products.Common.Diagnostics;
using David.Products.Common.Enums;
using David.Products.Domain.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.Business.BL
{
    public class BlCustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        private IAudience blAUdience = null;
        private IParameter blParameter = null;

        /// <summary>
        /// Validate the request
        /// </summary>
        /// <param name="context">Context of the request</param>
        /// <returns>Request validated</returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            try
            {
                blAUdience = new BlAudience();

                string clientId = string.Empty;
                string clientSecret = string.Empty;

                if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
                {
                    context.TryGetFormCredentials(out clientId, out clientSecret);
                }

                if (context.ClientId == null)
                {
                    context.SetError("ClienteId_Invalido", Resources.Message_es.InvalidClient);
                    return Task.FromResult<object>(null);
                }

                Audience audience = blAUdience.GetAudienceByClientId(clientId);

                if (audience == null)
                {
                    context.SetError("ClienteId_Invalido", string.Format(Resources.Message_es.InvalidClient, context.ClientId));
                    return Task.FromResult<object>(null);
                }

                context.Validated();
                return Task.FromResult<object>(null);
            }
            catch (Exception ex)
            {
                ExceptionLogging.LogException(ex);
                return Task.FromResult<object>(null);
            }
        }

        /// <summary>
        /// Validate the user in the database and generate the token
        /// </summary>
        /// <param name="context">context of the request</param>
        /// <returns>Token generated</returns>
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            try
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" }); //Habilita CORS(Peticiones de origen cruzado) Para la generación del token.

                IUser blUser = new BlUser();

                if (!string.IsNullOrEmpty(context.UserName)
                    && !string.IsNullOrEmpty(context.Password))
                {
                    User userLogin = blUser.GetUserByLogin(context.UserName, out responseMessage);

                    if (userLogin != null)
                    {
                        return ValidateAndGenerate(context, ref responseMessage, blUser, userLogin);
                    }
                    else
                    {
                        Task messageUserNameNotFound = ResponseMessage(context, Resources.Message_es.NotFoundLogin, "UsuarioNoEncontrado");
                        return messageUserNameNotFound;
                    }
                }
                else
                {
                    Task messageUserPasswordInvalid = ValidateUserPassword(context);
                    return messageUserPasswordInvalid;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.LogException(ex);
                responseMessage.Content = new StringContent(Resources.Message_es.JWT);
                responseMessage.ReasonPhrase = ex.Message;
                responseMessage.StatusCode = HttpStatusCode.Forbidden;
                return Task.FromResult<object>(null);
            }
        }

        private Task ValidateAndGenerate(OAuthGrantResourceOwnerCredentialsContext context, ref HttpResponseMessage responseMessage, IUser blUser, User userLogin)
        {
            if (userLogin.Active == false)
            {
                return ResponseMessage(context, Resources.Message_es.InactiveUser, "UsuarioInactivo");
            }


            blParameter = new BlParameter();
            HttpResponseMessage httpResponse;
            var parameters = blParameter.GetParametersByType(Parameters.PASSWORDATTEMPS.ToString(), out httpResponse).FirstOrDefault();
            int NumberOfAttemps = 0;

            if (parameters != null && !string.IsNullOrEmpty(parameters.Value))
            {
                NumberOfAttemps = int.Parse(parameters.Value);
            }

            User user = blUser.AuthenticateUser(context.UserName, context.Password, out responseMessage);
            if (user == null || user.Id == 0 || user.NumberOfAttemps >= NumberOfAttemps)
            {
                if (userLogin.NumberOfAttemps >= NumberOfAttemps)
                {
                    Task messagePasswordBlock = BlockPasswordUser(context);
                    return messagePasswordBlock;
                }
                else
                {
                    Task messagePasswordIncorrect = UpdateNumberAttempsUser(context, userLogin);
                    return messagePasswordIncorrect;
                }
            }
            else
            {
                Task jwt = GenerateJwt(context, user);
                return jwt;
            }
        }

        /// <summary>
        /// Update number attemps of login of a user
        /// </summary>
        /// <param name="context">Context of request</param>
        /// <param name="userLogin">Object user</param>
        /// <returns>Context message error</returns>
        private Task UpdateNumberAttempsUser(OAuthGrantResourceOwnerCredentialsContext context, User userLogin)
        {
            HttpResponseMessage responseMessage;
            IUser blUser = new BlUser();
            userLogin.NumberOfAttemps += 1;
            blUser.UpdateUserAttemps(userLogin, out responseMessage);

            context.SetError("ContraseñaIncorrecta", Resources.Message_es.IncorrectPassword);
            context.Response.Headers.Add(Constants.OwinChallengeFlag, new[] { ((int)HttpStatusCode.Unauthorized).ToString() }); //Little trick to get this to throw 401, refer to AuthenticationMiddleware for more
            return Task.FromResult<object>(context);
        }

        /// <summary>
        /// Generate claims identity for token
        /// </summary>
        /// <param name="context">Context of request</param>
        /// <param name="user">Object user</param>
        /// <returns>Identity generated</returns>
        private Task GenerateJwt(OAuthGrantResourceOwnerCredentialsContext context, User user)
        {
            HttpResponseMessage responseMessage;
            IUser blUser = new BlUser();

            user.NumberOfAttemps = 0;
            blUser.UpdateUserAttemps(user, out responseMessage);
            blUser.SaveAuditLoginUser(user.Id);
            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("UserName", context.UserName));
            identity.AddClaim(new Claim("Id", user.Id.ToString()));

            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                {
                    "audience", (context.ClientId == null) ? string.Empty : context.ClientId
                },
                {
                    "userName", context.UserName
                },
            });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            return Task.FromResult<object>(context);
        }

        /// <summary>
        /// Block the user password
        /// </summary>
        /// <param name="context">Context of the request</param>
        /// <returns>Context message error</returns>
        private Task BlockPasswordUser(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.SetError("UsuarioBloqueado", Resources.Message_es.BlockPassword);
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Validate the userName and password
        /// </summary>
        /// <param name="context">Context of the request</param>
        /// <returns>Context message error</returns>
        private Task ValidateUserPassword(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (context.UserName == null)
            {
                context.SetError("UsuarioInvalido", Resources.Message_es.RequiredUser);
                context.Response.Headers.Add(Constants.OwinChallengeFlag, new[] { ((int)HttpStatusCode.Unauthorized).ToString() });
                return Task.FromResult<object>(null);
            }
            else
            {
                context.SetError("ContraseñaInvalida", Resources.Message_es.RequiredPassword);
                context.Response.Headers.Add(Constants.OwinChallengeFlag, new[] { ((int)HttpStatusCode.Unauthorized).ToString() });
                return Task.FromResult<object>(null);
            }
        }

        /// <summary>
        /// Message response
        /// </summary>
        /// <param name="context">Context of the request</param>
        /// <returns>Context message error</returns>
        private Task ResponseMessage(OAuthGrantResourceOwnerCredentialsContext context, string message, string type)
        {
            context.SetError(type, message);
            context.Response.Headers.Add(Constants.OwinChallengeFlag, new[] { ((int)HttpStatusCode.Unauthorized).ToString() }); //Little trick to get this to throw 401, refer to AuthenticationMiddleware for more

            return Task.FromResult<object>(null);
        }
    }
}
