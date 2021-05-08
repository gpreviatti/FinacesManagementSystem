using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.Wallet;

namespace Domain.Interfaces.Services
{
    public interface IWalletService : IBaseService
    {
        Task<WalletResultDto> FindByIdAsync(Guid id);
        Task<WalletUpdateDto> FindByIdUpdateAsync(Guid id);
        Task<IEnumerable<WalletResultDto>> FindAsyncWalletsUser();
        Task<WalletResultDto> CreateAsync(WalletCreateDto wallet);
        Task<WalletResultDto> UpdateAsync(WalletUpdateDto wallet);
        Task<bool> DeleteAsync(Guid id);
    }
}
