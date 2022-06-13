using Domain.Dtos.Entrance;
using Domain.Dtos.EntranceTypeDto;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Mappers;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services;

public class EntranceService : IEntranceService
{
    private readonly IEntranceRepository _repository;
    private readonly IWalletService _walletService;
    private readonly ICategoryService _categoryService;

    public EntranceService(
        IEntranceRepository repository,
        IWalletService walletService,
        ICategoryService categoryService
    )
    {
        _repository = repository;
        _walletService = walletService;
        _categoryService = categoryService;
    }

    #region Find
    public async Task<EntranceResultDto> FindByIdAsync(Guid id)
    {
        var result = await _repository.FindByIdAsync(id);
        return result.MapperToResultDto();
    }

    public async Task<EntranceUpdateDto> FindByIdUpdateAsync(Guid id)
    {
        var result = await _repository.FindByIdAsync(id);
        return result.MapperToUpateDto();
    }

    public async Task<IEnumerable<EntranceResultDto>> FindAllWithCategory(
        string currentSort,
        string searchString,
        Guid userId
    )
    {
        var userWalletsIds = await _walletService.FindAsyncWalletsUserIds(userId);

        if (!userWalletsIds.Any())
            return null;

        var entrances = await _repository.FindAllAsyncWithCategory(userWalletsIds.ToList());

        if (!entrances.Any())
            return null;

        if (!string.IsNullOrEmpty(searchString))
        {
            entrances = entrances.Where(
                m => m.Description.Contains(searchString) ||
                        m.Observation.Contains(searchString) ||
                        m.Category.Name.Contains(searchString) ||
                        m.Value.Equals(searchString)
            );
        }

        if (!string.IsNullOrEmpty(currentSort))
            entrances = SortEntrances(currentSort, entrances);

        return entrances;
    }

    private static IQueryable<EntranceResultDto> SortEntrances(
        string currentSort,
        IQueryable<EntranceResultDto> entrancesData
    )
    {
        return currentSort switch
        {
            "description" => entrancesData.OrderBy(e => e.Description),
            "description_desc" => entrancesData.OrderByDescending(e => e.Type),
            "value" => entrancesData.OrderBy(e => e.Value),
            "value_desc" => entrancesData.OrderByDescending(e => e.Value),
            "category" => entrancesData.OrderBy(e => e.Category.Name),
            "category_desc" => entrancesData.OrderByDescending(e => e.Category.Name),
            "createdAt" => entrancesData.OrderBy(e => e.CreatedAt),
            _ => entrancesData.OrderByDescending(e => e.CreatedAt),
        };
    }

    /// <summary>
    /// Take last five entraces ordered by CreatedAt field
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<EntranceResultDto>> FindAsyncLastFiveEntrancesWithCategories(Guid userId)
    {
        var userWalletsIds = await _walletService.FindAsyncWalletsUserIds(userId);

        var result = await _repository.FindAllAsyncWithCategory(userWalletsIds.ToList());

        return result.Take(5);
    }

    public List<EntranceTypeResultDto> FindEntranceTypes()
    {
        return new List<EntranceTypeResultDto>
        {
            new () { Value = (int) EntranceType.Income, Name = "Income"},
            new () { Value = (int) EntranceType.Expanse, Name = "Expanse"},
            new () { Value = (int) EntranceType.Transference, Name = "Transference"},
        };
    }
    #endregion

    #region Setup view models
    public async Task<EntranceCreateViewModel> SetupEntranceCreateViewModel(Guid userId)
    {
        var entraceCreateViewModel = new EntranceCreateViewModel
        {
            Entrance = new (),
            Wallets = await _walletService.FindAsyncWalletsUser(userId)
        };
        
        if (entraceCreateViewModel.Wallets == null || !entraceCreateViewModel.Wallets.Any())
            throw new ArgumentException("Any Wallet was found");

        entraceCreateViewModel.Categories = await _categoryService.FindAsyncNameAndIdUserCategories(userId);

        if (entraceCreateViewModel.Categories == null || !entraceCreateViewModel.Categories.Any())
            throw new ArgumentException("Any Category was found");

        entraceCreateViewModel.EntranceTypes = FindEntranceTypes();
        return entraceCreateViewModel;
    }

    public async Task<EntranceUpdateViewModel> SetupEntranceUpdateViewModel(Guid userId, Guid id)
    {
        return new ()
        {
            Entrance = await FindByIdUpdateAsync(id),
            Wallets = await _walletService.FindAsyncWalletsUser(userId),
            Categories = await _categoryService.FindAsyncAllCommonAndUserCategories(userId),
            EntranceTypes = FindEntranceTypes()
        };
    }
    #endregion

    public async Task<EntranceResultDto> CreateAsync(EntranceCreateDto entraceCreateDto)
    {
        var updateWalletValue = await _walletService

            .UpdateWalletValue(entraceCreateDto.WalletId, entraceCreateDto.Type, entraceCreateDto.Value);
        if (updateWalletValue == null)
            return null;

        var category = await _categoryService.FindByIdAsync(entraceCreateDto.CategoryId);
        if (category == null)
            return null;

        var entrance = entraceCreateDto.Mapper();
        _ = _repository.CreateAsync(entrance);

        return entrance.MapperToResultDto();
    }

    public async Task<EntranceResultDto> UpdateAsync(EntranceUpdateDto entraceUpdateDto)
    {
        var result = await _repository.FindByIdAsync(entraceUpdateDto.Id);

        if (result == null)
            return null;

        var updateWalletValue = await _walletService

            .UpdateWalletValue(entraceUpdateDto.WalletId, entraceUpdateDto.Type, entraceUpdateDto.Value);
        if (updateWalletValue == null)
            return null;

        if (await _categoryService.FindByIdAsync(entraceUpdateDto.CategoryId) == null)
            return null;

        var entrace = entraceUpdateDto.Mapper();

        var savedChanges = await _repository.SaveChangesAsync();

        if (savedChanges > 0)
            return entrace.MapperToResultDto();

        return null;
    }

    public async Task<bool> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
}
