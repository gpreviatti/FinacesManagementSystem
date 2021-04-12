using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Controllers;
using Domain.Dtos.User;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests.Controller
{
    public class UserControllerTest
    {
        private UserController _controller;

        public UserControllerTest()
        {
        }

        [Fact(DisplayName = "Should list users")]
        [Trait("Crud", "ShouldListUser")]
        public async void ShouldListUsers()
        {
            // Arrange
            var serviceMock = new Mock<IUserService>();
            var users = new List<UserResultDto>()
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

            // Action
            serviceMock.Setup(m => m.FindAllAsync()).ReturnsAsync(users);

            _controller = new UserController(serviceMock.Object);

            var result = await _controller.Get();
            var resultValues = (IEnumerable<UserResultDto>)((OkObjectResult)result).Value;

            // Assert
            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);
            Assert.True(resultValues.Count().Equals(10));
        }

        [Fact(DisplayName = "Should NOT list users")]
        [Trait("Crud", "ShouldNotListUsers")]
        public async void ShouldNotListUsers()
        {
            // Arrange
            var serviceMock = new Mock<IUserService>();

            // Action
            serviceMock.Setup(m => m.FindAllAsync()).ReturnsAsync(new List<UserResultDto>());

            _controller = new UserController(serviceMock.Object);
            _controller.ModelState.AddModelError("Model", "Model Invalid");

            var result = await _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.True(result is BadRequestObjectResult);
        }

        [Fact(DisplayName = "Should create user")]
        [Trait("Crud", "ShouldCreateUser")]
        public async void ShouldCreateUser()
        {
            // Arrange
            var serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            var userCreateDto = new UserCreateDto()
            {
                Name = nome,
                Email = email
            };

            var userResultDto = new UserResultDto()
            {
                Name = nome,
                Email = email
            };

            // Action
            serviceMock.Setup(m => m.CreateAsync(It.IsAny<UserCreateDto>())).ReturnsAsync(userResultDto);

            _controller = new UserController(serviceMock.Object);

            var result = await _controller.Post(userCreateDto);
            var resultValue = (UserResultDto) ((CreatedResult) result).Value;

            // Assert
            Assert.True(result is CreatedResult);
            Assert.NotNull(resultValue);
            Assert.Equal(userCreateDto.Name, resultValue.Name);
            Assert.Equal(userCreateDto.Email, resultValue.Email);
        }

        [Fact(DisplayName = "Should update user")]
        [Trait("Crud", "ShouldUpdateUser")]
        public async void ShouldUpdateUser()
        {
            // Arrange
            var serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            var userUpdate = new UserUpdateDto()
            {
                Name = nome,
                Email = email
            };

            var userResultDto = new UserResultDto()
            {
                Name = nome,
                Email = email
            };

            // Action
            serviceMock.Setup(m => m.UpdateAsync(It.IsAny<UserUpdateDto>())).ReturnsAsync(userResultDto);

            _controller = new UserController(serviceMock.Object);

            var result = await _controller.Put(userUpdate);
            var resultValue = (UserResultDto)( (OkObjectResult)result ).Value;

            // Assert
            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);
            Assert.Equal(userUpdate.Name, resultValue.Name);
            Assert.Equal(userUpdate.Email, resultValue.Email);

        }

        [Fact(DisplayName = "Should NOT update user")]
        [Trait("Crud", "ShouldNotUpdateUser")]
        public async void ShouldNotUpdateUser()
        {
            // Arrange
            var serviceMock = new Mock<IUserService>();

            // Action
            serviceMock.Setup(m => m.UpdateAsync(It.IsAny<UserUpdateDto>())).ReturnsAsync(new UserResultDto());

            _controller = new UserController(serviceMock.Object);
            _controller.ModelState.AddModelError("Email", "Is required field");

            var result = await _controller.Put(new UserUpdateDto());

            // Assert
            Assert.NotNull(result);
            Assert.True(result is BadRequestObjectResult);
        }

        [Fact(DisplayName = "Should delete user")]
        [Trait("Crud", "ShouldDeleteUser")]
        public async void ShouldDeleteUser()
        {
            // Arrange
            var serviceMock = new Mock<IUserService>();

            // Action
            serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            _controller = new UserController(serviceMock.Object);

            var result = await _controller.Delete(It.IsAny<Guid>());

            // Assert
            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

        }

        [Fact(DisplayName = "Should NOT delete user")]
        [Trait("Crud", "ShouldNotDeleteUser")]
        public async void ShouldNotDeleteUser()
        {
            // Arrange
            var serviceMock = new Mock<IUserService>();

            // Action
            serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            _controller = new UserController(serviceMock.Object);

            var result = await _controller.Delete(It.IsAny<Guid>());

            // Assert
            Assert.NotNull(result);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
