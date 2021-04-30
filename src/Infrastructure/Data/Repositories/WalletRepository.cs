using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(MyContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Wallet>> FindAsyncWalletsUser(Guid UserId)
        {
            return await _dataset
                .Where(w => w.UserId == UserId)
                .OrderBy(w => w.CreatedAt)
                .ToListAsync();
        }

    }
}
