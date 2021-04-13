using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IWalletTypeService : IBaseService
    {
        Task<IEnumerable<WalletType>> FindAllAsync();
        Task<WalletType> CreateAsync(WalletType walletType);
        Task<WalletType> UpdateAsync(WalletType walletType);
    }
}
