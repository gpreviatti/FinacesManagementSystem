using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Dtos.Category;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Xunit;
using Domain.Mappers;
using Domain.Services;
using System.Threading.Tasks;

namespace Tests.Domain.Service;

public class CategoryServiceTest : BaseServiceTest
{
    private readonly ICategoryService _service;
    private readonly Mock<ICategoryRepository> _repositoryMock = new();

    public CategoryServiceTest()
    {
        _service = new CategoryService(_repositoryMock.Object);
    }

    [Fact(DisplayName = "Create category")]
    [Trait("Service", "Category")]
    public async Task ShouldCreateCategory()
    {
        // Arrange
        var mainCategoryGuid = Guid.NewGuid();
        var categoryCreateDto = new CategoryCreateDto { 
            Name = _fakerName, CategoryId = mainCategoryGuid 
        };
        
        var category = categoryCreateDto.MapperToCreateDto();

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
        
        // Act
        var result = await _service.CreateAsync(categoryCreateDto, _userAdminId);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.UserId.Equals(Guid.Empty));
        Assert.Equal(_fakerName, result.Name);
    }

    [Fact(DisplayName = "Find name and id of all common user categories")]
    [Trait("Service", "Category")]
    public async void ShouldFindAsyncNameAndIdUserCategories()
    {
        // Arrange
        var listCategoryResultDto = new List<Category>
        {
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
        }.AsQueryable();

        _repositoryMock.Setup(m => m.FindAsyncNameAndIdUserCategories(_userAdminId).Result).Returns(listCategoryResultDto);
        
        // Act
        var result = await _service.FindAsyncNameAndIdUserCategories(_userAdminId);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() == 10);
        Assert.Equal(listCategoryResultDto.FirstOrDefault().Name, result.FirstOrDefault().Name);
        _repositoryMock.Verify(m => m.FindAsyncNameAndIdUserCategories(It.IsAny<Guid>()), Times.Once);
    }

    [Fact(DisplayName = "Should find all user categories")]
    [Trait("Service", "Category")]
    public async void Should_Find_All_User_Categories()
    {
        // Arrange
        var listCategoryResultDto = new List<Category>
        {
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
        }.AsQueryable();

        _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId)).Returns(listCategoryResultDto);

        // Act
        var result = await _service.FindUserCategories("", "", _userAdminId);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() == 10);
        Assert.Equal(listCategoryResultDto.FirstOrDefault().Name, result.FirstOrDefault().Name);
        
        _repositoryMock.Verify(m => m.FindAsyncAllCommonAndUserCategories(It.IsAny<Guid>()), Times.Once);
    }

    [Fact(DisplayName = "Find all common user categories")]
    [Trait("Service", "Category")]
    public async void ShouldFindAsyncAllCommonAndUserCategories()
    {
        // Arrange
        var listCategoryResultDto = new List<Category>
        {
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
            new () { Id = new (), Name = Faker.Name.FullName()},
        };

        _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId)).Returns(listCategoryResultDto.AsQueryable());

        // Act            
        var result = await _service.FindAsyncAllCommonAndUserCategories(_userAdminId);

        // Assert
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
        }.AsQueryable();

        // Act
        _repositoryMock.Setup(m => m.FindByIdAsync(categoryId, It.IsAny<bool>()).Result).Returns(category);
        _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId)).Returns(listCategoryResultDto);
        var result = await _service.SetupCategoryUpdateViewModel(categoryId, _userAdminId);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Categories.Count() == listCategoryResultDto.Count());
        Assert.True(result.Category.Id == category.Id);
        Assert.True(result.Category.Name == category.Name);
        _repositoryMock.Verify(m => m.FindByIdAsync(categoryId, It.IsAny<bool>()), Times.Once);
        _repositoryMock.Verify(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId), Times.Once);
    }

    //[Theory(DisplayName = "Find all common user and categories with datatables format")]
    //[Trait("Service", "Category")]
    //[InlineData("Home", 1)]
    //[InlineData("Salary", 1)]
    //[InlineData("Gasoline", 1)]
    //[InlineData("Health", 0)]
    //public async void ShouldFindAsyncAllCommonAndUserCategoriesDatatables(string search, int quantityData)
    //{
    //    try
    //    {
    //        // Arrange
    //        var listCategoryResultDto = new List<Category>
    //        {
    //            new { Id = new (), Name = Faker.Name.FullName()},
    //            new { Id = new (), Name = Faker.Name.FullName()},
    //            new { Id = new (), Name = Faker.Name.FullName()},
    //            new { Id = new (), Name = Faker.Name.FullName()}
    //        }.AsQueryable();

    //        var datatablesModel = new DatatablesModel<CategoryResultDto>
    //        {
    //            Draw = "1",
    //            Start = "0",
    //            Length = "10",
    //            SortColumn = 1,
    //            SortColumnDirection = "asc"
    //        };

    //        var listCategoryResultDtoSearch = new List<Category>
    //        {
    //            new { Id = new (), Name = "Home"},
    //            new { Id = new (), Name = "Salary"},
    //            new { Id = new (), Name = "Gasoline"},
    //            new { Id = new (), Name = _fakerName}
    //        }.AsQueryable();

    //        var datatablesModelSearch = new DatatablesModel<CategoryResultDto>
    //        {
    //            Draw = "1",
    //            Start = "0",
    //            Length = "10",
    //            SortColumn = 1,
    //            SortColumnDirection = "asc",
    //            SearchValue = search
    //        };

    //        // Act
    //        _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId));
    //        var resultNull = await _service.FindAsyncAllCommonAndUserCategoriesDatatables(datatablesModel, _userAdminId);

    //        _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId)).Returns(listCategoryResultDto);
    //        var result = await _service.FindAsyncAllCommonAndUserCategoriesDatatables(datatablesModel, _userAdminId);

    //        _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId)).Returns(listCategoryResultDtoSearch);
    //        var resultSearch = await _service.FindAsyncAllCommonAndUserCategoriesDatatables(datatablesModelSearch, _userAdminId);

    //        // Assert
    //        Assert.Null(resultNull);
    //        Assert.NotNull(result);
    //        Assert.NotNull(resultSearch);
    //        Assert.True(result.Data.Count().Equals(listCategoryResultDto.Count()));
    //        Assert.True(resultSearch.Data.Count().Equals(quantityData));
    //        _repositoryMock.Verify(r => r.FindAsyncAllCommonAndUserCategories(_userAdminId), Times.Exactly(3));
    //    }
    //    catch (Exception exception)
    //    {
    //        Debug.WriteLine(exception.Message);
    //        Assert.True(false);
    //    }
    //}

    //[Fact(DisplayName = "Find all common user and categories with datatables format with sort data")]
    //[Trait("Service", "Category")]
    //public async void ShouldFindAsyncAllCommonAndUserCategoriesDatatablesSortTest()
    //{
    //    try
    //    {
    //        // Arrange

    //        // Arrange sort name
    //        var sortName = "Education";
    //        var datatablesModelSortName = new DatatablesModel<CategoryResultDto>
    //        {
    //            Draw = "1",
    //            Start = "0",
    //            Length = "10",
    //            SortColumnDirection = "asc",
    //            SortColumn = 0
    //        };

    //        var listCategorySortName = new List<Category>
    //        {
    //            new { Id = new (), Name = "Food"},
    //            new { Id = new (), Name = "Salary"},
    //            new { Id = new (), Name = sortName},
    //            new { Id = new (), Name = "Groceries"}
    //        }.AsQueryable();

    //        // Arrange sort total
    //        var total = 5.00;
    //        var datatablesModelSortTotal = new DatatablesModel<CategoryResultDto>
    //        {
    //            Draw = "1",
    //            Start = "0",
    //            Length = "10",
    //            SortColumnDirection = "asc",
    //            SortColumn = 1
    //        };
    //        datatablesModelSortTotal.SortColumn = 1;

    //        var listCategorySortTotal = new List<Category>
    //        {
    //            new Category {
    //                Id = new (),
    //                Name = "Food",
    //                Entrances = new List<Entrance> { new Entrance { Value = 30 } }
    //            },
    //            new Category {
    //                Id = new (),
    //                Name = "Salary",
    //                Entrances = new List<Entrance> { new Entrance { Value = 10 } }
    //            },
    //            new Category {
    //                Id = new (),
    //                Name = "Education",
    //                Entrances = new List<Entrance> { new Entrance { Value = 10 } }
    //            },
    //            new Category {
    //                Id = new (),
    //                Name = "Groceries",
    //                Entrances = new List<Entrance> { new Entrance { Value = 5 } }
    //            }
    //        }.AsQueryable();

    //        // Arrange sort created at
    //        var createdAt = DateTime.Now;
    //        var datatablesModelCreatedAt = new DatatablesModel<CategoryResultDto>
    //        {
    //            Draw = "1",
    //            Start = "0",
    //            Length = "10",
    //            SortColumnDirection = "asc",
    //            SortColumn = 2
    //        };

    //        var listCategorySortCreatedAt = new List<Category>
    //        {
    //            new Category {
    //                Id = new (),
    //                Name = "Food",
    //                CreatedAt = DateTime.Now.AddHours(1)
    //            },
    //            new Category {
    //                Id = new (),
    //                Name = "Salary",
    //                CreatedAt = DateTime.Now.AddHours(2)
    //            },
    //            new Category {
    //                Id = new (),
    //                Name = "Education",
    //                CreatedAt = createdAt
    //            },
    //            new Category {
    //                Id = new (),
    //                Name = "Groceries",
    //                CreatedAt = DateTime.Now.AddHours(4)
    //            }
    //        }.AsQueryable();

    //        // Act
    //        _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId)).Returns(listCategorySortName);
    //        var resultSortName = await _service.FindAsyncAllCommonAndUserCategoriesDatatables(datatablesModelSortName, _userAdminId);

    //        _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId)).Returns(listCategorySortTotal);
    //        var resultSortTotal = await _service.FindAsyncAllCommonAndUserCategoriesDatatables(datatablesModelSortTotal, _userAdminId);

    //        _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId)).Returns(listCategorySortCreatedAt);
    //        var resultSortCreatedAt = await _service.FindAsyncAllCommonAndUserCategoriesDatatables(datatablesModelCreatedAt, _userAdminId);

    //        // Assert
    //        Assert.NotNull(resultSortName);
    //        Assert.True(resultSortName.Data.FirstOrDefault().Name == sortName);
    //        Assert.True(resultSortName.Data.Count().Equals(listCategorySortName.Count()));

    //        Assert.NotNull(resultSortTotal);
    //        Assert.True(resultSortTotal.Data.FirstOrDefault().Total == total);
    //        Assert.True(resultSortTotal.Data.Count().Equals(listCategorySortTotal.Count()));

    //        Assert.NotNull(resultSortCreatedAt);
    //        Assert.True(resultSortCreatedAt.Data.FirstOrDefault().CreatedAt == createdAt);
    //        Assert.True(resultSortCreatedAt.Data.Count().Equals(listCategorySortCreatedAt.Count()));

    //        _repositoryMock.Verify(r => r.FindAsyncAllCommonAndUserCategories(_userAdminId), Times.Exactly(3));
    //    }
    //    catch (Exception exception)
    //    {
    //        Debug.WriteLine(exception.Message);
    //        Assert.True(false);
    //    }
    //}

    [Fact(DisplayName = "Find all common user categories")]
    [Trait("Service", "Category")]
    public async void ShouldFindAllCommonUserCategories()
    {
        // Arrange
        var listCategory = new List<Category>
        {
            new () { Id = new (), Name = Faker.Name.FullName(), UserId = _userAdminId},
            new () { Id = new (), Name = Faker.Name.FullName(), UserId = _userAdminId},
            new () { Id = new (), Name = Faker.Name.FullName(), UserId = _userAdminId},
            new () { Id = new (), Name = Faker.Name.FullName(), UserId = _userAdminId},
            new () { Id = new (), Name = Faker.Name.FullName(), UserId = _userAdminId},
            new () { Id = new (), Name = Faker.Name.FullName(), UserId = _userAdminId},
            new () { Id = new (), Name = Faker.Name.FullName(), UserId = _userAdminId},
            new () { Id = new (), Name = Faker.Name.FullName(), UserId = _userAdminId},
            new () { Id = new (), Name = Faker.Name.FullName(), UserId = _userAdminId},
            new () { Id = new (), Name = Faker.Name.FullName(), UserId = _userAdminId},
        }.AsQueryable();

        _repositoryMock.Setup(m => m.FindAsyncAllCommonAndUserCategories(_userAdminId)).Returns(listCategory);

        // Act
        var result = await _service.FindAsyncAllCommonAndUserCategories(_userAdminId);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() == 10);
        Assert.Equal(listCategory.FirstOrDefault().Name, result.FirstOrDefault().Name);
        Assert.Equal(listCategory.FirstOrDefault().Id, result.FirstOrDefault().Id);
        Assert.Equal(listCategory.FirstOrDefault().UserId, result.FirstOrDefault().UserId);
        _repositoryMock.Verify(m => m.FindAsyncAllCommonAndUserCategories(It.IsAny<Guid>()), Times.Once);
    }

    [Fact(DisplayName = "List category by id")]
    [Trait("Service", "Category")]
    public async void ShouldListCategoryById()
    {
        // Arrange
        var categoryResultDto = new Category()
        {
            Id = new Guid(),
            Name = Faker.Name.FullName(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Entrances = new List<Entrance>()
        };

        _repositoryMock.Setup(m => m.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()).Result).Returns(categoryResultDto);

        // Act
        var result = await _service.FindByIdAsync(It.IsAny<Guid>());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryResultDto.Id, result.Id);
        Assert.Equal(categoryResultDto.CreatedAt, result.CreatedAt);
        Assert.Equal(categoryResultDto.UpdatedAt, result.UpdatedAt);
        _repositoryMock.Verify(m => m.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()), Times.Once);
    }

    [Fact(DisplayName = "Find Category by id with an CategoryUpdateDto as result")]
    [Trait("Service", "Category")]
    public async void ShouldFindByIdUpdateAsync()
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
        _repositoryMock.Setup(m => m.FindByIdAsync(categoryId, It.IsAny<bool>()).Result).Returns(categoryResultDto);
        var result = await _service.FindByIdUpdateAsync(categoryId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryId, result.Id);
        _repositoryMock.Verify(m => m.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()), Times.Once);
    }

    [Fact(DisplayName = "Update category")]
    [Trait("Service", "Category")]
    public async void ShouldUpdateCategory()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var categoryToUpdate = new CategoryUpdateDto { 
            Id = categoryId, 
            Name = _fakerName
        };

        var category = new Category
        {
            Id = categoryId,
            Name = _fakerName,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Entrances = new List<Entrance>()
        };

        _repositoryMock.Setup(m => m.FindByIdAsync(categoryId, It.IsAny<bool>()).Result).Returns(category);
        
        _repositoryMock.Setup(m => m.UpdateAsync(It.IsAny<Category>())).ReturnsAsync(1);
        
        // Act
        var result = await _service.UpdateAsync(categoryToUpdate);

        _repositoryMock.Setup(m => m.FindByIdAsync(categoryId, It.IsAny<bool>()).Result);
        
        var resultCategoryNotFound = await _service.UpdateAsync(categoryToUpdate);

        // Assert
        Assert.NotNull(result);
        
        Assert.Null(resultCategoryNotFound);
        
        _repositoryMock.Verify(m => m.FindByIdAsync(categoryId, It.IsAny<bool>()), Times.Exactly(2));
        _repositoryMock.Verify(m => m.UpdateAsync(It.IsAny<Category>()), Times.Exactly(1));
    }

    [Fact(DisplayName = "Delete category")]
    [Trait("Service", "Category")]
    public async void ShouldDeleteCategory()
    {
        // Arrange
        _repositoryMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>()).Result).Returns(true);

        // Act
        var result = await _service.DeleteAsync(Guid.NewGuid());

        // Assert
        Assert.True(result);
        _repositoryMock.Verify(m => m.DeleteAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact(DisplayName = "Not Delete category")]
    [Trait("Service", "Category")]
    public async void ShouldNotDeleteCategory()
    {
        // Arrange
        _repositoryMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>()).Result).Returns(false);

        // Act
        var result = await _service.DeleteAsync(Guid.NewGuid());

        // Assert
        Assert.False(result);

        _repositoryMock.Verify(m => m.DeleteAsync(It.IsAny<Guid>()), Times.Once);
    }
}
