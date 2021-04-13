using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IEntraceService : IBaseService
    {
        Task<IEnumerable<Entrace>> FindAllAsync();
        Task<Entrace> CreateAsync(Entrace entrace);
        Task<Entrace> UpdateAsync(Entrace entrace);
    }
}
