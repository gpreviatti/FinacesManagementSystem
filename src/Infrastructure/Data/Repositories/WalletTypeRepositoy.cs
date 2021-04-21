using Data.Context;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Data.Repositories
{
    public class WalletTypeRepository : BaseRepository<WalletType>, IWalletTypeRepository
    {
        public WalletTypeRepository(MyContext context) : base(context)
        {
        }
    }
}
