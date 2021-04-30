using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IEntraceRepository : IBaseRepository<Entrace>
    {
        Task<IEnumerable<Entrace>> FindAllAsyncWithWallet();
        Task<IEnumerable<Entrace>> FindAllAsyncWithCategory();
        Task<IEnumerable<Entrace>> FindAsyncLastTenEntracesWithCategories();
        Task<IEnumerable<Entrace>> FindAllAsyncWithWalletAndCategory();
    }
}
