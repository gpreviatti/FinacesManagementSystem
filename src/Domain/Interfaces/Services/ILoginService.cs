using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Dtos.Login;

namespace Domain.Interfaces.Services
{
    public interface ILoginService
    {
        Task<LoginResultDto> Login(LoginDto loginDto);
    }
}
