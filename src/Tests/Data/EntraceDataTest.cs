using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Xunit;

namespace Tests.Data
{
    public class EntranceDataTest : BaseDataTest
    {
        private readonly IEntranceRepository _repository;
        public EntranceDataTest()
        {
            _repository = new EntranceRepository(_context);
        }

        private Entrance CreateEntranceEntity()
        {
            return new Entrance()
            {
                Description = Faker.Lorem.Sentence(10),
                Observation = Faker.Lorem.Sentence(10),
                Ticker = "TEST",
                Type = 1,
                Value = 100,
                Category = new CategoryDataTest().CreateCategoryEntity(),
                Wallet = new WalletDataTest().CreateWalletEntity(),
            };
        }

        [Fact(DisplayName = "Create Entrance")]
        [Trait("Data", "Entrance")]
        public async void ShouldCreateEntrance()
        {
            try
            {
                var entraceEntity = CreateEntranceEntity();
                var result = await _repository.CreateAsync(entraceEntity);
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
            catch (Exception e)
            {
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }

        [Fact(DisplayName = "List Entrances")]
        [Trait("Data", "Entrance")]
        public async void ShouldListEntrance()
        {
            try
            {
                var entraceEntity = CreateEntranceEntity(); ;
                await _repository.CreateAsync(entraceEntity);

                var result = await _repository.FindAllAsync();

                Assert.NotNull(result);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }

        [Fact(DisplayName = "List Entrance by Id")]
        [Trait("Data", "Entrance")]
        public async void ShouldListEntranceById()
        {
            try
            {
                var enntraceEntity = CreateEntranceEntity();
                await _repository.CreateAsync(enntraceEntity);

                var result = _repository.FindByIdAsync(enntraceEntity.Id).Result;

                Assert.NotNull(result);
                Assert.IsType<Entrance>(result);
                Assert.Equal(enntraceEntity.Id, result.Id);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }

        [Fact(DisplayName = "List all user entrances with their categories")]
        [Trait("Data", "Entrance")]
        public async void ShouldFindAllAsyncWithCategory()
        {
            try
            {
                var entranceEntity = CreateEntranceEntity();
                await _repository.CreateAsync(entranceEntity);
                var userWallets = new List<Guid> { entranceEntity.WalletId };

                var result = await _repository.FindAllAsyncWithCategory(userWallets);

                Assert.NotNull(result);
                Assert.IsType<List<Entrance>>(result);
                Assert.IsType<Guid>(result.FirstOrDefault().Id);
                Assert.IsType<double>(result.FirstOrDefault().Value);
                Assert.IsType<string>(result.FirstOrDefault().Description);
                Assert.IsType<int>(result.FirstOrDefault().Type);
                Assert.IsType<DateTime>(result.FirstOrDefault().CreatedAt);
                Assert.IsType<DateTime>(result.FirstOrDefault().UpdatedAt);
                Assert.NotNull(result.FirstOrDefault().Category);
                Assert.NotNull(result.FirstOrDefault().Category.Name);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Update Entrance")]
        [Trait("Data", "Entrance")]
        public async void ShouldUpdateEntrance()
        {
            try
            {
                var enntraceEntity = CreateEntranceEntity();
                await _repository.CreateAsync(enntraceEntity);
                enntraceEntity.Description = Faker.Name.First();

                var result = await _repository.SaveChangesAsync();

                Assert.Equal(1, result);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }

        [Fact(DisplayName = "Delete Entrance")]
        [Trait("Data", "Entrance")]
        public async void ShouldDeleteEntrance()
        {
            try
            {
                var enntraceEntity = CreateEntranceEntity();
                await _repository.CreateAsync(enntraceEntity);

                var result = await _repository.DeleteAsync(enntraceEntity.Id);

                Assert.True(result);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Assert.True(false);
            }
        }
    }
}
