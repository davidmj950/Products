using David.Products.Business.Interfaces;
using David.Products.DataAccess.DAO;
using David.Products.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dataAccess = David.Products.DataAccess.Interfaces;

namespace David.Products.Business.BL
{
    public class BlAudience : IAudience
    {
        private readonly dataAccess.IAudience daoAudience;

        public BlAudience()
        {
            daoAudience = new DaoAudience();
        }

        public Audience GetAudienceByClientId(string clientId)
        {
            return daoAudience.GetAudienceByClientId(clientId);
        }

        public List<Audience> GetAudiences()
        {
            throw new NotImplementedException();
        }
    }
}
