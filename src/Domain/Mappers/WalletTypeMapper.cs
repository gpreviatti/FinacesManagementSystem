using Domain.Dtos.WalletType;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Mappers;

public static class WalletTypeMapper
{
    public static WalletType Mapper(this WalletTypeCreateDto dto)
    {
        return new()
        {
            Name = dto.Name,
        };
    }

    public static WalletType Mapper(this WalletTypeUpdateDto dto)
    {
        return new()
        {
            Id = dto.Id,
            Name = dto.Name,
        };
    }

    public static WalletTypeResultDto MapperResultDto(this WalletType entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static IEnumerable<WalletTypeResultDto> MapperResultDto(this IEnumerable<WalletType> entity)
    {
        if (entity == null || !entity.Any())
            return new List<WalletTypeResultDto>();

        return entity.Select(e => e.MapperResultDto());
    }
}
