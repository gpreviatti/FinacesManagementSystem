using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Dtos.Entrance;
using Domain.Dtos.Wallet;
using Domain.Dtos.WalletType;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Tests.Service
{
    public class WalletServiceTest : BaseServiceTest
    {
        private IWalletService _service;
        private Mock<IWalletService> _serviceMock;

        public WalletServiceTest()
        {

        }

        [Fact(DisplayName = "Create wallet type")]
        [Trait("Crud", "ShouldCreateWallet")]
        public async void ShouldCreateWallet()
        {
            WalletCreateDto walletCreateDto = new WalletCreateDto() { 
                Name = FakerName,
                CloseDate = FakerDate,
                DueDate = FakerDate,
                Description = FakerName,
                UserId = Guid.NewGuid(),
                WalletTypeId = Guid.NewGuid()
            };

            WalletResultDto walletResultDto = new WalletResultDto()
            {
                Id = Guid.NewGuid(),
                Name = FakerName,
                CloseDate = FakerDate,
                DueDate = FakerDate,
                CurrentValue = 100,
                Description = FakerName,
                CreatedAt = FakerDate,
                UpdatedAt = FakerDate,
                Entrances = new List<EntranceResultDto>(),
                WalletType = new WalletTypeResultDto()
            };
            _serviceMock = new Mock<IWalletService>();
            _serviceMock.Setup(m => m.CreateAsync(walletCreateDto, It.IsAny<Guid>())).ReturnsAsync(walletResultDto);
            _service = _serviceMock.Object;

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

        [Fact(DisplayName = "List wallet types")]
        [Trait("Crud", "ShouldListWallets")]
        public async void ShouldListWallet()
        {
            IEnumerable<WalletResultDto> listWalletResultDto = new List<WalletResultDto>
            {
                new WalletResultDto() {
                    Id = Guid.NewGuid(),
                    Name = FakerName,
                    CloseDate = FakerDate,
                    DueDate = FakerDate,
                    CurrentValue = 100,
                    Description = FakerName,
                    CreatedAt = FakerDate,
                    UpdatedAt = FakerDate,
                    Entrances = new List<EntranceResultDto>(),
                    WalletType = new WalletTypeResultDto()
                },
                new WalletResultDto() {
                    Id = Guid.NewGuid(),
                    Name = FakerName,
                    CloseDate = FakerDate,
                    DueDate = FakerDate,
                    CurrentValue = 100,
                    Description = FakerName,
                    CreatedAt = FakerDate,
                    UpdatedAt = FakerDate,
                    Entrances = new List<EntranceResultDto>(),
                    WalletType = new WalletTypeResultDto()
                },
                new WalletResultDto() {
                    Id = Guid.NewGuid(),
                    Name = FakerName,
                    CloseDate = FakerDate,
                    DueDate = FakerDate,
                    CurrentValue = 100,
                    Description = FakerName,
                    CreatedAt = FakerDate,
                    UpdatedAt = FakerDate,
                    Entrances = new List<EntranceResultDto>(),
                    WalletType = new WalletTypeResultDto()
                },
            };
            //_serviceMock = new Mock<IWalletService>();
            //_serviceMock.Setup(m => m.FindAllAsync()).ReturnsAsync(listWalletResultDto);
            //_service = _serviceMock.Object;

            //var result = await _service.FindAllAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count() == 3);
        }

        [Fact(DisplayName = "List wallet type by id")]
        [Trait("Crud", "ShouldListWalletById")]
        public async void ShouldListWalletById()
        {
            var walletResultDto = new WalletResultDto() {
                Id = Guid.NewGuid(),
                Name = FakerName,
                CloseDate = FakerDate,
                DueDate = FakerDate,
                CurrentValue = 100,
                Description = FakerName,
                CreatedAt = FakerDate,
                UpdatedAt = FakerDate,
                Entrances = new List<EntranceResultDto>(),
                WalletType = new WalletTypeResultDto()
            };
            
            _serviceMock = new Mock<IWalletService>();
            _serviceMock.Setup(m => m.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(walletResultDto);
            _service = _serviceMock.Object;

            var result = await _service.FindByIdAsync(It.IsAny<Guid>());
            Assert.NotNull(result);
            Assert.False(result.Id.Equals(Guid.Empty));
            Assert.Equal(walletResultDto.Id, result.Id);
            Assert.Equal(walletResultDto.CreatedAt, result.CreatedAt);
            Assert.Equal(walletResultDto.UpdatedAt, result.UpdatedAt);
        }

        [Fact(DisplayName = "Update wallet type")]
        [Trait("Crud", "ShouldUpdateWallet")]
        public async void ShouldUpdateWallet()
        {
            WalletUpdateDto walletTypeUpdateDto = new WalletUpdateDto() {Name = FakerName};

            WalletResultDto walletResultDto = new WalletResultDto() {
                Id = Guid.NewGuid(),
                Name = FakerName,
                CreatedAt = FakerDate,
                UpdatedAt = FakerDate
            };

            _serviceMock = new Mock<IWalletService>();
            _serviceMock.Setup(m => m.UpdateAsync(walletTypeUpdateDto)).ReturnsAsync(walletResultDto);
            _service = _serviceMock.Object;

            var result = await _service.UpdateAsync(walletTypeUpdateDto);
            Assert.NotNull(result);
            Assert.False(result.Id.Equals(Guid.Empty));
            Assert.Equal(FakerName, result.Name);
        }

        [Fact(DisplayName = "Delete wallet type")]
        [Trait("Crud", "ShouldDeleteWallet")]
        public async void ShouldDeleteWallet()
        {
            _serviceMock = new Mock<IWalletService>();
            _serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result = await _service.DeleteAsync(Guid.NewGuid());
            Assert.True(result);
        }

        [Fact(DisplayName = "Not Delete wallet type")]
        [Trait("Crud", "ShouldNotDeleteWallet")]
        public async void ShouldNotDeleteWallet()
        {
            _serviceMock = new Mock<IWalletService>();
            _serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);
            _service = _serviceMock.Object;

            var result = await _service.DeleteAsync(Guid.NewGuid());
            Assert.False(result);
        }
    }
}
