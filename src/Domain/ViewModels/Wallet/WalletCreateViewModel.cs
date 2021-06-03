using System.Collections.Generic;
using Domain.Dtos.Wallet;
using Domain.Dtos.WalletType;

namespace Domain.ViewModels
{
    public class WalletCreateViewModel
    {
        public WalletCreateViewModel()
        {
            Wallet = new WalletCreateDto();
            WalletTypes = new List<WalletTypeResultDto>();
        }

        public WalletCreateDto Wallet { get; set; }
        public IEnumerable<WalletTypeResultDto> WalletTypes { get; set; }
    }
}
