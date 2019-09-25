using David.Products.Domain.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace David.Products.Domain.Models
{
    public class Product : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Nombre producto")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe de tener una longitud máxima de {1} caractéres")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "El campo {0}, es requerido")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Descripción del producto")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe de tener una longitud máxima de {1} caractéres")]
        public string Description { get; set; }
        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Precio del producto")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Disponible")]
        public bool Available { get; set; }
        #region Virtuals
        [Required]
        public virtual int categoryId { get; set; }
        [JsonProperty("Category")]
        public virtual Category Category { get; set; } 
        #endregion
        public DateTime LastUpdate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
    }
}
