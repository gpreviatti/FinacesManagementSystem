using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.Entrace;

namespace Domain.Interfaces.Services
{
    public interface IEntraceService : IBaseService
    {
        Task<IEnumerable<EntraceResultDto>> FindAllAsync();
        Task<EntraceResultDto> CreateAsync(EntraceCreateDto entrace);
        Task<EntraceResultDto> UpdateAsync(EntraceUpdateDto entrace);
        Task<bool> DeleteAsync(Guid id);
    }
}
