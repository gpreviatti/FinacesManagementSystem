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
            try
            {
                var categoryId = Guid.NewGuid();
                var walletId = Guid.NewGuid();
                EntraceCreateDto entraceCreateDto = new EntraceCreateDto()
                {
                    Description = FakerName,
                    Observation = FakerName,
                    Ticker = "TEST",
                    Type = 1,
                    Value = 100,
                    CategoryId = categoryId,
                    WalletId = walletId
                };

                EntraceResultDto entraceResultDto = new EntraceResultDto()
                {
                    Id = Guid.NewGuid(),
                    Description = FakerName,
                    Observation = FakerName,
                    Ticker = "TEST",
                    Type = 1,
                    Value = 100,
                    CategoryId = categoryId,
                    WalletId = walletId,
                    CreatedAt = FakerDate,
                    UpdatedAt = FakerDate,
                };
                _serviceMock = new Mock<IEntraceService>();
                _serviceMock.Setup(m => m.CreateAsync(entraceCreateDto)).ReturnsAsync(entraceResultDto);
                var _service = _serviceMock.Object;

                var result = await _service.CreateAsync(entraceCreateDto);

                Assert.NotNull(result);
                Assert.False(result.Id.Equals(Guid.Empty));
                Assert.Equal(entraceCreateDto.Description, result.Description);
                Assert.Equal(entraceCreateDto.Observation, result.Observation);
                Assert.Equal(entraceCreateDto.Ticker, result.Ticker);
                Assert.Equal(entraceCreateDto.Type, result.Type);
                Assert.Equal(entraceCreateDto.Value, result.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Not Create entrace when wallet is null")]
        [Trait("Crud", "ShouldNotCreateEntraceWhenWalletIsNull")]
        public async void ShouldNotCreateEntraceWhenWalletIsNull()
        {
            try
            {
                EntraceCreateDto entraceCreateDto = new EntraceCreateDto()
                {
                    Description = FakerName,
                    Observation = FakerName,
                    Ticker = "TEST",
                    Type = 1,
                    Value = 100,
                    CategoryId = Guid.NewGuid(),
                };
                _serviceMock = new Mock<IEntraceService>();
                _serviceMock.Setup(m => m.CreateAsync(entraceCreateDto));
                var _service = _serviceMock.Object;

                var result = await _service.CreateAsync(entraceCreateDto);

                Assert.Null(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Not Create entrace when category is null")]
        [Trait("Crud", "ShouldNotCreateEntraceWhenCategoryIsNull")]
        public async void ShouldNotCreateEntraceWhenCategoryIsNull()
        {
            try
            {
                EntraceCreateDto entraceCreateDto = new EntraceCreateDto()
                {
                    Description = FakerName,
                    Observation = FakerName,
                    Ticker = "TEST",
                    Type = 1,
                    Value = 100,
                    WalletId = Guid.NewGuid()
                };
                _serviceMock = new Mock<IEntraceService>();
                _serviceMock.Setup(m => m.CreateAsync(entraceCreateDto));
                var _service = _serviceMock.Object;

                var result = await _service.CreateAsync(entraceCreateDto);

                Assert.Null(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "List categories")]
        [Trait("Crud", "ShouldListCategories")]
        public async void ShouldListEntrace()
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "List entrace by id")]
        [Trait("Crud", "ShouldListEntraceById")]
        public async void ShouldListEntraceById()
        {
            try
            {
                var entraceResultDto = new EntraceResultDto()
                {
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Update entrace")]
        [Trait("Crud", "ShouldUpdateEntrace")]
        public async void ShouldUpdateEntrace()
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Delete entrace")]
        [Trait("Crud", "ShouldDeleteEntrace")]
        public async void ShouldDeleteEntrace()
        {
            try
            {
                _serviceMock = new Mock<IEntraceService>();
                _serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);
                _service = _serviceMock.Object;
                var result = await _service.DeleteAsync(Guid.NewGuid());

                Assert.True(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.True(false);
            }
            
        }

        [Fact(DisplayName = "Not Delete entrace")]
        [Trait("Crud", "ShouldNotDeleteEntrace")]
        public async void ShouldNotDeleteEntrace()
        {
            try
            {
                _serviceMock = new Mock<IEntraceService>();
                _serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);
                _service = _serviceMock.Object;
                var result = await _service.DeleteAsync(Guid.NewGuid());

                Assert.False(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.True(false);
            }
        }
    }
}
