using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.Entrance;
using Domain.Dtos.EntranceTypeDto;
using Domain.Models;
using Domain.ViewModels;

namespace Domain.Interfaces.Services
{
    public interface IEntranceService : IBaseService
    {
        Task<EntranceResultDto> FindByIdAsync(Guid id);
        Task<EntranceUpdateDto> FindByIdUpdateAsync(Guid id);
        Task<DatatablesModel<EntranceResultDto>> FindAllAsyncWithCategoryDatatables(DatatablesModel<EntranceResultDto> datatablesModel, Guid userId);

        Task<IEnumerable<EntranceResultDto>> FindAllWithCategory(
            string currentSort,
            string searchString,
            int? page,
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
}
