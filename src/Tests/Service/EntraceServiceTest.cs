using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Domain.Dtos.Category;
using Domain.Dtos.Entrance;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
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

        #region Find and List Tests
        [Fact(DisplayName = "Not Create entrace when wallet is null")]
        [Trait("Service", "Entrance")]
        public async void ShouldNotCreateEntranceWhenWalletIsNull()
        {
            try
            {
                // Arrange
                var entrance = new Entrance
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

        [Fact(DisplayName = "List entrances and return datatables formart")]
        [Trait("Service", "Entrance")]
        public async void ShouldListEntrancesAndReturnDatatablesFormat()
        {
            try
            {
                // Arrange
                var walletsId = new List<Guid>
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                };

                var listEntranceResultDto = new List<EntranceResultDto>
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
                }.AsQueryable();

                var datatablesTenRows = new DatatablesModel<EntranceResultDto> 
                {
                    Skip = 0,
                    Length = "10"
                };

                // Act
                _walletServiceMock
                    .Setup(w => w.FindAsyncWalletsUserIds(It.IsAny<Guid>()).Result);

                var resultDontHasWallets = await _service.FindAllAsyncWithCategoryDatatables(datatablesTenRows, _userAdminId);

                _walletServiceMock
                    .Setup(w => w.FindAsyncWalletsUserIds(_userAdminId).Result)
                    .Returns(walletsId);

                _repositoryMock
                    .Setup(m => m.FindAllAsyncWithCategory(walletsId).Result);

                var resultDontHasEntrances = await _service.FindAllAsyncWithCategoryDatatables(datatablesTenRows, _userAdminId);

                _repositoryMock
                    .Setup(m => m.FindAllAsyncWithCategory(walletsId).Result)
                    .Returns(listEntranceResultDto);

                var result = await _service.FindAllAsyncWithCategoryDatatables(datatablesTenRows, _userAdminId);

                // Assert
                Assert.Null(resultDontHasWallets);

                Assert.Null(resultDontHasEntrances);

                Assert.NotNull(result);
                Assert.True(result.Data.Count().Equals(listEntranceResultDto.Count()));

                _repositoryMock.Verify(r => r.FindAllAsyncWithCategory(walletsId), Times.Exactly(2));
                _walletServiceMock.Verify(r => r.FindAsyncWalletsUserIds(_userAdminId), Times.Exactly(3));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Assert.True(false);
            }
        }

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
        #endregion

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

        [Fact(DisplayName = "Update entrance")]
        [Trait("Service", "Entrance")]
        public async void ShouldUpdateEntrance()
        {
            try
            {
                // Arrange
                var entranceId = Guid.NewGuid();
                var categoryId = Guid.NewGuid();
                var walletId = Guid.NewGuid();
                var entrance = new Entrance
                {
                    Id = entranceId,
                    Description = _fakerName,
                    Observation = _fakerName,
                    Ticker = "TEST",
                    Type = 1,
                    Value = 100,
                    CategoryId = categoryId,
                    WalletId = walletId
                };
                var entranceUpdateDto = _mapper.Map<EntranceUpdateDto>(entrance);

                var categoryResultDto = new CategoryResultDto
                {
                    Id = categoryId,
                    Name = _fakerName
                };

                // Act
                _repositoryMock
                    .Setup(r => r.FindByIdAsync(entranceId).Result)
                    .Returns(entrance);

                _walletServiceMock
                    .Setup(w => w.UpdateWalletValue(walletId, It.IsAny<int>(), It.IsAny<double>()).Result)
                    .Returns(1);

                _categoryServiceMock
                    .Setup(c => c.FindByIdAsync(categoryId).Result)
                    .Returns(categoryResultDto);

                _repositoryMock
                    .Setup(m => m.SaveChangesAsync().Result)
                    .Returns(1);

                var result = await _service.UpdateAsync(entranceUpdateDto);

                _categoryServiceMock
                    .Setup(c => c.FindByIdAsync(categoryId).Result);
                var resultCategoryNotFound = await _service.UpdateAsync(entranceUpdateDto);

                _repositoryMock.Setup(m => m.SaveChangesAsync().Result);
                var resultEntranceNotSaved = await _service.UpdateAsync(entranceUpdateDto);

                _walletServiceMock
                    .Setup(w => w.UpdateWalletValue(walletId, It.IsAny<int>(), It.IsAny<double>()).Result);
                var resultNotUpdateWallet = await _service.UpdateAsync(entranceUpdateDto);

                _repositoryMock
                    .Setup(r => r.FindByIdAsync(entranceId).Result);
                var resultEntranceNotFound = await _service.UpdateAsync(entranceUpdateDto);

                // Assert
                Assert.NotNull(result);
                Assert.Null(resultEntranceNotFound);
                Assert.Null(resultNotUpdateWallet);
                Assert.Null(resultCategoryNotFound);
                Assert.Null(resultEntranceNotSaved);
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
