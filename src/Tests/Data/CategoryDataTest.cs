using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Xunit;

namespace Tests.Data
{
    public class CategoryDataTest : BaseDataTest
    {
        private readonly ICategoryRepository _repository;

        public CategoryDataTest()
        {
            _repository = new CategoryRepository(_context);
        }

        public Category CreateCategoryEntity()
        {
            return new Category()
            {
                Name = Faker.Name.FullName()
            };
        }

        [Fact(DisplayName = "Create Category")]
        [Trait("Crud", "ShouldCreateCategory")]
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
                Console.WriteLine(e);
            }
        }

        [Fact(DisplayName = "List Categorys")]
        [Trait("Crud", "ShouldListCategory")]
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
                Console.WriteLine(e);
            }
        }

        [Fact(DisplayName = "List Category by Id")]
        [Trait("Crud", "ShouldListCategoryById")]
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
                Console.WriteLine(e);
            }
        }

        [Fact(DisplayName = "Update Category")]
        [Trait("Crud", "ShouldUpdateCategory")]
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
                Console.WriteLine(e);
            }
            
        }

        [Fact(DisplayName = "Delete Category")]
        [Trait("Crud", "ShouldDeleteCategory")]
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
                Console.WriteLine(e);
            }
        }
    }
}
