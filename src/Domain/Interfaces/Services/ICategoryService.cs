using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ICategoryService : IBaseService
    {
        Task<IEnumerable<Category>> FindAllAsync();
        Task<Category> CreateAsync(Category category);
        Task<Wallet> UpdateAsync(Category category);
    }
}
