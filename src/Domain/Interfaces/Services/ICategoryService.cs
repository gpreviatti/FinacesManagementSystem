using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.Category;

namespace Domain.Interfaces.Services
{
    public interface ICategoryService : IBaseService
    {
        Task<IEnumerable<CategoryResultDto>> FindAllAsync();
        Task<CategoryResultDto> CreateAsync(CategoryCreateDto category);
        Task<CategoryResultDto> UpdateAsync(CategoryUpdateDto category);
        Task<bool> DeleteAsync(Guid id);
    }
}
