using System;
using System.Collections.Generic;
using Domain.Dtos.Category;
using Domain.Dtos.Wallet;

namespace Domain.Dtos.User
{
    public class UserResultDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public IEnumerable<WalletResultDto> Wallets { get; set; }

        public IEnumerable<CategoryResultDto> Categories { get; set; }
    }
}
