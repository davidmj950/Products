using David.Products.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.Business.Interfaces
{
    public interface IAudience
    {
        /// <summary>
        /// Get list of Audience from database
        /// </summary>
        /// <returns></returns>
        List<Audience> GetAudiences();
        /// <summary>
        /// Get a Audience object by client id
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Audience GetAudienceByClientId(string clientId);
    }
}
