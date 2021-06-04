using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Domain.Dtos.Wallet;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(MyContext context) : base(context) { }

        public async Task<IEnumerable<Wallet>> FindAsyncWalletsUser(Guid UserId)
        {
            return await _dataset
                .Where(w => w.UserId == UserId)
                .OrderBy(w => w.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<WalletValuesDto>> FindAsyncWalletsValues(Guid UserId) 
        {
            return await _dataset
                .Where(w => w.UserId.Equals(UserId))
                .Select(w => new WalletValuesDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    CurrentValue = w.CurrentValue,
                    TotalIncomes = w.Entrances.Where(e => e.Type.Equals(1)).Sum(e => e.Value),
                    TotalExpanses = w.Entrances.Where(e => e.Type.Equals(2)).Sum(e => e.Value),
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
