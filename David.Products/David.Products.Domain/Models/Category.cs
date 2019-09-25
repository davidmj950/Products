using David.Products.Domain.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace David.Products.Domain.Models
{
    public class Category : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "El campo {0} debe de tener una longitud máxima de {1} caractéres")]
        [Display(Name = "Nombre Categoria")]
        public string CategoryName { get; set; }

        #region Virtuals
        [JsonProperty("Products")]
        public virtual ICollection<Product> Products { get; set; } 
        #endregion
        public DateTime LastUpdate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
    }
}
