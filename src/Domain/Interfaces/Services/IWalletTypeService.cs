using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.WalletType;

namespace Domain.Interfaces.Services
{
    public interface IWalletTypeService : IBaseService
    {
        Task<IEnumerable<WalletTypeResultDto>> FindAllAsync();
        Task<WalletTypeResultDto> CreateAsync(WalletTypeCreateDto walletType);
        Task<WalletTypeResultDto> UpdateAsync(WalletTypeUpdateDto walletType);
        Task<bool> DeleteAsync(Guid id);
    }
}
