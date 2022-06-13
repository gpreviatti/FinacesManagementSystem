using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.Entrance;
using Domain.Dtos.EntranceTypeDto;
using Domain.ViewModels;

namespace Domain.Interfaces.Services;

public interface IEntranceService : IBaseService
{
    Task<EntranceResultDto> FindByIdAsync(Guid id);
    Task<EntranceUpdateDto> FindByIdUpdateAsync(Guid id);

    Task<IEnumerable<EntranceResultDto>> FindAllWithCategory(
        string currentSort,
        string searchString,
        Guid userId
    );

    Task<IEnumerable<EntranceResultDto>> FindAsyncLastFiveEntrancesWithCategories(Guid userId);
    List<EntranceTypeResultDto> FindEntranceTypes();

    Task<EntranceCreateViewModel> SetupEntranceCreateViewModel(Guid userId);
    Task<EntranceUpdateViewModel> SetupEntranceUpdateViewModel(Guid userId, Guid id);

    Task<EntranceResultDto> CreateAsync(EntranceCreateDto entraceCreateDto);
    Task<EntranceResultDto> UpdateAsync(EntranceUpdateDto entraceUpdateDto);
    Task<bool> DeleteAsync(Guid id);
}
