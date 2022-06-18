using System;
using System.Threading.Tasks;
using Domain.Dtos.User;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Mappers;
using Moq;
using Service.Services;
using Xunit;

namespace Tests.Domain.Service;

public class UserServiceTest : BaseServiceTest
{
    private readonly Mock<IUserRepository> _repositoryMock;

    private readonly IUserService _service;

    public UserServiceTest()
    {
        _repositoryMock = new Mock<IUserRepository>();

        _service = new UserService(_repositoryMock.Object);
    }

    [Fact(DisplayName = "Create user")]
    [Trait("Service", "User")]
    public async Task ShouldCreateUser()
    {
        // Arrange
        var name = Faker.Name.FullName();
        var email = Faker.Internet.Email();
        var password = "123456789";

        var userCreateDto = new UserCreateDto()
        {
            Email = email,
            Name = name,
            Password = password
        };

        _repositoryMock
            .Setup(m => m.CreateAsync(It.IsAny<User>()))
            .ReturnsAsync(It.IsAny<User>());

        // Act
        var result = await _service.CreateAsync(userCreateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(name, result.Name);
        Assert.Equal(email, result.Email);

        _repositoryMock.Verify(m => m.CreateAsync(It.IsAny<User>()), Times.Once);
    }

    [Fact(DisplayName = "List user by id")]
    [Trait("Service", "User")]
    public async void ShouldListUserById()
    {
        // Arrange
        var user = new User()
        {
            Id = new Guid(),
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        _repositoryMock
            .Setup(m => m.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
            .ReturnsAsync(user);

        // Act
        var result = await _service.FindByIdAsync(It.IsAny<Guid>());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.CreatedAt, result.CreatedAt);
        Assert.Equal(user.UpdatedAt, result.UpdatedAt);
    }

    [Fact(DisplayName = "Update user")]
    [Trait("Service", "User")]
    public async void ShouldUpdateUser()
    {
        // Arrange
        var name = Faker.Name.FullName();
        var email = Faker.Internet.Email();
        var password = "123456789";

        var userUpdateDto = new UserUpdateDto()
        {
            Email = email,
            Name = name,
            Password = password
        };

        var user = userUpdateDto.Mapper();

        _repositoryMock
            .Setup(m => m.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
            .ReturnsAsync(user);

        _repositoryMock.Setup(m => m.SaveChangesAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _service.UpdateAsync(userUpdateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(name, result.Name);
        Assert.Equal(email, result.Email);

        _repositoryMock.Verify(m => m.SaveChangesAsync(), Times.Once);
    }

    [Fact(DisplayName = "Delete user")]
    [Trait("Service", "User")]
    public async void ShouldDeleteUser()
    {
        // Arrange
        _repositoryMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);

        // Act
        var result = await _service.DeleteAsync(Guid.NewGuid());

        // Assert
        Assert.True(result);
    }

    [Fact(DisplayName = "Not Delete user")]
    [Trait("Service", "User")]
    public async void ShouldNotDeleteUser()
    {
        // Arrange
        _repositoryMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);

        // Act
        var result = await _service.DeleteAsync(Guid.NewGuid());

        // Assert
        Assert.False(result);
    }
}
