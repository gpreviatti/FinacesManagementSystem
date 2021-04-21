using Data.Context;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Data.Repositories
{
    public class EntraceRepository : BaseRepository<Entrace>, IEntraceRepository
    {
        public EntraceRepository(MyContext context) : base(context)
        {
        }
    }
}
