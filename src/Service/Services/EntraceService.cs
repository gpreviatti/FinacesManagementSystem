using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos.Entrance;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.ViewModels;
using Domain.Dtos.EntranceTypeDto;
using Domain.Enums;

namespace Service.Services
{
    public class EntranceService : BaseService, IEntranceService
    {
        private readonly IEntranceRepository _repository;
        private readonly IWalletService _walletService;
        private readonly ICategoryService _categoryService;

        public EntranceService(
            IMapper mapper,
            IEntranceRepository repository,
            IWalletService walletService,
            ICategoryService categoryService
        )
        {
            _mapper = mapper;
            _repository = repository;
            _walletService = walletService;
            _categoryService = categoryService;
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

        public async Task<DatatablesModel<EntranceResultDto>> FindAllAsyncWithCategoryDatatables(DatatablesModel<EntranceResultDto> datatablesModel, Guid userId)
        {
            var userWalletsIds = _walletService.FindAsyncWalletsUserIds(userId);
            var entrances = await _repository.FindAllAsyncWithCategory(userWalletsIds);
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
                entrancesData = SortDatatables(datatablesModel, entrancesData);

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
        public async Task<IEnumerable<EntranceResultDto>> FindAsyncLastFiveEntrancesWithCategories(Guid userId)
        {
            var userWalletsIds = _walletService.FindAsyncWalletsUserIds(userId);
            var result = await _repository.FindAsyncLastFiveEntrancesWithCategories(userWalletsIds);
            return _mapper.Map<IEnumerable<EntranceResultDto>>(result);
        }

        public List<EntranceTypeResultDto> FindEntranceTypes()
        {
            return new List<EntranceTypeResultDto>
            {
                new EntranceTypeResultDto() { Value = (int) EntranceType.Income, Name = "Income"},
                new EntranceTypeResultDto() { Value = (int) EntranceType.Expanse, Name = "Expanse"},
                new EntranceTypeResultDto() { Value = (int) EntranceType.Transference, Name = "Transference"},
            };  
        }
        #endregion

        public async Task<EntranceCreateViewModel> SetupEntranceCreateViewModel(Guid userId)
        {
            var entraceCreateViewModel = new EntranceCreateViewModel();
            entraceCreateViewModel.Entrance = new EntranceCreateDto();
            entraceCreateViewModel.Wallets = await _walletService.FindAsyncWalletsUser(userId);
            if (entraceCreateViewModel.Wallets.Count() == 0)
                throw new Exception("Any Wallet was found");

            entraceCreateViewModel.Categories = await _categoryService.FindAsyncNameAndIdUserCategories(userId);
            if (entraceCreateViewModel.Categories == null)
                throw new Exception("Any Category was found");

            entraceCreateViewModel.EntranceTypes = FindEntranceTypes();
            return entraceCreateViewModel;
        }

        public async Task<EntranceUpdateViewModel> SetupEntranceUpdateViewModel(Guid userId, Guid id)
        {
            var entraceUpdateViewModel = new EntranceUpdateViewModel();
            entraceUpdateViewModel.Entrance = await FindByIdUpdateAsync(id);
            entraceUpdateViewModel.Wallets = await _walletService.FindAsyncWalletsUser(userId);
            entraceUpdateViewModel.Categories = await _categoryService.FindAsyncAllCommonAndUserCategories(userId);
            entraceUpdateViewModel.EntranceTypes = FindEntranceTypes();
            return entraceUpdateViewModel;
        }

        public async Task<EntranceResultDto> CreateAsync(EntranceCreateDto entraceCreateDto)
        {
            var updateWalletValue = await _walletService
                .UpdateWalletValue(entraceCreateDto.WalletId, entraceCreateDto.Type, entraceCreateDto.Value);
            if (updateWalletValue == 0)
                return null;

            var category = await _categoryService.FindByIdAsync(entraceCreateDto.CategoryId);
            if (category == null)
                return null;

            var entrace = _mapper.Map<Entrance>(entraceCreateDto);

            await _repository.CreateAsync(entrace);
            return _mapper.Map<EntranceResultDto>(entrace);
        }

        public async Task<EntranceResultDto> UpdateAsync(EntranceUpdateDto entraceUpdateDto)
        {
            var result = await _repository.FindByIdAsync(entraceUpdateDto.Id);

            if (result == null)
                return null;

            var updateWalletValue = await _walletService
                .UpdateWalletValue(entraceUpdateDto.WalletId, entraceUpdateDto.Type, entraceUpdateDto.Value);
            if (updateWalletValue == 0)
                return null;

            if (await _categoryService.FindByIdAsync(entraceUpdateDto.CategoryId) == null)
                return null;

            var entrace = _mapper.Map(entraceUpdateDto, result);
            var savedChanges = await _repository.SaveChangesAsync();

            if (savedChanges > 0)
                return _mapper.Map<EntranceResultDto>(entrace);

            return null;
        }

        public async Task<bool> DeleteAsync(Guid Id) => await _repository.DeleteAsync(Id);
    }
}
