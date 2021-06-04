using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos;
using Domain.Dtos.Login;
using Domain.Dtos.User;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Security;
using Helpers;
using Microsoft.Extensions.Configuration;

namespace Service.Services
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly IUserRepository _repository;
        private readonly SigningConfigurations _signingConfigurations;
        private IConfiguration _configuration { get; }

        public LoginService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

            var loginResultDto = new LoginResultDto()
            {
                Authenticated = true,
                Message = "Authenticated With Success",
                User = _mapper.Map<UserResultDto>(user),
            };
            return loginResultDto;
        }
    }
}
