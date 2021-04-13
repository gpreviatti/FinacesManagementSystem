using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IWalletService : IBaseService
    {
        Task<IEnumerable<Wallet>> FindAllAsync();
        Task<Wallet> CreateAsync(Wallet wallet);
        Task<Wallet> UpdateAsync(Wallet wallet);
    }
}
