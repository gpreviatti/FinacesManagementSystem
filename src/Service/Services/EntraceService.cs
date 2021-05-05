using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos.Entrance;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Helpers.Enuns;
using Domain.Models;

namespace Service.Services
{
    public class EntranceService : BaseService, IEntranceService
    {
        private readonly IEntranceRepository _repository;
        private readonly IWalletRepository _walletRepository;
        private readonly ICategoryRepository _categoryRepository;

        public EntranceService(
            IMapper mapper,
            IEntranceRepository repository,
            IWalletRepository walletRepository,
            ICategoryRepository categoryRepository
        ) 
        {
            _mapper = mapper;
            _repository = repository;
            _walletRepository = walletRepository;
            _categoryRepository = categoryRepository;
        }

        #region "Find"
        public async Task<EntranceResultDto> FindByIdAsync(Guid Id)
        {
            try
            {
                var result = await _repository.FindByIdAsync(Id);
                return _mapper.Map<EntranceResultDto>(result);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }

        public async Task<EntranceUpdateDto> FindByIdUpdateAsync(Guid id)
        {
            try
            {
                var result = await _repository.FindByIdAsync(id);
                return _mapper.Map<EntranceUpdateDto>(result);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }

        public async Task<PaginationModel<EntranceResultDto>> FindAllAsyncWithCategoryPaginated(PaginationModel<EntranceResultDto> paginationModel)
        {
            try
            {
                var result = await _repository.FindAllAsyncWithCategory();
                if (!string.IsNullOrEmpty(paginationModel.SearchString))
                {
                    result = result.Where(e =>
                        e.Description.Contains(paginationModel.SearchString) ||
                        e.CreatedAt.ToString().Contains(paginationModel.SearchString) ||
                        e.Value.ToString().Contains(paginationModel.SearchString) ||
                        e.Category.Name.Contains(paginationModel.SearchString)
                    );
                }

                switch (paginationModel.SortOrder)
                {
                    case "Description":
                        result = result.OrderBy(e => e.Description);
                        break;
                    case "Type":
                        result = result.OrderBy(e => e.Type);
                        break;
                    case "Category":
                        result = result.OrderBy(e => e.Category.Name);
                        break;
                    case "Value":
                        result = result.OrderBy(e => e.Value);
                        break;
                    default:
                        result = result.OrderByDescending(e => e.CreatedAt);
                        break;
                }

                paginationModel.Count = result.Count();
                result = result
                    .Skip((paginationModel.CurrentPage - 1) * paginationModel.PageSize)
                    .Take(paginationModel.PageSize)
                    .ToList();

                var entrances = _mapper.Map<IEnumerable<EntranceResultDto>>(result);
                paginationModel.Data = entrances;
                
                return paginationModel;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }

        /// <summary>
        /// Take last ten entraces ordered by CreatedAt field
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EntranceResultDto>> FindAsyncLastTenEntrancesWithCategories()
        {
            try
            {
                var result = await _repository.FindAsyncLastTenEntrancesWithCategories();
                return _mapper.Map<IEnumerable<EntranceResultDto>>(result);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }
        #endregion

        public async Task<EntranceResultDto> CreateAsync(EntranceCreateDto entraceCreateDto)
        {
            try
            {
                var wallet = _walletRepository.FindByIdAsync(entraceCreateDto.WalletId).Result;
                if (wallet == null)
                {
                    return null;
                }

                switch (entraceCreateDto.Type)
                {
                    case (int) EEntranceType.income:
                        wallet.CurrentValue = wallet.CurrentValue + entraceCreateDto.Value;
                        break;
                    case (int) EEntranceType.expanse:
                        wallet.CurrentValue = wallet.CurrentValue - entraceCreateDto.Value;
                        break;
                    default:
                        wallet.CurrentValue = wallet.CurrentValue;
                        break;
                }
                if (_walletRepository.SaveChangesAsync().Result.Equals(0))
                {
                    return null;
                }

                var category = _categoryRepository.FindByIdAsync(entraceCreateDto.CategoryId).Result;
                if (category == null)
                {
                    return null;
                }

                var entrace = _mapper.Map<Entrance>(entraceCreateDto);
                entrace.Wallet = wallet;
                entrace.Category = category;

                var result = await _repository.CreateAsync(entrace);
                return _mapper.Map<EntranceResultDto>(entrace);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }

        public async Task<EntranceResultDto> UpdateAsync(EntranceUpdateDto entraceUpdateDto)
        {
            try
            {
                var result = await _repository.FindByIdAsync(entraceUpdateDto.Id);

                if (result == null)
                {
                    return null;
                }

                var wallet = _walletRepository.FindByIdAsync(entraceUpdateDto.WalletId).Result;
                if (wallet == null)
                {
                    return null;
                }

                switch (entraceUpdateDto.Type)
                {
                    case (int)EEntranceType.income:
                        wallet.CurrentValue = wallet.CurrentValue + entraceUpdateDto.Value;
                        break;
                    case (int)EEntranceType.expanse:
                        wallet.CurrentValue = wallet.CurrentValue - entraceUpdateDto.Value;
                        break;
                    default:
                        wallet.CurrentValue = wallet.CurrentValue;
                        break;
                }
                if (_walletRepository.SaveChangesAsync().Result.Equals(0))
                {
                    return null;
                }

                if (_categoryRepository.FindByIdAsync(entraceUpdateDto.CategoryId).Result == null)
                {
                    return null;
                }

                var entrace = _mapper.Map(entraceUpdateDto, result);

                var savedChanges = await _repository.SaveChangesAsync();

                if (savedChanges > 0)
                {
                    return _mapper.Map<EntranceResultDto>(entrace);
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
                Console.WriteLine(exception);
                return false;
            }
        }
    }
}
