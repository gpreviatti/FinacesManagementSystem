using System;
using System.Linq;
using Domain.Dtos.WalletType;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Mappers;
using Domain.Services;
using Moq;
using Xunit;

namespace Tests.Service
{
    public class WalletTypeServiceTest : BaseServiceTest
    {
        private IWalletTypeService _service;

        private Mock<IWalletTypeRepository> _repositoryMock;

        public WalletTypeServiceTest()
        {
            _repositoryMock = new Mock<IWalletTypeRepository>();

            _service = new WalletTypeService(_repositoryMock.Object);
        }

        [Fact(DisplayName = "Create wallet type")]
        [Trait("Service", "WalletType")]
        public void ShouldCreateWalletType()
        {
            // Arrange
            WalletTypeCreateDto walletTypeCreateDto = new() { Name = _fakerName };

            var wallet = walletTypeCreateDto.Mapper();

            WalletTypeResultDto userResultDto = new()
            {
                Id = Guid.NewGuid(),
                Name = _fakerName,
                CreatedAt = _fakerDate,
                UpdatedAt = _fakerDate
            };

            _repositoryMock
                .Setup(m => m.CreateAsync(wallet))
                .ReturnsAsync(wallet);

            // Act
            var result = _service.CreateAsync(walletTypeCreateDto);
            
            // Assert
            Assert.NotNull(result);
            Assert.Equal(walletTypeCreateDto.Name, result.Name);
        }

        [Fact(DisplayName = "List wallet types")]
        [Trait("Service", "WalletType")]
        public async void ShouldListWalletType()
        {
            // Arrange
            var listWalletTypeResultDto = new WalletType[]
            {
                new () { Id = new (), Name = _fakerName, CreatedAt = _fakerDate, UpdatedAt = _fakerDate},
                new () { Id = new (), Name = _fakerName, CreatedAt = _fakerDate, UpdatedAt = _fakerDate},
                new () { Id = new (), Name = _fakerName, CreatedAt = _fakerDate, UpdatedAt = _fakerDate},
                new () { Id = new (), Name = _fakerName, CreatedAt = _fakerDate, UpdatedAt = _fakerDate},
                new () { Id = new (), Name = _fakerName, CreatedAt = _fakerDate, UpdatedAt = _fakerDate},
                new () { Id = new (), Name = _fakerName, CreatedAt = _fakerDate, UpdatedAt = _fakerDate},
                new () { Id = new (), Name = _fakerName, CreatedAt = _fakerDate, UpdatedAt = _fakerDate},
                new () { Id = new (), Name = _fakerName, CreatedAt = _fakerDate, UpdatedAt = _fakerDate},
                new () { Id = new (), Name = _fakerName, CreatedAt = _fakerDate, UpdatedAt = _fakerDate},
                new () { Id = new (), Name = _fakerName, CreatedAt = _fakerDate, UpdatedAt = _fakerDate}
            };
            
            _repositoryMock
                .Setup(m => m.FindAllAsync())
                .ReturnsAsync(listWalletTypeResultDto);

            // Act
            var result = await _service.FindAllAsync();
            
            // Assert
            Assert.NotNull(result);
            Assert.Equal(listWalletTypeResultDto.Length, result.Count());
        }

        [Fact(DisplayName = "List wallet type by id")]
        [Trait("Service", "WalletType")]
        public async void ShouldListWalletTypeById()
        {
            var walletType = new WalletType()
            {
                Id = new Guid(),
                Name = _fakerName,
                CreatedAt = _fakerDate,
                UpdatedAt = _fakerDate
            };

            _repositoryMock
                .Setup(m => m.FindByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(walletType);

            // Act
            var result = await _service.FindByIdAsync(It.IsAny<Guid>());

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Id != Guid.Empty);
            Assert.Equal(walletType.Id, result.Id);
            Assert.Equal(walletType.CreatedAt, result.CreatedAt);
            Assert.Equal(walletType.UpdatedAt, result.UpdatedAt);
        }

        [Fact(DisplayName = "Update wallet type")]
        [Trait("Service", "WalletType")]
        public async void ShouldUpdateWalletType()
        {
            // Arrange
            WalletTypeUpdateDto walletTypeUpdateDto = new()
            {
                Id = Guid.NewGuid(),
                Name = _fakerName
            };

            var walletType = walletTypeUpdateDto.Mapper();

            _repositoryMock
                .Setup(m => m.FindByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(walletType);

            _repositoryMock
                .Setup(m => m.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _service.UpdateAsync(walletTypeUpdateDto);
            
            // Assert
            Assert.NotNull(result);
            Assert.False(result.Id.Equals(Guid.Empty));
            Assert.Equal(_fakerName, result.Name);
        }

        [Fact(DisplayName = "Delete wallet type")]
        [Trait("Service", "WalletType")]
        public async void ShouldDeleteWalletType()
        {
            // Arrange
            _repositoryMock
                .Setup(m => m.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync(Guid.NewGuid());

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Not Delete wallet type")]
        [Trait("Service", "WalletType")]
        public async void ShouldNotDeleteWalletType()
        {
            // Arrange
            _repositoryMock
                .Setup(m => m.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(false);

            // Act
            var result = await _service.DeleteAsync(Guid.NewGuid());

            // Assert
            Assert.False(result);
        }
    }
}
