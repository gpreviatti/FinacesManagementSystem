using Domain.Dtos.Wallet;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Mappers;
public static class WalletMapper
{
    public static Wallet Mapper(this WalletCreateDto dto)
    {
        return new()
        {
            WalletTypeId = dto.WalletTypeId,
            Name = dto.Name,
            Description = dto.Description,
            CloseDate = dto.CloseDate,
            DueDate = dto.DueDate
        };
    }

    public static WalletResultDto MapperResultDto(this Wallet entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            CloseDate = entity.CloseDate,
            DueDate = entity.DueDate,
            CurrentValue = entity.CurrentValue,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static IEnumerable<WalletResultDto> MapperResultDto(this IEnumerable<Wallet> entity)
    {
        if (entity == null || !entity.Any())
            return new List<WalletResultDto>();

        return entity.Select(e => e.MapperResultDto());
    }

    public static WalletUpdateDto MapperUpdateDto(this Wallet entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            CloseDate = entity.CloseDate,
            DueDate = entity.DueDate,
            CurrentValue = entity.CurrentValue
        };
    }

    public static Wallet Mapper(this WalletUpdateDto dto)
    {
        return new()
        {
            Id = dto.Id,
            WalletTypeId = dto.WalletTypeId,
            Name = dto.Name,
            Description = dto.Description,
            CloseDate = dto.CloseDate,
            DueDate = dto.DueDate
        };
    }
}
