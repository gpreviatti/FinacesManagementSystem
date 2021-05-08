using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IEntranceRepository : IBaseRepository<Entrance>
    {
        Task<IEnumerable<Entrance>> FindAllAsyncWithWallet();
        Task<IEnumerable<Entrance>> FindAllAsyncWithCategory();
        Task<IEnumerable<Entrance>> FindAsyncLastFiveEntrancesWithCategories();
        Task<double> TotalEntrancesByCategory(Guid categoryId);
        Task<double> TotalEntrancesByWallet(Guid walletId);
    }
}
