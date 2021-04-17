using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos.Category;
using Domain.Dtos.User;
using Domain.Dtos.Wallet;
using Domain.Entities;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Tests.Service
{
    public class UserServiceTest : BaseServiceTest
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        public UserServiceTest()
        {

        }

        [Fact(DisplayName = "Create user")]
        [Trait("Crud", "ShouldCreateUser")]
        public async void ShouldCreateUser()
        {
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();
            var password = "123456789";

            UserCreateDto userCreateDto = new UserCreateDto() {
                Email = email,
                Name = name,
                Password = password
            };

            UserResultDto loginResultDto = new UserResultDto()
            {
                Id = Guid.NewGuid(),
                Email = email,
                Name = name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Categories = new List<CategoryResultDto>(),
                Wallets = new List<WalletResultDto>()
            };
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.CreateAsync(userCreateDto)).ReturnsAsync(loginResultDto);
            _service = _serviceMock.Object;

            var result = await _service.CreateAsync(userCreateDto);

            Assert.NotNull(result);
            Assert.False(result.Id.Equals(Guid.Empty));
            Assert.Equal(name, result.Name);
            Assert.Equal(email, result.Email);
        }

        [Fact(DisplayName = "List users")]
        [Trait("Crud", "ShouldListUsers")]
        public async void ShouldListUser()
        {
            IEnumerable<UserResultDto> listUserResultDto = new List<UserResultDto>
            {
                new UserResultDto(){ Id = new Guid(), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
                new UserResultDto(){ Id = new Guid(), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
                new UserResultDto(){ Id = new Guid(), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
                new UserResultDto(){ Id = new Guid(), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
                new UserResultDto(){ Id = new Guid(), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
                new UserResultDto(){ Id = new Guid(), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
                new UserResultDto(){ Id = new Guid(), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
                new UserResultDto(){ Id = new Guid(), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
                new UserResultDto(){ Id = new Guid(), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
                new UserResultDto(){ Id = new Guid(), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()}
            };
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.FindAllAsync()).ReturnsAsync(listUserResultDto);
            _service = _serviceMock.Object;

            var result = await _service.FindAllAsync();

            Assert.NotNull(result);
            Assert.True(result.Count() == 10);
        }

        [Fact(DisplayName = "List user by id")]
        [Trait("Crud", "ShouldListUserById")]
        public async void ShouldListUserById()
        {
            var userResultDto = new UserResultDto() { 
                Id = new Guid(), 
                Email = Faker.Internet.Email(), 
                Name = Faker.Name.FullName(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(userResultDto);
            _service = _serviceMock.Object;

            var result = await _service.FindByIdAsync(It.IsAny<Guid>());

            Assert.NotNull(result);
            Assert.Equal(userResultDto.Id, result.Id);
            Assert.Equal(userResultDto.Email, result.Email);
            Assert.Equal(userResultDto.CreatedAt, result.CreatedAt);
            Assert.Equal(userResultDto.UpdatedAt, result.UpdatedAt);
        }

        [Fact(DisplayName = "Update user")]
        [Trait("Crud", "ShouldUpdateUser")]
        public async void ShouldUpdateUser()
        {
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();
            var password = "123456789";

            UserUpdateDto userUpdateDto = new UserUpdateDto()
            {
                Email = email,
                Name = name,
                Password = password
            };

            UserResultDto userResultDto = new UserResultDto()
            {
                Email = email,
                Name = name,
            };
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.UpdateAsync(userUpdateDto)).ReturnsAsync(userResultDto);
            _service = _serviceMock.Object;

            var result = await _service.UpdateAsync(userUpdateDto);

            Assert.NotNull(result);
            Assert.Equal(name, result.Name);
            Assert.Equal(email, result.Email);
        }

        [Fact(DisplayName = "Delete user")]
        [Trait("Crud", "ShouldDeleteUser")]
        public async void ShouldDeleteUser()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result = await _service.DeleteAsync(Guid.NewGuid());

            Assert.True(result);
        }

        [Fact(DisplayName = "Not Delete user")]
        [Trait("Crud", "ShouldNotDeleteUser")]
        public async void ShouldNotDeleteUser()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);
            _service = _serviceMock.Object;

            var result = await _service.DeleteAsync(Guid.NewGuid());

            Assert.False(result);
        }
    }
}