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

        private async Task<Wallet> createWallet()
        {
            var entityTest = new Wallet()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            return await _repository.CreateAsync(entityTest);
        }

        [Fact(DisplayName = "Create Wallet")]
        [Trait("Crud", "ShouldCreateWallet")]
        public async void ShouldCreateWallet()
        {
            var entityTest = new Wallet()
            {
                Name = Faker.Name.FullName(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CloseDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(15),
                Description = Faker.Lorem.Sentence(100),
                CurrentValue = 10000
            };

            var result = await _repository.CreateAsync(entityTest);

            Assert.NotNull(result);
            Assert.Equal(entityTest.Name, result.Name);
            Assert.False(result.Id == Guid.Empty);
        }

        [Fact(DisplayName = "List Wallets")]
        [Trait("Crud", "ShouldListWallet")]
        public async void ShouldListWallet()
        {
            await createWallet();
            var result = _repository.FindAllAsync().Result;

            Assert.NotNull(result);
        }

        [Fact(DisplayName = "List Wallet by Id")]
        [Trait("Crud", "ShouldListWalletById")]
        public async void ShouldListWalletById()
        {
            var entityTest = await createWallet();
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
            var entityTest = await createWallet();
            entityTest.Name = Faker.Name.FullName();
            var result = await _repository.SaveChangesAsync();

            Assert.Equal(1, result);
        }

        [Fact(DisplayName = "Delete Wallet")]
        [Trait("Crud", "ShouldDeleteWallet")]
        public async void ShouldDeleteWallet()
        {
            var entityTest = await createWallet();
            var result = await _repository.DeleteAsync(entityTest.Id);

            Assert.True(result);
        }
    }
}
