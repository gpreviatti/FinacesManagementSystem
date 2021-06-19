using Domain.Dtos.Entity;

namespace Domain.Dtos.User
{
    public class UserUpdateDto : EntityUpdateDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
