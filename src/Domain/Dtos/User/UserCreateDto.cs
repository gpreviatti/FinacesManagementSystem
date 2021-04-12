using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.User
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name should have {1} caracters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "E-mail is required")]
        [StringLength(100, ErrorMessage = "E-mail should have {1} caracters")]
        [EmailAddress(ErrorMessage = "E-mail format invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
