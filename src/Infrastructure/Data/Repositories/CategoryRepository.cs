using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Context;
using Domain.Entities;
using System.Linq;
using Domain.Interfaces.Repositories;
using System;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MyContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> FindAsyncAllCommonAndUserCategories(Guid userId)
        {
            return await _dataset
                .Select(c => new Category() { 
                    Name = c.Name,
                    UserId = c.UserId,
                    CreatedAt = c.CreatedAt,
                    Entrances = c.Entrances.ToList()
                })
                .OrderBy(c => c.CreatedAt)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
    }
}
