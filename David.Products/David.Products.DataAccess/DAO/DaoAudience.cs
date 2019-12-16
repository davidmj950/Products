using David.Products.DataAccess.Interfaces;
using David.Products.Domain;
using David.Products.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.DataAccess.DAO
{
    public class DaoAudience : IAudience
    {
        private readonly ApplicationDataContext context;

        public DaoAudience()
        {
            context = new ApplicationDataContext();
        }
        public Audience GetAudienceByClientId(string clientId)
        {
            return (from audience in context.Audiences
                    where audience.ClientId == clientId
                    select audience).First();
        }

        public List<Audience> GetAudiences()
        {
            return (from audience in context.Audiences select audience).ToList();
        }
    }
}
