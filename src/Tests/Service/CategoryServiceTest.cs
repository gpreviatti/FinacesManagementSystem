using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Dtos.Category;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Moq;
using Service.Services;
using Xunit;

namespace Tests.Service
{
    public class CategoryServiceTest : BaseServiceTest
    {
        private ICategoryService _service;
        private Mock<ICategoryRepository> _repositoryMock = new Mock<ICategoryRepository>();

        public CategoryServiceTest()
        {
            _service = new CategoryService(_repositoryMock.Object, _mapper);
        }

        [Fact(DisplayName = "Create category")]
        [Trait("Service", "Category")]
        public async void ShouldCreateCategory()
        {
            try
            {
                var mainCategoryGuid = Guid.NewGuid();
                var categoryCreateDto = new CategoryCreateDto { Name = _fakerName, CategoryId = mainCategoryGuid };
                var category = _mapper.Map<Category>(categoryCreateDto);

                var categoryResult = new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryId = mainCategoryGuid,
                    Name = _fakerName,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UserId = _userAdminId
                };

                _repositoryMock.Setup(r => r.CreateAsync(category).Result).Returns(categoryResult);
                var result = await _service.CreateAsync(categoryCreateDto, _userAdminId);

                Assert.NotNull(result);
                Assert.False(result.UserId.Equals(Guid.Empty));
                Assert.Equal(_fakerName, result.Name);
            }
            catch (Exception exception)
            {
                Assert.True(false, exception.Message);
            }
        }

