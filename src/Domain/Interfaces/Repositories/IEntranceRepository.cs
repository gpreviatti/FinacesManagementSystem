using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IEntranceRepository : IBaseRepository<Entrance>
    {
        Task<IEnumerable<Entrance>> FindAllAsyncWithWallet();
        Task<IEnumerable<Entrance>> FindAllAsyncWithCategory(List<Guid> userWallets);
        Task<IEnumerable<Entrance>> FindAsyncLastFiveEntrancesWithCategories(Guid userId);
        Task<double> TotalEntrancesByCategory(Guid categoryId);
        Task<double> TotalEntrancesByWallet(Guid walletId);
    }
}
