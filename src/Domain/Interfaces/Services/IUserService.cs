using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.User;

namespace Domain.Interfaces.Services
{
    public interface IUserService : IBaseService
    {
        Task<WalletResultDto> FindByIdAsync(Guid id);
        Task<IEnumerable<WalletResultDto>> FindAllAsync();
        Task<WalletResultDto> CreateAsync(WalletCreateDto entity);
        Task<WalletResultDto> UpdateAsync(WalletUpdateDto user);
    }
}
