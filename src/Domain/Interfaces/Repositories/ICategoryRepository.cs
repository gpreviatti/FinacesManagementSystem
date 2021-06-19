using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        IQueryable<Category> FindAsyncAllCommonAndUserCategories(Guid userId);
        Task<IQueryable<Category>> FindAsyncNameAndIdUserCategories(Guid userId);
    }
}
