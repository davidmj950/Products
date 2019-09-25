using David.Products.Domain.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace David.Products.Domain.Models
{
    public class Role : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Rol")]
        [MaxLength(30, ErrorMessage = "El campo {0}, debe tener una longitud máxima de {1} caractéres")]
        public string Name { get; set; }
        #region Virtuals
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
        [JsonProperty("ClaimActions")]
        public virtual ICollection<ClaimAction> ClaimActions { get; set; }
        #endregion
        public DateTime LastUpdate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
    }
}
