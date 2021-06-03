using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Context;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected readonly MyContext _context;
        protected readonly DbSet<T> _dataset;
        public BaseRepository(MyContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public async Task<T> CreateAsync(T item)
        {
            if (item.Id == Guid.Empty)
            {
                item.Id = Guid.NewGuid();
            }

            _dataset.Add(item);

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<T> FindByIdAsync(Guid id) => await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));

        public async Task<IEnumerable<T>> FindAllAsync() => await _dataset.ToListAsync();

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
            if (result == null)
            {
                return false;
            }

            _dataset.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
