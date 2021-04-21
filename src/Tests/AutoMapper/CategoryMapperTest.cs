using System;
using System.Collections.Generic;
using Domain.Dtos.Category;
using Domain.Dtos.Entrace;
using Domain.Entities;
using Xunit;

namespace Tests.AutoMapper
{
    public class CategoryMapperTest : BaseMapperTest
    {
        [Fact(DisplayName = "Should transform CategoryCreateDto to Category")]
        [Trait("AutoMapper", "CategoryCreateDtoToCategory")]
        public void CategoryCreateDtoToCategory()
        {
            var entityCreateDto = new CategoryCreateDto()
            {
                Name = Faker.Name.First()
            };

            var entity = _mapper.Map<Category>(entityCreateDto);
            Assert.NotNull(entity);
            Assert.Equal(entity.Name, entityCreateDto.Name);
        }

        [Fact(DisplayName = "Should transform CategoryUpdateDto to Category")]
        [Trait("AutoMapper", "CategoryCreateDtoToCategory")]
        public void CategoryUpdateDtoToCategory()
        {
            var entityUpdateDto = new CategoryUpdateDto()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.First()
                
            };

            var entity = _mapper.Map<Category>(entityUpdateDto);
            Assert.NotNull(entity);
            Assert.Equal(entity.Id, entityUpdateDto.Id);
            Assert.Equal(entity.Name, entityUpdateDto.Name);
        }

        [Fact(DisplayName = "Should transform Category to CategoryResultDto")]
        [Trait("AutoMapper", "CategoryCreateDtoToCategory")]
        public void CategoryToCategoryResultDto()
        {
            var entity = new Category()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.First(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Entraces = new List<Entrace>()
            };

            var entraceResultDto = _mapper.Map<IEnumerable<EntraceResultDto>>(entity.Entraces);
            var entityResultDto = _mapper.Map<CategoryResultDto>(entity);
            Assert.NotNull(entityResultDto);
            Assert.Equal(entity.Id, entityResultDto.Id);
            Assert.Equal(entity.Name, entityResultDto.Name);
            Assert.Equal(entity.Name, entityResultDto.Name);
            Assert.Equal(entity.CreatedAt, entityResultDto.CreatedAt);
            Assert.Equal(entity.UpdatedAt, entityResultDto.UpdatedAt);
            Assert.Equal(entraceResultDto, entityResultDto.Entraces);
        }
    }
}
