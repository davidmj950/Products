using David.Products.Domain.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace David.Products.Domain.Models
{
    public class ClaimAction : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int RoleId { get; set; }
        #region Virtuals
        [JsonProperty("Role")]
        public virtual Role Role { get; set; }
        public virtual ICollection<ClaimType> ClaimTypes { get; set; }
        #endregion
        public DateTime LastUpdate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
    }
}
