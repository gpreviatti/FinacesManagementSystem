using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> FindByLogin(string email);
    }
}
