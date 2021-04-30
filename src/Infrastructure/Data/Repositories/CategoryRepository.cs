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
                .Where(c => c.UserId == userId || c.UserId == null || c.UserId == Guid.Empty)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();
        }
    }
}
