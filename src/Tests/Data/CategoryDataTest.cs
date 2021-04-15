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

        private async Task<Category> createCategory()
        {
            var entityTest = new Category() {Name = Faker.Name.FullName()};
            return await _repository.CreateAsync(entityTest);
        }

        [Fact(DisplayName = "Create Category")]
        [Trait("Crud", "ShouldCreateCategory")]
        public async void ShouldCreateCategory()
        {
            var entityTest = new Category()
            {
                Name = Faker.Name.FullName()
            };

            var result = await _repository.CreateAsync(entityTest);

            Assert.NotNull(result);
            Assert.Equal(entityTest.Name, result.Name);
            Assert.False(result.Id == Guid.Empty);
        }

        [Fact(DisplayName = "List Categorys")]
        [Trait("Crud", "ShouldListCategory")]
        public async void ShouldListCategory()
        {
            await createCategory();
            var result = _repository.FindAllAsync().Result;

            Assert.NotNull(result);
        }

        [Fact(DisplayName = "List Category by Id")]
        [Trait("Crud", "ShouldListCategoryById")]
        public async void ShouldListCategoryById()
        {
            var entityTest = await createCategory();
            var result = _repository.FindByIdAsync(entityTest.Id).Result;

            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(entityTest.Id, result.Id);
            Assert.Equal(entityTest.Name, result.Name);
        }

        [Fact(DisplayName = "Update Category")]
        [Trait("Crud", "ShouldUpdateCategory")]
        public async void ShouldUpdateCategory()
        {
            var entityTest = await createCategory();
            entityTest.Name = Faker.Name.FullName();
            var result = await _repository.SaveChangesAsync();

            Assert.Equal(1, result);
        }

        [Fact(DisplayName = "Delete Category")]
        [Trait("Crud", "ShouldDeleteCategory")]
        public async void ShouldDeleteCategory()
        {
            var entityTest = await createCategory();
            var result = await _repository.DeleteAsync(entityTest.Id);

            Assert.True(result);
        }
    }
}
