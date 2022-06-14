using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MyContext context) : base(context) { }

        public IQueryable<Category> FindAsyncAllCommonAndUserCategories(Guid userId)
        {
            return _dataset
                .AsNoTracking()
                .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    UserId = c.UserId,
                    CreatedAt = c.CreatedAt,
                    Entrances = c.Entrances
                        .Where(e => e.Wallet.UserId == userId)
                        .Select(e => new Entrance { Value = e.Value })
                })
                .OrderBy(c => c.CreatedAt)
                .Where(c => c.UserId == userId || c.UserId == null);
        }

        public async Task<IQueryable<Category>> FindAsyncNameAndIdUserCategories(Guid userId)
        {
            return await Task.Run(() =>
            {
                return _dataset
                    .AsNoTracking()
                    .Select(c => new Category
                    {
                        Id = c.Id,
                        Name = c.Name,
                        UserId = c.UserId,
                        CreatedAt = c.CreatedAt,
                    })
                    .OrderBy(c => c.CreatedAt)
                    .Where(c => c.UserId == userId || c.UserId == null);
            });
        }
    }
}
