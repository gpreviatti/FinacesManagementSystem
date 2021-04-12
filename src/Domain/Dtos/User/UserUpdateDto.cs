using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.User
{
    public class UserUpdateDto
    {
        [Required(ErrorMessage = "Id is required to update")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
