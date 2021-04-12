using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "E-mail is required field")]
        [EmailAddress(ErrorMessage = "E-mail on invalid format")]
        [StringLength(100, ErrorMessage = "E-mail must have {1} caracters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is a required field")]
        public string Password { get; set; }
    }
}
