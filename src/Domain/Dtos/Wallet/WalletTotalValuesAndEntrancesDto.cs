using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Dtos.Entrance;

namespace Domain.Dtos.Wallet
{
    public class WalletTotalValuesAndEntrancesDto
    {
        [DataType(DataType.Currency)]
        public double TotalIncomes { get; set; }

        [DataType(DataType.Currency)]
        public double TotalExpanses { get; set; }

        public IEnumerable<WalletValuesDto> WalletsValues { get; set; }

        public IEnumerable<EntranceResultDto> Entrances { get; set; }
    }
}
