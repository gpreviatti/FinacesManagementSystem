using System;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Xunit;

namespace Tests.Data;

public class UserDataTest : BaseDataTest
{
    private readonly IUserRepository _repository;

    public UserDataTest()
    {
        _repository = new UserRepository(_context);
    }

    public static User CreateUserEntity()
    {
        return new ()
        {
            Name = Faker.Name.FullName(),
            Email = Faker.Internet.Email(),
            Password = "12345678"
        };
    }

    [Fact(DisplayName = "Create User")]
    [Trait("Data", "User")]
    public async void ShouldCreateUser()
    {
        // Arrange
        var userEntity = CreateUserEntity();

        // Act
        var result = await _repository.CreateAsync(userEntity);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Id == Guid.Empty);
        Assert.Equal(userEntity.Name, result.Name);
        Assert.Equal(userEntity.Email, result.Email);
        Assert.Equal(userEntity.Password, result.Password);

    }

    [Fact(DisplayName = "List Users")]
    [Trait("Data", "User")]
    public async void ShouldListUser()
    {
        // Arrange
        var userEntity = CreateUserEntity();

        await _repository.CreateAsync(userEntity);

        // Act
        var result = await _repository.FindAllAsync();

        // Assert
        Assert.NotNull(result);
    }

    [Fact(DisplayName = "List User by Id")]
    [Trait("Data", "User")]
    public async void ShouldListUserById()
    {
        // Arrange
        var userEntity = CreateUserEntity();

        await _repository.CreateAsync(userEntity);

        // Act
        var result = await _repository.FindByIdAsync(userEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<User>(result);
        Assert.Equal(userEntity.Id, result.Id);
        Assert.Equal(userEntity.Name, result.Name);
        Assert.Equal(userEntity.Email, result.Email);
    }

    [Fact(DisplayName = "Find User By Login")]
    [Trait("Data", "User")]
    public async void ShouldFindUserByLogin()
    {
        // Arrange
        var userEntity = CreateUserEntity();

        await _repository.CreateAsync(userEntity);

        // Act
        var result = await _repository.FindByLogin(userEntity.Email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userEntity.Email, result.Email);
    }

    [Fact(DisplayName = "Not Find User By Login")]
    [Trait("Data", "User")]
    public async void ShouldNotFindUserByLogin()
    {
        // Arrange
        var userEntity = CreateUserEntity();

        await _repository.CreateAsync(userEntity);

        // Act
        var result = await _repository.FindByLogin(Faker.Internet.Email());

        // Assert
        Assert.Null(result);
    }

    [Fact(DisplayName = "Update User")]
    [Trait("Data", "User")]
    public async void ShouldUpdateUser()
    {
        // Arrange
        var userEntity = CreateUserEntity();

        await _repository.CreateAsync(userEntity);

        userEntity.Name = Faker.Name.FullName();
        userEntity.Email = Faker.Internet.Email();

        // Act
        var result = await _repository.SaveChangesAsync();

        // Assert
        Assert.Equal(1, result);
    }

    [Fact(DisplayName = "Delete User")]
    [Trait("Data", "User")]
    public async void ShouldDeleteUser()
    {
        // Arrange
        var userEntity = CreateUserEntity();

        await _repository.CreateAsync(userEntity);
        
        // Act
        var result = await _repository.DeleteAsync(userEntity.Id);

        // Assert
        Assert.True(result);
    }
}
