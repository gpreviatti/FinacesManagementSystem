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

        public async Task<bool> DeleteAsync(Guid Id) => await _repository.DeleteAsync(Id);

        #region Find
        public async Task<CategoryResultDto> FindByIdAsync(Guid Id)
        {
            var result = await _repository.FindByIdAsync(Id);
            return _mapper.Map<CategoryResultDto>(result);
        }
        public async Task<CategoryUpdateDto> FindByIdUpdateAsync(Guid Id)
        {
            var result = await _repository.FindByIdAsync(Id);
            return _mapper.Map<CategoryUpdateDto>(result);
        }

        public async Task<IEnumerable<CategoryResultDto>> FindAsyncNameAndIdUserCategories(Guid userId)
        {
            var categories = await _repository.FindAsyncAllCommonAndUserCategories(userId);
            var result = categories.Select(c => new CategoryResultDto { Id = c.Id, Name = c.Name }).ToList();
            return _mapper.Map<IEnumerable<CategoryResultDto>>(result);
        }

        /// <summary>
        /// Return common categories and users categories
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryResultDto>> FindAsyncAllCommonAndUserCategories(Guid userId)
        {
            var result = await _repository.FindAsyncAllCommonAndUserCategories(userId);
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
        public async Task<DatatablesModel<CategoryResultDto>> FindAsyncAllCommonAndUserCategoriesDatatables(DatatablesModel<CategoryResultDto> datatablesModel, Guid userId)
        {
            var result = await _repository.FindAsyncAllCommonAndUserCategories(userId);
            if (result.Count() == 0)
                return null;

            var categories = _mapper.Map<IEnumerable<CategoryResultDto>>(result);
            foreach (var category in categories)
                category.Total = category.Entrances.Select(c => c.Value).Sum();

            datatablesModel.RecordsTotal = categories.Count();

            if (!string.IsNullOrEmpty(datatablesModel.SearchValue))
                categories = categories
                .Where(m => m.Name.Contains(datatablesModel.SearchValue, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(datatablesModel.SortColumnDirection))
                categories = SortDatatables(datatablesModel, categories);

            datatablesModel.RecordsFiltered = categories.Count();
            datatablesModel.Data = categories
                .Skip(datatablesModel.Skip)
                .Take(datatablesModel.PageSize)
                .ToList();

            return datatablesModel;
        }

        private static IEnumerable<CategoryResultDto> SortDatatables(DatatablesModel<CategoryResultDto> datatablesModel, IEnumerable<CategoryResultDto> entrancesData)
        {
            var sortDirection = datatablesModel.SortColumnDirection;
            switch (datatablesModel.SortColumn)
            {
                case 0:
                    if (sortDirection.Equals("asc"))
                        return entrancesData.OrderBy(e => e.Name);

                    return entrancesData.OrderByDescending(e => e.Name);
                case 1:
                    if (sortDirection.Equals("asc"))
                        return entrancesData.OrderBy(e => e.Total);

                    return entrancesData.OrderByDescending(e => e.Total);
                default:
                    if (sortDirection.Equals("asc"))
                        return entrancesData.OrderBy(e => e.CreatedAt);

                    return entrancesData.OrderByDescending(e => e.CreatedAt);
            }
        }
        #endregion
    }
}
