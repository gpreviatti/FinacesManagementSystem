using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.User;

namespace Domain.Interfaces.Services
{
    public interface IUserService : IBaseService
    {
        Task<UserResultDto> FindByIdAsync(Guid id);
        Task<IEnumerable<UserResultDto>> FindAllAsync();
        Task<UserResultDto> CreateAsync(UserCreateDto entity);
        Task<UserResultDto> UpdateAsync(UserUpdateDto user);
        Task<bool> DeleteAsync(Guid id);
    }
}
