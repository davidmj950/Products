using David.Products.Domain.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace David.Products.Domain.Models
{
    public class ClaimType : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        //Ejemplo:Consultar,Crear,Modificar,Eliminar.
        public string Name { get; set; }
        public int ClaimActionId { get; set; }
        #region Virtuals
        [JsonProperty("ClaimAction")]
        public virtual ClaimAction ClaimAction { get; set; } 
        #endregion
        public DateTime LastUpdate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
    }
}
