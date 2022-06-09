using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos.Entrance;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IEntranceRepository : IBaseRepository<Entrance>
    {
        Task<IQueryable<EntranceResultDto>> FindAllAsyncWithCategory(List<Guid> userWalletsId);
    }
}
