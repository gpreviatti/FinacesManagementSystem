using System;
using System.Collections.Generic;
using Domain.Dtos.Entrace;
using Domain.Dtos.WalletType;

namespace Domain.Dtos.Wallet
{
    public class WalletResultDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public double CurrentValue { get; set; }
        
        public DateTime DueDate { get; set; }
        
        public DateTime CloseDate { get; set; }

        public WalletTypeResultDto WalletType { get; set; }

        public IEnumerable<EntraceResultDto> Entraces { get; set; }
    }
}
