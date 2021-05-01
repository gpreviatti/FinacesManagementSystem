using System;
using System.Threading.Tasks;
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
        [Trait("Crud", "ShouldCreateEntrance")]
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
                Console.WriteLine(e);
            }
        }

        [Fact(DisplayName = "List Entrances")]
        [Trait("Crud", "ShouldListEntrance")]
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
                Console.WriteLine(e);
            }
        }

        [Fact(DisplayName = "List Entrance by Id")]
        [Trait("Crud", "ShouldListEntranceById")]
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
                Console.WriteLine(e);
            }
        }

        [Fact(DisplayName = "Update Entrance")]
        [Trait("Crud", "ShouldUpdateEntrance")]
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
                Console.WriteLine(e);
            }
        }

        [Fact(DisplayName = "Delete Entrance")]
        [Trait("Crud", "ShouldDeleteEntrance")]
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
                Assert.True(false);
                Console.WriteLine(e);
            }
        }
    }
}
