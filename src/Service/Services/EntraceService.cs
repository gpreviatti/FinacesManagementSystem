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
            var result = await _repository.FindByIdAsync(Id);
            return _mapper.Map<EntranceResultDto>(result);
        }

        public async Task<EntranceUpdateDto> FindByIdUpdateAsync(Guid id)
        {
            
            var result = await _repository.FindByIdAsync(id);
            return _mapper.Map<EntranceUpdateDto>(result);
        }

        public async Task<DatatablesModel<EntranceResultDto>> FindAllAsyncWithCategoryDatatables(DatatablesModel<EntranceResultDto> datatablesModel)
        {
            var entrances = await _repository.FindAllAsyncWithCategory();
            var entrancesData = _mapper.Map<IEnumerable<EntranceResultDto>>(entrances);
            datatablesModel.RecordsTotal = entrancesData.Count();

            if (!string.IsNullOrEmpty(datatablesModel.SearchValue))
            {
                entrancesData = entrancesData
                .Where(
                    m => m.Description.Contains(datatablesModel.SearchValue, StringComparison.OrdinalIgnoreCase) ||
                    m.Observation.Contains(datatablesModel.SearchValue, StringComparison.OrdinalIgnoreCase) ||
                    m.Category.Name.Contains(datatablesModel.SearchValue, StringComparison.OrdinalIgnoreCase)
                );
            }

            if (!string.IsNullOrEmpty(datatablesModel.SortColumnDirection))
            {
                entrancesData = SortDatatables(datatablesModel, entrancesData);
            }

            datatablesModel.RecordsFiltered = entrancesData.Count();
            datatablesModel.Data = entrancesData
                .Skip(datatablesModel.Skip)
                .Take(datatablesModel.PageSize)
                .ToList();

            return datatablesModel;
        }

        private static IEnumerable<EntranceResultDto> SortDatatables(DatatablesModel<EntranceResultDto> datatablesModel, IEnumerable<EntranceResultDto> entrancesData)
        {
            var sortDirection = datatablesModel.SortColumnDirection;
            switch (datatablesModel.SortColumn)
            {
                case 0:
                    if (sortDirection.Equals("asc"))
                    {
                        return entrancesData.OrderBy(e => e.Description);
                    }
                    return entrancesData.OrderByDescending(e => e.Description);
                case 1:
                    if (sortDirection.Equals("asc"))
                    {
                        return entrancesData = entrancesData.OrderBy(e => e.Type);
                    }
                    return entrancesData.OrderByDescending(e => e.Type);
                case 2:
                    if (sortDirection.Equals("asc"))
                    {
                        return entrancesData.OrderBy(e => e.Value);
                    }
                    return entrancesData.OrderByDescending(e => e.Value);
                case 3:
                    if (sortDirection.Equals("asc"))
                    {
                        return entrancesData.OrderBy(e => e.Category.Name);
                    }
                    return entrancesData.OrderByDescending(e => e.Category.Name);
                default:
                    if (sortDirection.Equals("asc"))
                    {
                        return entrancesData.OrderBy(e => e.CreatedAt);
                    }
                    return entrancesData.OrderByDescending(e => e.CreatedAt);
            }
        }

        /// <summary>
        /// Take last ten entraces ordered by CreatedAt field
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EntranceResultDto>> FindAsyncLastFiveEntrancesWithCategories()
        {
            var result = await _repository.FindAsyncLastFiveEntrancesWithCategories();
            return _mapper.Map<IEnumerable<EntranceResultDto>>(result);
        }
        #endregion

        public async Task<EntranceResultDto> CreateAsync(EntranceCreateDto entraceCreateDto)
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

        public async Task<EntranceResultDto> UpdateAsync(EntranceUpdateDto entraceUpdateDto)
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

        public async Task<bool> DeleteAsync(Guid Id) => await _repository.DeleteAsync(Id);

        public async Task<double> TotalEntrancesByCategory(Guid categoryId) => 
            await _repository.TotalEntrancesByCategory(categoryId);

        public async Task<double> TotalEntrancesByWallet(Guid walletId) => 
            await _repository.TotalEntrancesByWallet(walletId);
    }
}
