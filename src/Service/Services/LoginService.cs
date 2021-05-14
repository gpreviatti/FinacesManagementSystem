using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Security;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Domain.Dtos.Login;
using AutoMapper;
using Domain.Dtos.User;
using Helpers;

namespace Service.Services
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly IUserRepository _repository;
        private readonly SigningConfigurations _signingConfigurations;
        private IConfiguration _configuration { get; }

        public LoginService(
            IUserRepository repository,
            SigningConfigurations signingConfigurations,
            IConfiguration configuration,
            IMapper mapper
        )
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<LoginResultDto> Login(LoginDto loginDto)
        {
            try
            {
                if (loginDto != null && !string.IsNullOrWhiteSpace(loginDto.Email) && !string.IsNullOrWhiteSpace(loginDto.Password))
                {
                    var user = new User();
                    user = await _repository.FindByLogin(loginDto.Email);

                    if (user == null)
                    {
                        return new LoginResultDto()
                        {
                            Authenticated = false,
                            Message = "Fail to authenticate"
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

                    var identity = new ClaimsIdentity(
                        new GenericIdentity(user.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //jti O id do token
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                        }
                    );
                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(Convert.ToUInt32(Environment.GetEnvironmentVariable("Seconds")));

                    var handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, createDate, expirationDate, handler);
                    return new LoginResultDto()
                    {
                        Authenticated = true,
                        Message = "User authenticated with success",
                        AccessToken = token,
                        CreatedAt = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        Expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        User = _mapper.Map<UserResultDto>(user),
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                return null;
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Environment.GetEnvironmentVariable("Issuer"),
                Audience = Environment.GetEnvironmentVariable("Audience"),
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        public async Task<LoginResultDto> LoginWeb(LoginDto loginDto)
        {
            if (loginDto != null && !string.IsNullOrWhiteSpace(loginDto.Email) && !string.IsNullOrWhiteSpace(loginDto.Password))
            {
                var user = new User();
                user = await _repository.FindByLogin(loginDto.Email);

                if (user == null)
                {
                    return new LoginResultDto()
                    {
                        Authenticated = false,
                        Message = "Incorrect Password"
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

                return new LoginResultDto()
                {
                    Authenticated = true,
                    Message = "Authenticated With Success",
                    User = _mapper.Map<UserResultDto>(user),
                };
            }
            return null;
        }
    }
}
