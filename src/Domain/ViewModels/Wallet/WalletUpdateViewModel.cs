using System.Collections.Generic;
using Domain.Dtos.Wallet;
using Domain.Dtos.WalletType;

namespace Domain.ViewModels
{
    public class WalletUpdateViewModel
    {
        public WalletUpdateDto Wallet { get; set; }
        public IEnumerable<WalletTypeResultDto> WalletTypes { get; set; }
    }
}
