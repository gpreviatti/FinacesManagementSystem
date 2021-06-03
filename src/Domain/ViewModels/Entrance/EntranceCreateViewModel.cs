using System.Collections.Generic;
using Domain.Dtos.Category;
using Domain.Dtos.Entrance;
using Domain.Dtos.EntranceTypeDto;
using Domain.Dtos.Wallet;

namespace Domain.ViewModels
{
    public class EntranceCreateViewModel
    {
        public EntranceCreateDto Entrance { get; set; }

        public IEnumerable<EntranceTypeResultDto> EntranceTypes { get; set; }

        public IEnumerable<CategoryResultDto> Categories { get; set; }

        public IEnumerable<WalletResultDto> Wallets { get; set; }

    }
}
