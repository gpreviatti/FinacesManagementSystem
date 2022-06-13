using Domain.Dtos.Category;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Mappers;

public static class CategoryMapper
{
    public static Category MapperToCreateDto(this CategoryCreateDto dto)
    {
        return new()
        {
            Name = dto.Name,
            UserId = dto.UserId,
            CategoryId = dto.CategoryId
        };
    }

    public static CategoryResultDto MapperToResultDto(this Category entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
            UserId = entity.UserId,
            CategoryId = entity.CategoryId,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Entrances = entity.Entrances.MapperToResultDto()
        };
    }


    public static IEnumerable<CategoryResultDto> MapperToResultDto(this IEnumerable<Category> entity)
    {
        if (entity == null | !entity.Any())
            return new List<CategoryResultDto>();

        return entity.Select(e => e.MapperToResultDto());
    }

    public static CategoryUpdateDto MapperToUpdateDto(this Category entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
            CategoryId = entity.CategoryId
        };
    }

    public static Category Mapper(this CategoryUpdateDto dto)
    {
        return new()
        {
            Id = dto.Id,
            Name = dto.Name,
            CategoryId = dto.CategoryId
        };
    }
}
