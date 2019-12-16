using David.Products.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.Domain
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ClaimAction> ClaimActions { get; set; }
        public virtual DbSet<ClaimType> ClaimTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Custumers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Audience> Audiences { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
    }
}
