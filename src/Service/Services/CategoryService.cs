using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos.Category;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

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
                System.Console.WriteLine(exception);
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
                System.Console.WriteLine(exception);
                return null;
            }
        }

        /// <summary>
        /// Return all user and common categories
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
                System.Console.WriteLine(exception);
                return null;
            }
        }
        #endregion

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
                System.Console.WriteLine(exception);
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
                System.Console.WriteLine(exception);
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
                System.Console.WriteLine(exception);
                return false;
            }
        }
    }
}
