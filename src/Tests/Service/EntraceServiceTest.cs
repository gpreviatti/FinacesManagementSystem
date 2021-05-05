using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Dtos.Entrance;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Tests.Service
{
    public class EntranceServiceTest : BaseServiceTest
    {
        private IEntranceService _service;
        private Mock<IEntranceService> _serviceMock;

        public EntranceServiceTest()
        {

        }

        [Fact(DisplayName = "Create entrace")]
        [Trait("Crud", "ShouldCreateEntrance")]
        public async void ShouldCreateEntrance()
        {
            try
            {
                var categoryId = Guid.NewGuid();
                var walletId = Guid.NewGuid();
                EntranceCreateDto entraceCreateDto = new EntranceCreateDto()
                {
                    Description = FakerName,
                    Observation = FakerName,
                    Ticker = "TEST",
                    Type = 1,
                    Value = 100,
                    CategoryId = categoryId,
                    WalletId = walletId
                };

                EntranceResultDto entraceResultDto = new EntranceResultDto()
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
                _serviceMock = new Mock<IEntranceService>();
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
        [Trait("Crud", "ShouldNotCreateEntranceWhenWalletIsNull")]
        public async void ShouldNotCreateEntranceWhenWalletIsNull()
        {
            try
            {
                EntranceCreateDto entraceCreateDto = new EntranceCreateDto()
                {
                    Description = FakerName,
                    Observation = FakerName,
                    Ticker = "TEST",
                    Type = 1,
                    Value = 100,
                    CategoryId = Guid.NewGuid(),
                };
                _serviceMock = new Mock<IEntranceService>();
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
        [Trait("Crud", "ShouldNotCreateEntranceWhenCategoryIsNull")]
        public async void ShouldNotCreateEntranceWhenCategoryIsNull()
        {
            try
            {
                EntranceCreateDto entraceCreateDto = new EntranceCreateDto()
                {
                    Description = FakerName,
                    Observation = FakerName,
                    Ticker = "TEST",
                    Type = 1,
                    Value = 100,
                    WalletId = Guid.NewGuid()
                };
                _serviceMock = new Mock<IEntranceService>();
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
        public async void ShouldListEntrance()
        {
            try
            {
                IEnumerable<EntranceResultDto> listEntranceResultDto = new List<EntranceResultDto>
            {
                new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
                new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()}
            };

                //_serviceMock = new Mock<IEntranceService>();
                //_serviceMock.Setup(m => m.FindAllAsyncWithCategory()).ReturnsAsync(listEntranceResultDto);
                //_service = _serviceMock.Object;
                //var result = await _service.FindAllAsyncWithCategory();

                //Assert.NotNull(result);
                //Assert.True(result.Count() == 10);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "List entrace by id")]
        [Trait("Crud", "ShouldListEntranceById")]
        public async void ShouldListEntranceById()
        {
            try
            {
                var entraceResultDto = new EntranceResultDto()
                {
                    Id = new Guid(),
                    Description = Faker.Name.FullName(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _serviceMock = new Mock<IEntranceService>();
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
        [Trait("Crud", "ShouldUpdateEntrance")]
        public async void ShouldUpdateEntrance()
        {
            try
            {
                EntranceUpdateDto entraceUpdateDto = new EntranceUpdateDto()
                {
                    Description = FakerName,
                };

                EntranceResultDto entraceResultDto = new EntranceResultDto()
                {
                    Description = FakerName,
                    CreatedAt = FakerDate,
                    UpdatedAt = FakerDate
                };

                _serviceMock = new Mock<IEntranceService>();
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
        [Trait("Crud", "ShouldDeleteEntrance")]
        public async void ShouldDeleteEntrance()
        {
            try
            {
                _serviceMock = new Mock<IEntranceService>();
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
        [Trait("Crud", "ShouldNotDeleteEntrance")]
        public async void ShouldNotDeleteEntrance()
        {
            try
            {
                _serviceMock = new Mock<IEntranceService>();
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
