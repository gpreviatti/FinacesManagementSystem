using Domain.Dtos.Entrance;
using Domain.Entities;

namespace Domain.Mappers;

public static class EntranceMapper
{
    public static EntranceResultDto MapperToResultDto(this Entrance entity)
    {
        return new()
        {
            Id = entity.Id,
            Description = entity.Description,
            Ticker = entity.Ticker,
            Type = entity.Type,
            Observation = entity.Observation,
            Value = entity.Value,
            WalletId = entity.WalletId,
            CategoryId = entity.CategoryId,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static EntranceUpdateDto MapperToUpateDto(this Entrance entity)
    {
        return new()
        {
            Id = entity.Id,
            Description = entity.Description,
            Ticker = entity.Ticker,
            Type = entity.Type,
            Observation = entity.Observation,
            Value = entity.Value,
            WalletId = entity.WalletId,
            CategoryId = entity.CategoryId
        };
    }

    public static Entrance Mapper(this EntranceCreateDto dto)
    {
        return new()
        {
            Description = dto.Description,
            Ticker = dto.Ticker,
            Type = dto.Type,
            Observation = dto.Observation,
            Value = dto.Value,
            WalletId = dto.WalletId,
            CategoryId = dto.CategoryId
        };
    }

    public static Entrance Mapper(this EntranceUpdateDto dto)
    {
        return new()
        {
            Id = dto.Id,
            Description = dto.Description,
            Ticker = dto.Ticker,
            Type = dto.Type,
            Observation = dto.Observation,
            Value = dto.Value,
            WalletId = dto.WalletId,
            CategoryId = dto.CategoryId
        };
    }
}
