using System;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Xunit;

namespace Tests.Data
{
    public class EntraceDataTest : BaseDataTest
    {
        private readonly IEntraceRepository _repository;
        public EntraceDataTest()
        {
            _repository = new EntraceRepository(_context);
        }

        private Entrace CreateEntraceEntity()
        {
            return new Entrace()
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

        [Fact(DisplayName = "Create Entrace")]
        [Trait("Crud", "ShouldCreateEntrace")]
        public async void ShouldCreateEntrace()
        {
            try
            {
                var entraceEntity = CreateEntraceEntity();
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

        [Fact(DisplayName = "List Entraces")]
        [Trait("Crud", "ShouldListEntrace")]
        public async void ShouldListEntrace()
        {
            try
            {
                var entraceEntity = CreateEntraceEntity(); ;
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

        [Fact(DisplayName = "List Entrace by Id")]
        [Trait("Crud", "ShouldListEntraceById")]
        public async void ShouldListEntraceById()
        {
            try
            {
                var enntraceEntity = CreateEntraceEntity();
                await _repository.CreateAsync(enntraceEntity);

                var result = _repository.FindByIdAsync(enntraceEntity.Id).Result;

                Assert.NotNull(result);
                Assert.IsType<Entrace>(result);
                Assert.Equal(enntraceEntity.Id, result.Id);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Console.WriteLine(e);
            }
        }

        [Fact(DisplayName = "Update Entrace")]
        [Trait("Crud", "ShouldUpdateEntrace")]
        public async void ShouldUpdateEntrace()
        {
            try
            {
                var enntraceEntity = CreateEntraceEntity();
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

        [Fact(DisplayName = "Delete Entrace")]
        [Trait("Crud", "ShouldDeleteEntrace")]
        public async void ShouldDeleteEntrace()
        {
            try
            {
                var enntraceEntity = CreateEntraceEntity();
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
