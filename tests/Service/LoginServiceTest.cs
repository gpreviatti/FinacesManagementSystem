using System;
using Domain.Dtos;
using Domain.Dtos.Login;
using Domain.Dtos.User;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Tests.Service
{
    public class LoginServiceTest : BaseServiceTest
    {
        private ILoginService _service;
        private Mock<ILoginService> _serviceMock;

        public LoginServiceTest()
        {

        }

        [Fact(DisplayName = "Should Login")]
        [Trait("Service", "Login")]
        public async void ShouldLogin()
        {
            var email = Faker.Internet.Email();
            var loginDto = new LoginDto()
            {
                Email = email,
                Password = "change@1234"
            };

            var loginResultDto = new LoginResultDto()
            {
                AccessToken = "12231312312312",
                Authenticated = true,
                CreatedAt = _fakerDate.ToString("yyyy/mm/dd"),
                Expiration = _fakerDate.AddHours(8).ToString("yyyy/mm/dd"),
                Message = "Authenticated",
                User = new UserResultDto() { Id = Guid.NewGuid(), Email = email, Name = Faker.Name.First() }
            };

            _serviceMock = new Mock<ILoginService>();
            _serviceMock.Setup(m => m.Login(loginDto)).ReturnsAsync(loginResultDto);
            _service = _serviceMock.Object;
            var result = await _service.Login(loginDto);

            Assert.NotNull(result);
            Assert.Equal(result.User.Email, loginDto.Email);
        }

        [Fact(DisplayName = "Should not login")]
        [Trait("Service", "Login")]
        public async void ShouldNotLogin()
        {
            var email = Faker.Internet.Email();
            var loginDto = new LoginDto() { };

            _serviceMock = new Mock<ILoginService>();
            _serviceMock.Setup(m => m.Login(loginDto));
            _service = _serviceMock.Object;
            var result = await _service.Login(loginDto);

            Assert.Null(result);
        }
    }
}
