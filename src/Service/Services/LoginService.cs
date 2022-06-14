using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Dtos.Login;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Mappers;
using Helpers;

namespace Service.Services;

[ExcludeFromCodeCoverage]
public class LoginService : ILoginService
{
    private readonly IUserRepository _repository;

    public LoginService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<LoginResultDto> Login(LoginDto loginDto)
    {
        if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
        {
            return new LoginResultDto()
            {
                Authenticated = false,
                Message = "Please check your User informations"
            };
        }

        var user = await _repository.FindByLogin(loginDto.Email);

        if (user == null)
        {
            return new LoginResultDto()
            {
                Authenticated = false,
                Message = "Incorrect Email"
            };
        }

        var checkPassword = EncryptHelper.CheckHashedField(loginDto.Password, user.Password);
        if (!checkPassword)
        {
            return new LoginResultDto()
            {
                Authenticated = false,
                Message = "Incorrect Password"
            };
        }

        var loginResultDto = new LoginResultDto
        {
            Authenticated = true,
            Message = "Authenticated With Success",
            User = user.MapperResultDto()
        };
        return loginResultDto;
    }
}
