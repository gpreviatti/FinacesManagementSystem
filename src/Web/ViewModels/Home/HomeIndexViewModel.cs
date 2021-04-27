using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Dtos.Entrace;
using Domain.Dtos.EntraceTypeDto;
using Domain.Dtos.Wallet;

namespace Web.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        [DataType(DataType.Currency)]
        public double TotalIncome { get; set; }

        [DataType(DataType.Currency)]
        public double TotalExpanse { get; set; }

        public IEnumerable<EntraceResultDto> Entraces { get; set; }

        public IEnumerable<WalletResultDto> Wallets { get; set; }
    }
}
