using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Xunit;

namespace Tests.Data
{
    public class WalletTypeDataTest : BaseDataTest
    {
        private readonly IWalletTypeRepository _repository;

        public WalletTypeDataTest()
        {
            _repository = new WalletTypeRepository(_context);
        }

        private async Task<WalletType> createWalletType()
        {
            var entityTest = new WalletType()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            return await _repository.CreateAsync(entityTest);
        }

        [Fact(DisplayName = "Create WalletType")]
        [Trait("Crud", "ShouldCreateWalletType")]
        public async void ShouldCreateWalletType()
        {
            var entityTest = new WalletType()
            {
                Name = Faker.Name.FullName(),
                Wallets = new List<Wallet>() { new Wallet() { Name = "Test Wallet" } }
            };

            var result = await _repository.CreateAsync(entityTest);

            Assert.NotNull(result);
            Assert.Equal(entityTest.Name, result.Name);
            Assert.False(result.Id == Guid.Empty);
            Assert.NotNull(result.Wallets);
        }

        [Fact(DisplayName = "List WalletTypes")]
        [Trait("Crud", "ShouldListWalletType")]
        public async void ShouldListWalletType()
        {
            await createWalletType();
            var result = _repository.FindAllAsync().Result;

            Assert.NotNull(result);
        }

        [Fact(DisplayName = "List WalletType by Id")]
        [Trait("Crud", "ShouldListWalletTypeById")]
        public async void ShouldListWalletTypeById()
        {
            var entityTest = await createWalletType();
            var result = _repository.FindByIdAsync(entityTest.Id).Result;

            Assert.NotNull(result);
            Assert.IsType<WalletType>(result);
            Assert.Equal(entityTest.Id, result.Id);
            Assert.Equal(entityTest.Name, result.Name);
        }

        [Fact(DisplayName = "Update WalletType")]
        [Trait("Crud", "ShouldUpdateWalletType")]
        public async void ShouldUpdateWalletType()
        {
            var entityTest = await createWalletType();
            entityTest.Name = Faker.Name.FullName();
            var result = await _repository.SaveChangesAsync();

            Assert.Equal(1, result);
        }

        [Fact(DisplayName = "Delete WalletType")]
        [Trait("Crud", "ShouldDeleteWalletType")]
        public async void ShouldDeleteWalletType()
        {
            var entityTest = await createWalletType();
            var result = await _repository.DeleteAsync(entityTest.Id);

            Assert.True(result);
        }
    }
}
