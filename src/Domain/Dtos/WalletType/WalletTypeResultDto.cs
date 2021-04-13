using System;

namespace Domain.Dtos.WalletType
{
    public class WalletTypeResultDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
    }
}
