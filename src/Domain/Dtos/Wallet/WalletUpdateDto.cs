using System;
using Domain.Dtos.Entity;

namespace Domain.Dtos.Wallet
{
    public class WalletUpdateDto : EntityUpdateDto
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public double CurrentValue { get; set; }
        
        public DateTime DueDate { get; set; }
        
        public DateTime CloseDate { get; set; }
        
        public Guid WalletTypeId { get; set; }
    }
}
