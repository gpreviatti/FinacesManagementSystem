using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<T> CreateAsync(T entity);
        Task<bool> DeleteAsync(Guid Id);
        Task<T> FindByIdAsync(Guid Id);
        Task<IEnumerable<T>> FindAllAsync();
        Task<int> SaveChangesAsync();
    }
}
