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
            Audience aud = null;
            using (context)
            {
                aud = (from audience in context.Audiences
                       where audience.ClientId.Contains(clientId) && audience.Active
                       select audience).FirstOrDefault();
            }
            return aud;
        }

        public List<Audience> GetAudiences()
        {
            List<Audience> audiences = null;
            using (context)
            {
                audiences = (from audience in context.Audiences select audience).ToList();
            }
                return audiences;
        }
    }
}
