using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos.Category;
using Domain.Dtos.Entrance;
using Domain.Dtos.Wallet;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
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

                _repositoryMock.Setup(m => m.CreateAsync(category).Result).Returns(categoryResult);
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

        [Fact(DisplayName = "Find name and user categories")]
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
            var result = await _service.FindAsyncAllCommonAndUserCategories(_userAdminId);

            Assert.NotNull(result);
            Assert.True(result.Count() == 10);
            Assert.Equal(listCategoryResultDto.FirstOrDefault().Name, result.FirstOrDefault().Name);
            _repositoryMock.Verify(m => m.FindAsyncAllCommonAndUserCategories(It.IsAny<Guid>()), Times.Once);
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
