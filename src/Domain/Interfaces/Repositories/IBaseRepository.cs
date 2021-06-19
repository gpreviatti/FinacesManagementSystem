using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<T> CreateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<T> FindByIdAsync(Guid id);
        Task<IEnumerable<T>> FindAllAsync();
        Task<int> SaveChangesAsync();
    }
}
