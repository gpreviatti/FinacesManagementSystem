using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.Entrace;

namespace Domain.Interfaces.Services
{
    public interface IEntraceService : IBaseService
    {
        Task<EntraceResultDto> FindByIdAsync(Guid id);
        Task<EntraceUpdateDto> FindByIdUpdateAsync(Guid id);
        Task<IEnumerable<EntraceResultDto>> FindAllAsync();
        Task<IEnumerable<EntraceResultDto>> FindAllAsyncWithWallet();
        Task<IEnumerable<EntraceResultDto>> FindAllAsyncWithCategory();
        Task<IEnumerable<EntraceResultDto>> FindAsyncLastTenEntracesWithCategories();
        Task<IEnumerable<EntraceResultDto>> FindAllAsyncWithWalletAndCategory();
        Task<EntraceResultDto> CreateAsync(EntraceCreateDto entrace);
        Task<EntraceResultDto> UpdateAsync(EntraceUpdateDto entrace);
        Task<bool> DeleteAsync(Guid id);
    }
}
