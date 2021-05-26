using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.Wallet;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IWalletRepository : IBaseRepository<Wallet>
    {
        Task<IEnumerable<Wallet>> FindAsyncWalletsUser(Guid UserId);
        Task<IEnumerable<WalletValuesDto>> FindAsyncWalletsValues(Guid UserId);
        Task<IEnumerable<Wallet>> FindWalletEntrances(Guid userId);
    }
}
