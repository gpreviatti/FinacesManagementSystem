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
            Total = entity.GetTotalValues()
        };
    }


    public static IEnumerable<CategoryResultDto> MapperToResultDto(this IEnumerable<Category> entities)
    {
        var resultDto = new List<CategoryResultDto>();
        if (entities == null | !entities.Any())
            return resultDto;

        foreach (var entity in entities)
            resultDto.Add(entity.MapperToResultDto());

        return resultDto;
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
