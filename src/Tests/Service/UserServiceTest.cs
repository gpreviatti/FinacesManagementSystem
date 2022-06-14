using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Dtos.User;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Tests.Service;

public class UserServiceTest : BaseServiceTest
{
    private Mock<IUserRepository> _repositoryMock;

    private IUserService _service;

    public UserServiceTest()
    {
        _repositoryMock = new Mock<IUserRepository>();
    }

    [Fact(DisplayName = "Create user")]
    [Trait("Service", "User")]
    public async void ShouldCreateUser()
    {
        // Arrange
        var name = Faker.Name.FullName();
        var email = Faker.Internet.Email();
        var password = "123456789";

        UserCreateDto userCreateDto = new()
        {
            Email = email,
            Name = name,
            Password = password
        };

        UserResultDto loginResultDto = new()
        {
            Id = Guid.NewGuid(),
            Email = email,
            Name = name,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        _repositoryMock.Setup(m => m.CreateAsync(userCreateDto)).ReturnsAsync(loginResultDto);

        // Act
        var result = await _service.CreateAsync(userCreateDto);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Id.Equals(Guid.Empty));
        Assert.Equal(name, result.Name);
        Assert.Equal(email, result.Email);
    }

    [Fact(DisplayName = "List users")]
    [Trait("Service", "User")]
    public async void ShouldListUser()
    {
        // Arrange
        IEnumerable<UserResultDto> listUserResultDto = new List<UserResultDto>
        {
            new () { Id = new (), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
            new () { Id = new (), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
            new () { Id = new (), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
            new () { Id = new (), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
            new () { Id = new (), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
            new () { Id = new (), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
            new () { Id = new (), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
            new () { Id = new (), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
            new () { Id = new (), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()},
            new () { Id = new (), Email = Faker.Internet.Email(), Name = Faker.Name.FullName()}
        };

        _repositoryMock.Setup(m => m.FindAllAsync()).ReturnsAsync(listUserResultDto);

        // Act
        var result = await _service.FindAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() == 10);
    }

    [Fact(DisplayName = "List user by id")]
    [Trait("Service", "User")]
    public async void ShouldListUserById()
    {
        // Arrange
        var userResultDto = new UserResultDto()
        {
            Id = new Guid(),
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        _repositoryMock.Setup(m => m.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(userResultDto);

        // Act
        var result = await _service.FindByIdAsync(It.IsAny<Guid>());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userResultDto.Id, result.Id);
        Assert.Equal(userResultDto.Email, result.Email);
        Assert.Equal(userResultDto.CreatedAt, result.CreatedAt);
        Assert.Equal(userResultDto.UpdatedAt, result.UpdatedAt);
    }

    [Fact(DisplayName = "Update user")]
    [Trait("Service", "User")]
    public async void ShouldUpdateUser()
    {
        // Arrange
        var name = Faker.Name.FullName();
        var email = Faker.Internet.Email();
        var password = "123456789";

        UserUpdateDto userUpdateDto = new()
        {
            Email = email,
            Name = name,
            Password = password
        };

        UserResultDto userResultDto = new()
        {
            Email = email,
            Name = name,
        };

        _repositoryMock.Setup(m => m.UpdateAsync(userUpdateDto)).ReturnsAsync(userResultDto);

        // Act
        var result = await _service.UpdateAsync(userUpdateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(name, result.Name);
        Assert.Equal(email, result.Email);
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
