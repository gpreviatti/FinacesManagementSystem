using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Dtos.Entrace;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Tests.Service
{
    public class EntraceServiceTest : BaseServiceTest
    {
        private IEntraceService _service;
        private Mock<IEntraceService> _serviceMock;

        public EntraceServiceTest()
        {

        }

        [Fact(DisplayName = "Create entrace")]
        [Trait("Crud", "ShouldCreateEntrace")]
        public async void ShouldCreateEntrace()
        {
            EntraceCreateDto entraceCreateDto = new EntraceCreateDto() {
                Description = FakerName,
                Observation = FakerName,
                Ticker = "TEST",
                Type = 1,
                Value = 100,
                CategoryId = Guid.NewGuid(),
                WalletId = Guid.NewGuid()
            };

            EntraceResultDto entraceResultDto = new EntraceResultDto()
            {
                Id = Guid.NewGuid(),
                Description = FakerName,
                Observation = FakerName,
                Ticker = "TEST",
                Type = 1,
                Value = 100,
                CategoryId = Guid.NewGuid(),
                WalletId = Guid.NewGuid(),
                CreatedAt = FakerDate,
                UpdatedAt = FakerDate,
            };

            _serviceMock = new Mock<IEntraceService>();
            _serviceMock.Setup(m => m.CreateAsync(entraceCreateDto)).ReturnsAsync(entraceResultDto);
            _service = _serviceMock.Object;
            var result = await _service.CreateAsync(entraceCreateDto);

            Assert.NotNull(result);
            Assert.False(result.Id.Equals(Guid.Empty));
            Assert.Equal(entraceCreateDto.Description, result.Description);
            Assert.Equal(entraceCreateDto.Observation, result.Observation);
            Assert.Equal(entraceCreateDto.Ticker, result.Ticker);
            Assert.Equal(entraceCreateDto.Type, result.Type);
            Assert.Equal(entraceCreateDto.Value, result.Value);
            Assert.Equal(entraceCreateDto.CategoryId, result.CategoryId);
        }

        [Fact(DisplayName = "List categories")]
        [Trait("Crud", "ShouldListCategories")]
        public async void ShouldListEntrace()
        {
            IEnumerable<EntraceResultDto> listEntraceResultDto = new List<EntraceResultDto>
            {
                new EntraceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntraceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntraceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntraceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntraceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntraceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntraceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntraceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntraceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntraceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()}
            };

            _serviceMock = new Mock<IEntraceService>();
            _serviceMock.Setup(m => m.FindAllAsync()).ReturnsAsync(listEntraceResultDto);
            _service = _serviceMock.Object;
            var result = await _service.FindAllAsync();

            Assert.NotNull(result);
            Assert.True(result.Count() == 10);
        }

        [Fact(DisplayName = "List entrace by id")]
        [Trait("Crud", "ShouldListEntraceById")]
        public async void ShouldListEntraceById()
        {
            var entraceResultDto = new EntraceResultDto() {
                Id = new Guid(),
                Description = Faker.Name.FullName(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            
            _serviceMock = new Mock<IEntraceService>();
            _serviceMock.Setup(m => m.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(entraceResultDto);
            _service = _serviceMock.Object;
            var result = await _service.FindByIdAsync(It.IsAny<Guid>());

            Assert.NotNull(result);
            Assert.Equal(entraceResultDto.Id, result.Id);
            Assert.Equal(entraceResultDto.CreatedAt, result.CreatedAt);
            Assert.Equal(entraceResultDto.UpdatedAt, result.UpdatedAt);
        }

        [Fact(DisplayName = "Update entrace")]
        [Trait("Crud", "ShouldUpdateEntrace")]
        public async void ShouldUpdateEntrace()
        {
            EntraceUpdateDto entraceUpdateDto = new EntraceUpdateDto()
            {
                Description = FakerName,
            };

            EntraceResultDto entraceResultDto = new EntraceResultDto()
            {
                Description = FakerName,
                CreatedAt = FakerDate,
                UpdatedAt = FakerDate
            };

            _serviceMock = new Mock<IEntraceService>();
            _serviceMock.Setup(m => m.UpdateAsync(entraceUpdateDto)).ReturnsAsync(entraceResultDto);
            _service = _serviceMock.Object;
            var result = await _service.UpdateAsync(entraceUpdateDto);

            Assert.NotNull(result);
            Assert.Equal(FakerName, result.Description);
        }

        [Fact(DisplayName = "Delete entrace")]
        [Trait("Crud", "ShouldDeleteEntrace")]
        public async void ShouldDeleteEntrace()
        {
            _serviceMock = new Mock<IEntraceService>();
            _serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _service = _serviceMock.Object;
            var result = await _service.DeleteAsync(Guid.NewGuid());

            Assert.True(result);
        }

        [Fact(DisplayName = "Not Delete entrace")]
        [Trait("Crud", "ShouldNotDeleteEntrace")]
        public async void ShouldNotDeleteEntrace()
        {
            _serviceMock = new Mock<IEntraceService>();
            _serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);
            _service = _serviceMock.Object;
            var result = await _service.DeleteAsync(Guid.NewGuid());

            Assert.False(result);
        }
    }
}