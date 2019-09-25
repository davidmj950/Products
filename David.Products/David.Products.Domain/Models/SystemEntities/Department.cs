using David.Products.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace David.Products.Domain.Models
{
    public class Department : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string DaneCode { get; set; }
        #region Virtuals
        public virtual ICollection<City> Cities { get; set; } 
        #endregion
        public DateTime LastUpdate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
    }
}