        [Fact(DisplayName = "Find name and id of all common user categories")]
        [Trait("Service", "Category")]
        public async void ShouldFindAsyncNameAndIdUserCategories()
        {
            var listCategoryResultDto = new List<Category>
            {
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
            };

            _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId).Result).Returns(listCategoryResultDto);
            var result = await _service.FindAsyncNameAndIdUserCategories(_userAdminId);

            Assert.NotNull(result);
            Assert.True(result.Count() == 10);
            Assert.Equal(listCategoryResultDto.FirstOrDefault().Name, result.FirstOrDefault().Name);
            _repositoryMock.Verify(m => m.FindAsyncAllCommonAndUserCategories(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "Find all common user categories")]
        [Trait("Service", "Category")]
        public async void ShouldFindAsyncAllCommonAndUserCategories()
        {
            var listCategoryResultDto = new List<Category>
            {
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName()},
            };

            _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId).Result).Returns(listCategoryResultDto);
            var result = await _service.FindAsyncAllCommonAndUserCategories(_userAdminId);

            Assert.NotNull(result);
            Assert.True(result.Count() == 10);
            Assert.Equal(listCategoryResultDto.FirstOrDefault().Name, result.FirstOrDefault().Name);
            _repositoryMock.Verify(m => m.FindAsyncAllCommonAndUserCategories(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "Setup category update view model")]
        [Trait("Service", "Category")]
        public async void ShouldSetupCategoryUpdateViewModel()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = new Category { Id = categoryId, Name = Faker.Name.FullName(), UserId = _userAdminId };

            var listCategoryResultDto = new List<Category>
            {
                new Category { Id = new Guid(), Name = Faker.Name.FullName()},
                new Category { Id = new Guid(), Name = Faker.Name.FullName()},
                new Category { Id = new Guid(), Name = Faker.Name.FullName()},
                new Category { Id = new Guid(), Name = Faker.Name.FullName()}
            };

            // Act
            _repositoryMock.Setup(m => m.FindByIdAsync(categoryId).Result).Returns(category);
            _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId).Result).Returns(listCategoryResultDto);
            var result = await _service.SetupCategoryUpdateViewModel(categoryId, _userAdminId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Categories.Count() == listCategoryResultDto.Count());
            Assert.True(result.Category.Id == category.Id);
            Assert.True(result.Category.Name == category.Name);
            _repositoryMock.Verify(m => m.FindByIdAsync(categoryId), Times.Once);
            _repositoryMock.Verify(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId), Times.Once);
        }

        [Theory(DisplayName = "Find all common user and categories with datatables format")]
        [Trait("Service", "Category")]
        [InlineData("Home", 1)]
        [InlineData("Salary", 1)]
        [InlineData("Gasoline", 1)]
        [InlineData("Health", 0)]
        public async void ShouldFindAsyncAllCommonAndUserCategoriesDatatables(string search, int quantityData)
        {
            // Arrange
            var listCategoryResultDto = new List<Category>
            {
                new Category { Id = new Guid(), Name = Faker.Name.FullName()},
                new Category { Id = new Guid(), Name = Faker.Name.FullName()},
                new Category { Id = new Guid(), Name = Faker.Name.FullName()},
                new Category { Id = new Guid(), Name = Faker.Name.FullName()}
            };

            var datatablesModel = new DatatablesModel<CategoryResultDto>
            {
                Draw = "1",
                Start = "0",
                Length = "10",
                SortColumn = 1,
                SortColumnDirection = "asc"
            };

            var listCategoryResultDtoSearch = new List<Category>
            {
                new Category { Id = new Guid(), Name = "Home"},
                new Category { Id = new Guid(), Name = "Salary"},
                new Category { Id = new Guid(), Name = "Gasoline"},
                new Category { Id = new Guid(), Name = _fakerName}
            };

            var datatablesModelSearch = new DatatablesModel<CategoryResultDto>
            {
                Draw = "1",
                Start = "0",
                Length = "10",
                SortColumn = 1,
                SortColumnDirection = "asc",
                SearchValue = search
            };

            // Act
            _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId).Result);
            var resultNull = await _service.FindAsyncAllCommonAndUserCategoriesDatatables(datatablesModel, _userAdminId);

            _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId).Result).Returns(listCategoryResultDto);
            var result = await _service.FindAsyncAllCommonAndUserCategoriesDatatables(datatablesModel, _userAdminId);

            _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId).Result).Returns(listCategoryResultDtoSearch);
            var resultSearch = await _service.FindAsyncAllCommonAndUserCategoriesDatatables(datatablesModelSearch, _userAdminId);

            // Assert
            Assert.Null(resultNull);
            Assert.NotNull(result);
            Assert.NotNull(resultSearch);
            Assert.True(result.Data.Count().Equals(listCategoryResultDto.Count()));
            Assert.True(resultSearch.Data.Count().Equals(quantityData));
            _repositoryMock.Verify(r => r.FindAsyncAllCommonAndUserCategories(_userAdminId), Times.Exactly(3));
        }

        [Fact(DisplayName = "Find all common user and categories with datatables format with sort data")]
        [Trait("Service", "Category")]
        public async void ShouldFindAsyncAllCommonAndUserCategoriesDatatablesSortTest()
        {
            // Arrange

            // Arrange sort name
            var sortName = "Education";
            var datatablesModelSortName = new DatatablesModel<CategoryResultDto>
            {
                Draw = "1",
                Start = "0",
                Length = "10",
                SortColumnDirection = "asc",
                SortColumn = 0
            };

            var listCategorySortName = new List<Category>
            {
                new Category { Id = new Guid(), Name = "Food"},
                new Category { Id = new Guid(), Name = "Salary"},
                new Category { Id = new Guid(), Name = sortName},
                new Category { Id = new Guid(), Name = "Groceries"}
            };

            // Arrange sort total
            var total = 5.00;
            var datatablesModelSortTotal = new DatatablesModel<CategoryResultDto>
            {
                Draw = "1",
                Start = "0",
                Length = "10",
                SortColumnDirection = "asc",
                SortColumn = 1
            };
            datatablesModelSortTotal.SortColumn = 1;

            var listCategorySortTotal = new List<Category>
            {
                new Category { 
                    Id = new Guid(), 
                    Name = "Food",
                    Entrances = new List<Entrance> { new Entrance { Value = 30 } }
                },
                new Category { 
                    Id = new Guid(), 
                    Name = "Salary", 
                    Entrances = new List<Entrance> { new Entrance { Value = 10 } } 
                },
                new Category { 
                    Id = new Guid(), 
                    Name = "Education",
                    Entrances = new List<Entrance> { new Entrance { Value = 10 } }
                },
                new Category { 
                    Id = new Guid(), 
                    Name = "Groceries",
                    Entrances = new List<Entrance> { new Entrance { Value = 5 } }
                }
            };

            // Arrange sort created at
            var createdAt = DateTime.Now;
            var datatablesModelCreatedAt = new DatatablesModel<CategoryResultDto> 
            {
                Draw = "1",
                Start = "0",
                Length = "10",
                SortColumnDirection = "asc",
                SortColumn = 2
            };

            var listCategorySortCreatedAt = new List<Category>
            {
                new Category {
                    Id = new Guid(),
                    Name = "Food",
                    CreatedAt = DateTime.Now.AddHours(1)
                },
                new Category {
                    Id = new Guid(),
                    Name = "Salary",
                    CreatedAt = DateTime.Now.AddHours(2)
                },
                new Category {
                    Id = new Guid(),
                    Name = "Education",
                    CreatedAt = createdAt
                },
                new Category {
                    Id = new Guid(),
                    Name = "Groceries",
                    CreatedAt = DateTime.Now.AddHours(4)
                }
            };

            // Act
            _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId).Result).Returns(listCategorySortName);
            var resultSortName = await _service.FindAsyncAllCommonAndUserCategoriesDatatables(datatablesModelSortName, _userAdminId);

            _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId).Result).Returns(listCategorySortTotal);
            var resultSortTotal = await _service.FindAsyncAllCommonAndUserCategoriesDatatables(datatablesModelSortTotal, _userAdminId);

            _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId).Result).Returns(listCategorySortCreatedAt);
            var resultSortCreatedAt = await _service.FindAsyncAllCommonAndUserCategoriesDatatables(datatablesModelCreatedAt, _userAdminId);

            // Assert
            Assert.NotNull(resultSortName);
            Assert.True(resultSortName.Data.FirstOrDefault().Name == sortName);
            Assert.True(resultSortName.Data.Count().Equals(listCategorySortName.Count()));

            Assert.NotNull(resultSortTotal);
            Assert.True(resultSortTotal.Data.FirstOrDefault().Total == total);
            Assert.True(resultSortTotal.Data.Count().Equals(listCategorySortTotal.Count()));

            Assert.NotNull(resultSortCreatedAt);
            Assert.True(resultSortCreatedAt.Data.FirstOrDefault().CreatedAt == createdAt);
            Assert.True(resultSortCreatedAt.Data.Count().Equals(listCategorySortCreatedAt.Count()));

            _repositoryMock.Verify(r => r.FindAsyncAllCommonAndUserCategories(_userAdminId), Times.Exactly(3));
        }

        [Fact(DisplayName = "Find all common user categories")]
        [Trait("Service", "Category")]
        public async void ShouldFindAllCommonUserCategories()
        {
            var listCategoryResultDto = new List<Category>
            {
                new Category{ Id = new Guid(), Name = Faker.Name.FullName(), UserId = _userAdminId},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName(), UserId = _userAdminId},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName(), UserId = _userAdminId},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName(), UserId = _userAdminId},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName(), UserId = _userAdminId},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName(), UserId = _userAdminId},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName(), UserId = _userAdminId},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName(), UserId = _userAdminId},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName(), UserId = _userAdminId},
                new Category{ Id = new Guid(), Name = Faker.Name.FullName(), UserId = _userAdminId},
            };

            _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId).Result).Returns(listCategoryResultDto);
            var result = await _service.FindAsyncAllCommonAndUserCategories(_userAdminId);

            Assert.NotNull(result);
            Assert.True(result.Count() == 10);
            Assert.Equal(listCategoryResultDto.FirstOrDefault().Name, result.FirstOrDefault().Name);
            Assert.Equal(listCategoryResultDto.FirstOrDefault().Id, result.FirstOrDefault().Id);
            Assert.Equal(listCategoryResultDto.FirstOrDefault().UserId, result.FirstOrDefault().UserId);
            _repositoryMock.Verify(m => m.FindAsyncAllCommonAndUserCategories(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "List category by id")]
        [Trait("Service", "Category")]
        public async void ShouldListCategoryById()
        {
            try
            {
                var categoryResultDto = new Category()
                {
                    Id = new Guid(),
                    Name = Faker.Name.FullName(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Entrances = new List<Entrance>()
                };

                _repositoryMock.Setup(m => m.FindByIdAsync(It.IsAny<Guid>()).Result).Returns(categoryResultDto);
                var result = await _service.FindByIdAsync(It.IsAny<Guid>());

                Assert.NotNull(result);
                Assert.Equal(categoryResultDto.Id, result.Id);
                Assert.Equal(categoryResultDto.CreatedAt, result.CreatedAt);
                Assert.Equal(categoryResultDto.UpdatedAt, result.UpdatedAt);
                _repositoryMock.Verify(m => m.FindByIdAsync(It.IsAny<Guid>()), Times.Once);
            }
            catch (Exception exception)
            {
                Assert.True(false, exception.Message);
            }
        }

        [Fact(DisplayName = "Find Category by id with an CategoryUpdateDto as result")]
        [Trait("Service", "Category")]
        public async void ShouldFindByIdUpdateAsync()
        {
            try
            {
                // Arrange
                var categoryId = Guid.NewGuid();
                var categoryResultDto = new Category()
                {
                    Id = categoryId,
                    Name = Faker.Name.FullName(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Entrances = new List<Entrance>()
                };

                // Act
                _repositoryMock.Setup(m => m.FindByIdAsync(categoryId).Result).Returns(categoryResultDto);
                var result = await _service.FindByIdUpdateAsync(categoryId);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(categoryId, result.Id);
                _repositoryMock.Verify(m => m.FindByIdAsync(It.IsAny<Guid>()), Times.Once);
            }
            catch (Exception exception)
            {
                Assert.True(false, exception.Message);
            }
        }

        [Fact(DisplayName = "Update category")]
        [Trait("Service", "Category")]
        public async void ShouldUpdateCategory()
        {
            try
            {
                var categoryId = Guid.NewGuid();
                var categoryToUpdate = new CategoryUpdateDto { Id = categoryId, Name = Faker.Name.FullName() };

                var category = new Category
                {
                    Id = categoryId,
                    Name = _fakerName,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Entrances = new List<Entrance>()
                };

                _repositoryMock.Setup(m => m.FindByIdAsync(categoryId).Result).Returns(category);
                _repositoryMock.Setup(m => m.SaveChangesAsync().Result).Returns(1);
                var result = await _service.UpdateAsync(categoryToUpdate);

                _repositoryMock.Setup(m => m.FindByIdAsync(categoryId).Result);
                var resultCategoryNotFound = await _service.UpdateAsync(categoryToUpdate);

                _repositoryMock.Setup(m => m.FindByIdAsync(categoryId).Result).Returns(category);
                _repositoryMock.Setup(m => m.SaveChangesAsync().Result);
                var resultCategoryNotUpdated = await _service.UpdateAsync(categoryToUpdate);

                Assert.NotNull(result);
                Assert.NotNull(result.Name);
                Assert.NotNull(result.Entrances);
                Assert.NotEqual(_fakerName, result.Name);
                Assert.Equal(category.CreatedAt, result.CreatedAt);
                Assert.Equal(category.UpdatedAt, result.UpdatedAt);
                Assert.Null(resultCategoryNotFound);
                Assert.Null(resultCategoryNotUpdated);
                _repositoryMock.Verify(m => m.FindByIdAsync(categoryId), Times.Exactly(3));
                _repositoryMock.Verify(m => m.SaveChangesAsync(), Times.Exactly(2));
            }
            catch (Exception exception)
            {
                Assert.True(false, exception.Message);
            }
        }

        [Fact(DisplayName = "Delete category")]
        [Trait("Service", "Category")]
        public async void ShouldDeleteCategory()
        {
            try
            {
                _repositoryMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>()).Result).Returns(true);
                var result = await _service.DeleteAsync(Guid.NewGuid());

                Assert.True(result);
                _repositoryMock.Verify(m => m.DeleteAsync(It.IsAny<Guid>()), Times.Once);
            }
            catch (Exception exception)
            {
                Assert.True(false, exception.Message);
            }
        }

        [Fact(DisplayName = "Not Delete category")]
        [Trait("Service", "Category")]
        public async void ShouldNotDeleteCategory()
        {
            try
            {
                _repositoryMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>()).Result).Returns(false);
                var result = await _service.DeleteAsync(Guid.NewGuid());

                Assert.False(result);
                _repositoryMock.Verify(m => m.DeleteAsync(It.IsAny<Guid>()), Times.Once);
            }
            catch (Exception exception)
            {
                Assert.True(false, exception.Message);
            }
        }
    }
}
