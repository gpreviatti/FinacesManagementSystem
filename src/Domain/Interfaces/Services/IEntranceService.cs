using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.Entrance;
using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IEntranceService : IBaseService
    {
        Task<EntranceResultDto> FindByIdAsync(Guid id);
        Task<EntranceUpdateDto> FindByIdUpdateAsync(Guid id);
        Task<DatatablesModel<EntranceResultDto>> FindAllAsyncWithCategoryDatatables(DatatablesModel<EntranceResultDto> paginationModel, Guid userId);
        Task<IEnumerable<EntranceResultDto>> FindAsyncLastFiveEntrancesWithCategories(Guid userId);
        Task<EntranceResultDto> CreateAsync(EntranceCreateDto entrace);
        Task<EntranceResultDto> UpdateAsync(EntranceUpdateDto entrace);
        Task<bool> DeleteAsync(Guid id);
    }
}
