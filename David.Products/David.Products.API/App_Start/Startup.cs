using David.Products.Business.BL;
using David.Products.Business.Interfaces;
using David.Products.Domain.Models;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;

[assembly: OwinStartup(typeof(David.Products.API.Startup))]
namespace David.Products.API
{
    /// <summary>
    /// Owin Startup specify components for the application requests.
    /// </summary>
    public class Startup
    {
        private IAudience blAUdience = null;
        /// <summary>
        ///  Configuration of how respond to the application on each HTTP request
        /// </summary>
        /// <param name="app">Param of current context</param>
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            // Web API routes
            config.MapHttpAttributeRoutes();

            ConfigureOAuth(app);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        /// <summary>
        /// Configuration of server of authorization
        /// </summary>
        /// <param name="app">Param of current context</param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            blAUdience = new BlAudience();
            List<string> allowedAudienceIds = new List<string>();
            List<IIssuerSecurityTokenProvider> providers = new List<IIssuerSecurityTokenProvider>();

            var issuer = ConfigurationManager.AppSettings["issuer"];
            List<Audience> AudiencesList = blAUdience.GetAudiences();

            app.Use<BlAuthenticationMiddleware>(); //Allows override of Invoke OWIN commands ////El objetivo de esta petición es sobreescribir la petición para
                                                   //// invocar el negocio BlAuthenticationMiddleware y retornar el codigo de no autorizado en la generación del token.

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = bool.Parse(ConfigurationManager.AppSettings["allowInsecureHttp"]),
                TokenEndpointPath = new PathString(ConfigurationManager.AppSettings["pathToken"]),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToInt16(ConfigurationManager.AppSettings["expiredToken"])),
                Provider = new BlCustomOAuthProvider(),
                AccessTokenFormat = new BlCustomJwtFormat(issuer)
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions); //// El objetivo de esta instrucción es configurar el servidor de autorización.

            foreach (var validAudience in AudiencesList)
            {
                allowedAudienceIds.Add(validAudience.ClientId);
                providers.Add(new SymmetricKeyIssuerSecurityTokenProvider(issuer, TextEncodings.Base64Url.Decode(validAudience.Secret)));
            }

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
              new JwtBearerAuthenticationOptions
              {
                  AuthenticationMode = AuthenticationMode.Active,
                  AllowedAudiences = allowedAudienceIds.ToArray(),
                  IssuerSecurityTokenProviders = providers.ToArray()
              });
        }
    }
}