using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.Category;
using Domain.Models;
using Domain.ViewModels;

namespace Domain.Interfaces.Services
{
    public interface ICategoryService : IBaseService
    {
        Task<CategoryResultDto> FindByIdAsync(Guid id);
        Task<CategoryUpdateDto> FindByIdUpdateAsync(Guid id);
        Task<IEnumerable<CategoryResultDto>> FindAsyncNameAndIdUserCategories(Guid userId);
        Task<IEnumerable<CategoryResultDto>> FindAsyncAllCommonAndUserCategories(Guid userId);
        Task<DatatablesModel<CategoryResultDto>> FindAsyncAllCommonAndUserCategoriesDatatables(DatatablesModel<CategoryResultDto> datatablesModel, Guid userId);
        Task<CategoryUpdateViewModel> SetupCategoryUpdateViewModel(Guid id, Guid userId);
        Task<CategoryResultDto> CreateAsync(CategoryCreateDto entityCreateDto, Guid userId);
        Task<CategoryResultDto> UpdateAsync(CategoryUpdateDto entityUpdateDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
