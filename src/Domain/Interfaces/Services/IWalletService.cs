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
        Task<IEnumerable<WalletResultDto>> FindAsyncWalletsUser(Guid userId);
        Task<IEnumerable<Guid>> FindAsyncWalletsUserIds(Guid userId);
        Task<WalletTotalValuesDto> WalletsTotalValues(Guid userId);
        Task<WalletResultDto> CreateAsync(WalletCreateDto walletCreateDto, Guid userId);
        Task<WalletResultDto> UpdateAsync(WalletUpdateDto walletUpdateDto);
        Task<WalletResultDto> UpdateWalletValue(Guid id, int type, double value);
        Task<bool> DeleteAsync(Guid id);
    }
}
