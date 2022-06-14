using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Context;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
{
    protected readonly MyContext _context;
    protected readonly DbSet<T> _dataset;
    public BaseRepository(MyContext context)
    {
        _context = context;

        _dataset = _context.Set<T>();
    }

    public async Task<T> CreateAsync(T entity)
    {
        if (entity.Id == Guid.Empty)
            entity.Id = Guid.NewGuid();

        _ = _dataset.Add(entity);

        _ = await SaveChangesAsync();

        return entity;
    }

    public async Task<T> FindByIdAsync(Guid id) => await _dataset
        .AsNoTracking()
        .SingleOrDefaultAsync(p => p.Id.Equals(id));

    public async Task<IEnumerable<T>> FindAllAsync() => await _dataset
        .AsNoTracking()
        .ToListAsync();

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _dataset
            .SingleOrDefaultAsync(p => p.Id.Equals(id));

        if (result == null)
            return false;

        _ = _dataset.Remove(result);

        _ = await SaveChangesAsync();
        
        return true;
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}
