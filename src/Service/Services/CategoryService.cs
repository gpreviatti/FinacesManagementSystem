using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos.Category;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.ViewModels;

namespace Service.Services;
public class CategoryService : BaseService, ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CategoryResultDto> CreateAsync(CategoryCreateDto entityCreateDto, Guid userId)
    {
        if (entityCreateDto.CategoryId == Guid.Empty || userId == Guid.Empty)
            throw new ArgumentException("Main Category or User not found");

        entityCreateDto.UserId = userId;
        var entity = _mapper.Map<Category>(entityCreateDto);

        await _repository.CreateAsync(entity);
        return _mapper.Map<CategoryResultDto>(entity);
    }

    public async Task<CategoryResultDto> UpdateAsync(CategoryUpdateDto entityUpdateDto)
    {
        var result = await _repository.FindByIdAsync(entityUpdateDto.Id);

        if (result == null)
            return null;

        var entity = _mapper.Map(entityUpdateDto, result);

        var savedChanges = await _repository.SaveChangesAsync();

        if (savedChanges > 0)
            return _mapper.Map<CategoryResultDto>(entity);

        return null;
    }

    public async Task<bool> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);

    #region Find
    public async Task<CategoryResultDto> FindByIdAsync(Guid id)
    {
        var result = await _repository.FindByIdAsync(id);
        return _mapper.Map<CategoryResultDto>(result);
    }

    public async Task<CategoryUpdateDto> FindByIdUpdateAsync(Guid id)
    {
        var result = await _repository.FindByIdAsync(id);
        return _mapper.Map<CategoryUpdateDto>(result);
    }

    public async Task<IEnumerable<CategoryResultDto>> FindAsyncNameAndIdUserCategories(Guid userId) {
            var result = await _repository.FindAsyncNameAndIdUserCategories(userId);
            return _mapper.Map<IEnumerable<CategoryResultDto>>(result.ToList());
    }

    /// <summary>
    /// Return common categories and users categories
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<CategoryResultDto>> FindAsyncAllCommonAndUserCategories(Guid userId)
    {
        var result = await Task.Run(() => _repository.FindAsyncAllCommonAndUserCategories(userId));
        return _mapper.Map<IEnumerable<CategoryResultDto>>(result);
    }

    public async Task<CategoryUpdateViewModel> SetupCategoryUpdateViewModel(Guid id, Guid userId)
    {
        return new ()
        {
            Category = await FindByIdUpdateAsync(id),
            Categories = await FindAsyncAllCommonAndUserCategories(userId)
        };
    }

    /// <summary>
    /// Return common categories and users categories
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<CategoryResultDto>> FindAllAndUserCategories(
        string currentSort,
        string searchString,
        Guid userId
    )
    {
        var categories = await Task.Run(() => _repository
            .FindAsyncAllCommonAndUserCategories(userId));

        if (!categories.Any())
            return null;

        if (!string.IsNullOrEmpty(searchString))
            categories = categories.Where(m => m.Name.Contains(searchString));

        var categoriesData = _mapper.Map<IEnumerable<CategoryResultDto>>(categories);

        foreach (var category in categoriesData)
            category.Total = category.Entrances.Select(c => c.Value).Sum();

        if (!string.IsNullOrEmpty(currentSort))
            categoriesData = SortCategories(currentSort, categoriesData);

        return categoriesData;
    }

    private static IEnumerable<CategoryResultDto> SortCategories(
        string currentSort,
        IEnumerable<CategoryResultDto> categories
    )
    {
        return currentSort switch
        {
            "name" => categories.OrderBy(e => e.Name),
            "name_desc" => categories.OrderByDescending(e => e.Name),
            "total" => categories.OrderBy(e => e.Total),
            "total_desc" => categories.OrderByDescending(e => e.Total),
            "createdAt" => categories.OrderBy(e => e.CreatedAt),
            _ => categories.OrderByDescending(e => e.CreatedAt),
        };
    }
    #endregion
}
