using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Xunit;

namespace Tests.Data
{
    public class WalletDataTest : BaseDataTest
    {
        private readonly IWalletRepository _repository;

        public WalletDataTest()
        {
            _repository = new WalletRepository(_context);
        }

        public  async Task<Wallet> CreateWallet()
        {
            var walletTest = new Wallet()
            {
                Name = Faker.Name.First(),
                CloseDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(15),
                Description = Faker.Name.First(),
                CurrentValue = 1000,
                WalletType = new WalletTypeDataTest().CreateWalletType().Result,
                User = new UserDataTest().CreateUser().Result
            };
            var result = await _repository.CreateAsync(walletTest);
            Assert.NotNull(result);
            Assert.False(result.Id == Guid.Empty);
            Assert.Equal(walletTest.Name, result.Name);
            Assert.Equal(walletTest.CloseDate, result.CloseDate);
            Assert.Equal(walletTest.DueDate, result.DueDate);
            Assert.Equal(walletTest.Description, result.Description);
            Assert.Equal(walletTest.CurrentValue, result.CurrentValue);
            Assert.Equal(walletTest.WalletType, result.WalletType);
            Assert.Equal(walletTest.User, result.User);
            return result;
        }

        [Fact(DisplayName = "Create Wallet")]
        [Trait("Crud", "ShouldCreateWallet")]
        public async void ShouldCreateWallet()
        {
            await CreateWallet();
        }

        [Fact(DisplayName = "List Wallets")]
        [Trait("Crud", "ShouldListWallet")]
        public async void ShouldListWallet()
        {
            await CreateWallet();
            var result = _repository.FindAllAsync().Result;

            Assert.NotNull(result);
        }

        [Fact(DisplayName = "List Wallet by Id")]
        [Trait("Crud", "ShouldListWalletById")]
        public async void ShouldListWalletById()
        {
            var entityTest = await CreateWallet();
            var result = _repository.FindByIdAsync(entityTest.Id).Result;

            Assert.NotNull(result);
            Assert.IsType<Wallet>(result);
            Assert.Equal(entityTest.Id, result.Id);
            Assert.Equal(entityTest.Name, result.Name);
        }

        [Fact(DisplayName = "Update Wallet")]
        [Trait("Crud", "ShouldUpdateWallet")]
        public async void ShouldUpdateWallet()
        {
            var entityTest = await CreateWallet();
            entityTest.Name = Faker.Name.FullName();
            var result = await _repository.SaveChangesAsync();

            Assert.Equal(1, result);
        }

        [Fact(DisplayName = "Delete Wallet")]
        [Trait("Crud", "ShouldDeleteWallet")]
        public async void ShouldDeleteWallet()
        {
            var entityTest = await CreateWallet();
            var result = await _repository.DeleteAsync(entityTest.Id);

            Assert.True(result);
        }
    }
}
