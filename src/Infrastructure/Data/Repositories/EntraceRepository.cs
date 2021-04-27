using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class EntraceRepository : BaseRepository<Entrace>, IEntraceRepository
    {
        public EntraceRepository(MyContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Entrace>> FindAllAsyncWithWallet()
        {
            return await _dataset
                .Include(e => e.Wallet)
                .ToListAsync();
        }

        public async Task<IEnumerable<Entrace>> FindAllAsyncWithCategory()
        {
            return await _dataset
                .Include(e => e.Category)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Entrace>> FindAsyncLastTenEntracesWithCategories()
        {
            return await _dataset
                .Include(e => e.Category)
                .OrderByDescending(e => e.CreatedAt)
                .Take(10)
                .ToListAsync();
        }

        public async Task<IEnumerable<Entrace>> FindAllAsyncWithWalletAndCategory()
        {
             return await _dataset
                .Include(e => e.Category)
                .Include(e => e.Wallet)
                .ToListAsync();
        }
    }
}
