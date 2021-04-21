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

        public WalletType CreateWalletTypeEntity()
        {
            return new WalletType()
            {
                Name = Faker.Name.First()
            };
        }

        [Fact(DisplayName = "Create WalletType")]
        [Trait("Crud", "ShouldCreateWalletType")]
        public async void ShouldCreateWalletType()
        {
            try
            {
                var walletTypeEntity = CreateWalletTypeEntity();
                var result = await _repository.CreateAsync(walletTypeEntity);

                Assert.NotNull(result);
                Assert.False(result.Id == Guid.Empty);
                Assert.Equal(walletTypeEntity.Name, result.Name);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Console.WriteLine(e);
            }
        }

        [Fact(DisplayName = "List WalletTypes")]
        [Trait("Crud", "ShouldListWalletType")]
        public async void ShouldListWalletType()
        {
            try
            {
                var walletTypeEntity = CreateWalletTypeEntity();
                await _repository.CreateAsync(walletTypeEntity);

                var result = await _repository.FindAllAsync();

                Assert.NotNull(result);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Console.WriteLine(e);
            }
        }

        [Fact(DisplayName = "List WalletType by Id")]
        [Trait("Crud", "ShouldListWalletTypeById")]
        public async void ShouldListWalletTypeById()
        {
            try
            {
                var walletTypeEntity = CreateWalletTypeEntity();
                await _repository.CreateAsync(walletTypeEntity);

                var result = await _repository.FindByIdAsync(walletTypeEntity.Id);

                Assert.NotNull(result);
                Assert.IsType<WalletType>(result);
                Assert.Equal(walletTypeEntity.Id, result.Id);
                Assert.Equal(walletTypeEntity.Name, result.Name);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Console.WriteLine(e);
            }
        }

        [Fact(DisplayName = "Update WalletType")]
        [Trait("Crud", "ShouldUpdateWalletType")]
        public async void ShouldUpdateWalletType()
        {
            try
            {
                var walletTypeEntity = CreateWalletTypeEntity();
                await _repository.CreateAsync(walletTypeEntity);

                walletTypeEntity.Name = Faker.Name.FullName();
                var result = await _repository.SaveChangesAsync();

                Assert.Equal(1, result);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Console.WriteLine(e);
            }
        }

        [Fact(DisplayName = "Delete WalletType")]
        [Trait("Crud", "ShouldDeleteWalletType")]
        public async void ShouldDeleteWalletType()
        {
            try
            {
                var walletTypeEntity = CreateWalletTypeEntity();
                await _repository.CreateAsync(walletTypeEntity);

                var result = await _repository.DeleteAsync(walletTypeEntity.Id);

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
