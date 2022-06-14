using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Xunit;

namespace Tests.Data;

public class EntranceDataTest : BaseDataTest
{
    private readonly IEntranceRepository _repository;
    public EntranceDataTest()
    {
        _repository = new EntranceRepository(_context);
    }

    private static Entrance CreateEntranceEntity()
    {
        return new()
        {
            Description = Faker.Lorem.Sentence(10),
            Observation = Faker.Lorem.Sentence(10),
            Ticker = "TEST",
            Type = 1,
            Value = 100,
            Category = CategoryDataTest.CreateCategoryEntity(),
            Wallet = WalletDataTest.CreateWalletEntity(),
        };
    }

    [Fact(DisplayName = "Create Entrance")]
    [Trait("Data", "Entrance")]
    public async Task ShouldCreateEntrance()
    {
        // Arrange
        var entraceEntity = CreateEntranceEntity();

        // Act
        var result = await _repository.CreateAsync(entraceEntity);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Id == Guid.Empty);
        Assert.Equal(entraceEntity.Description, result.Description);
        Assert.Equal(entraceEntity.Observation, result.Observation);
        Assert.Equal(entraceEntity.Ticker, result.Ticker);
        Assert.Equal(entraceEntity.Type, result.Type);
        Assert.Equal(entraceEntity.Value, result.Value);
        Assert.Equal(entraceEntity.CreatedAt, result.CreatedAt);
        Assert.Equal(entraceEntity.UpdatedAt, result.UpdatedAt);
        Assert.Equal(entraceEntity.Category, result.Category);
        Assert.Equal(entraceEntity.Wallet, result.Wallet);
    }

    [Fact(DisplayName = "List Entrances")]
    [Trait("Data", "Entrance")]
    public async Task ShouldListEntrance()
    {
        // Arrange
        var entraceEntity = CreateEntranceEntity();

        await _repository.CreateAsync(entraceEntity);

        // Act
        var result = await _repository.FindAllAsync();

        // Assert
        Assert.NotNull(result);
    }

    [Fact(DisplayName = "List Entrance by Id")]
    [Trait("Data", "Entrance")]
    public async Task ShouldListEntranceById()
    {
        var enntraceEntity = CreateEntranceEntity();

        await _repository.CreateAsync(enntraceEntity);

        var result = await _repository.FindByIdAsync(enntraceEntity.Id);

        Assert.NotNull(result);
        Assert.IsType<Entrance>(result);
        Assert.Equal(enntraceEntity.Id, result.Id);
    }

    [Fact(DisplayName = "List all user entrances with their categories")]
    [Trait("Data", "Entrance")]
    public async Task ShouldFindAllAsyncWithCategory()
    {
        var entranceEntity = CreateEntranceEntity();

        await _repository.CreateAsync(entranceEntity);
        
        var userWallets = new List<Guid> { entranceEntity.WalletId };

        var result = await _repository.FindAllAsyncWithCategory(userWallets);

        Assert.NotNull(result);
        Assert.IsType<Guid>(result.FirstOrDefault().Id);
        Assert.IsType<double>(result.FirstOrDefault().Value);
        Assert.IsType<string>(result.FirstOrDefault().Description);
        Assert.IsType<int>(result.FirstOrDefault().Type);
        Assert.IsType<DateTime>(result.FirstOrDefault().CreatedAt);
        Assert.IsType<DateTime>(result.FirstOrDefault().UpdatedAt);
        Assert.NotNull(result.FirstOrDefault().Category);
        Assert.NotNull(result.FirstOrDefault().Category.Name);
    }

    [Fact(DisplayName = "Update Entrance")]
    [Trait("Data", "Entrance")]
    public async Task ShouldUpdateEntrance()
    {
        var enntraceEntity = CreateEntranceEntity();

        await _repository.CreateAsync(enntraceEntity);
        
        enntraceEntity.Description = Faker.Name.First();

        var result = await _repository.SaveChangesAsync();

        Assert.Equal(1, result);
    }

    [Fact(DisplayName = "Delete Entrance")]
    [Trait("Data", "Entrance")]
    public async Task ShouldDeleteEntrance()
    {
        var enntraceEntity = CreateEntranceEntity();

        await _repository.CreateAsync(enntraceEntity);

        var result = await _repository.DeleteAsync(enntraceEntity.Id);

        Assert.True(result);
    }
}
