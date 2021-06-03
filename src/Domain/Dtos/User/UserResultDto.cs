using Domain.Dtos.Entity;

namespace Domain.Dtos.User
{
    public class UserResultDto : EntityResultDto
    {   
        public string Name { get; set; }
        
        public string Email { get; set; }
    }
}
