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

        private async Task<Entrace> CreateEntrace()
        {
            var entityTest = new Entrace()
            {
                Id = Guid.NewGuid(),
                Description = Faker.Lorem.Sentence(200),
                Observation = Faker.Lorem.Sentence(200),
                Ticker = "TEST",
                Type = 1,
                Value = 100,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Category = new CategoryDataTest().CreateCategory().Result,
                Wallet = new WalletDataTest().CreateWallet().Result,
            };
            var result = await _repository.CreateAsync(entityTest);

            return result;
        }

        [Fact(DisplayName = "Create Entrace")]
        [Trait("Crud", "ShouldCreateEntrace")]
        public async void ShouldCreateEntrace()
        {
            try
            {
                await CreateEntrace();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Fact(DisplayName = "List Entraces")]
        [Trait("Crud", "ShouldListEntrace")]
        public async void ShouldListEntrace()
        {
            await CreateEntrace();
            var result = _repository.FindAllAsync().Result;

            Assert.NotNull(result);
        }

        [Fact(DisplayName = "List Entrace by Id")]
        [Trait("Crud", "ShouldListEntraceById")]
        public async void ShouldListEntraceById()
        {
            var entityTest = await CreateEntrace();
            var result = _repository.FindByIdAsync(entityTest.Id).Result;

            Assert.NotNull(result);
            Assert.IsType<Entrace>(result);
            Assert.Equal(entityTest.Id, result.Id);
        }

        [Fact(DisplayName = "Update Entrace")]
        [Trait("Crud", "ShouldUpdateEntrace")]
        public async void ShouldUpdateEntrace()
        {
            var entityTest = await CreateEntrace();
            var result = await _repository.SaveChangesAsync();

            Assert.Equal(1, result);
        }

        [Fact(DisplayName = "Delete Entrace")]
        [Trait("Crud", "ShouldDeleteEntrace")]
        public async void ShouldDeleteEntrace()
        {
            var entityTest = await CreateEntrace();
            var result = await _repository.DeleteAsync(entityTest.Id);

            Assert.True(result);
        }
    }
}
