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

namespace Service.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IEntranceService _entranceService;

        public CategoryService(ICategoryRepository repository, IEntranceService entranceService, IMapper mapper)
        {
            _repository = repository;
            _entranceService = entranceService;
            _mapper = mapper;
        }

        #region Find
        public async Task<CategoryResultDto> FindByIdAsync(Guid Id)
        {
            try
            {
                var result = await _repository.FindByIdAsync(Id);
                return _mapper.Map<CategoryResultDto>(result);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }
        public async Task<CategoryUpdateDto> FindByIdUpdateAsync(Guid Id)
        {
            try
            {
                var result = await _repository.FindByIdAsync(Id);
                return _mapper.Map<CategoryUpdateDto>(result);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }

        /// <summary>
        /// Return common categories and users categories
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryResultDto>> FindAsyncAllCommonAndUserCategories()
        {
            try
            {
                var result = await _repository.FindAsyncAllCommonAndUserCategories(UserId);
                return _mapper.Map<IEnumerable<CategoryResultDto>>(result);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }

        /// <summary>
        /// Return common categories and users categories on datatables format
        /// </summary>
        /// <param name="datatablesModel"></param>
        /// <returns></returns>
        public async Task<DatatablesModel<CategoryResultDto>> FindAsyncAllCommonAndUserCategoriesDatatables(DatatablesModel<CategoryResultDto> datatablesModel)
        {
            try
            {
                var result = await _repository.FindAsyncAllCommonAndUserCategories(UserId);
                var categoriesData = _mapper.Map<IEnumerable<CategoryResultDto>>(result);
                foreach (var category in categoriesData)
                {
                    category.Total = _entranceService.FindEntrancesByCategory(category.Id).Result;
                }

                datatablesModel.RecordsTotal = categoriesData.Count();

                if (!string.IsNullOrEmpty(datatablesModel.SearchValue))
                {
                    categoriesData = categoriesData
                    .Where(m => m.Name.Contains(datatablesModel.SearchValue, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrEmpty(datatablesModel.SortColumnDirection))
                {
                    categoriesData = SortDatatables(datatablesModel, categoriesData);
                }

                datatablesModel.RecordsFiltered = categoriesData.Count();
                datatablesModel.Data = categoriesData
                    .Skip(datatablesModel.Skip)
                    .Take(datatablesModel.PageSize)
                    .ToList();

                return datatablesModel;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }
        #endregion

        private static IEnumerable<CategoryResultDto> SortDatatables(DatatablesModel<CategoryResultDto> datatablesModel, IEnumerable<CategoryResultDto> entrancesData)
        {
            var sortDirection = datatablesModel.SortColumnDirection;
            switch (datatablesModel.SortColumn)
            {
                case 0:
                    if (sortDirection.Equals("asc"))
                    {
                        return entrancesData.OrderBy(e => e.Name);
                    }
                    return entrancesData.OrderByDescending(e => e.Name);
                default:
                    if (sortDirection.Equals("asc"))
                    {
                        return entrancesData.OrderBy(e => e.CreatedAt);
                    }
                    return entrancesData.OrderByDescending(e => e.CreatedAt);
            }
        }

        public async Task<CategoryResultDto> CreateAsync(CategoryCreateDto entityCreateDto)
        {
            try
            {
                if (entityCreateDto.CategoryId == Guid.Empty)
                {
                    return null;
                }

                entityCreateDto.UserId = UserId;
                var entity = _mapper.Map<Category>(entityCreateDto);

                var result = await _repository.CreateAsync(entity);
                return _mapper.Map<CategoryResultDto>(entity);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }

        public async Task<CategoryResultDto> UpdateAsync(CategoryUpdateDto entityUpdateDto)
        {
            try
            {
                var result = await _repository.FindByIdAsync(entityUpdateDto.Id);

                if (result == null)
                {
                    return null;
                }

                var entity = _mapper.Map(entityUpdateDto, result);

                var savedChanges = await _repository.SaveChangesAsync();

                if (savedChanges > 0)
                {
                    return _mapper.Map<CategoryResultDto>(entity);
                }
                return null;

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                return await _repository.DeleteAsync(Id);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }
    }
}
