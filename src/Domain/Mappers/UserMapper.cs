using Domain.Dtos.User;
using Domain.Entities;

namespace Domain.Mappers;
public static class UserMapper
{
    public static User Mapper(this UserCreateDto dto)
    {
        return new ()
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = dto.Password,            
        };
    }

    public static User Mapper(this UserUpdateDto dto)
    {
        return new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
            Password = dto.Password
        };
    }

    public static UserResultDto MapperResultDto(this User entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
