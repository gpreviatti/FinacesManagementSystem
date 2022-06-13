using System;
using System.Diagnostics;
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

        public static Wallet CreateWalletEntity()
        {
            return new Wallet()
            {
                Name = Faker.Name.First(),
                Description = Faker.Name.First(),
                CurrentValue = 1000,
                WalletType = WalletTypeDataTest.CreateWalletTypeEntity(),
                User = UserDataTest.CreateUserEntity()
            };
        }

        [Fact(DisplayName = "Create Wallet")]
        [Trait("Data", "Wallet")]
        public async void ShouldCreateWalletEntity()
        {
            try
            {
                var walletEntity = CreateWalletEntity();
                var result = await _repository.CreateAsync(walletEntity);

                Assert.NotNull(result);
                Assert.False(result.Id == Guid.Empty);
                Assert.Equal(walletEntity.Name, result.Name);
                Assert.Equal(walletEntity.CloseDate, result.CloseDate);
                Assert.Equal(walletEntity.DueDate, result.DueDate);
                Assert.Equal(walletEntity.Description, result.Description);
                Assert.Equal(walletEntity.CurrentValue, result.CurrentValue);
                Assert.Equal(walletEntity.WalletType, result.WalletType);
                Assert.Equal(walletEntity.User, result.User);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }

        [Fact(DisplayName = "List Wallets")]
        [Trait("Data", "Wallet")]
        public async void ShouldListWallet()
        {
            try
            {
                var walletEntity = CreateWalletEntity();
                await _repository.CreateAsync(walletEntity);

                var result = _repository.FindAllAsync().Result;

                Assert.NotNull(result);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }

        [Fact(DisplayName = "List Wallet by Id")]
        [Trait("Data", "Wallet")]
        public async void ShouldListWalletById()
        {
            try
            {
                var walletEntity = CreateWalletEntity();
                await _repository.CreateAsync(walletEntity);

                var result = _repository.FindByIdAsync(walletEntity.Id).Result;

                Assert.NotNull(result);
                Assert.IsType<Wallet>(result);
                Assert.Equal(walletEntity.Id, result.Id);
                Assert.Equal(walletEntity.Name, result.Name);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }

        [Fact(DisplayName = "Update Wallet")]
        [Trait("Data", "Wallet")]
        public async void ShouldUpdateWallet()
        {
            try
            {
                var walletEntity = CreateWalletEntity();
                await _repository.CreateAsync(walletEntity);

                walletEntity.Name = Faker.Name.FullName();
                var result = await _repository.SaveChangesAsync();

                Assert.Equal(1, result);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }

        [Fact(DisplayName = "Delete Wallet")]
        [Trait("Data", "Wallet")]
        public async void ShouldDeleteWallet()
        {
            try
            {
                var walletEntity = CreateWalletEntity();
                await _repository.CreateAsync(walletEntity);

                var result = await _repository.DeleteAsync(walletEntity.Id);

                Assert.True(result);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }
    }
}
