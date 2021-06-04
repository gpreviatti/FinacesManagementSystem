using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Dtos.Category;
using Domain.Dtos.WalletType;
using Domain.Dtos.Wallet;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Tests.Service
{
    public class WalletTypeServiceTest : BaseServiceTest
    {
        private IWalletTypeService _service;
        private Mock<IWalletTypeService> _serviceMock;

        public WalletTypeServiceTest()
        {

        }

        [Fact(DisplayName = "Create wallet type")]
        [Trait("Service", "WalletType")]
        public async void ShouldCreateWalletType()
        {
            WalletTypeCreateDto walletTypeCreateDto = new WalletTypeCreateDto() { Name = FakerName };

            WalletTypeResultDto userResultDto = new WalletTypeResultDto()
            {
                Id = Guid.NewGuid(),
                Name = FakerName,
                CreatedAt = FakerDate,
                UpdatedAt = FakerDate
            };
            _serviceMock = new Mock<IWalletTypeService>();
            _serviceMock.Setup(m => m.CreateAsync(walletTypeCreateDto)).ReturnsAsync(userResultDto);
            _service = _serviceMock.Object;

            var result = await _service.CreateAsync(walletTypeCreateDto);
            Assert.NotNull(result);
            Assert.Equal(walletTypeCreateDto.Name, result.Name);
        }

        [Fact(DisplayName = "List wallet types")]
        [Trait("Service", "WalletType")]
        public async void ShouldListWalletType()
        {
            IEnumerable<WalletTypeResultDto> listWalletTypeResultDto = new List<WalletTypeResultDto>
            {
                new WalletTypeResultDto(){ Id = new Guid(), Name = FakerName, CreatedAt = FakerDate, UpdatedAt = FakerDate},
                new WalletTypeResultDto(){ Id = new Guid(), Name = FakerName, CreatedAt = FakerDate, UpdatedAt = FakerDate},
                new WalletTypeResultDto(){ Id = new Guid(), Name = FakerName, CreatedAt = FakerDate, UpdatedAt = FakerDate},
                new WalletTypeResultDto(){ Id = new Guid(), Name = FakerName, CreatedAt = FakerDate, UpdatedAt = FakerDate},
                new WalletTypeResultDto(){ Id = new Guid(), Name = FakerName, CreatedAt = FakerDate, UpdatedAt = FakerDate},
                new WalletTypeResultDto(){ Id = new Guid(), Name = FakerName, CreatedAt = FakerDate, UpdatedAt = FakerDate},
                new WalletTypeResultDto(){ Id = new Guid(), Name = FakerName, CreatedAt = FakerDate, UpdatedAt = FakerDate},
                new WalletTypeResultDto(){ Id = new Guid(), Name = FakerName, CreatedAt = FakerDate, UpdatedAt = FakerDate},
                new WalletTypeResultDto(){ Id = new Guid(), Name = FakerName, CreatedAt = FakerDate, UpdatedAt = FakerDate},
                new WalletTypeResultDto(){ Id = new Guid(), Name = FakerName, CreatedAt = FakerDate, UpdatedAt = FakerDate}
            };
            _serviceMock = new Mock<IWalletTypeService>();
            _serviceMock.Setup(m => m.FindAllAsync()).ReturnsAsync(listWalletTypeResultDto);
            _service = _serviceMock.Object;

            var result = await _service.FindAllAsync();
            Assert.NotNull(result);
            Assert.True(result.Count() == 10);
        }

        [Fact(DisplayName = "List wallet type by id")]
        [Trait("Service", "WalletType")]
        public async void ShouldListWalletTypeById()
        {
            var walletTypeResultDto = new WalletTypeResultDto() { 
                Id = new Guid(), 
                Name = FakerName,
                CreatedAt = FakerDate,
                UpdatedAt = FakerDate
            };
            
            _serviceMock = new Mock<IWalletTypeService>();
            _serviceMock.Setup(m => m.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(walletTypeResultDto);
            _service = _serviceMock.Object;

            var result = await _service.FindByIdAsync(It.IsAny<Guid>());
            Assert.NotNull(result);
            Assert.False(result.Id != Guid.Empty);
            Assert.Equal(walletTypeResultDto.Id, result.Id);
            Assert.Equal(walletTypeResultDto.CreatedAt, result.CreatedAt);
            Assert.Equal(walletTypeResultDto.UpdatedAt, result.UpdatedAt);
        }

        [Fact(DisplayName = "Update wallet type")]
        [Trait("Service", "WalletType")]
        public async void ShouldUpdateWalletType()
        {
            WalletTypeUpdateDto walletTypeUpdateDto = new WalletTypeUpdateDto() {Name = FakerName};

            WalletTypeResultDto walletTypeResultDto = new WalletTypeResultDto() {
                Id = Guid.NewGuid(),
                Name = FakerName,
                CreatedAt = FakerDate,
                UpdatedAt = FakerDate
            };

            _serviceMock = new Mock<IWalletTypeService>();
            _serviceMock.Setup(m => m.UpdateAsync(walletTypeUpdateDto)).ReturnsAsync(walletTypeResultDto);
            _service = _serviceMock.Object;

            var result = await _service.UpdateAsync(walletTypeUpdateDto);
            Assert.NotNull(result);
            Assert.False(result.Id.Equals(Guid.Empty));
            Assert.Equal(FakerName, result.Name);
        }

        [Fact(DisplayName = "Delete wallet type")]
        [Trait("Service", "WalletType")]
        public async void ShouldDeleteWalletType()
        {
            _serviceMock = new Mock<IWalletTypeService>();
            _serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result = await _service.DeleteAsync(Guid.NewGuid());
            Assert.True(result);
        }

        [Fact(DisplayName = "Not Delete wallet type")]
        [Trait("Service", "WalletType")]
        public async void ShouldNotDeleteWalletType()
        {
            _serviceMock = new Mock<IWalletTypeService>();
            _serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);
            _service = _serviceMock.Object;

            var result = await _service.DeleteAsync(Guid.NewGuid());
            Assert.False(result);
        }
    }
}
