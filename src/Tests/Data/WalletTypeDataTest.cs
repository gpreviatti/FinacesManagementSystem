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

        public async Task<WalletType> CreateWalletType()
        {
            var walletTypeTest = new WalletType()
            {
                Name = Faker.Name.First()
            };

            var result = await _repository.CreateAsync(walletTypeTest);
            Assert.NotNull(result);
            Assert.False(result.Id == Guid.Empty);
            Assert.Equal(walletTypeTest.Name, result.Name);

            return result;
        }

        [Fact(DisplayName = "Create WalletType")]
        [Trait("Crud", "ShouldCreateWalletType")]
        public async void ShouldCreateWalletType()
        {
            await CreateWalletType();
        }

        [Fact(DisplayName = "List WalletTypes")]
        [Trait("Crud", "ShouldListWalletType")]
        public async void ShouldListWalletType()
        {
            await CreateWalletType();
            var result = _repository.FindAllAsync().Result;

            Assert.NotNull(result);
        }

        [Fact(DisplayName = "List WalletType by Id")]
        [Trait("Crud", "ShouldListWalletTypeById")]
        public async void ShouldListWalletTypeById()
        {
            var entityTest = await CreateWalletType();
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
            var entityTest = await CreateWalletType();
            entityTest.Name = Faker.Name.FullName();
            var result = await _repository.SaveChangesAsync();

            Assert.Equal(1, result);
        }

        [Fact(DisplayName = "Delete WalletType")]
        [Trait("Crud", "ShouldDeleteWalletType")]
        public async void ShouldDeleteWalletType()
        {
            var entityTest = await CreateWalletType();
            var result = await _repository.DeleteAsync(entityTest.Id);

            Assert.True(result);
        }
    }
}
