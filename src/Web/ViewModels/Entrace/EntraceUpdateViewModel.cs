using System.Collections.Generic;
using Domain.Dtos.Category;
using Domain.Dtos.Entrace;
using Domain.Dtos.Wallet;

namespace Web.ViewModels.Entrace
{
    public class EntraceUpdateViewModel
    {
        public EntraceResultDto Entrace { get; set; }

        public IEnumerable<CategoryResultDto> Categories { get; set; }

        public IEnumerable<WalletResultDto> Wallets { get; set; }
    }
}
