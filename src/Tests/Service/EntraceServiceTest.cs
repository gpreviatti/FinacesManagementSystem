using System;
using System.Collections.Generic;
using System.Diagnostics;
using Domain.Dtos.Category;
using Domain.Dtos.Entrance;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Service.Services;
using Xunit;

namespace Tests.Service
{
    public class EntranceServiceTest : BaseServiceTest
    {
        private Mock<IEntranceRepository> _repositoryMock;
        private Mock<IWalletService> _walletServiceMock;
        private Mock<ICategoryService> _categoryServiceMock;
        private IEntranceService _service;

        public EntranceServiceTest()
        {
            _repositoryMock = new Mock<IEntranceRepository>();
            _walletServiceMock = new Mock<IWalletService>();
            _categoryServiceMock = new Mock<ICategoryService>();
            _service = new EntranceService(
                _mapper, 
                _repositoryMock.Object,
                _walletServiceMock.Object,
                _categoryServiceMock.Object
            );
        }

        [Fact(DisplayName = "Create entrance")]
        [Trait("Service", "Entrance")]
        public async void ShouldCreateEntrance()
        {
            try
            {
                // Arrange
                var categoryId = Guid.NewGuid();
                var walletId = Guid.NewGuid();
                var entrance = new Entrance
                {
                    Id = Guid.NewGuid(),
                    Description = _fakerName,
                    Observation = _fakerName,
                    Ticker = "TEST",
                    Type = 1,
                    Value = 100,
                    CategoryId = categoryId,
                    WalletId = walletId
                };
                var entranceCreateDto = _mapper.Map<EntranceCreateDto>(entrance);

                var categoryResultDto = new CategoryResultDto
                {
                    Id = categoryId,
                    Name = _fakerName
                };

                // Act
                _walletServiceMock
                    .Setup(w => w.UpdateWalletValue(walletId, It.IsAny<int>(), It.IsAny<double>()).Result)
                    .Returns(1);
                _categoryServiceMock
                    .Setup(c => c.FindByIdAsync(categoryId).Result)
                    .Returns(categoryResultDto);

                _repositoryMock.Setup(m => m.CreateAsync(entrance).Result).Returns(entrance);
                var result = await _service.CreateAsync(entranceCreateDto);

                // Assert
                Assert.NotNull(result);
                Assert.False(result.Id.Equals(Guid.Empty));
                Assert.Equal(entrance.Description, result.Description);
                Assert.Equal(entrance.Observation, result.Observation);
                Assert.Equal(entrance.Ticker, result.Ticker);
                Assert.Equal(entrance.Type, result.Type);
                Assert.Equal(entrance.Value, result.Value);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Not Create entrace when wallet is null")]
        [Trait("Service", "Entrance")]
        public async void ShouldNotCreateEntranceWhenWalletIsNull()
        {
            try
            {
                // Arrange
                var entrance = new Entrance()
                {
                    Description = _fakerName,
                    Observation = _fakerName,
                    Ticker = "TEST",
                    Type = 1,
                    Value = 100,
                    CategoryId = Guid.NewGuid(),
                };
                var entranceCreateDto = _mapper.Map<EntranceCreateDto>(entrance);

                // Act
                _repositoryMock.Setup(m => m.CreateAsync(entrance));
                
                var result = await _service.CreateAsync(entranceCreateDto);

                // Assert
                Assert.Null(result);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Not Create entrace when category is null")]
        [Trait("Service", "Entrance")]
        public async void ShouldNotCreateEntranceWhenCategoryIsNull()
        {
            try
            {
                // Arrange
                var entrance = new Entrance()
                {
                    Description = _fakerName,
                    Observation = _fakerName,
                    Ticker = "TEST",
                    Type = 1,
                    Value = 100,
                    WalletId = Guid.NewGuid()
                };

                var entraceCreateDto = _mapper.Map<EntranceCreateDto>(entrance);

                // Act
                _repositoryMock.Setup(m => m.CreateAsync(entrance));
                var result = await _service.CreateAsync(entraceCreateDto);

                // Assert
                Assert.Null(result);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Assert.True(false);
            }
        }

        //[Fact(DisplayName = "List categories")]
        //[Trait("Service", "Entrance")]
        //public async void ShouldListEntrance()
        //{
        //    try
        //    {
        //        IEnumerable<EntranceResultDto> listEntranceResultDto = new List<EntranceResultDto>
        //    {
        //        new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
        //        new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
        //        new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
        //        new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
        //        new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
        //        new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
        //        new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
        //        new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
        //        new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()},
        //        new EntranceResultDto(){ Id = new Guid(), Description = Faker.Name.FullName()}
        //    };

        //        _repositoryMock.Setup(m => m.FindAllAsyncWithCategory()).ReturnsAsync(listEntranceResultDto);
        //        var result = await _service.FindAllAsyncWithCategoryDatatables();

        //        Assert.NotNull(result);
        //        Assert.True(result.Count() == 10);
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e);
        //        Assert.True(false);
        //    }
        //}

        //[Fact(DisplayName = "List entrace by id")]
        //[Trait("Service", "Entrance")]
        //public async void ShouldListEntranceById()
        //{
        //    try
        //    {
        //        // Arrange
        //        var entrance = new Entrance()
        //        {
        //            Id = new Guid(),
        //            Description = Faker.Name.FullName(),
        //            CreatedAt = DateTime.Now,
        //            UpdatedAt = DateTime.Now
        //        };

        //        // Act
        //        _repositoryMock.Setup(m => m.FindByIdAsync(It.IsAny<Guid>()).Result).Returns(entrance);
        //        var result = await _service.FindByIdAsync(It.IsAny<Guid>());

        //        // Assert
        //        Assert.NotNull(result);
        //        Assert.Equal(entrance.Id, result.Id);
        //        Assert.Equal(entrance.CreatedAt, result.CreatedAt);
        //        Assert.Equal(entrance.UpdatedAt, result.UpdatedAt);
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e);
        //        Assert.True(false);
        //    }
        //}

        [Fact(DisplayName = "List entrace by id and return EntranceUpdateDto")]
        [Trait("Service", "Entrance")]
        public async void ShouldListEntranceById()
        {
            try
            {
                var entrance = new Entrance()
                {
                    Id = new Guid(),
                    Description = Faker.Name.FullName(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _repositoryMock.Setup(m => m.FindByIdAsync(It.IsAny<Guid>()).Result).Returns(entrance);
                var result = await _service.FindByIdUpdateAsync(It.IsAny<Guid>());

                Assert.NotNull(result);
                Assert.Equal(entrance.Id, result.Id);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Delete entrace")]
        [Trait("Service", "Entrance")]
        public async void ShouldDeleteEntrance()
        {
            try
            {
                _repositoryMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);
                var result = await _service.DeleteAsync(Guid.NewGuid());

                Assert.True(result);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Assert.True(false);
            }

        }

        [Fact(DisplayName = "Not Delete entrace")]
        [Trait("Service", "Entrance")]
        public async void ShouldNotDeleteEntrance()
        {
            try
            {
                _repositoryMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);
                var result = await _service.DeleteAsync(Guid.NewGuid());

                Assert.False(result);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Assert.True(false);
            }
        }
    }
}
