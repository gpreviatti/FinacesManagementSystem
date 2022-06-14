using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Xunit;

namespace Tests.Data;

public class CategoryDataTest : BaseDataTest
{
    private readonly ICategoryRepository _repository;

    public CategoryDataTest() => _repository = new CategoryRepository(_context);

    public static Category CreateCategoryEntity() => new() { Name = Faker.Name.FullName() };

    [Fact(DisplayName = "Create Category")]
    [Trait("Data", "Category")]
    public async Task ShouldCreateCategory()
    {
        // Arrange
        var categoryEntity = CreateCategoryEntity();

        // Act
        var result = await _repository.CreateAsync(categoryEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryEntity.Name, result.Name);
        Assert.False(result.Id == Guid.Empty);
    }

    [Fact(DisplayName = "List Categorys")]
    [Trait("Data", "Category")]
    public async Task ShouldListCategory()
    {
        // Arrange
        var categoryEntity = CreateCategoryEntity();

        await _repository.CreateAsync(categoryEntity);

        // Act
        var result = await _repository.FindAllAsync();

        // Assert
        Assert.NotNull(result);
    }

    [Fact(DisplayName = "List Category by Id")]
    [Trait("Data", "Category")]
    public async Task ShouldListCategoryById()
    {
        // Arrange
        var categoryEntity = CreateCategoryEntity();

        await _repository.CreateAsync(categoryEntity);

        // Act
        var result = await _repository.FindByIdAsync(categoryEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Category>(result);
        Assert.Equal(categoryEntity.Id, result.Id);
        Assert.Equal(categoryEntity.Name, result.Name);
    }

    [Fact(DisplayName = "List Common User Categories for Datatables")]
    [Trait("Data", "Category")]
    public void ShouledFindAsyncAllCommonAndUserCategoriesDatatables()
    {
        // Arrange

        // Act
        var result = _repository.FindAsyncAllCommonAndUserCategories(_testUser01Id);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());
    }

    [Fact(DisplayName = "List name and id User Categories")]
    [Trait("Data", "Category")]
    public async Task ShouledFindAsyncNameAndIdUserCategories()
    {
        // Arrange

        // Act
        var result = await _repository.FindAsyncNameAndIdUserCategories(_testUser01Id);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());
    }

    [Fact(DisplayName = "Update Category")]
    [Trait("Data", "Category")]
    public async Task ShouldUpdateCategory()
    {
        // Arrange
        var categoryEntity = CreateCategoryEntity();
        
        await _repository.CreateAsync(categoryEntity);

        // Act
        categoryEntity.Name = Faker.Name.FullName();
        var result = await _repository.SaveChangesAsync();

        // Assert
        Assert.Equal(1, result);

    }

    [Fact(DisplayName = "Delete Category")]
    [Trait("Data", "Category")]
    public async Task ShouldDeleteCategory()
    {
        // Arrange
        var categoryEntity = CreateCategoryEntity();

        await _repository.CreateAsync(categoryEntity);

        // Act
        var result = await _repository.DeleteAsync(categoryEntity.Id);

        // Assert
        Assert.True(result);
    }
}
