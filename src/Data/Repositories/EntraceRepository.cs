using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Domain.Dtos.Category;
using Domain.Dtos.Entrance;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class EntranceRepository : BaseRepository<Entrance>, IEntranceRepository
    {
        public EntranceRepository(MyContext context) : base(context) { }

        public async Task<IQueryable<EntranceResultDto>> FindAllAsyncWithCategory(List<Guid> userWalletsId)
        {
            return await Task.Run(() =>
            {
                return _dataset
                    .AsNoTracking()
                    .Select(e => new EntranceResultDto
                    {
                        Id = e.Id,
                        CategoryId = e.CategoryId,
                        WalletId = e.WalletId,
                        Description = e.Description,
                        Observation = e.Observation,
                        Type = e.Type,
                        Value = e.Value,
                        CreatedAt = e.CreatedAt,
                        UpdatedAt = e.UpdatedAt,
                        Category = new CategoryResultDto() { Id = e.Category.Id, Name = e.Category.Name }
                    })
                    .Where(e => userWalletsId.Contains(e.WalletId))
                    .OrderBy(e => e.CreatedAt);
            });
        }
    }
}
