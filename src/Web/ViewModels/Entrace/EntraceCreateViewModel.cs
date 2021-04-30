using System.Collections.Generic;
using Domain.Dtos.Category;
using Domain.Dtos.Entrace;
using Domain.Dtos.EntraceTypeDto;
using Domain.Dtos.Wallet;

namespace Web.ViewModels.Entrace
{
    public class EntraceCreateViewModel
    {
        public EntraceCreateDto Entrace { get; set; }

        public IEnumerable<EntraceTypeResultDto> EntraceTypes { get; set; }

        public IEnumerable<CategoryResultDto> Categories { get; set; }

        public IEnumerable<WalletResultDto> Wallets { get; set; }

    }
}
