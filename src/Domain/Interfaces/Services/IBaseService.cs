using System;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IBaseService
    {
        Task<bool> DeleteAsync(Guid id);
    }
}
