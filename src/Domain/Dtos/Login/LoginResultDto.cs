using Domain.Dtos.User;

namespace Domain.Dtos.Login
{
    public class LoginResultDto
    {
        public bool Authenticated { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public string CreatedAt { get; set; }
        public string Expiration { get; set; }
        public UserResultDto User { get; set; }
    }
}
