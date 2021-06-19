using System;
using System.Diagnostics;
using System.Linq;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Xunit;

namespace Tests.Data
{
    public class CategoryDataTest : BaseDataTest
    {
        private readonly ICategoryRepository _repository;

        public CategoryDataTest() => _repository = new CategoryRepository(_context);

        public Category CreateCategoryEntity() => new Category { Name = Faker.Name.FullName() };

        [Fact(DisplayName = "Create Category")]
        [Trait("Data", "Category")]
        public async void ShouldCreateCategory()
        {
            try
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
            catch (Exception e)
            {
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }

        [Fact(DisplayName = "List Categorys")]
        [Trait("Data", "Category")]
        public async void ShouldListCategory()
        {
            try
            {
                // Arrange
                var categoryEntity = CreateCategoryEntity();
                await _repository.CreateAsync(categoryEntity);

                // Act
                var result = await _repository.FindAllAsync();

                // Assert
                Assert.NotNull(result);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }

        [Fact(DisplayName = "List Category by Id")]
        [Trait("Data", "Category")]
        public async void ShouldListCategoryById()
        {
            try
            {
                // Arrange
                var categoryEntity = CreateCategoryEntity();
                await _repository.CreateAsync(categoryEntity);

                // Act
                var result = _repository.FindByIdAsync(categoryEntity.Id).Result;

                // Assert
                Assert.NotNull(result);
                Assert.IsType<Category>(result);
                Assert.Equal(categoryEntity.Id, result.Id);
                Assert.Equal(categoryEntity.Name, result.Name);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }

        [Fact(DisplayName = "List Common User Categories for Datatables")]
        [Trait("Data", "Category")]
        public void ShouledFindAsyncAllCommonAndUserCategoriesDatatables()
        {
            try
            {
                // Arrange

                // Act
                var result = _repository.FindAsyncAllCommonAndUserCategories(_testUser01Id);

                // Assert
            }
            catch (Exception exception)
            {
                Assert.True(false);
                Debug.WriteLine(exception);
            }
        }

        [Fact(DisplayName = "List name and id User Categories")]
        [Trait("Data", "Category")]
        public async void ShouledFindAsyncNameAndIdUserCategories()
        {
            try
            {
                // Arrange

                // Act
                var result = await _repository.FindAsyncNameAndIdUserCategories(_testUser01Id);

                // Assert
                Assert.NotNull(result);
                Assert.True(result.Count() > 0);
            }
            catch (Exception exception)
            {
                Assert.True(false);
                Debug.WriteLine(exception);
            }
        }

        [Fact(DisplayName = "Update Category")]
        [Trait("Data", "Category")]
        public async void ShouldUpdateCategory()
        {
            try
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
            catch (Exception e)
            {
                Assert.True(false);
                Debug.WriteLine(e);
            }

        }

        [Fact(DisplayName = "Delete Category")]
        [Trait("Data", "Category")]
        public async void ShouldDeleteCategory()
        {
            try
            {
                // Arrange
                var categoryEntity = CreateCategoryEntity();
                await _repository.CreateAsync(categoryEntity);

                // Act
                var result = await _repository.DeleteAsync(categoryEntity.Id);

                // Assert
                Assert.True(result);
            }
            catch (Exception e)
            {
                Assert.True(false);
                Debug.WriteLine(e);
            }
        }
    }
}
