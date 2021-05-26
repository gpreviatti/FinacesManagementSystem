using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.Category;
using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface ICategoryService : IBaseService
    {
        Task<CategoryResultDto> FindByIdAsync(Guid id);
        Task<CategoryUpdateDto> FindByIdUpdateAsync(Guid id);
        Task<IEnumerable<CategoryResultDto>> FindAsyncAllCommonAndUserCategories(Guid userId);
        Task<DatatablesModel<CategoryResultDto>> FindAsyncAllCommonAndUserCategoriesDatatables(DatatablesModel<CategoryResultDto> datatablesModel, Guid userId);
        Task<CategoryResultDto> CreateAsync(CategoryCreateDto category, Guid userId);
        Task<CategoryResultDto> UpdateAsync(CategoryUpdateDto category);
        Task<bool> DeleteAsync(Guid id);
    }
}
