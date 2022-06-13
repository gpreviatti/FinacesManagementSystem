using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.WalletType;

namespace Domain.Interfaces.Services;

public interface IWalletTypeService : IBaseService
{
    Task<WalletTypeResultDto> FindByIdAsync(Guid id);
    Task<IEnumerable<WalletTypeResultDto>> FindAllAsync();
    WalletTypeResultDto CreateAsync(WalletTypeCreateDto walletType);
    Task<WalletTypeResultDto> UpdateAsync(WalletTypeUpdateDto walletType);
    Task<bool> DeleteAsync(Guid id);
}
