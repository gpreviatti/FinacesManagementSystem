using System;
using System.Diagnostics;
using Data.Repositories;
using Domain.Dtos.Category;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Xunit;

namespace Tests.Data
{
    public class CategoryDataTest : BaseDataTest
    {
        private readonly ICategoryRepository _repository;

        public CategoryDataTest() => _repository = new CategoryRepository(_context);

        public Category CreateCategoryEntity()
        {
            return new Category
            {
                Name = Faker.Name.FullName()
            };
        }

        [Fact(DisplayName = "Create Category")]
        [Trait("Data", "Category")]
        public async void ShouldCreateCategory()
        {
            try
            {
                var categoryEntity = CreateCategoryEntity();
                var result = await _repository.CreateAsync(categoryEntity);

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
                var categoryEntity = CreateCategoryEntity();
                await _repository.CreateAsync(categoryEntity);

                var result = await _repository.FindAllAsync();

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
                var categoryEntity = CreateCategoryEntity();
                await _repository.CreateAsync(categoryEntity);

                var result = _repository.FindByIdAsync(categoryEntity.Id).Result;

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
        public async void ShouledFindAsyncAllCommonAndUserCategoriesDatatables()
        {
            try
            {
                // Arrange
                var datatatablesModel = new DatatablesModel<CategoryResultDto>
                {

                };
                var testUserGuid = Guid.Parse("CB43D078-87F1-4864-853A-E626922B8109");

                // Act
                var result = await _repository.FindAsyncAllCommonAndUserCategories(testUserGuid);

                // Assert
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
                var categoryEntity = CreateCategoryEntity();
                await _repository.CreateAsync(categoryEntity);

                categoryEntity.Name = Faker.Name.FullName();
                var result = await _repository.SaveChangesAsync();

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
                var categoryEntity = CreateCategoryEntity();
                await _repository.CreateAsync(categoryEntity);

                var result = await _repository.DeleteAsync(categoryEntity.Id);

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
