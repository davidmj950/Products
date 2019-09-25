using David.Products.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace David.Products.Domain.Models
{
    public class Customer : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0}, debe tener una longitud máxima de {1} caractéres")]
        [Display(Name = "Nombres")]
        public string FisrtName { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0}, debe tener una longitud máxima de {1} caractéres")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(15, ErrorMessage = "El campo {0}, debe tener una longitud máxima de {1} caractéres")]
        [Display(Name = "Telefono")]
        public string Telephone { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(20, ErrorMessage = "El campo {0}, debe tener una longitud máxima de {1} caractéres")]
        [Display(Name = "Telefono celular")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0}, debe tener una longitud máxima de {1} caractéres")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0}, debe tener una longitud máxima de {1} caractéres")]
        [Display(Name = "Dirección")]
        public string Address { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0}, debe tener una longitud máxima de {1} caractéres")]
        [Display(Name = "Barrio")]
        public string Neighboorhood { get; set; }
        public DateTime DateOfRegistration { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Autorización de Habeas data")]
        public bool AuthorizationHabeasData { get; set; }
        [Required]
        public int MaritalStatusId { get; set; }
        [Required]
        public int GenderId { get; set; }
        [Required]
        public int CityId { get; set; }
        #region Virtuals
        public virtual MaritalStatus MaritalStatus { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual City City { get; set; } 
        #endregion
        public DateTime LastUpdate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
    }
}
