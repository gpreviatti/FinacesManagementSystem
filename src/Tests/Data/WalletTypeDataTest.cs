using System;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Xunit;

namespace Tests.Data;

public class WalletTypeDataTest : BaseDataTest
{
    private readonly IWalletTypeRepository _repository;

    public WalletTypeDataTest()
    {
        _repository = new WalletTypeRepository(_context);
    }

    public static WalletType CreateWalletTypeEntity()
    {
        return new()
        {
            Name = Faker.Name.First()
        };
    }

    [Fact(DisplayName = "Create WalletType")]
    [Trait("Data", "WalletType")]
    public async void ShouldCreateWalletType()
    {
        // Arrange
        var walletTypeEntity = CreateWalletTypeEntity();

        // Act
        var result = await _repository.CreateAsync(walletTypeEntity);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Id == Guid.Empty);
        Assert.Equal(walletTypeEntity.Name, result.Name);
    }

    [Fact(DisplayName = "List WalletTypes")]
    [Trait("Data", "WalletType")]
    public async void ShouldListWalletType()
    {
        // Arrange
        var walletTypeEntity = CreateWalletTypeEntity();

        await _repository.CreateAsync(walletTypeEntity);

        var result = await _repository.FindAllAsync();

        Assert.NotNull(result);
    }

    [Fact(DisplayName = "List WalletType by Id")]
    [Trait("Data", "WalletType")]
    public async void ShouldListWalletTypeById()
    {
        // Arrange
        var walletTypeEntity = CreateWalletTypeEntity();

        await _repository.CreateAsync(walletTypeEntity);
            
        // Act
        var result = await _repository.FindByIdAsync(walletTypeEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<WalletType>(result);
        Assert.Equal(walletTypeEntity.Id, result.Id);
        Assert.Equal(walletTypeEntity.Name, result.Name);
    }

    [Fact(DisplayName = "Update WalletType")]
    [Trait("Data", "WalletType")]
    public async void ShouldUpdateWalletType()
    {
        // Arrange
        var walletTypeEntity = CreateWalletTypeEntity();

        await _repository.CreateAsync(walletTypeEntity);

        walletTypeEntity.Name = Faker.Name.FullName();
        
        // Act
        var result = await _repository.SaveChangesAsync();

        // Assert
        Assert.Equal(1, result);
    }

    [Fact(DisplayName = "Delete WalletType")]
    [Trait("Data", "WalletType")]
    public async void ShouldDeleteWalletType()
    {
        // Arrange
        var walletTypeEntity = CreateWalletTypeEntity();

        await _repository.CreateAsync(walletTypeEntity);

        // Act
        var result = await _repository.DeleteAsync(walletTypeEntity.Id);

        // Assert
        Assert.True(result);
    }
}
