using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Dtos.Entrance;
using Domain.Dtos.EntranceTypeDto;
using Domain.Dtos.Wallet;

namespace Web.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        [DataType(DataType.Currency)]
        public double TotalIncome { get; set; }

        [DataType(DataType.Currency)]
        public double TotalExpanse { get; set; }

        public IEnumerable<EntranceResultDto> Entrances { get; set; }

        public IEnumerable<WalletResultDto> Wallets { get; set; }
    }
}
