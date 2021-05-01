using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class EntranceRepository : BaseRepository<Entrance>, IEntranceRepository
    {
        public EntranceRepository(MyContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Entrance>> FindAllAsyncWithWallet()
        {
            return await _dataset
                .Include(e => e.Wallet)
                .ToListAsync();
        }

        public async Task<IEnumerable<Entrance>> FindAllAsyncWithCategory()
        {
            return await _dataset
                .Include(e => e.Category)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Entrance>> FindAsyncLastTenEntrancesWithCategories()
        {
            return await _dataset
                .Include(e => e.Category)
                .OrderByDescending(e => e.CreatedAt)
                .Take(10)
                .ToListAsync();
        }

        public async Task<IEnumerable<Entrance>> FindAllAsyncWithWalletAndCategory()
        {
             return await _dataset
                .Include(e => e.Category)
                .Include(e => e.Wallet)
                .ToListAsync();
        }
    }
}
