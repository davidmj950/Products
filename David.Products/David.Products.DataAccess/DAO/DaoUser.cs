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
    public class DaoUser : IUser
    {
        private ApplicationDataContext context;

        public DaoUser()
        {
            context = new ApplicationDataContext();
        }

        public User AuthenticateUser(string username, string password)
        {
            User model = null;
            using (context)
            {
                model = (from users in context.Users
                         where users.UserName.Equals(username) && users.Password.Equals(password)
                         select users).FirstOrDefault();
            }
            return model;
        }

        public List<MaritalStatus> GetmaritalStatusList()
        {
            List<MaritalStatus> maritalStatuses = null;
            using (context)
            {
                maritalStatuses = (from maritals in context.MaritalStatuses
                                   where maritals.Active
                                   select maritals).ToList();
            }
            return maritalStatuses;
        }

        public User GetUserByLogin(string username)
        {
            User model = null;
            using (context)
            {
                model = (from users in context.Users
                         where users.UserName.Equals(username)
                         select users).FirstOrDefault();
            }
            return model;
        }

        public bool UserIsActive(string username)
        {
            User model = null;
            using (context)
            {
                model = (from users in context.Users
                         where users.UserName.Equals(username)
                         select users).FirstOrDefault();
            }
            return model.Active;
        }
    }
}
