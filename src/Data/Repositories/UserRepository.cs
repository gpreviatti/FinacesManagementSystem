using System.Threading.Tasks;
using Data.Context;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(MyContext context) : base(context) { }

    public async Task<User> FindByLogin(string email) => await _dataset
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.Email.Equals(email));
}
