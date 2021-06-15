using System;
using System.Collections.Generic;
using System.Diagnostics;
using Domain.Dtos.Entrance;
using Domain.Dtos.Wallet;
using Domain.Dtos.WalletType;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Service.Services;
using Xunit;

namespace Tests.Service
{
    public class WalletServiceTest : BaseServiceTest
    {
        private IWalletService _service;
        private Mock<IWalletRepository> _repository;

        
        public WalletServiceTest()
        {
            _repository = new Mock<IWalletRepository>();
            _service = new WalletService(_repository.Object, _mapper);
        }

        [Fact(DisplayName = "Create wallet type")]
        [Trait("Service", "Wallet")]
        public async void ShouldCreateWallet()
        {
            try
            {
                // Arrange
                WalletCreateDto walletCreateDto = new WalletCreateDto()
                {
                    Name = _fakerName,
                    CloseDate = _fakerDate,
                    DueDate = _fakerDate,
                    Description = _fakerName,
                    UserId = Guid.NewGuid(),
                    WalletTypeId = Guid.NewGuid()
                };

                WalletResultDto walletResultDto = new WalletResultDto()
                {
                    Id = Guid.NewGuid(),
                    Name = _fakerName,
                    CloseDate = _fakerDate,
                    DueDate = _fakerDate,
                    CurrentValue = 100,
                    Description = _fakerName,
                    CreatedAt = _fakerDate,
                    UpdatedAt = _fakerDate,
                    Entrances = new List<EntranceResultDto>(),
                    WalletType = new WalletTypeResultDto()
                };

                // Act
                _repository
                .Setup(m => m.CreateAsync(walletCreateDto, It.IsAny<Guid>()))
                .ReturnsAsync(walletResultDto);

                // Assert
                var result = await _service.CreateAsync(walletCreateDto, It.IsAny<Guid>());
                Assert.NotNull(result);
                Assert.False(result.Id.Equals(Guid.Empty));
                Assert.Equal(walletCreateDto.Name, result.Name);
                Assert.Equal(walletCreateDto.CloseDate, result.CloseDate);
                Assert.Equal(walletCreateDto.DueDate, result.DueDate);
                Assert.Equal(walletCreateDto.Description, result.Description);
                Assert.False(result.Entrances.Equals(null));
                Assert.False(result.WalletType.Equals(null));
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "List wallet types")]
        [Trait("Service", "Wallet")]
        public async void ShouldListWallet()
        {
            try
            {
                // Arrange
                var listWallet = new List<Wallet>
                {
                    new Wallet() {
                        Id = Guid.NewGuid(),
                        Name = _fakerName,
                        CloseDate = _fakerDate,
                        DueDate = _fakerDate,
                        CurrentValue = 100,
                        Description = _fakerName,
                        CreatedAt = _fakerDate,
                        UpdatedAt = _fakerDate,
                        Entrances = new List<Entrance>(),
                        WalletType = new WalletType()
                    },
                    new Wallet() {
                        Id = Guid.NewGuid(),
                        Name = _fakerName,
                        CloseDate = _fakerDate,
                        DueDate = _fakerDate,
                        CurrentValue = 100,
                        Description = _fakerName,
                        CreatedAt = _fakerDate,
                        UpdatedAt = _fakerDate,
                        Entrances = new List<Entrance>(),
                        WalletType = new WalletType()
                    },
                    new Wallet() {
                        Id = Guid.NewGuid(),
                        Name = _fakerName,
                        CloseDate = _fakerDate,
                        DueDate = _fakerDate,
                        CurrentValue = 100,
                        Description = _fakerName,
                        CreatedAt = _fakerDate,
                        UpdatedAt = _fakerDate,
                        Entrances = new List<Entrance>(),
                        WalletType = new WalletType()
                    },
                };

                // Act
                _repository
                .Setup(m => m.FindAsyncWalletsUser(_userAdminId).Result)
                .Returns(listWallet);
                var result = await _service.FindAsyncWalletsUserIds(_userAdminId);

                // Assert
                Assert.NotNull(result);
                Assert.True(result.Count() == 3);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "List wallet type by id")]
        [Trait("Service", "Wallet")]
        public async void ShouldListWalletById()
        {
            try
            {
                // Arrange
                var wallet = new Wallet()
                {
                    Id = Guid.NewGuid(),
                    Name = _fakerName,
                    CloseDate = _fakerDate,
                    DueDate = _fakerDate,
                    CurrentValue = 100,
                    Description = _fakerName,
                    CreatedAt = _fakerDate,
                    UpdatedAt = _fakerDate,
                    Entrances = new List<Entrance>(),
                    WalletType = new WalletType()
                };

                // Act
                _repository
                .Setup(m => m.FindByIdAsync(It.IsAny<Guid>()).Result)
                .Returns(wallet);
                var result = await _service.FindByIdAsync(It.IsAny<Guid>());

                // Assert
                Assert.NotNull(result);
                Assert.False(result.Id.Equals(Guid.Empty));
                Assert.Equal(wallet.Id, result.Id);
                Assert.Equal(wallet.CreatedAt, result.CreatedAt);
                Assert.Equal(wallet.UpdatedAt, result.UpdatedAt);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Update wallet type")]
        [Trait("Service", "Wallet")]
        public async void ShouldUpdateWallet()
        {
            try
            {
                // Arrange
                WalletUpdateDto walletTypeUpdateDto = new WalletUpdateDto() { Name = _fakerName };

                WalletResultDto walletResultDto = new WalletResultDto()
                {
                    Id = Guid.NewGuid(),
                    Name = _fakerName,
                    CreatedAt = _fakerDate,
                    UpdatedAt = _fakerDate
                };

                // Act
                _repository
                    .Setup(m => m.SaveChangesAsync().Result)
                    .Returns(1);

                var result = await _service.UpdateAsync(walletTypeUpdateDto);

                // Assert
                Assert.NotNull(result);
                Assert.False(result.Id.Equals(Guid.Empty));
                Assert.Equal(_fakerName, result.Name);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Delete wallet type")]
        [Trait("Service", "Wallet")]
        public async void ShouldDeleteWallet()
        {
            try
            {
                // Arrange

                // Act
                _repository
                .Setup(m => m.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);
                var result = await _service.DeleteAsync(Guid.NewGuid());

                // Assert
                Assert.True(result);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Not Delete wallet type")]
        [Trait("Service", "Wallet")]
        public async void ShouldNotDeleteWallet()
        {
            try
            {
                // Arrange

                // Act
                _repository
                .Setup(m => m.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(false);
                var result = await _service.DeleteAsync(Guid.NewGuid());

                // Assert
                Assert.False(result);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                Assert.True(false);
            }
        }
    }
}
