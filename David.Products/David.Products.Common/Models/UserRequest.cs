using System.ComponentModel.DataAnnotations;

namespace David.Products.Common.Models
{
    public class UserRequest
    {
        
    }

    public class UserLoginRequest
    {
        [Required(ErrorMessage = "El Campo: Correo es Requerido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo: Contraseña es Requerido.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe de resolver el recaptcha")]
        public string Recaptcha { get; set; }
    }
}
