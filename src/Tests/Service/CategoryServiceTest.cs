using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Dtos.Category;
using Domain.Dtos.Entrace;
using Domain.Dtos.Wallet;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Tests.Service
{
    public class CategoryServiceTest : BaseServiceTest
    {
        private ICategoryService _service;
        private Mock<ICategoryService> _serviceMock;

        public CategoryServiceTest()
        {

        }

        [Fact(DisplayName = "Create category")]
        [Trait("Crud", "ShouldCreateCategory")]
        public async void ShouldCreateCategory()
        {
            CategoryCreateDto categoryCreateDto = new CategoryCreateDto() {
                Name = FakerName,
            };

            CategoryResultDto categoryResultDto = new CategoryResultDto()
            {
                Id = Guid.NewGuid(),
                Name = FakerName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            _serviceMock = new Mock<ICategoryService>();
            _serviceMock.Setup(m => m.CreateAsync(categoryCreateDto)).ReturnsAsync(categoryResultDto);
            _service = _serviceMock.Object;
            var result = await _service.CreateAsync(categoryCreateDto);

            Assert.NotNull(result);
            Assert.False(result.Id.Equals(Guid.Empty));
            Assert.Equal(FakerName, result.Name);
        }

        [Fact(DisplayName = "List categories")]
        [Trait("Crud", "ShouldListCategories")]
        public async void ShouldListCategory()
        {
            IEnumerable<CategoryResultDto> listCategoryResultDto = new List<CategoryResultDto>
            {
                new CategoryResultDto(){ Id = new Guid(), Name = Faker.Name.FullName(), Entraces = new List<EntraceResultDto>()},
                new CategoryResultDto(){ Id = new Guid(), Name = Faker.Name.FullName(), Entraces = new List<EntraceResultDto>()},
                new CategoryResultDto(){ Id = new Guid(), Name = Faker.Name.FullName(), Entraces = new List<EntraceResultDto>()},
                new CategoryResultDto(){ Id = new Guid(), Name = Faker.Name.FullName(), Entraces = new List<EntraceResultDto>()},
                new CategoryResultDto(){ Id = new Guid(), Name = Faker.Name.FullName(), Entraces = new List<EntraceResultDto>()},
                new CategoryResultDto(){ Id = new Guid(), Name = Faker.Name.FullName(), Entraces = new List<EntraceResultDto>()},
                new CategoryResultDto(){ Id = new Guid(), Name = Faker.Name.FullName(), Entraces = new List<EntraceResultDto>()},
                new CategoryResultDto(){ Id = new Guid(), Name = Faker.Name.FullName(), Entraces = new List<EntraceResultDto>()},
                new CategoryResultDto(){ Id = new Guid(), Name = Faker.Name.FullName(), Entraces = new List<EntraceResultDto>()},
                new CategoryResultDto(){ Id = new Guid(), Name = Faker.Name.FullName(), Entraces = new List<EntraceResultDto>()}
            };

            _serviceMock = new Mock<ICategoryService>();
            _serviceMock.Setup(m => m.FindAllAsync()).ReturnsAsync(listCategoryResultDto);
            _service = _serviceMock.Object;
            var result = await _service.FindAllAsync();

            Assert.NotNull(result);
            Assert.True(result.Count() == 10);
        }

        [Fact(DisplayName = "List category by id")]
        [Trait("Crud", "ShouldListCategoryById")]
        public async void ShouldListCategoryById()
        {
            var categoryResultDto = new CategoryResultDto() {
                Id = new Guid(),
                Name = Faker.Name.FullName(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Entraces = new List<EntraceResultDto>()
            };
            
            _serviceMock = new Mock<ICategoryService>();
            _serviceMock.Setup(m => m.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(categoryResultDto);
            _service = _serviceMock.Object;
            var result = await _service.FindByIdAsync(It.IsAny<Guid>());

            Assert.NotNull(result);
            Assert.Equal(categoryResultDto.Id, result.Id);
            Assert.Equal(categoryResultDto.CreatedAt, result.CreatedAt);
            Assert.Equal(categoryResultDto.UpdatedAt, result.UpdatedAt);
        }

        [Fact(DisplayName = "Update category")]
        [Trait("Crud", "ShouldUpdateCategory")]
        public async void ShouldUpdateCategory()
        {
            CategoryUpdateDto categoryUpdateDto = new CategoryUpdateDto()
            {
                Name = FakerName,
            };

            CategoryResultDto categoryResultDto = new CategoryResultDto()
            {
                Name = FakerName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Entraces = new List<EntraceResultDto>()
            };

            _serviceMock = new Mock<ICategoryService>();
            _serviceMock.Setup(m => m.UpdateAsync(categoryUpdateDto)).ReturnsAsync(categoryResultDto);
            _service = _serviceMock.Object;
            var result = await _service.UpdateAsync(categoryUpdateDto);

            Assert.NotNull(result);
            Assert.Equal(FakerName, result.Name);
        }

        [Fact(DisplayName = "Delete category")]
        [Trait("Crud", "ShouldDeleteCategory")]
        public async void ShouldDeleteCategory()
        {
            _serviceMock = new Mock<ICategoryService>();
            _serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _service = _serviceMock.Object;
            var result = await _service.DeleteAsync(Guid.NewGuid());

            Assert.True(result);
        }

        [Fact(DisplayName = "Not Delete category")]
        [Trait("Crud", "ShouldNotDeleteCategory")]
        public async void ShouldNotDeleteCategory()
        {
            _serviceMock = new Mock<ICategoryService>();
            _serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);
            _service = _serviceMock.Object;
            var result = await _service.DeleteAsync(Guid.NewGuid());

            Assert.False(result);
        }
    }
}
