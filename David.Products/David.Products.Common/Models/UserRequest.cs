using System.ComponentModel.DataAnnotations;

namespace David.Products.Common.Models
{
    public class UserRequest
    {
        
    }

    public class UserLoginRequest
    {
        [Required(ErrorMessage = "The Field {0} is required")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
            ErrorMessage = "The email is invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The Field {0} is required")]
        [StringLength(15, ErrorMessage = "The length of the field {0} must be of {1} characters",
                      MinimumLength = 6)]
        public string Password { get; set; }

        //[Required(ErrorMessage = "The Field {0} is required")]
        public string Recaptcha { get; set; }
    }
}
