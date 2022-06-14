using System;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Xunit;

namespace Tests.Data;

public class WalletDataTest : BaseDataTest
{
    private readonly IWalletRepository _repository;

    public WalletDataTest()
    {
        _repository = new WalletRepository(_context);
    }

    public static Wallet CreateWalletEntity()
    {
        return new()
        {
            Name = Faker.Name.First(),
            Description = Faker.Name.First(),
            CurrentValue = 1000,
            WalletType = WalletTypeDataTest.CreateWalletTypeEntity(),
            User = UserDataTest.CreateUserEntity()
        };
    }

    [Fact(DisplayName = "Create Wallet")]
    [Trait("Data", "Wallet")]
    public async void ShouldCreateWalletEntity()
    {
        // Arrange
        var walletEntity = CreateWalletEntity();

        // Act
        var result = await _repository.CreateAsync(walletEntity);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Id == Guid.Empty);
        Assert.Equal(walletEntity.Name, result.Name);
        Assert.Equal(walletEntity.CloseDate, result.CloseDate);
        Assert.Equal(walletEntity.DueDate, result.DueDate);
        Assert.Equal(walletEntity.Description, result.Description);
        Assert.Equal(walletEntity.CurrentValue, result.CurrentValue);
        Assert.Equal(walletEntity.WalletType, result.WalletType);
        Assert.Equal(walletEntity.User, result.User);
    }

    [Fact(DisplayName = "List Wallets")]
    [Trait("Data", "Wallet")]
    public async void ShouldListWallet()
    {
        // Arrange
        var walletEntity = CreateWalletEntity();

        await _repository.CreateAsync(walletEntity);

        // Act
        var result = await _repository.FindAllAsync();

        // Assert
        Assert.NotNull(result);
    }

    [Fact(DisplayName = "List Wallet by Id")]
    [Trait("Data", "Wallet")]
    public async void ShouldListWalletById()
    {
        // Arrange
        var walletEntity = CreateWalletEntity();

        await _repository.CreateAsync(walletEntity);

        // Act
        var result = await _repository.FindByIdAsync(walletEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Wallet>(result);
        Assert.Equal(walletEntity.Id, result.Id);
        Assert.Equal(walletEntity.Name, result.Name);
    }

    [Fact(DisplayName = "Update Wallet")]
    [Trait("Data", "Wallet")]
    public async void ShouldUpdateWallet()
    {
        // Arrange
        var walletEntity = CreateWalletEntity();

        await _repository.CreateAsync(walletEntity);

        walletEntity.Name = Faker.Name.FullName();

        // Act
        var result = await _repository.SaveChangesAsync();

        // Assert
        Assert.Equal(1, result);
    }

    [Fact(DisplayName = "Delete Wallet")]
    [Trait("Data", "Wallet")]
    public async void ShouldDeleteWallet()
    {
        // Arramge
        var walletEntity = CreateWalletEntity();

        await _repository.CreateAsync(walletEntity);

        // Act
        var result = await _repository.DeleteAsync(walletEntity.Id);

        // Assert
        Assert.True(result);
    }
}
