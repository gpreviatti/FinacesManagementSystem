using System;
using System.Collections.Generic;
using Domain.Dtos.Entity;
using Domain.Dtos.Entrace;
using Domain.Dtos.WalletType;

namespace Domain.Dtos.Wallet
{
    public class WalletResultDto : EntityResultDto
    {   
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public double CurrentValue { get; set; }
        
        public DateTime DueDate { get; set; }
        
        public DateTime CloseDate { get; set; }

        public WalletTypeResultDto WalletType { get; set; }

        public IEnumerable<EntraceResultDto> Entraces { get; set; }
    }
}
