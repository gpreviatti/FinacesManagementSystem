using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IWalletRepository : IBaseRepository<Wallet>
    {
        Task<IEnumerable<Wallet>> FindAsyncWalletsUser(Guid UserId);
    }
}
