using David.Products.Business.Interfaces;
using David.Products.Domain.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Tokens;

namespace David.Products.Business.BL
{
    public class BlCustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string issuerGenerated;
        private IAudience blAUdience = null;
        private const string AudiencePropertyKey = "audience";

        /// <summary>
        /// 
        /// </summary>
        public BlCustomJwtFormat(string issuer)
        {
            issuerGenerated = issuer;
        }

        /// Generated the token in format JWT
        /// </summary>
        /// <param name="data">Data for generated token</param>
        /// <returns>Token generated</returns>
        /// <author> Natalia Ladino - naladino@personalsoft.com.co </author>
        public string Protect(AuthenticationTicket data)
        {
            string audienceId = data.Properties.Dictionary.ContainsKey(AudiencePropertyKey) ? data.Properties.Dictionary[AudiencePropertyKey] : null;

            if (string.IsNullOrWhiteSpace(audienceId))
            {
                throw new InvalidOperationException(Resources.Message_es.InvalidAudience);
            }

            blAUdience = new BlAudience();
            Audience audience = blAUdience.GetAudienceByClientId(audienceId);

            if (!string.IsNullOrEmpty(audience.Secret))
            {
                string symmetricKeyAsBase64 = audience.Secret;
                var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);
                var signingKey = new  HmacSigningCredentials(keyByteArray);
                var issued = data.Properties.IssuedUtc;
                var expires = data.Properties.ExpiresUtc;

                var token = new JwtSecurityToken(issuerGenerated, audienceId, data.Identity.Claims, issued.Value.LocalDateTime, expires.Value.LocalDateTime, signingKey);

                var handler = new JwtSecurityTokenHandler();

                var jwt = handler.WriteToken(token);

                return jwt;
            }
            else
            {
                throw new InvalidOperationException(Resources.Message_es.ExceptionSecret);
            }
        }

        /// <summary>
        /// Unprotect a text
        /// </summary>
        /// <param name="protectedText">Text Protected</param>
        /// <returns>New exception</returns>
        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}
