using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos.Category;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.ViewModels;

namespace Service.Services
{
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
            return new CategoryUpdateViewModel
            {
                Category = await FindByIdUpdateAsync(id),
                Categories = await FindAsyncAllCommonAndUserCategories(userId)
            };
        }

        /// <summary>
        /// Return common categories and users categories on datatables format
        /// </summary>
        /// <param name="datatablesModel"></param>
        /// <returns></returns>
        public async Task<DatatablesModel<CategoryResultDto>> FindAsyncAllCommonAndUserCategoriesDatatables(
            DatatablesModel<CategoryResultDto> datatablesModel, 
            Guid userId
        )
        {
            var categories = await Task.Run(() => _repository.FindAsyncAllCommonAndUserCategories(userId));

            if (categories.Count() == 0)
                return null;

            datatablesModel.RecordsTotal = categories.Count();

            if (!string.IsNullOrEmpty(datatablesModel.SearchValue))
                categories = categories.Where(m => m.Name.Contains(datatablesModel.SearchValue));

            var categoriesData = _mapper.Map<IEnumerable<CategoryResultDto>>(categories);

            foreach (var category in categoriesData)
                category.Total = category.Entrances.Select(c => c.Value).Sum();

            
            if (!string.IsNullOrEmpty(datatablesModel.SortColumnDirection))
                categoriesData = SortDatatables(datatablesModel, categoriesData);

            datatablesModel.RecordsFiltered = categoriesData.Count();
            await Task.Run(() => {
                datatablesModel.Data = categoriesData
                .Skip(datatablesModel.Skip)
                .Take(datatablesModel.PageSize);
            });
            return datatablesModel;
        }

        private static IEnumerable<CategoryResultDto> SortDatatables(
            DatatablesModel<CategoryResultDto> datatablesModel, 
            IEnumerable<CategoryResultDto> categories
        )
        {
            var sortDirection = datatablesModel.SortColumnDirection;
            switch (datatablesModel.SortColumn)
            {
                case 0:
                    if (sortDirection.Equals("asc"))
                        return categories.OrderBy(e => e.Name);

                    return categories.OrderByDescending(e => e.Name);
                case 1:
                    if (sortDirection.Equals("asc"))
                        return categories.OrderBy(e => e.Total);

                    return categories.OrderByDescending(e => e.Total);
                default:
                    if (sortDirection.Equals("asc"))
                        return categories.OrderBy(e => e.CreatedAt);

                    return categories.OrderByDescending(e => e.CreatedAt);
            }
        }
        #endregion
    }
}
