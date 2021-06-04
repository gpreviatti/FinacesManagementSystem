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
    public class EntranceRepository : BaseRepository<Entrance>, IEntranceRepository
    {
        public EntranceRepository(MyContext context) : base(context) {}

        public async Task<IEnumerable<Entrance>> FindAllAsyncWithCategory(List<Guid> userWalletsId)
        {
            return await _dataset
                .Include(e => e.Category)
                .Select(e => new Entrance 
                {
                    Id = e.Id,
                    CategoryId = e.CategoryId,
                    WalletId = e.WalletId,
                    Description = e.Description,
                    Type = e.Type,
                    Value = e.Value,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt,
                    Category = new Category() { Id = e.Category.Id, Name = e.Category.Name}
                })
                .Where(e => userWalletsId.Contains(e.WalletId))
                .OrderBy(e => e.CreatedAt)
                .ToListAsync();
        }
    }
}
